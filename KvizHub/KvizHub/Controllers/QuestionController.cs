using KvizHub.DTO.Quiz;
using KvizHub.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KvizHub.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IQuestionService _questionService;

        public QuestionController(IQuizService quizService, IQuestionService questionService)
        {
            _quizService = quizService;
            _questionService = questionService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult FetchQuestion(int id)
        {
            var dto = _questionService.GetById(id);
            return Ok(dto);
        }

        [HttpGet("{id}/options")]
        [Authorize(Roles = "admin")]
        public IActionResult FetchQuestionOptions(int id)
        {
            var dto = _questionService.GetAnswers(id);
            return Ok(dto);
        }

        [HttpGet("by-quiz/{quizId}")]
        [Authorize]
        public IActionResult FetchByQuiz(int quizId)
        {
            var list = _questionService.GetAllByQuizId(quizId);
            return Ok(list);
        }

        [HttpGet("by-quiz/{quizId}/with-options")]
        [Authorize(Roles = "admin")]
        public IActionResult FetchWithOptions(int quizId)
        {
            var list = _questionService.GetAllWithAnswersForQuiz(quizId);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult RemoveQuestion(int id)
        {
            _questionService.Remove(id);
            return Ok();
        }

        [HttpPost("to-quiz/{quizId}")]
        [Authorize(Roles = "admin")]
        public IActionResult AddQuestion(int quizId, [FromBody] NewQuestionDTO dto)
        {
            var created = _questionService.Add(quizId, dto);
            return Ok(created);
        }

        [HttpPatch("to-quiz/{quizId}")]
        [Authorize(Roles = "admin")]
        public IActionResult EditQuestion(int quizId, [FromBody] EditQuestionDTO dto)
        {
            var updated = _questionService.Edit(quizId, dto);
            return Ok(updated);
        }
    }
}
