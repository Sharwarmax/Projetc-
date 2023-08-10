using Carsharing_Lombardi_Saturnio.DAL;
using Carsharing_Lombardi_Saturnio.DAL.IDAL;

namespace Carsharing_Lombardi_Saturnio.Models
{
    public class Request : TravelDetails
    {
        private User user = new();

        public User User { get => user; set => user = value; }

        public static List<Request> GetRequests(IRequestDAL _requestDAL) => _requestDAL.GetRequests();

        public static Request GetRequest(int id, IRequestDAL _requestDAL) => _requestDAL.GetRequest(id);

        public bool InsertRequest(IRequestDAL _requestDAL) => _requestDAL.InsertRequest(this);

		public bool RemoveRequest(IRequestDAL _requestDAL) => _requestDAL.RemoveRequest(this);
    }
}
