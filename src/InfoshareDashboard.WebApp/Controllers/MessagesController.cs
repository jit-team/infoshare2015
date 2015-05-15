using Microsoft.AspNet.Mvc;
using InfoshareDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Controllers
{
    public class MessagesController : Controller
    {
        static List<Message> messages = new List<Message>();
        
        InfoshareDashboard.Client.Client client = new InfoshareDashboard.Client.Client("http://localhost:8080"); 

        public IActionResult Poll()
        {
            return Json(messages.Where(msg => DateTime.Now.Subtract(msg.Created).Seconds < 5));
        }

        [HttpPost]
        public IActionResult Push([FromBody]Message message)
        {
            Console.WriteLine(message.ToString());
            messages.Add(message);
            return new EmptyResult();
        }
    }
}