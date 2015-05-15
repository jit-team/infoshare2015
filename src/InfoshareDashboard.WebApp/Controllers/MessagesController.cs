using Microsoft.AspNet.Mvc;
using InfoshareDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Controllers
{
    static class MessagesBus
    {
        static InfoshareDashboard.Client.Client client = new InfoshareDashboard.Client.Client("http://localhost:8080", 8080, message => messages.Add(message));
        public static List<Message> messages = new List<Message>();
    }

    public class MessagesController : Controller
    {

        public IActionResult Poll()
        {
            return Json(MessagesBus.messages.Where(msg => DateTime.Now.Subtract(msg.Created).Seconds < 5));
        }

        [HttpPost]
        public IActionResult Push([FromBody]Message message)
        {
            MessagesBus.messages.Add(message);
            return new EmptyResult();
        }
    }
}