﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JasonCarter.BudgetDashboard.Web.Controllers
{
    public class WidgetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}