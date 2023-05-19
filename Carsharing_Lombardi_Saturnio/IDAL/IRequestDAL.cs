using Carsharing_Lombardi_Saturnio.Models;

namespace Carsharing_Lombardi_Saturnio.IDAL
{
    public interface IRequestDAL
    {
        public List<Request> GetRequests();
        public Request GetRequest(int Id_Request);

        public bool RemoveRequest(Request request);


    }
}
