// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WDPR_A.ViewModels;
using System.Data;
using System.Linq;
/// <summary>
///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
///     directly from your code. This API may change or be removed in future releases.
/// </summary>

namespace WDPR_A.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly WDPRContext _context;
        private readonly RoleSystem _roleSystem;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, WDPRContext context,
            RoleSystem roleSystem)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _roleSystem = roleSystem;
        }




        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            public string UserId { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Voer wachtwoord opnieuw in")]
            [Compare("Password", ErrorMessage = "De wachtwoord en bevestigingswachtwoord matchen niet.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null, string userId = null)
        {
            ViewData["userId"] = userId;
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        { 
            var identityUser = _context.Users.SingleOrDefault(i => i.Id == Input.UserId);
            var client =  _context.Clients.SingleOrDefault(c => c.Id == Input.UserId);
            var guardian = _context.Guardians.SingleOrDefault(g => g.Id == Input.UserId);
            if (Input.UserId == null || (client == null && guardian == null) || identityUser.PasswordHash != null || !identityUser.EmailConfirmed)
                RedirectToAction("Index","Home");

            if (client != null)
            {
                await CompleteAccount(client, Input);
                await _roleSystem.AddUserToRole(client, "Client");
            }
            else if (guardian != null)
            {
                await CompleteAccount(guardian, Input);
                await _roleSystem.AddUserToRole(guardian, "Guardian");
            }
            return RedirectToAction("Succes", "Appointment");
        }

        private async Task CompleteAccount(IdentityUser user, InputModel input)
        {
            await _userStore.SetUserNameAsync(user, user.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, user.Email, CancellationToken.None);
            var result = await _userManager.AddPasswordAsync(user, input.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
