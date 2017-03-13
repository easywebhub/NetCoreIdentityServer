using id.application.Dtos;
using id.application.Entities;
using id.common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace id.application.Managers
{
    public interface IAccountManager: IXEntityBase
    {
        XClient XClientAdded { get; }
        XClient InitClient();
        XClient GetClient(int clientId);
        List<XClient> GetListClient();

        XUser XUserAdded { get; }
        XUser XUserLogged { get; }
        XUser InitUser();
        XUser GetUser(string userId);
        XUser GetUserByUsername(string username);
        Task<bool> CreateUser(AddUserDto dto);
        Task<bool> SignIn(SignInRequestDto dto);
        List<XUser> GetListUser();
    }
}
