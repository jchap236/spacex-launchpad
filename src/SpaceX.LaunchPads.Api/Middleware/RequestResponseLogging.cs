using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;

namespace Middleware
{
   public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<RequestResponseLoggingMiddleware> log;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> log)
    {
        this.next = next;
        this.log = log;
    }

    public async Task Invoke(HttpContext context)
    {

        var request = await FormatRequest(context.Request);
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            this.log.LogInformation($"Incoming Request: {request}");

            await next(context);

            var response = await FormatResponse(context.Response);

            this.log.LogInformation(response);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        var body = request.Body;

        request.EnableRewind();

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        var bodyAsText = Encoding.UTF8.GetString(buffer);

        request.Body = body;

        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);

        string text = await new StreamReader(response.Body).ReadToEndAsync();

        response.Body.Seek(0, SeekOrigin.Begin);

        return $"{response.StatusCode}: {text}";
    }
}
}