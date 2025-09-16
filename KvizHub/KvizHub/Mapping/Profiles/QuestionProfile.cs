using AutoMapper;
using KvizHub.DTO.Quiz;
using KvizHub.Mapping.ConversionModel;
using QuizWebServer.Mapping.Converters;

namespace QuizWebServer.Mapping.Profiles
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

