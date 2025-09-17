using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Mapping.ConversionModel;
using QuizWebServer.Mapping.Converters;
using QuizWebServer.Mapping.TypeConverters;
using QuizWebServer.Models.QuizSolution;

namespace QuizWebServer.Mapping.Profiles
{
    public class SolutionProfile : Profile
    {
        public SolutionProfile()
        {
            CreateMap<SolQuizDTO, QuizSolutionConversionModel>()
                .ConvertUsing<DtoToQuizSolutionConverter>();

            CreateMap<QuizAttempt, QuizAttemptDTO>()
                .ForMember(d => d.AnswerSolutionIds,
                           o => o.MapFrom(s => s.QuestionSolutions
                                                .Select(q => q.QuizQuestionInfoId)
                                                .ToList()));

            CreateMap<QuizAttempt, QuizAttemptInfoDTO>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.ParentQuiz.Title))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.ParentQuiz.Description))
                .ForMember(d => d.difficulty, o => o.MapFrom(s => s.ParentQuiz.Difficulty));

            CreateMap<QuestionSolutionConversionModel, QuestionSolutionResponseDTO>()
                .ConvertUsing<QuestionSolutionToDtoConverter>();

            CreateMap<QuizAttempt, QuizSolutionTopListElement>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Username))
                .ForMember(d => d.Points, o => o.MapFrom(s => s.ScorePoints))
                .ForMember(d => d.SolutionTime, o => o.MapFrom(s => s.AttemptDate));
        }
    }
}
