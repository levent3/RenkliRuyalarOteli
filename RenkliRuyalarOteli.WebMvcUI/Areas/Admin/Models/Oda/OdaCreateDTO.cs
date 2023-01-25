namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Oda
{
    public class OdaCreateDTO
    {
        public Guid? OdaId { get; set; }

        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "Oda No Zorunlu Alandir")]
        public string OdaNo { get; set; }
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "Kişi Sayısı Zorunlu Alandir")]
        public byte KisiSayisi { get; set; }
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "Oda Durumu Zorunlu Alandir")]
        public bool Durum { get; set; } = true;
        public IFormFile? ImageFile { get; set; }
    }
}
