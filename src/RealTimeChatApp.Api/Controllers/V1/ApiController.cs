using Asp.Versioning;

using ErrorOr;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using RealTimeChatApp.Api.Common.Http;

namespace RealTimeChatApp.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class ApiController : ControllerBase
{
  [ApiExplorerSettings(IgnoreApi = true)]
  protected IActionResult Problem(List<Error> errors)
  {
    if (errors.Count is 0)
    {
      return Problem();
    }

    if (errors.All(error => error.Type is ErrorType.Validation))
    {
      return ValidationProblem(errors);
    }

    HttpContext.Items[HttpContextItemKeys.Errors] = errors;
    return Problem(errors[0]);
  }

  [ApiExplorerSettings(IgnoreApi = true)]
  private IActionResult Problem(Error error)
  {
    var statusCode = error.Type switch
    {
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
      _ => StatusCodes.Status500InternalServerError,
    };

    return Problem(statusCode: statusCode, title: error.Description);
  }

  private IActionResult ValidationProblem(List<Error> errors)
  {
    var modelStateDictionary = new ModelStateDictionary();

    foreach (var error in errors)
    {
      modelStateDictionary.AddModelError(
        error.Code,
        error.Description);
    }

    return ValidationProblem(modelStateDictionary);
  }
}