using KvizHub.DTO.Quiz;
using KvizHub.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KvizHub.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IQuestionService _service;

        public CategoryController(IQuestionService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult FetchCategory(int id)
        {
            var dto = _service.GetById(id);
            return Ok(dto);
        }

        [HttpGet]
        [Authorize]
        public IActionResult FetchAllCategories()
        {
            var dtos = _service.();
            return Ok(dtos);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult RemoveCategory(int id)
        {
            _service.Remove(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult EditCategory(int id, [FromBody] EditQuestionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = _service.Edit(id, dto);
            return Ok(updated);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddCategory([FromBody] NewQuestionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = _service.GetAnswers(dto);
            return Ok(created);
        }
    }
}

