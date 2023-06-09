﻿using Ciber.Data.Entities;
using Ciber.Models;
using Ciber.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Plugins;
using PagedList;

namespace Ciber.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductAppService _productAppService;
        private readonly IUserAppService _userRepository;
        public OrderController(IOrderAppService orderAppService,
            UserManager<ApplicationUser> userManager,
            IProductAppService productAppService,
            IUserAppService userRepository)
        {
            _orderAppService = orderAppService;
            _userManager = userManager;
            _productAppService = productAppService;
            _userRepository = userRepository;
        }
        // GET: OrderController
        public async Task<ActionResult> Index(string searchString = "", string message = "")
        {
            var id = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            var models = await _orderAppService.GetAllAsync(searchString);
            int pageSize = 10;
            ViewBag.Message = message;
            var pageList = models.ToPagedList(1, pageSize);
            return View(pageList);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public async Task<ActionResult> Create()
        {
            OrderModel model = new OrderModel();
            var customers = await _userRepository.GetAllAsync();
            model.CustomerSelectListItems = customers.Select(c => new SelectListItem
            {
                Text = c.FullName,
                Value = c.Id,
                Selected = c.Id == "1"
            }).ToList();
            var products = await _productAppService.GetAllAsync();
            model.ProductSelectListItems = products.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = c.Id.ToString() == "1"
            }).ToList();
            ViewBag.Message = "";
            return View(model);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderModel model)
        {
            try
            {
                if ((await _productAppService.IsAmountGraterQuantityOfProductAsync(model.ProductId, model.Amount)))
                {
                    ViewBag.Message = "Số lượng đặt hàng lớn hơn số lượng sản phẩm, mời order lại";
                    return View(model);
                }

                var item = new Order()
                {
                    OrderName = model.OrderName,
                    OrderDate = model.OrderDate,
                    Amount = model.Amount,
                    ProductId = model.ProductId,
                    UserId = model.UserId
                };
                var product = await _productAppService.GetAsync(model.ProductId);
                item.ProductName = product.Name;
                await _orderAppService.CreateAsync(item);
                return RedirectToAction("Index", new { message = "Tạo order thành công" });
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _orderAppService.GetByIdAsync(id);
            var customers = await _userRepository.GetAllAsync();
            model.CustomerSelectListItems = customers.Select(c => new SelectListItem
            {
                Text = c.FullName,
                Value = c.Id,
                Selected = c.Id == model.UserId
            }).ToList();
            var products = await _productAppService.GetAllAsync();
            model.ProductSelectListItems = products.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = c.Id.ToString() == model.ProductId.ToString()
            }).ToList();
            return View(model);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, OrderModel model)
        {
            try
            {
                var item = await _orderAppService.GetByIdAsync(id);
                if (item == null)
                {
                    ViewBag.Message = "Không tông tại Order";
                    return View(model);
                }
                item.OrderName = model.OrderName;
                item.OrderDate = model.OrderDate;
                item.Amount = model.Amount;
                item.UserId = model.UserId;
                if (item.ProductId != model.ProductId)
                {
                    var product = await _productAppService.GetAsync(model.ProductId);
                    item.ProductId = model.ProductId;
                    item.ProductName = product.Name;
                }
                return RedirectToAction("Index", new { message = "Sửa order thành công" });
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await _orderAppService.DeleteAsync(id);
            return RedirectToAction("Index", new { message = "Xóa order thành công" });
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
