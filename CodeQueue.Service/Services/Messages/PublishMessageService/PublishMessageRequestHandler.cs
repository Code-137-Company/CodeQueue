using CodeQueue.Domain.Models;
using MediatR;

namespace CodeQueue.Service.Services.Messages.PublishMessageService
{
    public class PublishMessageRequestHandler : IRequestHandler<PublishMessageRequest, DefaultResponse>
    {
        public async Task<DefaultResponse> Handle(PublishMessageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
