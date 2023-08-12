using Carsharing_Lombardi_Saturnio.Models;

namespace Carsharing_Lombardi_Saturnio.DAL.IDAL
{
    public interface IRequestDAL
    {
        public List<Request> GetRequests(User passenger);
        public Request GetRequest(int Id_Request);

        public bool RemoveRequest(Request request);
        public bool InsertRequest(Request request);

    }
}
