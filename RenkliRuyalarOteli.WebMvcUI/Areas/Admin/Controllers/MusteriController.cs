using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RenkliRuyalarOteli.BL.Abstract;
using RenkliRuyalarOteli.Entities.Entites.Concrete;
using RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Musteri;
using System.Security.Claims;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MusteriController : Controller
    {
        private readonly IMusteriManager musteriManager;

        public MusteriController(IMusteriManager musteriManager)
        {
            this.musteriManager = musteriManager;
        }
        public async Task<IActionResult> Index()
        {
            var result = await musteriManager.FindAllAsnyc();
            return View(result);
        }

        #region CREATE
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var createDto = new MusteriCreateDTO();


            return View(createDto);
        }



        [HttpPost]
        public async Task<IActionResult> Create(MusteriCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
            {

                return View(createDTO);
            }
            var qwe = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);

            var musteri = new Musteri
            {
                Ad = createDTO.Ad,
                Soyadi = createDTO.Soyadi,
                Cinsiyet = true,
                MusteriTcNo = createDTO.MusteriTcNo,
                CepNo = createDTO.CepNo,
                KullaniciId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value)


            };


            var sonuc = await musteriManager.CreateAsync(musteri);




            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");

                return View(createDTO);
            }
        }
        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var kullanici = musteriManager.FindAsync(p => p.Id == Id).Result;
            MusteriUpdateDTO updateDto = new MusteriUpdateDTO
            {
                Id = kullanici.Id,
                Ad = kullanici.Ad,
                Soyadi = kullanici.Soyadi,
                MusteriTcNo = kullanici.MusteriTcNo,
                Cinsiyet = kullanici.Cinsiyet,

            };
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MusteriUpdateDTO musteriUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(musteriUpdateDTO);

            }

            var musteri2 = musteriManager.FindAsync(p => p.Id == musteriUpdateDTO.Id).Result;



            musteri2.Ad = musteriUpdateDTO.Ad;
            musteri2.Soyadi = musteriUpdateDTO.Soyadi;
            musteri2.CepNo = musteriUpdateDTO.CepNo;
            musteri2.MusteriTcNo = musteriUpdateDTO.MusteriTcNo;
            musteri2.Cinsiyet = musteriUpdateDTO.Cinsiyet;



            var sonuc = await musteriManager.UpdateAsync(musteri2);
            if (sonuc > 0)
            {
                return RedirectToAction("Index", "Musteri");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Lutfen Biraz sonra tekrar denbeyiniz");

                return View(musteriUpdateDTO);
            }


        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await musteriManager.FindAsync(p => p.Id == id);
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Musteri musteri)
        {

            var result = await musteriManager.FindAsync(p => p.Id == musteri.Id);
            musteriManager.DeleteAsync(result);

            return RedirectToAction("Index", "Musteri");
        }





        #endregion

    }
}
