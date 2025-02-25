using CodeQueue.Service.Common;
using CodeQueue.Service.Services.ConfigurationService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace CodeQueue.Service.Middleware
{
    public class ValidateRequestToken
    {
        private readonly RequestDelegate _next;
        private readonly IMediator _mediator;

        public ValidateRequestToken(RequestDelegate next, IMediator mediator)
        {
            _next = next;
            _mediator = mediator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            StringValues token = string.Empty;
            context.Request.Headers.TryGetValue("token", out token);

            if (string.IsNullOrEmpty(token))
                throw new Exception("It is necessary to pass the Token in the Header.");

            var configuration = await _mediator.Send(new ConfigurationRequest());

            if (configuration.Token != token)
                throw new Exception("Incorrect token.");

            await _next(context);
        }
    }
}
