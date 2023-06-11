using Carsharing_Lombardi_Saturnio.Models;

namespace Carsharing_Lombardi_Saturnio.IDAL
{
    public interface IUserDAL
    {
        public bool CheckUsername(string username);

        public bool Login(User user);

        public bool Register(User user);
    }
}
