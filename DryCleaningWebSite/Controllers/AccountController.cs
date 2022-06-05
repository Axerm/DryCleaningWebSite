using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Data;
using DryCleaningWebSite.Models;
using DryCleaningWebSite.Services;
using DryCleaningWebSite.ViewModils;
using System.Text.RegularExpressions;

namespace DryCleaningWebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Employee> _signInManager;

        public AccountController(ApplicationDbContext applicationDbContext
            , ILogger<AccountController> logger
            , UserManager<Employee> userManager
            , RoleManager<IdentityRole> roleManager
            , SignInManager<Employee> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = ConstantValues.AdminRoleName)]
        public async Task<IActionResult> Register()
        {
            var filialsItems = await _applicationDbContext.Filials.Select(f => new
            {
                Id = f.Id,
                Name = f.Name
            }).AsNoTracking().ToListAsync();
            var rolesItems = await _roleManager.Roles.Select(r => new { Name = r.Name }).ToListAsync();

            UserRegisterViewModel model = new UserRegisterViewModel()
            {
                FilialsList = new SelectList(filialsItems, "Id", "Name"),
                RoleNamesList = new SelectList(rolesItems, "Name", "Name")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ConstantValues.AdminRoleName)]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Filial filial = await
                    _applicationDbContext.Filials.FirstOrDefaultAsync(f => f.Id == model.FilialId);
                IdentityRole role = await _roleManager.FindByNameAsync(model.RoleName);
                
                var matches = Regex.Match(model.TelephoneNumber, ConstantValues.TelephoneNumberRegex);

                if ((filial != null) && (role != null) && (matches.Success))
                {
                    Employee employee = new Employee
                    {
                        UserName = model.Login
                        , Name = model.Name
                        , FamilyName = model.FamilyName
                        , FatherName = model.FatherName
                        , PhoneNumber = $"{matches.Groups[1].Value}{matches.Groups[2].Value}{matches.Groups[3].Value}{matches.Groups[4].Value}{matches.Groups[5].Value}"
                        , Filial = filial
                    };
                    var result = await _userManager.CreateAsync(employee, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRolesAsync(employee, new[] { model.RoleName });
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            var filialsItems = await _applicationDbContext.Filials.Select(f => new
            {
                Id = f.Id,
                Name = f.Name
            }).AsNoTracking().ToListAsync();
            var rolesItems = await _roleManager.Roles.Select(r => new { Name = r.Name }).ToListAsync();

            model.FilialsList = new SelectList(filialsItems, "Id", "Name");
            model.RoleNamesList = new SelectList(rolesItems, "Name", "Name");
            return View(model);
        }

        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    userLoginViewModel.Login
                    , userLoginViewModel.Password
                    , userLoginViewModel.RememberMe
                    , lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User '{userLoginViewModel.Login}' login");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверный логин/пароль");
                    return View();
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(/*string returnUrl = null*/)
        {
            //_logger.LogInformation($"User '{(await _userManager.GetUserAsync(User)).UserName}' logout");
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            Employee employee = await _userManager.FindByIdAsync(id);
            
            if (employee == null)
                return NotFound();

            var filialsItems = await _applicationDbContext.Filials.Select(f => new
            {
                Id = f.Id,
                Name = f.Name
            }).AsNoTracking().ToListAsync();

            var rolesItems = await _roleManager.Roles.Select(r => new { Name = r.Name }).ToListAsync();
            var roleNamesListTemp = await _userManager.GetRolesAsync(employee);

            UserEditViewModel model = new UserEditViewModel
            {
                Id = employee.Id,
                UserName = employee.UserName,
                Name = employee.Name,
                FamilyName = employee.FamilyName,
                FatherName = employee.FatherName,
                IsDeprecated = employee.IsDeprecated,
                TelephoneNumber = employee.PhoneNumber,

                FilialId = employee?.FilialId ?? 0,
                FilialsList = new SelectList(filialsItems, "Id", "Name"),

                RoleName = (roleNamesListTemp.Count <= 0) ? "" : roleNamesListTemp[0],
                RoleNamesList = new SelectList(rolesItems, "Name", "Name")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ConstantValues.AdminRoleName)]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await _userManager.FindByIdAsync(model.Id);
                if (employee != null)
                {
                    Filial filial = await
                        _applicationDbContext.Filials.FirstOrDefaultAsync(f => f.Id == model.FilialId);
                    IdentityRole role = await _roleManager.FindByNameAsync(model.RoleName);

                    var matches = Regex.Match(model.TelephoneNumber, ConstantValues.TelephoneNumberRegex);

                    if ((filial != null) && (role != null) && (matches.Success))
                    {
                        employee.Name = model.Name;
                        employee.FamilyName = model.FamilyName;
                        employee.FatherName = model.FatherName;
                        employee.PhoneNumber = $"{matches.Groups[1].Value}{matches.Groups[2].Value}{matches.Groups[3].Value}{matches.Groups[4].Value}{matches.Groups[5].Value}";
                        employee.IsDeprecated = model.IsDeprecated;
                        employee.Filial = filial;

                        await _userManager.UpdateAsync(employee);

                        var roles = await _userManager.GetRolesAsync(employee);
                        await _userManager.RemoveFromRolesAsync(employee, roles.ToArray());
                        await _userManager.AddToRolesAsync(employee, new[] { role.Name });

                        if (!String.IsNullOrEmpty(model.NewPassword))
                        {
                            var _passwordValidator =
                                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<Employee>)) as IPasswordValidator<Employee>;
                            var _passwordHasher =
                                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<Employee>)) as IPasswordHasher<Employee>;

                            IdentityResult result =
                                await _passwordValidator.ValidateAsync(_userManager, employee, model.NewPassword);
                            if (result.Succeeded)
                            {
                                employee.PasswordHash = _passwordHasher.HashPassword(employee, model.NewPassword);
                                await _userManager.UpdateAsync(employee);
                            }
                            else
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }
                            }
                        }

                        return RedirectToAction("Index");
                    }
                }
            }

            var filialsItems = await _applicationDbContext.Filials.Select(f => new
            {
                Id = f.Id,
                Name = f.Name
            }).AsNoTracking().ToListAsync();
            var rolesItems = await _roleManager.Roles.Select(r => new { Name = r.Name }).AsNoTracking().ToListAsync();

            model.FilialsList = new SelectList(filialsItems, "Id", "Name");
            model.RoleNamesList = new SelectList(rolesItems, "Name", "Name");

            return View(model);
        }

        private bool IndexHelper(Employee e)
        {
            var tempRoles = _userManager.GetRolesAsync(e);
            bool resultRole;

            if (tempRoles.Result.Count <= 0)
                resultRole = false;
            else
                resultRole = tempRoles.Result[0].Contains(ViewData["SortAccountsFilter"] as string, StringComparison.OrdinalIgnoreCase);

            return e.UserName.Contains(ViewData["SortAccountsFilter"] as string, StringComparison.OrdinalIgnoreCase)
                || ($"{e.FamilyName} {e.Name} {e.FatherName}").Contains(ViewData["SortAccountsFilter"] as string, StringComparison.OrdinalIgnoreCase)
                || resultRole
                || (e?.Filial?.Name?.Contains(ViewData["SortAccountsFilter"] as string, StringComparison.OrdinalIgnoreCase) ?? false);
        }

        private string IndexHelperSortByRole(Employee e)
        {
            var roles = _userManager.GetRolesAsync(e);
            if (roles.Result.Count <= 0)
                return String.Empty;
            else
                return roles.Result[0];
        }

        private string IndexHelperSortByFullName(Employee e)
        {
            return $"{e.FamilyName} {e.Name} {e.FatherName}";
        }

        private string IndexHelperSortByFilialName(Employee e)
        {
            return (e?.Filial?.Name ?? "");
        }

        [Authorize(Roles = ConstantValues.AdminRoleName)]
        public IActionResult Index(
              string sortOrder
            , string currentFilter
            , string searchString
            , int? pageNumber)
        {
            ViewData["SortAccountsCurrentSort"] = sortOrder;
            ViewData["SortAccountsUserName"] = String.IsNullOrEmpty(sortOrder) ? "UserName_desc" : "";
            ViewData["SortAccountsFullName"] = sortOrder == "FullName" ? "FullName_desc" : "FullName";
            ViewData["SortAccountsRole"] = sortOrder == "Role" ? "Role_desc" : "Role";
            ViewData["SortAccountsFilial"] = sortOrder == "Filial" ? "Filial_desc" : "Filial";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SortAccountsFilter"] = searchString;

            var accounts = _applicationDbContext.Users.Include(i => i.Filial).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                accounts = accounts.Where(IndexHelper).AsQueryable();

            switch (sortOrder)
            {
                case "UserName_desc":
                    accounts = accounts.OrderByDescending(s => s.UserName);
                    break;

                case "FullName":
                    accounts = accounts.OrderBy(IndexHelperSortByFullName).AsQueryable();
                    break;
                case "FullName_desc":
                    accounts = accounts.OrderByDescending(IndexHelperSortByFullName).AsQueryable();
                    break;

                case "Role":
                    accounts = accounts.OrderBy(IndexHelperSortByRole).AsQueryable();
                    break;
                case "Role_desc":
                    accounts = accounts.OrderByDescending(IndexHelperSortByRole).AsQueryable();
                    break;

                case "Filial":
                    accounts = accounts.OrderBy(IndexHelperSortByFilialName).AsQueryable();
                    break;
                case "Filial_desc":
                    accounts = accounts.OrderByDescending(IndexHelperSortByFilialName).AsQueryable();
                    break;

                default:
                    accounts = accounts.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 10;
            return View(PaginatedList<Employee>.Create(accounts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}