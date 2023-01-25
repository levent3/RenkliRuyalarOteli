using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenkliRuyalarOteli.BL.Abstract;
using RenkliRuyalarOteli.Entities.Entites.Concrete;
using RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Oda;
using System.Security.Claims;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class OdaController : Controller
    {
        private readonly IOdaManager odaManager;
        private readonly IOdaFiyatManager odaFiyatManager;

        public OdaController(IOdaManager odaManager, IOdaFiyatManager odaFiyatManager)
        {
            this.odaManager = odaManager;
            this.odaFiyatManager = odaFiyatManager;
        }




        public async Task<IActionResult> Index()
        {
            var result = await odaManager.FindAllAsnyc(p => p.Durum == true); ;
            return View(result);
        }

        #region update
        [HttpGet]

        public async Task<IActionResult> Update(Guid Id)
        {
            var oda = odaManager.FindAsync(p => p.Id == Id).Result;
            OdaUpdateDTO updateDTO = new OdaUpdateDTO()


            {
                Id = oda.Id,
                OdaNo = oda.OdaNo,
                Durum = oda.Durum,
                KisiSayisi = oda.KisiSayisi



            };
            return View(updateDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Update(OdaUpdateDTO odaUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(odaUpdateDTO);

            }

            var oda2 = odaManager.FindAsync(p => p.Id == odaUpdateDTO.Id).Result;



            oda2.OdaNo = odaUpdateDTO.OdaNo;
            oda2.Durum = odaUpdateDTO.Durum;
            oda2.KisiSayisi = odaUpdateDTO.KisiSayisi;


            var sonuc = await odaManager.UpdateAsync(oda2);
            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Oda");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Lutfen Biraz sonra tekrar denbeyiniz");

                return View(odaUpdateDTO);
            }

        }
        #endregion


        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var createDto = new OdaCreateDTO();

            return View(createDto);


        }
        [HttpPost]
        public async Task<IActionResult> Create(OdaCreateDTO odaCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(odaCreateDTO);
                }

                var odaVarmi = await odaManager.FindAsync(p => p.OdaNo == odaCreateDTO.OdaNo);
                //bool a = Convert.ToBoolean(odaVarmi);
                if (odaVarmi != null)
                {
                    //TempData["alertMessage"] = "Whatever you want to alert the user with";

                    ModelState.AddModelError("", "Bu oda daha onceden kaydedilmistir.");
                    return View(odaCreateDTO);




                }
                else
                {
                    Oda room = new Oda
                    {
                        OdaNo = odaCreateDTO.OdaNo,
                        Durum = odaCreateDTO.Durum,
                        KisiSayisi = odaCreateDTO.KisiSayisi,
                        KullaniciId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value)
                    };


                    if (odaCreateDTO.ImageFile != null)
                    {
                        var extent = Path.GetExtension(odaCreateDTO.ImageFile.FileName);
                        var randomName = ($"{Guid.NewGuid()}{extent}");
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", randomName);
                        room.ImageUrl = "Uploads\\" + randomName;
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await odaCreateDTO.ImageFile.CopyToAsync(stream);
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            //postedFile.CopyTo(ms);	
                            odaCreateDTO.ImageFile.CopyTo(ms);
                        }
                    }

                    var sonuc = await odaManager.CreateAsync(room);

                    if (sonuc > 0)
                    {
                        return RedirectToAction("Index", "Oda");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return View(odaCreateDTO);

        }
        #endregion







    }
}
