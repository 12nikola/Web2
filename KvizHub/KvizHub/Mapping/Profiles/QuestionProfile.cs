using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Mapping.ConversionModel;
using KvizHub.Mapping.Converters;

namespace KvizHub.Mapping.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<NewQuestionDTO, QuestionConversionModel>()
                .ConvertUsing<DtoToQuestionConverter>();

            CreateMap<EditQuestionDTO, QuestionConversionModel>()
                .ConvertUsing<DtoToUpdateQuestionConverter>();

            CreateMap<QuestionConversionModel, QuizQuestionDTO>()
                .ConvertUsing<QuestionToDtoConverter>();
        }
    }
}

