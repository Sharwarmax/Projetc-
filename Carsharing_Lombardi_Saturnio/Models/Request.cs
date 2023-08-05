using Carsharing_Lombardi_Saturnio.IDAL;

namespace Carsharing_Lombardi_Saturnio.Models
{
    public class Request : TravelDetails
    {
        private User user = new();

        public User User { get => user; set => user = value; }

        public static List<Request> GetRequests(IRequestDAL _requestDAL) => _requestDAL.GetRequests();

        public static Request GetRequest(int id, IRequestDAL _requestDAL) => _requestDAL.GetRequest(id);

        public void InsertRequest() { }

        public bool RemoveRequest(IRequestDAL _requestDAL) => _requestDAL.RemoveRequest(this);
    }
}
