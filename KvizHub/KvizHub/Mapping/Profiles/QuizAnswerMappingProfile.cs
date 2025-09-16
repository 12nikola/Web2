using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Models.Answers;

namespace KvizHub.Mapping.Profiles
{
    public class QuizAnswerMappingProfile:Profile
    {
        public QuizAnswerMappingProfile() { 
        // Create DTO → Entity
            CreateMap<NewAnswerDTO, SingleOptionAnswer>();
            CreateMap<NewAnswerDTO, MultipleOptionAnswer>();
            CreateMap<NewAnswerDTO, TextEntryAnswer>();
            CreateMap<NewAnswerDTO, BooleanAnswer>();

            // Update DTO → Entity
            CreateMap<EditAnswerDTO, SingleOptionAnswer>().ApplyIsCorrectCondition();
            CreateMap<EditAnswerDTO, MultipleOptionAnswer>().ApplyIsCorrectCondition();
            CreateMap<EditAnswerDTO, TextEntryAnswer>().ApplyIsCorrectCondition();
            CreateMap<EditAnswerDTO, BooleanAnswer>().ApplyIsCorrectCondition();

        // Entity → Simplified DTO
            CreateMap<SingleOptionAnswer, BasicAnswerDTO>();
            CreateMap<MultipleOptionAnswer, BasicAnswerDTO>();
            CreateMap<TextEntryAnswer, BasicAnswerDTO>();
            CreateMap<BooleanAnswer, BasicAnswerDTO>();

            // Entity → Full DTO
            CreateMap<SingleOptionAnswer, NewAnswerDTO>().ApplyIncludeDetailsCondition();
            CreateMap<MultipleOptionAnswer, NewAnswerDTO>().ApplyIncludeDetailsCondition();
            CreateMap<TextEntryAnswer, NewAnswerDTO>().ApplyIncludeDetailsCondition();
            CreateMap<BooleanAnswer, NewAnswerDTO>().ApplyIncludeDetailsCondition();
    }
}
public static class MappingExtensions
{
    public static IMappingExpression<EditAnswerDTO, T> ApplyIsCorrectCondition<T>(
        this IMappingExpression<EditAnswerDTO, T> mapping) where T : class
    {
        return mapping.ForMember(dest => dest,
            opt => opt.MapFrom((src, dest) => src.Correct ?? (dest as dynamic).IsCorrect));
    }

    public static IMappingExpression<TSource, NewAnswerDTO> ApplyIncludeDetailsCondition<TSource>(
        this IMappingExpression<TSource, NewAnswerDTO> mapping)
    {
        return mapping.ForMember(dest => dest.Valid, opt =>
        {
            opt.Condition((src, dest, srcMember, destMember, context) =>
                srcMember != null &&
                context.Items.ContainsKey("IncludeDetails") &&
                (bool)context.Items["IncludeDetails"]);
        });
    }
}
}
