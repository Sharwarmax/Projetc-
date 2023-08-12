using Carsharing_Lombardi_Saturnio.Models;
using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.ViewModels
{
    public class EditOfferViewModel : TravelDetails
    {
        private float numkm;
        private float price;

        [Display(Name = "Number of kms")]
        [Required(ErrorMessage = "The kilometers field is required!"), Range(1, 2000, ErrorMessage = "Invalid value for kms!")]
        public float Numkm { get => numkm; set => numkm = value; }
        [Display(Name = "Price (€)")]
        [Required(ErrorMessage = "The price field is required!"), Range(0.5, 3, ErrorMessage = "The price must be between 0.5 and 3 €!")]
        public float Price { get => price; set => price = value; }

        public EditOfferViewModel() { }
       
    }
}
