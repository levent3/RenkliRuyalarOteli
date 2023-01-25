using System.ComponentModel.DataAnnotations;

namespace RenkliRuyalarOteli.WebMvcUI.Areas.Admin.Models.Oda
{
    public class OdaUpdateDTO
    {

        public Guid Id { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "OdaNO  Zorunlu Alandir")]
        public string OdaNo { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Kisi sayısı  Zorunlu Alandir")]
        public byte KisiSayisi { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Durum  Zorunlu Alandir")]

        public bool Durum { get; set; } = true;


    }
}
