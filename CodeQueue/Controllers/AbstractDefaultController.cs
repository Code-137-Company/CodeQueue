using Code137.JsonDb;
using CodeQueue.Domain.Models;
using CodeQueue.Domain.Models.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

        protected async Task<IActionResult> SendAsync(IRequest<DefaultResponse> request, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(request, cancellationToken);

                var response = new ObjectResult(result);
                response.StatusCode = (int)result.StatusCode;

                return response;
            }
            catch (ResponseException ex)
            {
                var result = new DefaultResponse()
                {
                    StatusCode = ex.StatusCode,
                    Exception = new ExceptionModel(ex)
                };

                switch (ex.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return new BadRequestObjectResult(result);

                    default:
                        var response = new ObjectResult(result);
                        response.StatusCode = (int)result.StatusCode;

                        return response;
                }
            }
            catch (Exception ex)
            {
                var result = new ObjectResult(new DefaultResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Exception = new ExceptionModel(ex)
                });

                result.StatusCode = (int)HttpStatusCode.InternalServerError;

                return result;
            }
        }
    }
}
