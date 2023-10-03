using System.ComponentModel.DataAnnotations;

namespace Brotten.Models
{
    public class Tbl_Intagen
    {
        public Tbl_Intagen() { }

       
        public int Intagen_nr { get; set; }

        [Required(ErrorMessage = "Förnamn måste fyllas i ")]
        public string Fornamn { get; set; }
        [Required(ErrorMessage = "Efternamn måste fyllas i ")]
        public string Efternamn { get; set; }

        public string Brott_Typ { get; set; }

    }
}
