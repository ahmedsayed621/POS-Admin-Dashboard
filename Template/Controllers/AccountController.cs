using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.BL.Helper;
using Template.BL.Models;

namespace Template.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(ILogger logger,UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Registration (Sign Up)


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = new IdentityUser()
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                }

                return View(model);

            }catch(Exception es)
            {
                return View(model);
            }
            
        }

        #endregion


        #region Login (Sign In)


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                  var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("","User or Password Invalid");
                    }
                   
                
                }

                return View(model);

            }
            catch (Exception es)
            {
                return View(model);
            }
        }
        #endregion


        #region Log of ( Sign Out )

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {

                        // Generate Token
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);

                        // Generate Password Reset Link
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                        SendMailHelper.MailSender(new MailVM() { Title = "Password Reset" , Message = passwordResetLink , Email = user.Email });

                        logger.Log(LogLevel.Information, passwordResetLink);

                        return RedirectToAction("ConfirmForgetPassword");
                    }

                    return RedirectToAction("ConfirmForgetPassword");
                }

                return View(model);

            }
            catch (Exception es)
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        #endregion


        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string Email,string Token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("ConfirmResetPassword");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }

                    return RedirectToAction("ConfirmResetPassword");
                }

                return View(model);

            }
            catch (Exception es)
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        #endregion


    }
}
