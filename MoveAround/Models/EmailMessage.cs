using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MoveAround.Models
{
    public class EmailMessage
    {
        public DateTime DateCreated { get; set; }
        public int id { get; set; }
        [Required(ErrorMessage = "Kuo jūs vardu?")]       
        [StringLength(100, ErrorMessage = "Vardas negali būti ilgesnis nei 100 ženklų")]
        [DisplayName("Jūsų vardas")]
        public string Name { get; set; }


        [Required (ErrorMessage ="Kokiu elektronino pašto adresu jums atrašyti?")]       
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "e-mail negali būti ilgesnis nei 100 ženklų")]
        [DisplayName("Elektroninio pašto adresas")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Prašome nurodyti laiško temą")]
        [StringLength(100, ErrorMessage = "Tema negali būti ilgesnė nei 100 ženklų")]
        [DisplayName("Tema")]
        public string Subject { get; set; }

        [MaxLength(500, ErrorMessage ="Maksimalus žinutės ilgis 500 ženklų.")]
        [Required(ErrorMessage ="Ką norite mums pasakyti")]
        [DisplayName("Jūsų žinutė")]
        public string Message { get; set; }

        
    }
}
