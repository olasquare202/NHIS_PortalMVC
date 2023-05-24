//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using NHISWeb.Dto.ResponseDto;

//namespace NHISWeb.Controllers
//{
//    public class SweetAlertController : Controller
//    {
//        public void notify(string message, string title = "Sweet alert", SweetAlertNotification sweetAlertNotification = SweetAlertNotification.success)
//        {
//            var msg = new
//            {
//                Title = title,
//                message = message,
//                icon = sweetAlertNotification.ToString(),
//                type = sweetAlertNotification.ToString(),
//                provider = GetProvider()
//            };
//            TempData["Message"] = JsonConvert.SerializeObject(msg);
//        }
//        private string GetProvider()
//        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsetings.json", optional: reloadOnChange: true)

//                IConfigurationRoot configurationRoot = builder.Build();
//            var value = configurationRoot["NotificationProvider"];
//            return value;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
