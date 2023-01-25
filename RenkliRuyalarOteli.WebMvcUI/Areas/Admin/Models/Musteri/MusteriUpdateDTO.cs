using System.ComponentModel.DataAnnotations;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Musteri
{
    public class MusteriUpdateDTO
    {

        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ad  Zorunlu Alandir")]
        public string Ad { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Soyad  Zorunlu Alandir")]
        public string Soyadi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cinsiyet  Zorunlu Alandir")]
        public bool Cinsiyet { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "TcNo  Zorunlu Alandir")]
        public string MusteriTcNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CepNo  Zorunlu Alandir")]
        public string CepNo { get; set; }






    }
}
