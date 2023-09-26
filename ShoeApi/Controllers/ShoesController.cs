using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeApi.Repositories.Interfaces;
using System.Security.Claims;

namespace ShoeApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly ILogger<ShoesController> _logger;

        public ShoesController(ILogger<ShoesController> logger, IShoeRepository shoeRepository)
        {
            _logger = logger;
            _shoeRepository = shoeRepository;

        }

        [HttpGet]
        public IActionResult Get() {

            var user = User.Identity;

            return Ok(_shoeRepository.GetShoes());
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool shoeDeleted = _shoeRepository.DeleteShoe(id);

            return shoeDeleted ? NoContent() : NotFound();
        }
    }
}