using Carsharing_Lombardi_Saturnio.Models;
using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.ViewModels
{
    public class EditOfferViewModel : TravelDetails
    {
        private int id_offer;
        private float numkm;
        private float price;

        public int Id_Offer { get => id_offer; set => id_offer = value; }
        [Display(Name = "Number of kms")]
        [Required(ErrorMessage = "The kilometers field is required!"), Range(1, 2000, ErrorMessage = "Invalid value for kms!")]
        public float Numkm { get => numkm; set => numkm = value; }
        [Required(ErrorMessage = "The price field is required!"), Range(0.5, 3, ErrorMessage = "The price must be between 0.5 and 3 €!")]
        public float Price { get => price; set => price = value; }

        public EditOfferViewModel() { }
        public EditOfferViewModel(Offer offer)
        {
            Id_Offer = offer.Id_Offer;
            Numkm = offer.Numkm;
            Price = offer.Price;
            Destination= offer.Destination;
            StartPoint= offer.StartPoint;
            Date = offer.Date;
            DepartureTime= offer.DepartureTime;
        }
    }
}
