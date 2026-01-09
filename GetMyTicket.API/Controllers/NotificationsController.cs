using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        public async Task GetNotificationsForUser (Guid userId)
        {
            throw new NotImplementedException ();
        }

    }
}
