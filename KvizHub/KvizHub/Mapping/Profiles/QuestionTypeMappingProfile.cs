using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Enums;

namespace KvizHub.Mapping.Profiles
{
    public class QuestionTypeMappingProfile : Profile
    {
        public QuestionTypeMappingProfile()
        {
            // Entity → DTO
            CreateMap<QuestionType, QuizDTO>();
            // DTO → Entity
            CreateMap<NewQuestionDTO, QuestionType>();
            CreateMap<EditQuestionDTO, QuestionType>();
        }
    }
}
