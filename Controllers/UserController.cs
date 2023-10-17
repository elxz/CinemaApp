using Cinema.Context;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Web;
using Npgsql;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Components.Forms;
using Cinema.Service.Interfaces;
using Cinema.Models.ViewModels;
using Cinema.Service.Implementations;

namespace Cinema.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsers();
            if (users.StatusCode == Enum.StatusCode.OK)
            {
                return View(users.Data.ToList());
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var users = await userService.GetUser(id);
            if (users.StatusCode == Enum.StatusCode.OK)
            {
                return View(users.Data);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.user_id == 0)
                {
                    await userService.CreateUser(user);
                }
                else
                {
                    await userService.Edit(user);
                }
            }

            return RedirectToAction("Index", "Database");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser()
        {
            var temp_users = await userService.GetUsers();
            if (temp_users.StatusCode == Enum.StatusCode.OK)
            {
                var users = temp_users.Data.Where(x => x.role_ == "Cashier").ToList();
                if (users.Count != 0)
                {
                    var Users = users.Select(x => new
                    {
                        user_id = x.user_id,
                        req_info = x.user_firstname + " " + x.user_lastname + " | " + x.user_login
                    });
                    ViewBag.Users = new SelectList(Users, "user_id", "req_info");

                    return View();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(User user)
        {
            var users = await userService.DeleteUser(user);
            if (users.StatusCode == Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "Database");
            }

            return RedirectToAction("Index");
        }
    }
}