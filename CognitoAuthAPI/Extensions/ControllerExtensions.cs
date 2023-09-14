using CognitoAuthAPI.BLL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Extensions;

public static class ControllerExtensions
{
    public static IActionResult FromErrorResult(this ControllerBase controller, ServiceResult result)
    {
        return HandleErrorResult(controller, result.ErrorType, result.Message);
    }

    public static IActionResult FromErrorResult<T>(this ControllerBase controller, ServiceResult<T> result) where T : class
    {
        return HandleErrorResult(controller, result.ErrorType, result.Message);
    }

    private static IActionResult HandleErrorResult(ControllerBase controller, ErrorType errorType, string? errorMessage)
    {
        return errorType switch
        {
            ErrorType.InvalidParameter => controller.BadRequest(new { Message = errorMessage }),
            ErrorType.Unauthorized => controller.Unauthorized(new { Message = errorMessage }),
            ErrorType.NotFound => controller.NotFound(new { Message = errorMessage }),
            ErrorType.Conflict => controller.Conflict(new { Message = errorMessage }),
            ErrorType.LimitExceeded => controller.StatusCode(StatusCodes.Status429TooManyRequests, new { Message = errorMessage }),
            ErrorType.TooManyFailedAttempts => controller.StatusCode(StatusCodes.Status429TooManyRequests, new { Message = errorMessage }),
            _ => throw new Exception("An unhandled result has occurred as a result of a service call.")
        };
    }
}
