using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        protected JsonResult MultipleResult(IEnumerable<string> message, bool success = true)
        {
            return Json(new
            {
                Success = success,
                Result = message
            });
        }
    }
}