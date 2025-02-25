using Code137.JsonDb;
using CodeQueue.Domain.Entities;
using CodeQueue.Domain.Models;
using MediatR;
using System.Net;

namespace CodeQueue.Service.Services.Messages.PublishMessageService
{
    public class PublishMessageRequestHandler : IRequestHandler<PublishMessageRequest, DefaultResponse>
    {
        private readonly JsonDb _jsonDb;

        public PublishMessageRequestHandler(JsonDb jsonDb)
        {
            _jsonDb = jsonDb;
        }

        public async Task<DefaultResponse> Handle(PublishMessageRequest request, CancellationToken cancellationToken)
        {
            var queue = _jsonDb.GetOne<Queue>(x => x.Name == request.QueueName);

            if (queue == null)
                return new DefaultResponse(
                    HttpStatusCode.BadRequest,
                    "Queue Not Found");

            queue.Messages.Add(new Message()
            {
                Payload = request.Message
            });

            var inserted = _jsonDb.Update(queue, out string message);

            if (!inserted)
                return new DefaultResponse(
                    HttpStatusCode.InternalServerError, 
                    "Error Publish Message");

            return new DefaultResponse();
        }
    }
}
