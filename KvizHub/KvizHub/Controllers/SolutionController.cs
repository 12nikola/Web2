using KvizHub.DTO.Quiz;
using KvizHub.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KvizHub.Interfaces;
using KvizHub.Exceptions;

namespace KvizHub.Controllers
{
    [Route("api/solutions")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpPost("quiz/{quizId}")]
        [Authorize]
        public IActionResult SubmitQuiz(int quizId, [FromBody] SolQuizDTO dto)
        {
            var user = User?.Identity?.Name;
            var result = _solutionService.IsQuizSolutionByUser(dto, quizId);
            return Ok(result);
        }

        [HttpGet("question/{id}")]
        [Authorize]
        public IActionResult FetchQuestionSolution(int id)
        {
            var user = User?.Identity?.Name;

            if (!User.IsInRole("admin"))
                _solutionService.IsQuizSolutionByUser(user, id);

            var dto = _solutionService.IsQuizSolutionByUser(user, id);
            return Ok(dto);
        }

        [HttpGet("quiz/{id}")]
        [Authorize]
        public IActionResult FetchQuizSolution(int id)
        {
            var user = User?.Identity?.Name;

            if (!User.IsInRole("admin"))
                _solutionService.IsQuizSolutionByUser(user, id);

            var dto = _solutionService.IsQuizSolutionByUser(user, id);
            return Ok(dto);
        }

        [HttpGet("user/{username}")]
        [Authorize]
        public IActionResult FetchUserSolutions(string username)
        {
            var user = User?.Identity?.Name;

            if (user != username && !User.IsInRole("admin"))
                throw new AccessDeniedException("You are not allowed to view this user's solutions.");

            var list = _solutionService.IsQuestionSolutionByUser(username);
            return Ok(list);
        }

        [HttpGet("quiz/{quizId}/top")]
        [Authorize]
        public IActionResult FetchTopForQuiz(int quizId, DateTime? from = null)
        {
            var user = User?.Identity?.Name;
            var date = from ?? DateTime.MinValue;

            var dto = _solutionService.GetTopListForQuiz(user, quizId, date);
            return Ok(dto);
        }

        [HttpGet("ranking/global")]
        [Authorize]
        public IActionResult FetchGlobalRanking()
        {
            var user = User?.Identity?.Name;
            var dto = _solutionService(user);
            return Ok(dto);
        }
    }
}
