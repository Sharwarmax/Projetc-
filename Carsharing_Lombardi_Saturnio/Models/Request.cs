namespace Carsharing_Lombardi_Saturnio.Models
{
    public class Request : TravelDetails
    {
        private int id_request;

        public int Id_Request { get => id_request; set => id_request = value; }

        public void GetRequests() { }

        public void GetRequest(int id) { }

        public void AcceptRequest() { }

        public void InsertRequest() { }

        public void RemoveRequest() { }
    }
}
