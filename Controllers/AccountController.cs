using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SIGEM_BIDSS.Controllers
{
    public class AccountController : Controller
    {

        public AccountController(IOptionsMonitor<AzureADOptions> options)
        {
            Options = options;
        }

        public IOptionsMonitor<AzureADOptions> Options { get; }

        [HttpGet]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action(nameof(HomeController.Index), "Home");
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        //[HttpGet]
        //public IActionResult SignOut()
        //{
        //    var callbackUrl = Url.Action(nameof(SignedOut), "Account", values: null, protocol: Request.Scheme);
        //    SignOut(
        //        new AuthenticationProperties { RedirectUri = callbackUrl },
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        OpenIdConnectDefaults.AuthenticationScheme);
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        // Redirect to home page if the user is authenticated.
        //        return RedirectToAction(nameof(HomeController.Index), "Home");
        //    }

        //    return RedirectToAction(nameof(LoginController.Index), "Login");

        //}

        //[HttpGet]
        //public IActionResult SignOut()
        //{
        //    var callbackUrl = Url.Action(nameof(SignedOut), "Account", values: null, protocol: Request.Scheme);
        //    return SignOut(
        //        new AuthenticationProperties { RedirectUri = callbackUrl },
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        OpenIdConnectDefaults.AuthenticationScheme);
        //}


        [HttpGet]
        public IActionResult SignOut([FromRoute] string scheme)
        {
            scheme = scheme ?? AzureADDefaults.AuthenticationScheme;
            var options = Options.Get(scheme);
            //var callbackUrl = Url.Page("/Account/SignedOut", pageHandler: null, values: null, protocol: Request.Scheme);
            var callbackUrl = Url.Action(nameof(SignedOut), "Account", values: null, protocol: Request.Scheme);
            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                options.CookieSchemeName,
                options.OpenIdConnectSchemeName);
        }

        [HttpGet]
        public IActionResult SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to home page if the user is authenticated.
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return RedirectToAction(nameof(LoginController.Index), "Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}