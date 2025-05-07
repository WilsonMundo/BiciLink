using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using System.Net;

namespace BiciReserva.Services
{
    public class ResponseService
    {
        public ActionResult CreateResponse<T>(ResultAPI<T> data, StatusHttpResponse statusCode)
        {

            switch (statusCode)
            {
                case StatusHttpResponse.OK:
                    if (data == null || (data is ResultAPI<object?> resultApi && resultApi.result == null))
                        return new OkResult();
                    else
                        return new OkObjectResult(data.result);

                case StatusHttpResponse.Created:
                    if (data == null || (data is ResultAPI<object?> apiresult && apiresult.result == null))
                        throw new ArgumentException("null response in code 201");
                    else
                        return new ObjectResult(data)
                        {
                            StatusCode = (int)HttpStatusCode.Created
                        };
                case StatusHttpResponse.NoContent:
                    return new NoContentResult();
                case StatusHttpResponse.BadRequest:
                    return new BadRequestObjectResult(data);
                case StatusHttpResponse.InternalServerError:
                    return new ObjectResult(data)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                case StatusHttpResponse.Unauthorized:
                    return new UnauthorizedObjectResult(data);
                case StatusHttpResponse.NotFound:
                    return new NotFoundObjectResult(data);
                case StatusHttpResponse.Conflict:
                    return new ConflictObjectResult(data);
                default:
                    return new ObjectResult(data)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };


            }

        }
    }
}
