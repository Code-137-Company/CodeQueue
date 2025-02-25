using Code137.JsonDb;
using CodeQueue.Domain.Models;
using CodeQueue.Service.Services.Messages.PublishMessageService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeQueue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : AbstractDefaultController
    {
        public MessageController(JsonDb jsonDb, IMediator mediator) : base(jsonDb, mediator) { }

        [HttpPost("Publish")]
        public async Task<IActionResult> PublishMessageAsync([FromBody] PublishMessageRequest request, CancellationToken cancellationToken)
        {
            return await SendAsync(request, cancellationToken);
        }
    }
}
