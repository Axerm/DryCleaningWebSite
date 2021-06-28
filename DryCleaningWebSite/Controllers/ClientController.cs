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
    [Authorize(Roles = ConstantValues.AllRoleNames)]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger
            , ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        private bool IndexHelper(Client c)
        {
            return c.Id.ToString().Contains(ViewData["SortClientsFilter"] as string)
                    || c.FullName.Contains(ViewData["SortClientsFilter"] as string, StringComparison.OrdinalIgnoreCase)
                    || c.TelephoneNumber.Contains(ViewData["SortClientsFilter"] as string);
        }

        public IActionResult Index(
              string sortOrder
            , string currentFilter
            , string searchString
            , int? pageNumber)
        {
            ViewData["SortClientsCurrentSort"] = sortOrder;

            ViewData["SortClientsId"] = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";

            ViewData["SortClientsFullName"] = sortOrder == "SortClientsFullName" ? "SortClientsFullName_desc" : "SortClientsFullName";

            ViewData["SortClientsTel"] = sortOrder == "SortClientsTel" ? "SortClientsTel_desc" : "SortClientsTel";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SortClientsFilter"] = searchString;


            var clients = _applicationDbContext.Clients/*.Include(c => c.Discount)*/.AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
                clients = clients.Where(IndexHelper).AsQueryable();

            switch (sortOrder)
            {
                case "Id_desc":
                    clients = clients.OrderByDescending(s => s.Id);
                    break;

                case "SortClientsFullName":
                    clients = clients.OrderBy(s => s.FullName);
                    break;
                case "SortClientsFullName_desc":
                    clients = clients.OrderByDescending(s => s.FullName);
                    break;

                case "SortClientsTel":
                    clients = clients.OrderBy(s => s.TelephoneNumber);
                    break;
                case "SortClientsTel_desc":
                    clients = clients.OrderByDescending(s => s.TelephoneNumber);
                    break;

                default:
                    clients = clients.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 10;
            return View(PaginatedList<Client>.Create(clients.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(await students.AsNoTracking().ToListAsync());
        }

        //[HttpGet]
        public IActionResult Create()
        {
            var tempDiscountItems = _applicationDbContext.Discounts.AsNoTracking().AsEnumerable();

            var clientViewModel = new ClientViewModel()
            {
                DiscountList = new SelectList(tempDiscountItems, "Id", "Value"),
                ClientData = new Client()
            };

            return View(clientViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                clientViewModel.ClientData.Discount = await
                    _applicationDbContext.Discounts.FirstOrDefaultAsync(d => d.Id == clientViewModel.ClientData.DiscountId);

                if (clientViewModel.ClientData.Discount != null)
                {
                    var matches = Regex.Match(clientViewModel.ClientData.TelephoneNumber
                        , ConstantValues.TelephoneNumberRegex);

                    if (matches.Success)
                    {
                        string disformedTelephoneNumber
                            = $"{matches.Groups[1].Value}{matches.Groups[2].Value}{matches.Groups[3].Value}{matches.Groups[4].Value}{matches.Groups[5].Value}";
                        clientViewModel.ClientData.TelephoneNumber = disformedTelephoneNumber;

                        _applicationDbContext.Clients.Add(clientViewModel.ClientData);
                        await _applicationDbContext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }

            var tempDiscountItems = _applicationDbContext.Discounts.AsNoTracking().AsEnumerable();
            clientViewModel.DiscountList = new SelectList(tempDiscountItems, "Id", "Value");

            return View(clientViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Client client
                    = await _applicationDbContext.Clients.FirstOrDefaultAsync(p => p.Id == id);

                if (client == null)
                    return NotFound();

                var tempDiscountItems = _applicationDbContext.Discounts.AsNoTracking().AsEnumerable();

                var clientViewModel = new ClientViewModel()
                {
                    ClientData = client,
                    DiscountList = new SelectList(tempDiscountItems, "Id", "Value")
                };
                
                return View(clientViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                clientViewModel.ClientData.Discount = await
                    _applicationDbContext.Discounts.FirstOrDefaultAsync(d => d.Id == clientViewModel.ClientData.DiscountId);

                if (clientViewModel.ClientData.Discount != null)
                {
                    var matches = Regex.Match(clientViewModel.ClientData.TelephoneNumber
                        , ConstantValues.TelephoneNumberRegex);

                    if (matches.Success)
                    {
                        string disformedTelephoneNumber
                            = $"{matches.Groups[1].Value}{matches.Groups[2].Value}{matches.Groups[3].Value}{matches.Groups[4].Value}{matches.Groups[5].Value}";
                        clientViewModel.ClientData.TelephoneNumber = disformedTelephoneNumber;

                        _applicationDbContext.Clients.Update(clientViewModel.ClientData);
                        await _applicationDbContext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }

            var tempDiscountItems = _applicationDbContext.Discounts.AsNoTracking().AsEnumerable();
            clientViewModel.DiscountList = new SelectList(tempDiscountItems, "Id", "Value");

            return View(clientViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
