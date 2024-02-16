using AbsencesAPI.Business.Exceptions;
using AbsencesAPI.Common.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AbsencesAPI;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; }

    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch(NotFoundException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Detail = string.Empty,
                Instance = "",
                Title = $"{ex.Type} for id {ex.Id} not found",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (MissingEntitiesException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Missing {ex.Type} for ids {JsonSerializer.Serialize(ex.Ids)} not found",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (DependentEntitiesException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = string.Empty,
                Instance = "",
                Title = $"Dependent {ex.DependentEntityName} id's {JsonSerializer.Serialize(ex.DependentEntities)} exist",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = JsonSerializer.Serialize(ex.Errors),
                Instance = "",
                Title = "Validation Error",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = "",
                Title = "Internal Server Error - something went wrong",
                Type = "Error"
            };

            var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailsJson);
        }
    }
}
