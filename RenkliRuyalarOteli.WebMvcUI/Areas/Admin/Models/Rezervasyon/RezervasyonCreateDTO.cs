using System.ComponentModel.DataAnnotations;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Rezervasyon
{
    public class RezervasyonCreateDTO
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Tarih Zorunlu Alandir")]
        public DateTime GirisTarihi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tarih Zorunlu Alandir")]
        public DateTime CikisTarihi { get; set; }

        public List<string>? Odalar { get; set; }
        public List<string>? Fiyatlar { get; set; }



    }
}
