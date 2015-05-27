using Microsoft.AspNet.Mvc;
using InfoshareDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Controllers
{
    static class MessagesBus
    {
        //TODO, use DI and config
        static InfoshareDashboard.Client.Client client = new InfoshareDashboard.Client.Client("http://localhost:8081", 8080, request => messages.Add(request.Message));
        public static readonly List<Message> messages = new List<Message>();
    }
    [Route("[controller]")]
    public class MessagesController : Controller
    {

        [HttpGet]
        public IEnumerable<InfoshareDashboard.Models.Message> Get()
        {
            return MessagesBus.messages
            .OrderByDescending(f => f.Created);
        }

        [HttpGet("{guid}")]
        public IEnumerable<InfoshareDashboard.Models.Message> Get(string guid)
        {
            return MessagesBus.messages
            .OrderByDescending(f => f.Created)
            .TakeWhile(message => message.Guid.ToString() != guid);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Message message)
        {
            if (message != null)
            {
                MessagesBus.messages.Add(message);
                return new EmptyResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}