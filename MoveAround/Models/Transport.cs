using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MoveAround.Models
{
    public class Transport
    {
        [Key]
        public int TransportId { get; set; }
        //REFERENCES
        public AppUser Uzsakovas { get; set; }
        public int UzsakovasId { get; set; }


        //LOCATION
        [Display(Name ="Adresas kur yra transportas")]
        [MaxLength(300,ErrorMessage ="Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        public string FullFromLocation { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Šalis")]
        public string LocationFromCountry { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Apskirtis")]
        public string LocationFromDistrict { get; set; }
        [MaxLength(100, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Miestas")]
        public string LocationFromCity { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Gatvė")]
        public string LocationFromStreet { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Namo numeris")]
        public string LocationFronNumber { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Pašto kodas")]
        public string LocationFromZipCode { get; set; }
        public double LocationFromLatitude { get; set; }
        public double LocationFromLongitude { get; set; }

        [MaxLength(300, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Adresas kur norima važiuoti")]
        public string FullToLocation { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Šalis")]
        public string LocationToCountry { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Apskritiss")]
        public string LocationToDistrict { get; set; }
        [MaxLength(100, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Miestas")]
        public string LocationToCity { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Gatvė")]
        public string LocationToStreet { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Namo numeris")]
        public string LocationFromNumber { get; set; }
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        [Display(Name = "Pašto kodas")]
        public string LocationToZipCode { get; set; }
       

        public double LocationToLatitude { get; set; }
      
        public double LocationToLongitude { get; set; }
     
        //LOCATION FINISH
        //DATE
       [Display(Name ="Galima naudotis nuo:")]
        public DateTime? GalimaNuoData { get; set; }
        [Display(Name ="Tokių galima")]
        public int TransportoSkaicius { get; set; } = 1;
        [Display(Name = "Pakrovimo Tipas")]

        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        public string PakrovimoTipas { get; set; }

        [Display(Name = "Sunkvežimio tipas")]
        [MaxLength(50, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        public string SunkvezimioTipas { get; set; }
        //NOTMAPPED
        [NotMapped]
        public List<SelectListItem> SunkvezimioTipai { get; } = new List<SelectListItem>
        {  new SelectListItem {Value = "Automobilvežis", Text = "Automobilvežis"},
           new SelectListItem {Value = "Gyvuliaviažis", Text = "Gyvuliaviažis"},  
           new SelectListItem {Value = "Konteinerinė Puspriekabė", Text = "Konteinerinė Puspriekabė"},
           new SelectListItem {Value = "Mega 100 m3", Text = "Mega 100 m3"}, 
           new SelectListItem {Value = "Miškavežis", Text = "Miškavežis"},            
           new SelectListItem {Value = "Puspriekabės su refrižeratoriumi", Text = "Puspriekabės su refrižeratoriumi"},
           new SelectListItem {Value = "Puspriekabė su platforma", Text = "Puspriekabė su platforma"},       
           new SelectListItem {Value = "Savivartis", Text = "Savivartis"},
           new SelectListItem {Value = "Tentas 120 m3", Text = "Tentas 120 m3"},
           new SelectListItem {Value = "Tentas 82-92 m3", Text = "Tentas 82-92 m3"},
           new SelectListItem {Value = "Izoterminis", Text = "Izoterminis"},
           new SelectListItem {Value = "Vilkikas su cisterna", Text = "Vilkikas su cisterna"},
        };
        public bool ArGalimas { get; set; } = true;

        [NotMapped]
        public List<SelectListItem> PakrovimoTipai { get; } = new List<SelectListItem>
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

        //EXTRAINFO

        [Display(Name = "Maksimalus krovinio svoris (kg.).")]       
        public double Svoris { get; set; }

        [Display(Name = "Maksimalus krovinio tūris (kūb. m.).")]
        public double Turis { get; set; }
        [Display(Name = " Maksimalus krovinio ilgis (m.).")]
        public double Ilgis { get; set; }
        [Display(Name = "Maksimalus palečių skaičius (vnt.).")]
        public int PaleciuSk { get; set; } = 1;

        [Display(Name = "Reguliuojama temperatūra?")]
        public bool Temperatura { get; set; } = false;

        [Display(Name = "Papildomas komentras.")]
        [MaxLength(1500, ErrorMessage = "Eilutės '{0}' maksimalus ženklų skaičius yra: {1}.")]
        public string PapildomaInfo { get; set; }


        [Display(Name = "Pageidaujama kaina (Eur).")]
        public double Kaina { get; set; }


        //METHODS
        public string GetFromLocation()
        {
            return this.LocationFromCountry + ", " + this.LocationFromCity + ", " + this.LocationFromStreet + ", " + this.LocationFromNumber;
        }
    }

    
}
