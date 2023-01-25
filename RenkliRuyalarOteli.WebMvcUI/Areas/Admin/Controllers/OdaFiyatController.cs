using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenkliRuyalarOteli.BL.Abstract;
using RenkliRuyalarOteli.Entities.Entites.Concrete;
using RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.OdaFiyat;
using System.Security.Claims;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class OdaFiyatController : Controller
    {
        private readonly IOdaFiyatManager odaFiyatManager;
        private readonly IOdaManager odaManager;

        public OdaFiyatController(IOdaFiyatManager odaFiyatManager, IOdaManager odaManager)
        {
            this.odaFiyatManager = odaFiyatManager;
            this.odaManager = odaManager;
        }
        public async Task<IActionResult> Index()
        {
            var result = await odaFiyatManager.FindAllIncludeAsync(null, p => p.Oda);
            return View(result.ToList());
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            // var oda = odaFiyatManager.FindAsync(p => p.Id == Id).Result;
            var oda = odaFiyatManager.FindAllIncludeAsync(z => z.Id == Id, p => p.Oda).Result.FirstOrDefault();

            OdaFiyatUpdateDTO updateDTO = new OdaFiyatUpdateDTO()
            {
                Id = oda.Id,
                OdaNo = oda.Oda.OdaNo,
                Baslangic = oda.Baslangic,
                Bitis = oda.Bitis,
                Fiyat = oda.Fiyat
            };
            return View(updateDTO);



        }

        [HttpPost]
        public async Task<IActionResult> Update(OdaFiyatUpdateDTO odaFiyatUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(odaFiyatUpdateDTO);

                }
                var result = await odaFiyatManager.FindAsync(p => p.Id == odaFiyatUpdateDTO.Id);

                result.Baslangic = odaFiyatUpdateDTO.Baslangic;
                result.Bitis = odaFiyatUpdateDTO.Bitis;
                result.Fiyat = odaFiyatUpdateDTO.Fiyat;
                result.Oda.OdaNo = odaFiyatUpdateDTO.OdaNo;

                var sonuc = await odaFiyatManager.UpdateAsync(result);
                if (sonuc > 0)
                {

                    return RedirectToAction("Index", "OdaFiyat");
                }
                else
                {
                    ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Lutfen Biraz sonra tekrar denbeyiniz");

                    return View(odaFiyatUpdateDTO);
                }

            }
            catch (Exception)
            {

                throw;
            }


        }





        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var createDto = new OdaFiyatCreateDTO();
            @ViewBag.Odalar = GetOdalar();
            return View(createDto);


        }
        [HttpPost]
        public async Task<IActionResult> Create(OdaFiyatCreateDTO odaCreateFiyatDTO)
        {

            int sonuc = 0;
            List<OdaFiyat> odaFiyatlar = new();

            if (!ModelState.IsValid)
            {
                @ViewBag.Odalar = GetOdalar();
                return View(odaCreateFiyatDTO);
            }

            //var odaNo = odaFiyatManager.FindAllIncludeAsync(null, p => p.Oda.OdaNo);

            foreach (var item in odaCreateFiyatDTO.Odalar)
            {
                OdaFiyat odaFiyat = new OdaFiyat()
                {
                    Fiyat = odaCreateFiyatDTO.Fiyat,
                    Bitis = odaCreateFiyatDTO.Bitis,
                    Baslangic = odaCreateFiyatDTO.Baslangic,
                    KullaniciId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value),
                    OdaId = Guid.Parse(item)

                };

                sonuc = await odaFiyatManager.CreateAsync(odaFiyat);
            }




            if (sonuc > 0)
            {
                return RedirectToAction("Index", "OdaFiyat");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");
                @ViewBag.Odalar = GetOdalar();
                return View(odaCreateFiyatDTO);
            }




        }


        [NonAction]
        private List<SelectListItem> GetOdalar()
        {
            var odalar = odaManager.FindAllAsnyc(null).Result;

            List<SelectListItem> ListItemRols = new();
            foreach (Oda oda in odalar)
            {
                ListItemRols.Add(new SelectListItem(oda.OdaNo, oda.Id.ToString()));
            }
            return ListItemRols;
        }

        #endregion

    }
}
