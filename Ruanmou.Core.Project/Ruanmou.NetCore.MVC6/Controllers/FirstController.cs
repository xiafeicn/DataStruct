using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ruanmou.NetCore.MVC6.Models;
using Ruanmou.NetCore.MVC6.Unitility.Filters;

using Ruanmou.NetCore.Interface;
using Microsoft.AspNetCore.Http;
using Ruanmou.EFCore.Model;

namespace Ruanmou.NetCore.MVC6.Controllers
{
    [CustomActionFilterAttribute]
    [CustomResultFilterAttribute]
    public class FirstController : Controller
    {
        private ITestServiceA _iTestServiceA = null;
        private ITestServiceB _iTestServiceB = null;
        private ITestServiceC _iTestServiceC = null;
        private ICategoryService _iCategoryService = null;
        private ICommodityService _iCommodityService = null;
        private IUserMenuService _iUserMenuService = null;
        public FirstController(ITestServiceA iTestServiceA,
            ITestServiceB iTestServiceB, ITestServiceC iTestServiceC,
            ICategoryService iCategoryService,
            ICommodityService iCommodityService, IUserMenuService iUserMenuService)
        {
            this._iTestServiceA = iTestServiceA;
            this._iTestServiceB = iTestServiceB;
            this._iTestServiceC = iTestServiceC;
            this._iCategoryService = iCategoryService;
            this._iCommodityService = iCommodityService;
            this._iUserMenuService = iUserMenuService;
        }



        public IActionResult Index(int? id)
        {
            base.ViewData["User1"] = new CurrentUser()
            {
                Id = 7,
                Name = "Y",
                Account = " ╰つ Ｈ ♥. 花心胡萝卜",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };

            base.ViewData["Something"] = 12345;

            base.ViewBag.Name = "Eleven";
            base.ViewBag.Description = "Teacher";//js
            base.ViewBag.User = new CurrentUser()
            {
                Id = 7,
                Name = "IOC",
                Account = "限量版",
                Email = "莲花未开时",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };

            base.TempData["User"] = new CurrentUser()
            {
                Id = 7,
                Name = "CSS",
                Account = "季雨林",
                Email = "KOKE",
                Password = "落单的候鸟",
                LoginTime = DateTime.Now
            };//后台可以跨action  基于session

            if (id == null)
                return this.RedirectToAction("TempDataPage");
            else
                //return View(new CurrentUser()
                //{
                //    Id = 7,
                //    Name = "一点半",
                //    Account = "季雨林",
                //    Email = "KOKE",
                //    Password = "落单的候鸟",
                //    LoginTime = DateTime.Now
                //});
                //Tuple<
                //自建一个实体
                //viewbag

                return View(new CurrentUser()
                {
                    Id = 7,
                    Name = "一点半",
                    Account = "季雨林",
                    Email = "KOKE",
                    Password = "落单的候鸟",
                    LoginTime = DateTime.Now
                });

            //return View();
        }

        public ActionResult TempDataPage()
        {
            base.ViewBag.User = base.TempData["User"];
            return View();
        }


        [CustomResourceFilterAttribute]
        public ActionResult TestFilter()
        {
            base.ViewBag.Back = DateTime.Now;
            System.Threading.Thread.Sleep(2000);
            return View();
        }

        public ViewResult TestFilterException()
        {
            throw new Exception("TestFilterException Exception");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        public void TestDI()
        {
            HttpContext.Response.ContentType = "text/html";
            HttpContext.Response.WriteAsync($"<h1>Controller Log</h1>");
            HttpContext.Response.WriteAsync($"<h6>Transient => {this._iTestServiceA.GetType()}</h6>");
            HttpContext.Response.WriteAsync($"<h6>Scoped => {this._iTestServiceB.GetType()}</h6>");
            HttpContext.Response.WriteAsync($"<h6>Singleton => {this._iTestServiceC.GetType()}</h6>");


            var category = this._iCategoryService.Find<Category>(1);
            var commodity = this._iCategoryService.Find<Commodity>(1);
            var user = this._iCategoryService.Find<User>(1);

            HttpContext.Response.WriteAsync($"<h6> {category?.Name}</h6>");
            HttpContext.Response.WriteAsync($"<h6> {commodity?.Title}</h6>");
            HttpContext.Response.WriteAsync($"<h6> {user?.Name}</h6>");
        }
    }
}