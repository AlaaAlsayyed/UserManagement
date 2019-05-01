using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _usersService;

        public UsersController(UserService userService)
        {
            _usersService = userService;
        }

        /// <summary>
        /// List all users in mongo db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _usersService.Get();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var User = _usersService.Get(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        /// <summary>
        ///  insert new user in Users collection
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _usersService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        /// <summary>
        /// get users who are matching the filter criteria
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("search")]
        public ActionResult<List<User>> Search(SearchUserModel search)
        {
            var matchingUsers = _usersService.Get();

            if (!string.IsNullOrEmpty(search.name))
                matchingUsers = matchingUsers.Where(k => k.name.Contains(search.name)).ToList();

            if (!string.IsNullOrEmpty(search.username))
                matchingUsers = matchingUsers.Where(k => k.username.Contains(search.username)).ToList();

            if (!string.IsNullOrEmpty(search.companyName))
                matchingUsers = matchingUsers.Where(k => k.company != null && k.company.name.Contains(search.companyName)).ToList();

            if (!string.IsNullOrEmpty(search.addressZipcode))
                matchingUsers = matchingUsers.Where(k =>  k.address != null && k.address.zipcode.Contains(search.addressZipcode)).ToList();

            return matchingUsers;
        }

        /// <summary>
        /// replace user who has this id with current current users
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserIn"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User UserIn)
        {
            var User = _usersService.Get(id);

            if (User == null)
            {
                return NotFound();
            }

            _usersService.Update(id, UserIn);

            return NoContent();
        }

        /// <summary>
        ///  Delete user who has this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var User = _usersService.Get(id);

            if (User == null)
            {
                return NotFound();
            }

            _usersService.Remove(User.Id);

            return NoContent();
        }
    }
}