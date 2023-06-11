using Carsharing_Lombardi_Saturnio.DateAttribute;
using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.Models
{
    public abstract class TravelDetails
    {
        private string destination;
        private DateTime date;
        private DateTime departuretime;
        private string startpoint;

        [Required(ErrorMessage = "The destination field is required!"), StringLength(30, ErrorMessage = "the name of destination is invalid!")]
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        [Required(ErrorMessage = "The date field is required!"), DateY(ErrorMessage = "The date must be either today or next days.")]
        [DataType(DataType.Date)]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        [Display(Name = "Departure time")]
        [Required(ErrorMessage ="The departure time field is required!")]
        [DataType(DataType.Time)]
        public DateTime DepartureTime
        {
            get { return departuretime; }
            set { departuretime = value; }
        }


        [Display(Name = "Starting point")]
        [Required(ErrorMessage = "The starting point field is required!"), StringLength(30, ErrorMessage = "the name of the starting point is invalid!")]
        public string StartPoint
        {
            get { return startpoint; }
            set { startpoint = value; }
        }
    }
}
