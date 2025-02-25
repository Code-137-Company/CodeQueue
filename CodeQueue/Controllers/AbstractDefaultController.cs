using Code137.JsonDb;
using CodeQueue.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeQueue.Controllers
{
    public abstract class AbstractDefaultController : ControllerBase
    {
        private readonly JsonDb _jsonDb;
        private readonly IMediator _mediator;

        public AbstractDefaultController(JsonDb jsonDb, IMediator mediator)
        {
            _jsonDb = jsonDb;
            _mediator = mediator;
        }

        protected async Task<DefaultResponse> SendAsync(IRequest<DefaultResponse> request, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(request, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                return new DefaultResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = new ErrorModel(ex)
                };
            }
        }
    }
}
