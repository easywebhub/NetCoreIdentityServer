using id.application.Entities;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace id.application.Dtos
{
    public class UserDetailDto
    {
        #region properties
        public string UserId { get; private set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public List<UserClaim> UserClaims { get; set; }
        #endregion

        public UserDetailDto() { }

        public UserDetailDto(XUser user)
        {
            this.UserId = user.UserId;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.Status = user.Status;
            this.UserClaims = new List<UserClaim>();
        }
    }
}
