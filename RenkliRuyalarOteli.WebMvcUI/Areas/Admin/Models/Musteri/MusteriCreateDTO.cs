namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Musteri
{
    public class MusteriCreateDTO
    {
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "Ad Zorunlu Alandir")]
        public string Ad { get; set; }

        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "Soyad Zorunlu Alandir")]
        public string Soyadi { get; set; }
        public bool Cinsiyet { get; set; }

        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "Tc Zorunlu Alandir")]
        public string MusteriTcNo { get; set; }
        public string CepNo { get; set; }



    }
}
