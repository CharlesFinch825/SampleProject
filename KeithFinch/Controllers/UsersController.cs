using KeithFinch.Models;
using KeithFinch.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KeithFinch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets a List of Users (URL:  GET https://localhost:44376/users )
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        /// <summary>
        /// Gets a user By Id  (URL:  GET https://localhost:44376/users/{id} )
        /// </summary>
        /// <param name="id">Id of the user to retrieve</param>
        /// <returns>A User</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _userService.GetUser(id));
        }

        /// <summary>
        /// Creates a new User (URL: POST https://localhost:44376/users )
        /// </summary>
        /// <param name="request">ex: {
        ///                             "Email":"testing@email.com",
        ///                             "Enabled": true
        ///                           }
        /// </param>
        /// <returns>Newly Created User</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User request)
        {
            return Ok(await _userService.CreateUser(request));
        }

        /// <summary>
        /// Updates a user (URL: PUT https://localhost:44376/users/{id} )
        /// </summary>
        /// <param name="id">Id of the user to update</param>
        /// <param name="request">User Model</param>
        /// <returns>Updated User</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserModel request)
        {
            if (id != request.Id) { return BadRequest("Id Mismatch"); }

            var user = _userService.GetUser(id);
            if (user == null) { return NotFound("User Not Found"); }

            return Ok(await _userService.UpdateUser(request));
        }

        /// <summary>
        /// Updates a user with a list of Patches (URL: PATCH https://localhost:44376/users/{id} )
        /// </summary>
        /// <param name="id">Id of the user to update</param>
        /// <param name="patchDocument">List of Json Patch values 
        /// ex: [{
        ///     "value": "true",
        ///   "path": "/enabled",
        ///   "op": "replace"
        ///  },
        ///{
        ///   "value": "new@email.com",
        ///   "path": "/email",
        ///   "op": "replace"
        /// }]</param>
        /// <returns>Updated User</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, [FromBody] JsonPatchDocument<UserModel> patchDocument)
        {
            UserModel user = await _userService.GetUser(id); 

            patchDocument.ApplyTo(user);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.UpdateUser(user));
        }

        /// <summary>
        /// Deletes a User By Id  (URL: DELETE https://localhost:44376/users/{id} )
        /// </summary>
        /// <param name="id">Id of the user to delete</param>
        /// <returns>Boolean representing Success or Failure</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = _userService.GetUser(id);
            if (user == null) { return NotFound("User Not Found"); }

            return Ok(await _userService.DeleteUser(id));
        }
    }
}
