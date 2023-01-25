using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenkliRuyalarOteli.BL.Abstract;
using RenkliRuyalarOteli.Entities.Entites.Concrete;
using RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Rezervasyon;
using System.Security.Claims;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class RezervasyonController : Controller
    {
        private readonly IRezervasyonManager rezervasyonManager;
        private readonly IOdaManager odaManager;
        private readonly IOdaFiyatManager odaFiyatManager;

        public RezervasyonController(IRezervasyonManager rezervasyonManager, IOdaManager odaManager, IOdaFiyatManager odaFiyatManager)
        {
            this.rezervasyonManager = rezervasyonManager;
            this.odaManager = odaManager;
            this.odaFiyatManager = odaFiyatManager;
        }
        public async Task<IActionResult> Index()
        {
            var result = await rezervasyonManager.FindAllAsnyc();
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var createDTO = new RezervasyonCreateDTO();
            @ViewBag.Odalar = GetOdalar();
            ViewBag.Fiyatlar = GetOdaFiyatlari();
            return View(createDTO);
        }



        [HttpPost]
        public async Task<IActionResult> Create(RezervasyonCreateDTO rezervasyonCreateDTO)
        {
            int sonuc = 0;

            List<Oda> odalar = new();
            List<Oda> odafiyatlari = new();

            if (!ModelState.IsValid)
            {
                @ViewBag.Odalar = GetOdalar();

                return View(rezervasyonCreateDTO);
            }

            var odaNo = rezervasyonManager.FindAllIncludeAsync(null, p => p.Oda.OdaNo);
            foreach (var item in rezervasyonCreateDTO.Odalar)
            {
                foreach (var item2 in rezervasyonCreateDTO.Fiyatlar)
                {
                    Rezervasyon rezervasyon = new Rezervasyon()
                    {
                        GirisTarihi = rezervasyonCreateDTO.GirisTarihi,
                        CikisTarihi = rezervasyonCreateDTO.CikisTarihi,

                        KullaniciId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value),
                        OdaFiyatId = Guid.Parse(item2),
                        OdaId = Guid.Parse(item)


                    };



                    sonuc = await rezervasyonManager.CreateAsync(rezervasyon);
                }



            }



            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Rezervasyon");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");
                @ViewBag.Odalar = GetOdalar();
                return View(rezervasyonCreateDTO);
            }



        }





        [NonAction]
        private List<SelectListItem> GetOdalar()
        {
            var odalar = odaManager.FindAllAsnyc(p => p.Durum == true).Result;

            List<SelectListItem> ListItemRols = new();
            foreach (Oda oda in odalar)
            {
                ListItemRols.Add(new SelectListItem(oda.OdaNo, oda.Id.ToString()));
            }
            return ListItemRols;
        }

        [NonAction]


        private List<SelectListItem> GetOdaFiyatlari()
        {
            var odaFiyatlari = odaFiyatManager.FindAllAsnyc(null).Result;


            List<SelectListItem> ListItemRols = new();
            foreach (OdaFiyat fiyat in odaFiyatlari)
            {

                ListItemRols.Add(new SelectListItem(fiyat.Fiyat.ToString(), fiyat.Id.ToString()));

            }
            return ListItemRols;


        }
    }
}
