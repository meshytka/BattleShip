using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Api.Controllers
{
    public class BaseController : Controller
    {
        protected JsonResult ErrorResponse(string message = "Bad response")
        {
            return Json(new
            {
                Success = false,
                Result = message
            });
        }

        protected JsonResult MessageResult(string message, bool success = true)
        {
            return Json(new
            {
                Success = success,
                Result = message
            });
        }
    }
}
