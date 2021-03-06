﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ItProject.Models;
using ItProject.Models.ManageViewModels;
using ItProject.Services;
using ItProject.Data;
using ItProject.Models.ArticleViewModels;
using ItProject.Models.ArticleModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ItProject.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _appEnvironment;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          IHostingEnvironment appEnvironment,
          UrlEncoder urlEncoder,
          ApplicationDbContext application
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _db = application;
            _appEnvironment = appEnvironment;
            _db.InitialDBComponent();
        }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task<ApplicationUser> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Sort(ArticleSortViewModel sort)
        {
            var user = await GetUser();
            var model = new List<ArticleModel>();
            if (sort.SortType == "Desk")
            {
                switch (sort.Name)
                {
                    case "Name":
                        {
                            model = user.Articles.OrderByDescending(a => a.Name).ToList();
                        }break;
                    case "Date":
                        {
                            model = user.Articles.OrderByDescending(a => a.Date).ToList();
                        }break;
                    case "Rating":
                        {
                            model = user.Articles.OrderByDescending(a => a.Rating).ToList();
                        }break;
                }
            }
            else
            {
                switch (sort.Name)
                {
                    case "Name":
                        {
                            model = user.Articles.OrderBy(a => a.Name).ToList();
                        }
                        break;
                    case "Date":
                        {
                            model = user.Articles.OrderBy(a => a.Date).ToList();
                        }
                        break;
                    case "Rating":
                        {
                            model = user.Articles.OrderBy(a => a.Rating).ToList();
                        }
                        break;
                }
            }
            return View("Article",model);
        }

        [HttpGet]
        public IActionResult CreateStep(int id)
        {
            var model = _db.Articles.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateStep(StepCreateViewModel step)
        {
            _db.Steps.Add(new StepModel(step));
            _db.Articles.Find(step.ArticleId).Date = DateTime.Now;
            _db.SaveChanges();
            return View(_db.Articles.Find(step.ArticleId));
        }

        private Tag CreateTag(string tagName)
        {
            var tag = new Tag(tagName);
            _db.Tags.Add(tag);
            _db.SaveChanges();
            return tag;
        }

        private Tag CheakTag(Tag tag,string tagName)
        {
            if (tag == null)
            {
             tag=  CreateTag(tagName);
            }
            return tag;
        }

        private List<Tag> FindTag(IEnumerable<string> tags)
        {
            List<Tag> resualtTags = new List<Tag>();
            foreach(var tag in tags)
            {
                if (tag != "")
                {
                    var dbTag = _db.Tags.Where(t => t.Name == tag).SingleOrDefault();
                    resualtTags.Add(CheakTag(dbTag, tag));
                }
            }
            return resualtTags;
        }

        private void CreateTagArticle(ArticleModel article,IEnumerable<Tag> tags)
        {
            foreach(var tag in tags)
            {
                _db.TagArticle.Add(new TagArticle(article,tag));
                _db.SaveChanges();
            }
        }

        private async Task<string>  UploadFile(IFormFile uploadedFile)
        {
            string path ="\\images\\no_image.gif";
            if (uploadedFile != null)
            {
                // путь к папке Files
                path = "\\images\\" + uploadedFile.FileName.Split("\\").Last();
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName.Split("\\").Last(), Path = path };
                _db.Files.Add(file);
            }
            return path;
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            ViewBag.List = Theme.ListOfTheme;
            var model = _db.Tags.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleCreateViewModel article)
        {
            var path = await UploadFile(article.ImagePath);
            var currentUser = await GetUser();
            var tags = FindTag(article.Tags.Split(' '));
            var res = _db.Articles.Add(new ArticleModel(article, currentUser,path));
            _db.SaveChanges();
            CreateTagArticle(res.Entity, tags);
            ViewBag.List = Theme.ListOfTheme;
            return View("UpdateArticle", res.Entity);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult UpdateArticle(int id)
        {
            var model = _db.Articles.Find(id);
            ViewBag.List = Theme.ListOfTheme;
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateArticle (ArticleUpdateViewModel article)
        {
            var dbArticle = _db.Articles.Find(article.Id);
            if (article.Description != dbArticle.Description)
            {
                dbArticle.Description = article.Description;
            }
            if (article.Name!= dbArticle.Name)
            {
                dbArticle.Name = article.Name;
            }
            if(article.Theme!= dbArticle.Theme)
            {
                dbArticle.Theme = article.Theme;
            }
            dbArticle.Date = DateTime.Now;
            _db.SaveChanges();
            ViewBag.List = Theme.ListOfTheme;
            return View(dbArticle);
        }

        [HttpGet]
        public IActionResult UpdateStep(int id)
        {
            var model = _db.Steps.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateStep(StepUpdateViewModel step)
        {
            var dbStep = _db.Steps.Find(step.Id);

            if(step.Name != dbStep.Name)
            {
                dbStep.Name = step.Name;
            }
            if(step.Body != dbStep.Body)
            {
                dbStep.Body = step.Body;
            }
            dbStep.Date = DateTime.Now;
            dbStep.Article.Date = dbStep.Date;
            _db.SaveChanges();
            return View(dbStep);
        }

        [Route("{id:int}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            _db.Articles.Remove(_db.Articles.Find(id));
            await _db.SaveChangesAsync();
            var user = await GetUser();
            var model = user.Articles.ToList();
            return View("Article", model);
        }

        [HttpGet]
        public async Task<IActionResult> Article()
        {
            var user =await GetUser();
            var model = user.Articles.ToList();
            ViewBag.List = Theme.ListOfTheme;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetUser();

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Country = user.Country,
                Company = user.Company,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            if (model.FirstName != user.FirstName)
            {
                var setFirstNameResult = await _userManager.SetFirstNameAsync(user, model.FirstName);
                if (!setFirstNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting first name for user with ID '{user.Id}'.");
                }
            }
            
            if (model.LastName != user.LastName)
            {
                var setLastNameResult = await _userManager.SetLastNameAsync(user, model.LastName);
                if (!setLastNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting last name for user with ID '{user.Id}'.");
                }
            }
            
            if (model.Country != user.Country)
            {
                var setLastNameResult = await _userManager.SetCountryAsync(user, model.Country);
                if (!setLastNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting country for user with ID '{user.Id}'.");
                }
            }

            if (model.City != user.City)
            {
                var setLastNameResult = await _userManager.SetCityAsync(user, model.City);
                if (!setLastNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting city for user with ID '{user.Id}'.");
                }
            }

            if (model.Company != user.Company)
            {
                var setLastNameResult = await _userManager.SetCompanyAsync(user, model.Company);
                if (!setLastNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting company for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            var email = user.Email;
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogins()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new ExternalLoginsViewModel { CurrentLogins = await _userManager.GetLoginsAsync(user) };
            model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            model.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1;
            model.StatusMessage = StatusMessage;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback));
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(user.Id);
            if (info == null)
            {
                throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "The external login was added.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'.");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "The external login was removed.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Disable2faWarning()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            return View(nameof(Disable2fa));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable2fa()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            _logger.LogInformation("User with ID {UserId} has disabled 2fa.", user.Id);
            return RedirectToAction(nameof(TwoFactorAuthentication));
        }

        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            var model = new EnableAuthenticatorViewModel
            {
                SharedKey = FormatKey(unformattedKey),
                AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Strip spaces and hypens
            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                ModelState.AddModelError("model.Code", "Verification code is invalid.");
                return View(model);
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            _logger.LogInformation("User with ID {UserId} has enabled 2FA with an authenticator app.", user.Id);
            return RedirectToAction(nameof(GenerateRecoveryCodes));
        }

        [HttpGet]
        public IActionResult ResetAuthenticatorWarning()
        {
            return View(nameof(ResetAuthenticator));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation("User with id '{UserId}' has reset their authentication app key.", user.Id);

            return RedirectToAction(nameof(EnableAuthenticator));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Cannot generate recovery codes for user with ID '{user.Id}' as they do not have 2FA enabled.");
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            var model = new GenerateRecoveryCodesViewModel { RecoveryCodes = recoveryCodes.ToArray() };

            _logger.LogInformation("User with ID {UserId} has generated new 2FA recovery codes.", user.Id);

            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenicatorUriFormat,
                _urlEncoder.Encode("ItProject"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        #endregion
    }
}
