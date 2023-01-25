using System.ComponentModel.DataAnnotations;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.OdaFiyat
{
    public class OdaFiyatUpdateDTO
    {

        public Guid Id { get; set; }
        public string? OdaNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Baslangıc Zamanı Zorunlu Alandir")]
        public DateTime Baslangic { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bitis Zamanı Zorunlu Alandir")]
        public DateTime Bitis { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fiyat Zorunlu Alandir")]

        public float Fiyat { get; set; }




    }
}
