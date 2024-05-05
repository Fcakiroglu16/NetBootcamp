using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using System.Net;

namespace NetBootcamp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(ResponseModelDto<T> response, string methodName,
            object routeValues)
        {
            if (response.StatusCodes == HttpStatusCode.Created)
            {
                return CreatedAtAction(methodName, routeValues, response);
            }


            return new ObjectResult(response) { StatusCode = (int)response.StatusCodes };
        }


        public IActionResult CreateActionResult<T>(ResponseModelDto<T> response)
        {
            if (response.StatusCodes == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = 204 };
            }

            return new ObjectResult(response) { StatusCode = (int)response.StatusCodes };


            // okResult = sta


            //ObjectResult okResult = new ObjectResult(null) { StatusCode = 200 };

            //ObjectResult badRequest = new ObjectResult(null) { StatusCode = 400 };


            //if (response.StatusCodes == HttpStatusCode.OK)
            //{
            //    return Ok(response.Data);
            //}
            //else if (response.StatusCodes == HttpStatusCode.Created)
            //{
            //    return CreatedAtAction("GetById", new { productId = response.Data }, null);
            //}
            //else if (response.StatusCodes == HttpStatusCode.NoContent)
            //{
            //    return StatusCode(StatusCodes.Status204NoContent);
            //}
            //else if (response.StatusCodes == HttpStatusCode.BadRequest)
            //{
            //    return BadRequest(response.FailMessages);
            //}
            //else if (response.StatusCodes == HttpStatusCode.NotFound)
            //{
            //    return NotFound(response.FailMessages);
            //}
            //else
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, response.FailMessages);
            //}


            //return response.IsSuccess switch
            //{
            //    true => response.Data switch
            //    {
            //        NoContent => StatusCode(StatusCodes.Status204NoContent),
            //        _ => Ok(response.Data)
            //    },
            //    false => response.StatusCodes switch
            //    {
            //        HttpStatusCode.BadRequest => BadRequest(response.FailMessages),
            //        HttpStatusCode.NotFound => NotFound(response.FailMessages),
            //        _ => StatusCode(StatusCodes.Status500InternalServerError, response.FailMessages)
            //    }
            //};
        }
    }
}