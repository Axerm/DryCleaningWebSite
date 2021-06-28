using Microsoft.AspNetCore.Authorization;
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

namespace DryCleaningWebSite.Controllers
{
    [Authorize(Roles = "reseptionist, technologist, admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<Employee> _userManager;

        public object OrderDetailsViewModel { get; private set; }

        public OrderController(ApplicationDbContext applicationDbContext
            , ILogger<OrderController> logger
            , UserManager<Employee> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _userManager = userManager;
        }

        private IQueryable<Order> GetIncludedDataOrderWithoutAsNoTracking()
        {
            return _applicationDbContext.Orders
                .Include(o => o.Employee)
                    .ThenInclude(e => e.Filial)
                .Include(c => c.Client)
                    .ThenInclude(c => c.Discount)
                .Include(f => f.Filial)
                .Include(f => f.Filial)
                .Include(t => t.ThingType)
                .Include(m => m.ManufacturerMarking)
                .Include(p => p.PollutionDegree)
                .Include(b => b.BurnoutDegree)
                .Include(g => g.GlueParts)
                .Include(o => o.OperationalWearDegree)
                .Include(s => s.ServiceType)
                .Include(s => s.Stock)
                .Include(s => s.StatusOrder)
                .Include(s => s.StatusPay)
                .Include(o => o.OrderAdditionalInfos)
                    .ThenInclude(o => o.Employee)

                .AsSplitQuery();
        }

        private bool IndexHelper(Order o)
        {
            return o.Id.ToString().Contains(ViewData["SortOrdersFilter"] as string)
                || o.DateOfReceipt.Date.ToString("dd.MM.yyyy").Contains(ViewData["SortOrdersFilter"] as string)
                || o.DateOfPlainIssue.Date.ToString("dd.MM.yyyy").Contains(ViewData["SortOrdersFilter"] as string)
                || ((o.DateOfAffectedPlainIssue == null)
                    ? false 
                    : (o.DateOfReceipt.Date.ToString("dd.MM.yyyy").Contains(ViewData["SortOrdersFilter"] as string)))
                || ((o.Filial == null) 
                    ? false 
                    : o.Filial.Name.Contains(ViewData["SortOrdersFilter"] as string, StringComparison.OrdinalIgnoreCase))
                || ((o.StatusOrder == null) 
                    ? false
                    : o.StatusOrder.Name.Contains(ViewData["SortOrdersFilter"] as string, StringComparison.OrdinalIgnoreCase)
                );
        }

        private string IndexHelperSortByFilialName(Order o)
        {
            return ((o.Filial == null) ? String.Empty : o.Filial.Name);
        }

        private string IndexHelperSortByDateOfAffectedPlainIssue(Order o)
        {
            return ((o.DateOfAffectedPlainIssue == null)
                ? String.Empty : (o.DateOfReceipt.Date.ToString("dd.MM.yyyy")));
        }

        private string IndexHelperSortByStatusOrder(Order o)
        {
            return ((o.StatusOrder == null) ? String.Empty : o.StatusOrder.Name);
        }

        public IActionResult Index(
              string sortOrder
            , string currentFilter
            , string searchString
            , int? pageNumber)
        {
            ViewData["SortOrderCurrentSort"] = sortOrder;
            ViewData["SortOrdersId"] = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewData["SortOrdersFilial"] = sortOrder == "Filial" ? "Filial_desc" : "Filial";
            ViewData["SortOrdersDateOfReceipt"] = sortOrder == "DateOfReceipt" ? "DateOfReceipt_desc" : "DateOfReceipt";
            ViewData["SortOrdersDateOfPlainIssue"] = sortOrder == "DateOfPlainIssue" ? "DateOfPlainIssue_desc" : "DateOfPlainIssue";
            ViewData["SortOrdersDateOfAffectedPlainIssue"] = sortOrder == "DateOfAffectedPlainIssue" ? "DateOfAffectedPlainIssue_desc" : "DateOfAffectedPlainIssue";
            ViewData["SortOrdersStatusOrder"] = sortOrder == "StatusOrder" ? "StatusOrder_desc" : "StatusOrder";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["SortOrdersFilter"] = searchString;

            var orders = GetIncludedDataOrderWithoutAsNoTracking().AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
                orders = orders.Where(IndexHelper).AsQueryable();

            switch (sortOrder)
            {
                case "Id_desc":
                    orders = orders.OrderByDescending(s => s.Id);
                    break;

                case "Filial":
                    orders = orders.OrderBy(IndexHelperSortByFilialName).AsQueryable();
                    break;
                case "Filial_desc":
                    orders = orders.OrderByDescending(IndexHelperSortByFilialName).AsQueryable();
                    break;

                case "DateOfReceipt":
                    orders = orders.OrderBy(s => s.DateOfReceipt);
                    break;
                case "DateOfReceipt_desc":
                    orders = orders.OrderByDescending(s => s.DateOfReceipt);
                    break;

                case "DateOfPlainIssue":
                    orders = orders.OrderBy(s => s.DateOfPlainIssue);
                    break;
                case "DateOfPlainIssue_desc":
                    orders = orders.OrderByDescending(s => s.DateOfPlainIssue);
                    break;

                case "DateOfAffectedPlainIssue":
                    orders = orders.OrderBy(IndexHelperSortByDateOfAffectedPlainIssue).AsQueryable();
                    break;
                case "DateOfAffectedPlainIssue_desc":
                    orders = orders.OrderByDescending(IndexHelperSortByDateOfAffectedPlainIssue).AsQueryable();
                    break;

                case "StatusOrder":
                    orders = orders.OrderBy(IndexHelperSortByStatusOrder).AsQueryable();
                    break;
                case "StatusOrder_desc":
                    orders = orders.OrderByDescending(IndexHelperSortByStatusOrder).AsQueryable();
                    break;

                default:
                    orders = orders.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 10;
            return View(PaginatedList<Order>.Create(orders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        private async Task<OrderComplexDataViewModel> GetData()
        {
            var clientItems = _applicationDbContext.Clients.Select(c => new
            {
                Id = c.Id,
                Info = $"{c.FullName} | {c.TelephoneNumber}"
            }).AsNoTracking().AsEnumerable();
            clientItems = clientItems.OrderBy(c => c.Info);

            var thingTypeItems = _applicationDbContext.ThingTypes.Select(t => new
            {
                Id = t.Id,
                Info = $"{t.Name} | {t.Price}"
            }).AsNoTracking().AsEnumerable();
            thingTypeItems = thingTypeItems.OrderBy(c => c.Info);

            var manufacturerMarkingItems = await _applicationDbContext.ManufacturerMarkings
                .Where(t => t.IsDeprecated == false).ToListAsync();

            var pollutionDegreeItems = await _applicationDbContext.PollutionDegrees
                .Where(t => t.IsDeprecated == false).ToListAsync();

            var burnoutDegreeItems = await _applicationDbContext.BurnoutDegrees
                .Where(t => t.IsDeprecated == false).ToListAsync();

            var gluePartsItems = await _applicationDbContext.GluePartss
                .Where(t => t.IsDeprecated == false).ToListAsync();

            var operationalWearDegreeItems = await _applicationDbContext.OperationalWearDegrees
                .Where(t => t.IsDeprecated == false).ToListAsync();

            var serviceTypeItems = await _applicationDbContext.ServiceTypes
                .Where(t => t.IsDeprecated == false).ToListAsync();

            var stockItems = await _applicationDbContext.Stocks
                .Where(t => t.IsDeprecated == false).ToListAsync();

            return new OrderComplexDataViewModel()
            {
                ClientsData = new SelectList(clientItems, "Id", "Info"),
                ThingTypesData = new SelectList(thingTypeItems, "Id", "Info"),
                ManufacturerMarkingsData = new SelectList(manufacturerMarkingItems, "Id", "Name"),
                PollutionDegreeData = new SelectList(pollutionDegreeItems, "Id", "Degree"),
                BurnoutDegreeData = new SelectList(burnoutDegreeItems, "Id", "Name"),
                GluePartssData = new SelectList(gluePartsItems, "Id", "Name"),
                OperationalWearDegreesData = new SelectList(operationalWearDegreeItems, "Id", "Name"),
                ServiceTypesData = new SelectList(serviceTypeItems, "Id", "Name"),
                StocksData = new SelectList(stockItems, "Id", "Value"),
                OrderViewModelData = new OrderDataViewModel()
            };
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Order order = await GetIncludedDataOrderWithoutAsNoTracking().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return RedirectToAction("Index");

            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel
            {
                AdditionalCost = order.AdditionalCost.ToString(),
                BurnoutDegree = order?.BurnoutDegree?.Name ?? "",
                Client = $"{order?.Client?.FullName} | {order?.Client?.TelephoneNumber}" ?? "",
                DateOfIssue = order?.DateOfIssue?.ToString("dd.MM.yyy") ?? "",
                DateOfAffectedPlainIssue = order?.DateOfAffectedPlainIssue?.ToString("dd.MM.yyy") ?? "",
                DateOfPlainIssue = order.DateOfPlainIssue.ToString("dd.MM.yyy"),
                DateOfReceipt = order.DateOfReceipt.ToString("dd.MM.yyy"),
                DefectsAfter = order.DefectsAfter,
                DefectsBefore = order.DefectsBefore,
                Employee = order?.Employee?.UserName ?? "",
                Filial = order?.Filial?.Name ?? "",
                FinalPrice = (order.FinalPrice == null) ? "" : order.FinalPrice.ToString(),
                GlueParts = order?.GlueParts?.Name ?? "",
                HasAbrasion = order.HasAbrasion,
                HasBloodWine = order.HasBloodWine,
                HasDeformation = order.HasDeformation,
                HasOilFat = order.HasOilFat,
                HasPaintBlackPasta = order.HasPaintBlackPasta,
                HasShine = order.HasShine,
                HasSpotUnknownOrigin = order.HasSpotUnknownOrigin,
                Id = order.Id,
                ManufacturerMarking = order?.ManufacturerMarking?.Name ?? "",
                NonRemovableFittings = order.NonRemovableFittings,
                OperationalWearDegree = order?.OperationalWearDegree?.Name.ToString() ?? "",
                PollutionDegree = order?.PollutionDegree?.Degree ?? "",
                RemovableFittings = order.RemovableFittings,
                ServiceType = order?.ServiceType?.Name ?? "",
                StatusOrder = order?.StatusOrder?.Name ?? "",
                StatusPay = order?.StatusPay?.Name ?? "",
                Stock = order?.Stock?.Value.ToString() ?? "",
                ThingComplect = order.ThingComplect,
                ThingSize = order.ThingSize.ToString(),
                ThingType = $"{order?.ThingType?.Name} | {order?.ThingType?.Price}" ?? ""
            };

            return View(orderDetailsViewModel);
        }

        public async Task<IActionResult> Create()
        {
            OrderComplexDataViewModel orderComplexDataViewModel = await GetData();
            return View(orderComplexDataViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            OrderComplexDataViewModel orderComplexDataViewModel)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    DateOfReceipt = orderComplexDataViewModel.OrderViewModelData.DateOfReceipt,
                    DateOfPlainIssue = orderComplexDataViewModel.OrderViewModelData.DateOfPlainIssue,

                    Client = await _applicationDbContext.Clients.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.ClientId),

                    ThingType = await _applicationDbContext.ThingTypes.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.ThingTypeId),

                    ThingSize = orderComplexDataViewModel.OrderViewModelData.ThingSize,

                    ThingComplect = orderComplexDataViewModel.OrderViewModelData.ThingComplect,

                    ManufacturerMarking = await _applicationDbContext.ManufacturerMarkings.FirstOrDefaultAsync(
                            m => m.Id == orderComplexDataViewModel.OrderViewModelData.ManufacturerMarkingId),

                    PollutionDegree = await _applicationDbContext.PollutionDegrees.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.PollutionDegreeId),

                    BurnoutDegree = await _applicationDbContext.BurnoutDegrees.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.BurnoutDegreeId),

                    GlueParts = await _applicationDbContext.GluePartss.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.GluePartsId),

                    HasBloodWine = orderComplexDataViewModel.OrderViewModelData.HasBloodWine,
                    HasPaintBlackPasta = orderComplexDataViewModel.OrderViewModelData.HasPaintBlackPasta,
                    HasOilFat = orderComplexDataViewModel.OrderViewModelData.HasOilFat,
                    HasShine = orderComplexDataViewModel.OrderViewModelData.HasShine,
                    HasSpotUnknownOrigin = orderComplexDataViewModel.OrderViewModelData.HasSpotUnknownOrigin,
                    HasAbrasion = orderComplexDataViewModel.OrderViewModelData.HasAbrasion,
                    HasDeformation = orderComplexDataViewModel.OrderViewModelData.HasDeformation,
                    DefectsBefore = orderComplexDataViewModel.OrderViewModelData.DefectsBefore,
                    DefectsAfter = orderComplexDataViewModel.OrderViewModelData.DefectsAfter,
                    RemovableFittings = orderComplexDataViewModel.OrderViewModelData.RemovableFittings,
                    NonRemovableFittings = orderComplexDataViewModel.OrderViewModelData.NonRemovableFittings,

                    OperationalWearDegree = await _applicationDbContext.OperationalWearDegrees.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.OperationalWearDegreeId),

                    ServiceType = await _applicationDbContext.ServiceTypes.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.ServiceTypeId),

                    Stock = await _applicationDbContext.Stocks.FirstOrDefaultAsync(
                            c => c.Id == orderComplexDataViewModel.OrderViewModelData.StockId),

                    AdditionalCost = orderComplexDataViewModel.OrderViewModelData.AdditionalCost,

                };

                Employee employee = await _userManager.GetUserAsync(User);
                
                if ((employee != null) && (employee.FilialId > 0))
                {
                    order.Employee = employee;
                    order.Filial = await _applicationDbContext.Filials.FirstOrDefaultAsync(
                        f => f.Id == employee.FilialId);

                    _applicationDbContext.Orders.Add(order);
                    _applicationDbContext.SaveChanges();

                    return RedirectToAction("AdditionalInfo", new { Id = order.Id });
                }
            }

            OrderComplexDataViewModel _orderWithManufacturerMarkingListViewModel = await GetData();
            return View(_orderWithManufacturerMarkingListViewModel);
        }

        public async Task<IActionResult> AdditionalInfo(int? id)
        {
            if (id == null)
                return NotFound();
            
            Order order = await GetIncludedDataOrderWithoutAsNoTracking().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            decimal finalPriceLocal = order?.FinalPrice ?? 0;

            if (finalPriceLocal == 0)
            {
                int discountLocal = order?.Client?.Discount?.Value ?? 0
                    , stokOrder = order?.Stock?.Value ?? 0;
                decimal stepFinalPrice = (order?.ThingType?.Price ?? 0) + order.AdditionalCost;

                discountLocal = ((discountLocal > stokOrder) ? discountLocal : stokOrder);

                if (discountLocal > 0)
                    finalPriceLocal = stepFinalPrice - ((stepFinalPrice * discountLocal) / 100);
                else
                    finalPriceLocal = stepFinalPrice;

                Order orderTemp = await _applicationDbContext.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
                orderTemp.FinalPrice = finalPriceLocal;

                _applicationDbContext.SaveChanges();
            }

            var statusOrderListItems = _applicationDbContext.StatusOrders.AsNoTracking().ToList();
            var statusPayListItems = _applicationDbContext.StatusPays.AsNoTracking().ToList();

            OrderAdditionalInfoHelperViewModel orderAdditionalInfoHelperViewModel = new OrderAdditionalInfoHelperViewModel()
            {
                Id = order.Id,
                DateOfAffectedPlainIssue = order.DateOfAffectedPlainIssue,
                DateOfIssue = order.DateOfIssue,
                FinalPriceValue = finalPriceLocal,
                StatusOrderListId = ((order.StatusOrder == null) ? 0 : order.StatusOrder.Id),
                StatusPayListId = ((order.StatusPay == null) ? 0 : order.StatusPay.Id)
            };

            OrderAdditionalInfoViewModel orderAdditionalInfoViewModel = new OrderAdditionalInfoViewModel()
            {
                AdditionalCostValue = order.AdditionalCost.ToString(),
                DiscountValue = order?.Client?.Discount?.Value.ToString() ?? String.Empty,
                OrderAdditionalInfoList = order.OrderAdditionalInfos,
                StatusOrderList = new SelectList(statusOrderListItems, "Id", "Name"),
                StatusPayList = new SelectList(statusPayListItems, "Id", "Name"),
                ThingTypeValue = $"{order?.ThingType?.Name} | {order?.ThingType?.Price}" ?? String.Empty,
                StockValue = order?.Stock?.Value.ToString() ?? String.Empty,

                OrderAdditionalInfoHelperViewModel = orderAdditionalInfoHelperViewModel
            };

            return View(orderAdditionalInfoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdditionalInfo(
            OrderAdditionalInfoViewModel orderAdditionalInfoViewModel)
        {
            if (ModelState.IsValid == false)
                return RedirectToAction("AdditionalInfo"
                    , new { id = orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.Id });

            Order order = await GetIncludedDataOrderWithoutAsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.Id);

            if (order != null)
            {
                if ((orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.DateOfAffectedPlainIssue != null)
                    && (orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.DateOfAffectedPlainIssue 
                        >= DateTime.Now.Date))
                    order.DateOfAffectedPlainIssue = orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.DateOfAffectedPlainIssue;

                if ((orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.DateOfIssue != null)
                    && (orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.DateOfIssue
                        >= DateTime.Now.Date))
                    order.DateOfIssue = orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.DateOfIssue;

                order.StatusPay = await _applicationDbContext.StatusPays.FirstOrDefaultAsync(s =>
                    s.Id == orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.StatusPayListId);
                order.StatusOrder = await _applicationDbContext.StatusOrders.FirstOrDefaultAsync(s =>
                    s.Id == orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.StatusOrderListId);

                if (!String.IsNullOrEmpty(orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.OrderAdditionalInfo))
                {
                    OrderAdditionalInfo orderAdditionalInfo = new OrderAdditionalInfo()
                    {
                        Data = orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.OrderAdditionalInfo,
                        Employee = await _userManager.GetUserAsync(User)
                    };

                    _applicationDbContext.OrderAdditionalInfos.Add(orderAdditionalInfo);
                    order.OrderAdditionalInfos.Add(orderAdditionalInfo);
                }

                _applicationDbContext.SaveChanges();
            }

            return RedirectToAction("AdditionalInfo"
                , new { id = orderAdditionalInfoViewModel.OrderAdditionalInfoHelperViewModel.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}