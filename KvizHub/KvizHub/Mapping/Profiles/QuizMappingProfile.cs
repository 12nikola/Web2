using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Models.Quiz;

namespace KvizHub.Mapping.Profiles
{
    public class QuizMappingProfile:Profile
    {
        public QuizMappingProfile()
        {
            // DTO → Entity
            CreateMap<NewQuizDTO, Quizz>();

            // Entity → DTO
            CreateMap<Quizz, QuizDTO>()
                .ForMember(dest => dest,
                           opt => opt.MapFrom(src => src.Description
                                                        .Select(q => q.)
                                                        .ToList()))
                .ForMember(dest => dest.Editable,
                           opt => opt.MapFrom(src => src == null
                                                   || !src.NumberOfQuestions.Any()));

            // Update DTO → Entity (uslovno mapiranje)
            CreateMap<EditQuizDTO, Quizz>()
                .ForMember(dest => dest.TimeLimit, opt => opt.Condition(src => src.MaxDuration.HasValue))
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.IsPublished.HasValue))
                .ForMember(dest => dest.Difficulty, opt => opt.Condition(src => src.difficulty.HasValue))
                .ForAllOtherMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));
        }
    }
}
