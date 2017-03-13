using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using id.middleware.common;
using MvcApi.Dtos;
using id.application.Managers;
using id.application.Dtos;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcApi.Controllers
{
    [ValidateModel]
    public class AuthController : BaseApiController
    {
        private readonly IAccountManager _accountManager;

        public AuthController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [Route("auth/register")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterAccountRequestDto dto)
        {
            if (_accountManager.CreateUser(dto.ToModel(new id.application.Dtos.AddUserDto())).Result)
            {
                return Ok(_accountManager.XUserAdded);
            }
            return ServerError(_accountManager);
        }

        public async Task<IActionResult> Login([FromBody] SignInRequestDto dto)
        {
            if (_accountManager.SignIn(dto).Result)
            {
                return Ok(_accountManager.XUserLogged.GetFullInfo());
            }
            return ServerError(_accountManager);
        }
        
    }
}
