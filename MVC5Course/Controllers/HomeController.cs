using MVC5Course.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
    {
        private static ModbusService _modbusService = new ModbusService();

        public ActionResult Index()
        {

            if (_modbusService != null)
            {
                _modbusService.StartReading();
            }

            return View();
        }
    }
}