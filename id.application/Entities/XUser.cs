using id.application.Dtos;
using id.common.Entities;
using id.core;
using id.data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace id.application.Entities
{
    public class XUser : XEntityBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;

        public XUser(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public XUser(ApplicationUser user, IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager): this(userRepository, userManager, signInManager)
        {
            MapFrom(user);
        }

        public XUser(string userId, IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager): this(userRepository, userManager, signInManager)
        {
            var user = _userRepository.Get(userId);
            MapFrom(user);
        }

        #region properties
        public string UserId { get; private set; }
        public string Password { get; private set; }
        public string PasswordSaft { get; private set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        #endregion

        #region ext properties
        private ApplicationUser _user;
        #endregion

        #region methods
        public UserDetailDto GetFullInfo()
        {
            return new UserDetailDto(this);
        }

        public async Task<bool> Create(AddUserDto dto)
        {
            this.Email = dto.Email;
            this.UserName = !string.IsNullOrEmpty(dto.Username) ? dto.Username : this.Email;
            this.PhoneNumber = dto.PhoneNumber;
            this.Status = "Active";

            _user = new ApplicationUser { UserName = this.UserName, Email = this.Email, PhoneNumber = this.PhoneNumber };
            var result = await _userManager.CreateAsync(_user, dto.Password);
            if (result.Succeeded)
            {
                UserId = _user.Id;
                //SelfSync();
                return true;
            }else
            {
                this.XStatus = common.GlobalStatus.Failed;
            }
            return false;
        }
       
        #endregion

        #region private methods
        private void MapFrom(ApplicationUser user)
        {
            if (user == null) return;
            this._user = user;

            this.UserId = user.Id;
            this.UserName = user.UserName;
            this.Status = "Active";
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
        }
        #endregion
    }
}
