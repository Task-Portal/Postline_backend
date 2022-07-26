using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postline.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForUpdate;

namespace Postline.Presentation.Controllers
{
    [Route("api/point")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PointController(IServiceManager service)
        {
            _service = service;
        }
        

        

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> CreatePoint([FromBody] PointForCreationDto point)
        {
            var response = await _service.PointService.CreatePointAsync(point, User.Identity.Name,false);

            return Ok(response);
        }

       

       
    }
}