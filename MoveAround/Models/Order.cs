
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoveAround.Models
{
    [Authorize]
    public  class Order
    {
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Krovinio tipas")]
        public string Tipas { get; set; } = "tipas"; //gauname is Select listo 

        [Required]
        [Display(Name = "Pakrovimo Būdas")]
        public string PakrovimoTipas { get; set; } =  "tipas";
        [NotMapped]
        public List<SelectListItem> PakrovimoTipai { get; } = new List<SelectListItem>() 
        {
            new SelectListItem {Value = "Pilnas", Text = "Pilnas"},
            new SelectListItem {Value = "Dalinis", Text = "Dalinis"},
            new SelectListItem {Value = "Per viršų", Text = "Per viršų"},
            new SelectListItem {Value = "Per šoną", Text = "Per šoną"},
            new SelectListItem {Value = "Per galą", Text = "Per galą"},
            new SelectListItem {Value = "TIR", Text = "TIR"},//kas tas?
            new SelectListItem {Value = "Su liftu", Text = "Su liftu"},
            new SelectListItem {Value = "Rankinis", Text = "Rankinis"},
            new SelectListItem {Value = "Kita", Text = "Kita"},
        };
        //Krovinių Tipams
        [NotMapped]
        public List<SelectListItem> Tipai { get; } = new List<SelectListItem>()
        {
           
            new SelectListItem {Value = "Alkoholiniai Gėrimai", Text = "Alkoholiniai Gėrimai"},
            new SelectListItem {Value = "Alus", Text = "Alus"},
            new SelectListItem {Value = "Audiniai, siūlai, verpalai", Text = "Audiniai, siūlai, verpalai"},
            new SelectListItem {Value = "Augalinis aliejus", Text = "Augalinis aliejus"},
            new SelectListItem {Value = "Automobiliai", Text = "Automobiliai"},
            new SelectListItem {Value = "Avalynė", Text = "Avalynė"},
            new SelectListItem {Value = "Bačkos", Text = "Bačkos"},
            new SelectListItem {Value = "Baldai", Text = "Baldai"},
            new SelectListItem {Value = "Dideli Maišai (Big Bag)", Text = "Dideli Maišai (Big Bag)"},
            new SelectListItem {Value = "Birus Krovinys", Text = "Birus Krovinys"},
            new SelectListItem {Value = "Buitnė technika", Text = "Buitnė technika"},
            new SelectListItem {Value = "Chemijos gaminiai", Text = "Chemijos gaminiai"},
            new SelectListItem {Value = "Cukrus", Text = "Cukrus"},
            new SelectListItem {Value = "Daržovės", Text = "Daržovės"},
            new SelectListItem {Value = "Dažai, lakas", Text = "Dažai, lakas"},
            new SelectListItem {Value = "Dėžės", Text = "Dėžės"},
            new SelectListItem {Value = "Drabužiai", Text = "Drabužiai"},
            new SelectListItem {Value = "Druska", Text = "Druska"},
            new SelectListItem {Value = "Durpės", Text = "Durpės"},
            new SelectListItem {Value = "Fanera", Text = "Fanera"},
            new SelectListItem {Value = "Gaivieji gėrimai", Text = "Gaivieji gėrimai"},

            new SelectListItem {Value = "Grūdai, Sėklos", Text = "Grūdai, Sėklos"},
            new SelectListItem {Value = "Grybai", Text = "Grybai"},
            new SelectListItem {Value = "Guma ir jos gaminiai", Text = "Guma ir jos gaminiai"},
            new SelectListItem {Value = "augalai", Text = "Gyvi augalai"},
            new SelectListItem {Value = "Kanceliarijos prekė", Text = "Kanceliarijos prekės"},
            new SelectListItem {Value = "Kazeinas", Text = "Kazeinas"},
            new SelectListItem {Value = "Kilimai", Text = "Kilimai"},
            new SelectListItem {Value = "Kompiuteriai ir elektronika", Text = "Kompiuteriai ir elektronika"},
            new SelectListItem {Value = "Konservai", Text = "Konservai"},
            new SelectListItem {Value = "Konteineris", Text = "Konteineris"},
            new SelectListItem {Value = "Ledai", Text = "Ledai"},
            new SelectListItem {Value = "Masito prekės", Text = "Masito prekės"},
            new SelectListItem {Value = "Makalatūra", Text = "Makalatūra"},
            new SelectListItem {Value = "Medvilnės", Text = "Medvilnės"},
            new SelectListItem {Value = "Medžio anglis", Text = "Medžio anglis"},
            new SelectListItem {Value = "Mėsa", Text = "Mėsa"},
            new SelectListItem {Value = "Metalo gaminiai", Text = "Metalo gaminiai"},
            new SelectListItem {Value = "Metalo laužas", Text = "Metalo laužas"},
            new SelectListItem {Value = "Naftos produktai", Text = "Naftos produktai"},
            new SelectListItem {Value = "Nesupakuotas", Text = "Nesupakuotas"},

            new SelectListItem {Value = "Odos Gaminiai", Text = "Odos Gaminiai"},
            new SelectListItem {Value = "Odos, sudytos", Text = "Odos, sudytos"},
            new SelectListItem {Value = "Padėklai", Text = "Padėklai"},
            new SelectListItem {Value = "Parfumerija, kosmetika", Text = "Parfumerija, kosmetika"},
            new SelectListItem {Value = "Paukštiena", Text = "Paukštiena"},
            new SelectListItem {Value = "PienoMiltai", Text = "PienoMiltai"},
            new SelectListItem {Value = "Plastikas", Text = "Plastikas"},
            new SelectListItem {Value = "Popierius", Text = "Popierius"},
            new SelectListItem {Value = "Puspriekabė", Text = "Puspriekabė"},
            new SelectListItem {Value = "Rinktinis krovinys", Text = "Rinktinis krovinys"},
            new SelectListItem {Value = "Stiklas, poceliasnas", Text = "Stiklas, poceliasnas"},
            new SelectListItem {Value = "Tabako gaminiai", Text = "Tabako gaminiai"},
            new SelectListItem {Value = "Tara ir Pakuotė", Text = "Tara ir Pakuotė"},
            new SelectListItem {Value = "Trašos", Text = "Trašos"},
            new SelectListItem {Value = "Ūkinės prekės", Text = "Ūkinės prekės"},
             new SelectListItem {Value = "Kita", Text = "Kita"}
       
    };

        
        [Display(Name = "Krovinio svoris (kg.)")]
        [Required(ErrorMessage ="Būtina nurodyti Krovino svorį (kg.)")]
        public double Svoris { get; set; }

        [Display(Name = "Krovinio tūris (kūb. m.)")]
        public double Turis { get; set; }
        [Display(Name = "Krovinio ilgis (m.)")]
        public double Ilgis { get; set; }
        [Display(Name = "Krovinio palečių skaičius (vnt.)")]
        public int PaleciuSk { get; set; }

        [Display(Name = "Būtina temperatūra (C)")]
        public double Temperatura { get; set; }

        [Display(Name = "Papildomas komentras")]
        public string PapildomaInfo { get; set; }


        [Display(Name = "Pageidaujama kaina (Eur)")]
        public double Kaina { get; set; }


        [Display(Name = "Pilnas pasikrovimo adresas ")]
        public string FromAddress { get; set; }


        [Display(Name = "Pilnas Išsikrovimo adresas ")]
        public string ToAddress { get; set; }
        public AppUser Uzsakovas { get; set; }
        public int UzsakovasId { get; set; }

        //ADRESAI IS:

       [Required(ErrorMessage ="Pasikrovimo adreso gatvės pavadinimas yra privalomas")]
       [MaxLength(50,ErrorMessage ="Gatvės pavadinimas laukas negali būti ilgesnis nei 50 ženklų.")]       
       [Display(Name ="Gatvės Pavadinimas")]
        public string FromStreet { get; set; }


        [Required(ErrorMessage = "Pasikrovimo adreso gatvės namo Numeris yra privalomas. ")]
        [MaxLength(15, ErrorMessage = "namo numeris negali būti ilgesnis nei 15 ženklų.")]
        [Display(Name = "Namo nr.")]
        public string FromHouseNo { get; set; }


        [Required(ErrorMessage = "Pasikrovimo adreso miestas yra privalomas. Spauskite ...")]
        [MaxLength(50, ErrorMessage = "Miesto Pavadinimas negali būti ilgesnis nei 50 ženklų.")]
        [Display(Name = "Miestas")]
        public string FromCity { get; set; }


        [MaxLength(75, ErrorMessage = "Apskritis negali būti ilgesnė nei 75 ženklų.")]
        [Display(Name = "Apskritis")]
        public string FromDistrict { get; set; }


        [MaxLength(16, ErrorMessage = "Pašto kodas negali būti ilgesnis nei 16 ženklų.")]
        [Display(Name = "Pašto Kodas")]
        public string FromZipCode { get; set; }


        [Required(ErrorMessage = "Pasikrovimo adreso  Šalis yra privaloma.")]
        [MaxLength(100, ErrorMessage = "Šalis negali būti ilgesnė nei 100 ženklų.")]
        [Display(Name = "Šalis")]
        public string FromCountry { get; set; }
        
        
        public double FromLatitude { get; set; }
        public double FromLongtitude { get; set; }


        //Adresai i
        [Required(ErrorMessage = "Išsikrovimo adreso Gatvės pavadinimas yra privalomas.")]
        [MaxLength(50, ErrorMessage = "Gatvės pavadinimas laukas negali būti ilgesnis nei 50 ženklų.")]
        [Display(Name = "Gatvės Pavadinimas")]
        public string ToStreet { get; set; }


        [Required(ErrorMessage = "Išsikrovimo adreso Namo Numeris yra privalomas.")]
        [MaxLength(15, ErrorMessage = "namo numeris negali būti ilgesnis nei 15 ženklų.")]
        [Display(Name = "Namo nr.")]
        public string ToHouseNo { get; set; }


        [Required(ErrorMessage = "Išsikrovimo adreso Miestas yra privalomas.")]
        [MaxLength(50, ErrorMessage = "Miesto Pavadinimas negali būti ilgesnis nei 50 ženklų.")]
        [Display(Name = "Miestas")]
        public string ToCity { get; set; }

        [MaxLength(75, ErrorMessage = "Apskritis negali būti ilgesnė nei 75 ženklų.")]
        [Display(Name = "Apskritis")]
        public string ToDistrict { get; set; }


        [MaxLength(16, ErrorMessage = "Pašto kodas negali būti ilgesnis nei 16 ženklų.")]
        [Display(Name = "Pašto Kodas")]
        public string ToZipCode { get; set; }


        [Required(ErrorMessage = "Išsikrovimo adreso Šalis yra privaloma.")]
        [MaxLength(100, ErrorMessage = "Šalis negali būti ilgesnė nei 100 ženklų.")]
        [Display(Name = "Šalis")]
        public string ToCountry { get; set; }
        
        //ilguma ir platuma

       //Reikes padaryti required arba implementuoti kita būda kaip gauti position
        public double ToLatitude { get; set; }
      
        public double ToLongtitude { get; set; }

        //data
        
         
        [Display(Name = "Pasikrovimo diena")]
        [Column(TypeName = "date")]        
        public DateTime? FDate { get; set; }

        
        
        [Display(Name = "Išsikrovimo diena")]
        [Column(TypeName = "date")]
       
        public DateTime? TDate { get; set; }

      





        //Gauname pilną gatvės pavadinimą iš kur važiuoja.
        public string GetFullFromAddress()
        {
            return this.FromStreet + " " + this.FromHouseNo + ", " + this.FromCity + ", " + this.FromCountry + ".";

        }

        //Gauname pilną gatvės pavadinimą į kur važiuoja.
        public string GetFullToAddress()
        {
            return this.ToStreet + " " + this.ToHouseNo + ", " + this.ToCity + ", " + this.ToCountry + ".";

        }
    }
    
}
