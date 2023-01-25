using System.ComponentModel.DataAnnotations;
namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.OdaFiyat
{
    public class OdaFiyatCreateDTO
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Baslangıc Zamanı Zorunlu Alandir")]
        public DateTime Baslangic { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bitis Zamanı Zorunlu Alandir")]
        public DateTime Bitis { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fiyat Zorunlu Alandir")]

        public float Fiyat { get; set; }


        public List<string>? Odalar { get; set; }
    }
}
