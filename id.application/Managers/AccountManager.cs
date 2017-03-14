using id.common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using id.application.Entities;
using id.data.Repositories;
using id.common;
using Microsoft.AspNetCore.Identity;
using id.core;
using id.application.Dtos;

namespace id.application.Managers
{
    public class AccountManager : XEntityBase, IAccountManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IXMapper _xMapper;

        public AccountManager(IClientRepository clientRepository, IUserRepository userRepository, IXMapper xMapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _xMapper = xMapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region client
        public XClient XClientAdded { get; private set; }

        public XClient GetClient(int clientId)
        {
            var client = _clientRepository.Get(clientId);
            if (client == null)
            {
                XStatus = GlobalStatus.NotFound;
                return null;
            }
            return new XClient(client, _clientRepository, _xMapper);
        }
        
        public List<XClient> GetListClient()
        {
            return _xMapper.ToXClients(_clientRepository.GetAll().ToList());
        }

        public XClient InitClient()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region user
        public XUser XUserAdded { get; private set; }
        public XUser XUserLogged { get; private set; }

        public XUser GetUser(string userId)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
            {
                XStatus = GlobalStatus.NotFound;
                return null;
            }
            return new XUser(user, _userRepository, _userManager, _signInManager);
        }

        public XUser GetUserByUsername(string username)
        {
            var user = _userRepository.GetAll().Where(x=>x.UserName==username).FirstOrDefault();
            if (user == null)
            {
                XStatus = GlobalStatus.NotFound;
                return null;
            }
            return new XUser(user, _userRepository, _userManager, _signInManager);
        }

        public async Task<bool> CreateUser(AddUserDto dto)
        {
            var ewhAccount = InitUser();
            var check = false;
            if (await ewhAccount.Create(dto))
            {
                check = true;
                this.XUserAdded = ewhAccount;
            }
            SyncStatus(this, ewhAccount);
            return check;
        }

        public async Task<bool> SignIn(SignInRequestDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var ewhAccount = GetUserByUsername(dto.Username);
                this.XUserLogged = ewhAccount;
                //_logger.LogInformation(1, "User logged in.");
                return true;
            }else
            {
                this.XStatus = GlobalStatus.UnSuccess;
            }
            return false;
        }

        public List<XUser> GetListUser()
        {
            return new List<XUser>();
        }

        public XUser InitUser()
        {
            return new XUser(_userRepository, _userManager, _signInManager);
        }

       
        #endregion
    }
}
