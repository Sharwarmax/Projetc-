using Carsharing_Lombardi_Saturnio.Models;
using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.ViewModels
{
    public class InsertOfferViewModel : TravelDetails
    {
        private int id_offer;
        private float numkm;
        private int nbpassengersmax;
        private float price;

        [Display(Name = "Number of kms")]
        [Required(ErrorMessage = "The kilometers field is required!"), Range(1, 2000, ErrorMessage = "Invalid value for kms!")]
        public float Numkm { get => numkm; set => numkm = value; }
        [Required(ErrorMessage = "The price field is required!"), Range(0.5, 3, ErrorMessage = "The price must be between 0.5 and 3 €!")]
        public float Price { get => price; set => price = value; }
        [Display(Name = "Maximum number of passengers")]
        [Required(ErrorMessage = "The maximum number of passengers must have a value!"), Range(1,4, ErrorMessage = "You must insert between 1 and 4 passengers")]
        public int NbPassengerMax { get => nbpassengersmax; set => nbpassengersmax = value; }
    }
}
