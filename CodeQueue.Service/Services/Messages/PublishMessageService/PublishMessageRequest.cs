using CodeQueue.Domain.Models;
using MediatR;

namespace CodeQueue.Service.Services.Messages.PublishMessageService
{
    public class PublishMessageRequest : IRequest<DefaultResponse>
    {
        public string QueueName { get; set; }
        public string Message { get; set; }
    }
}
