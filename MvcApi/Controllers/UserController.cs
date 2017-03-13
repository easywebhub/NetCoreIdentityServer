using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using id.data.Repositories;
using MvcApi.Dtos;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcApi.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("users")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            
            return new ObjectResult(_userRepository.GetAll().ToList());
        }

        [Authorize(Policy ="APIWRITE")]
        [Route("userswrite")]
        [HttpGet]
        public IActionResult Users()
        {
            var l = new List<string>() { "Tome", "Jerry", "Write" };
            return new ObjectResult(l);
        }

        
    }
}
