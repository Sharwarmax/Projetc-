namespace Carsharing_Lombardi_Saturnio.Models
{
    public abstract class TravelDetails
    {
        private string destination;
        private DateTime date;
        private DateTime departuretime;
        private string startpoint;


        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public DateTime DepartureTime
        {
            get { return departuretime; }
            set { departuretime = value; }
        }
        public string StartPoint
        {
            get { return startpoint; }
            set { startpoint = value; }
        }
    }
}
