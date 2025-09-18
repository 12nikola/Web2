using KvizHub.DTO;
using KvizHub.DTO.Quiz;
using KvizHub.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KvizHub.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        
            private readonly IQuizService _quizService;

            public QuizController(IQuizService quizService)
            {
                _quizService = quizService;
            }

            [HttpPost]
            [Authorize(Roles = "admin")]
            public IActionResult Create([FromBody] NewQuizDTO newQuiz)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                QuizDTO createdQuiz = _quizService.Add(newQuiz);
                return Ok(createdQuiz);
            }

            [HttpPatch("{quizId}")]
            [Authorize(Roles = "admin")]
            public IActionResult Update([FromBody] EditQuizDTO updatedQuiz, int quizId)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                QuizDTO quiz = _quizService.Edit(updatedQuiz, quizId);
                return Ok(quiz);
            }

            [HttpDelete("{quizId}")]
            [Authorize(Roles = "admin")]
            public IActionResult Delete(int quizId)
            {
                _quizService.Remove(quizId);
                return Ok();
            }

            [HttpGet("{quizId}")]
            [Authorize]
            public IActionResult GetById(int quizId)
            {
                QuizDTO quiz = _quizService.GetById(quizId);
                return Ok(quiz);
            }

            [HttpGet]
            [Authorize]
            public IActionResult GetAll([FromQuery] PageInfo pagingOptions)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                int totalCount;
                List<QuizDTO> quizzes;

                if (!string.IsNullOrEmpty(pagingOptions.SearchTerm))
                {
                    (quizzes, totalCount) = _quizService.SearchByKeyword(
                        pagingOptions.SearchTerm,
                        pagingOptions.PageSize,
                        pagingOptions.StartIndex
                    );
                }
                else if (pagingOptions.Category != null)
                {
                    (quizzes, totalCount) = _quizService.FilterByCategory(
                        (int)pagingOptions.Category,
                        pagingOptions.PageSize,
                        pagingOptions.StartIndex
                    );
                }
                else if (pagingOptions.Level != null)
                {
                    (quizzes, totalCount) = _quizService.FilterByDifficulty(
                        ()pagingOptions.Level,
                        pagingOptions.PageSize,
                        pagingOptions.StartIndex
                    );
                }
                else
                {
                    (quizzes, totalCount) = _quizService.GetAll(
                        pagingOptions.PageSize,
                        pagingOptions.StartIndex
                    );
                }

                return Ok(new { NumberOfQuizzes = totalCount, Quizzes = quizzes });
            }
        }

    }

