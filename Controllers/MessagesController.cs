using Microsoft.AspNet.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Controllers
{
    public class MessagesController : Controller
    {
        static List<Message> messages = new List<Message>();

        public IActionResult Poll()
        {
            return Json(messages.Where(msg => DateTime.Now.Subtract(msg.Created).Seconds < 5));
        }

        [HttpPost]
        public IActionResult Push([FromBody]Message message)
        {
            messages.Add(message);
            return new EmptyResult();
        }
    }
}