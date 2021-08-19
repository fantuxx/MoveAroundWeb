using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MoveAround.Models
{
    public class AppUser
    {

        //DUOMENYS APEI VARTOTOJA-------
        public int Id { get; set; }


        public virtual IdentityUser User { get; set; }
        public string IdentityUserId { get; set; }

        public string Tipas { get; set; } = "Vartotojas";
        [NotMapped]//parodo entity kad nereikia sekti jo
        public List<SelectListItem> Tipai { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "Vezejas", Text = "Vežėjas"},
            new SelectListItem {Value = "Uzsakovas", Text = "Užsakovas"}
        };
        [NotMapped]//parodo entity kad nereikia sekti jo
        public List<SelectListItem> AdvancedTipai { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "Vezejas", Text = "Vežėjas"},
            new SelectListItem {Value = "Uzsakovas", Text = "Užsakovas"},
            new SelectListItem {Value = "Admin", Text = "Administratorius"},
             new SelectListItem {Value = "Moderator", Text = "Moderatorius"}
        };
        [Required(ErrorMessage = "Šis lauakas yra privalomas")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Sąskaitos numeris turi būti nuo 6 iki 10 ženklų ilgio")]
        [DisplayName("Sąskaitos numeris")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Šis laukas yra privalomas")]
        [StringLength(30, ErrorMessage = "Vardas yra privalomas negali būsi ilgesnis nei 10 ženklų")]
        [DisplayName("Vardas")]
        public string FirstName { get; set; } = "";
        [Required]
        [StringLength(20, ErrorMessage = "Pavardė yra privaloma negali būsi ilgesnis nei 10 ženklų")]
        [DisplayName("Pavardė")]
        public string LastName { get; set; } = "";

        [DisplayName("Telefono Numeris")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Miesto lauakas yra privalomas")]
        [StringLength(50, ErrorMessage = "Šis laukas negali būti ilgesnis nei 50 ženklų")]
        [DisplayName("Miestas")]
        public string AddressCity { get; set; }

        [Required(ErrorMessage = "Adreso lauakas yra privalomas")]
        [StringLength(50, ErrorMessage = "Šis laukas negali būti ilgesnis nei 50 ženklų")]
        [DisplayName("Gatvė ir Numeris")]
        public string AddressGatveNr { get; set; }

        //NUO CIA PRASIDEDA BUISNESS DUOMENYS 

        [Required(ErrorMessage = "Įmonės vardo lauakas yra privalomas")]
        [StringLength(50, ErrorMessage = "Šis laukelis negali būsi ilgesnis nei 50 ženklų")]
        [DisplayName("Įmonės pavadinimas")]
        public string BuisnessName { get; set; }

        [Required(ErrorMessage = "Įmonės kodo lauakas yra privalomas")]
        [StringLength(50, ErrorMessage = "Šis laukas negali būti ilgesnis nei 50 ženklų")]
        [DisplayName("Įmonės kodas")]
        public string BuisnessCode { get; set; }

        [Required(ErrorMessage = "Pvm lauakas yra privalomas")]
        [StringLength(50, ErrorMessage = "Šis laukas  negali būti ilgesnis nei 50 ženklų")]
        [DisplayName("PVM")]
        public string PVM { get; set; }
        public bool IsConfirmed { get; set; }




        [Required(ErrorMessage = "Miesto lauakas yra privalomas")]
        [StringLength(20, ErrorMessage = "Šis laukas  negali būti ilgesnis nei 50 ženklų")]
        [DisplayName("Įmonės adresas 1")]
        public string BuisnesAdressCity { get; set; }


        [Required(ErrorMessage = "Adreso lauakas yra privalomas")]
        [StringLength(50, ErrorMessage = "Šis laukas  negali būti ilgesnis nei 50 ženklų")]
        [DisplayName("Įmonės adresas 2")]
        public string BuisnesAdressStreet { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Įkūrimo diena")]
        public string EstablishedDate { get; set; }

        [NotMapped]
        public List<string> AllUserROles { get; set; }

        [Required(ErrorMessage = "E-mail lauakas yra privalomas")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "e-mail negali būti ilgesnis nei 100 ženklų")]
        [DisplayName("Įmonės e-mail")]
        public string BuisnessEmail { get; set; }
        [NotMapped]//parodo entity kad nereikia sekti jo
        public List<SelectListItem> SuperTiap { get; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "Vezejas", Text = "Vežėjas"},
            new SelectListItem {Value = "Uzsakovas", Text = "Užsakovas"},
            new SelectListItem {Value = "Admin", Text = "Administratorius"},
            new SelectListItem {Value = "Moderator", Text = "Moderatorius"},
            new SelectListItem {Value = "SuperAdmin", Text = "SuperAdmin"}
        };
        [DisplayName("Vartotojo Elektroninis paštas")]
        public string UserEmail { get; set; }

        //METODAI///////////////////////////////////
        public void SetEmail()
        {
            UserEmail = this.User.Email;
        }

        public  ICollection<Order> PateiktiUzsakymai { get; set; }//VIsiPateikti
        public ICollection<Transport> PateiktiTransportai { get; set; }//VisiPateikti




        //Gal dar 






    }

}
