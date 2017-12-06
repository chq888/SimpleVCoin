using FCM.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace VCoinWeb.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            var registrationId = ""; // "dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym";
            var serverKey = "AAAAtCPm3kM:APA91bH9X5D_Wu6uoXfRp7vbR8GKC5HCW58_QlXxn3UCTe_KjF_s9jeI7Xu9UzXCnUWrGUdPl9FZ76aSiID_7yjvy8aGaAjCk0o7LJL5UAYpyHskb6PDGLTUNGxYGW_ScBgT8VaGimhW";
            var senderId = "773696446019";
            string title = "Teste .Net Core";

            return View();
        }

    }

}