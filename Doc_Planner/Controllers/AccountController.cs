
using Doc_Planner.Identity;
using Doc_Planner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace Doc_Planner.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<AppUser> SignInMgr;
        private UserManager<AppUser> UserMgr;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
        {
            UserMgr = userManager;
            SignInMgr = signManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserViewModel appUser)
        {
            var result = await SignInMgr.PasswordSignInAsync(appUser.UserName, appUser.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.Result = "result is : " + result.ToString();
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(AppUserViewModel appUser)
        {
            try
            {  
               AppUser user = await UserMgr.FindByEmailAsync(appUser.Email);
                    if (user == null)
                    {
                        user = new AppUser();
                        user.Email = appUser.Email;
                        user.Nom = appUser.Nom;
                        user.Prenom = appUser.Prenom;
                        user.UserName = appUser.UserName;
                       
                        IdentityResult result = await UserMgr.CreateAsync(user,appUser.Password);

                    if (result.Succeeded)
                    {
                        ViewBag.Message = "l'utilisateur a été crée";
                        await SignInMgr.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }

                }
                
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
            }

            return View(/*appUser*/);
        }


      
    }
}
