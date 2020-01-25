﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName,
                user.Role
            };
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("forAdmin")]
        public string GetForAdmin()
        {
            return "Hello ADMIN";
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("forUser")]
        public string GetForUser()
        {
            return "Hello normal USER";
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        [Route("forAll")]
        public string GetForAll()
        {
            return "Hello ALL";
        }
    }
}