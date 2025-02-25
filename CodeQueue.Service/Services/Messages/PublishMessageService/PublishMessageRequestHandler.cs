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
            var message = new Message()
            {
                Payload = request.Payload,
            };

            var inserted = _jsonDb.Insert(message, out string result);

            if (!inserted)
                return new DefaultResponse(
                    HttpStatusCode.InternalServerError, 
                    "Error Publish Message");

            return new DefaultResponse();
        }
    }
}
