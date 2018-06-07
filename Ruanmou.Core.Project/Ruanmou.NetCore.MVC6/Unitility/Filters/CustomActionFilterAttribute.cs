using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.NetCore.MVC6.Unitility.Filters
{
    public class CustomActionFilterAttribute : Attribute, IActionFilter
    {
        //private Logger logger = Logger.CreateLogger(typeof(CustomActionFilterAttribute));
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("ActionFilter Executed!");
            //logger.Info("ActionFilter Executed!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("ActionFilter Executing!");
            //logger.Info("ActionFilter Executing!");
        }
    }
}