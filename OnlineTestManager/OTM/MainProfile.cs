using AutoMapper;
using Data.Core.Domain;
using OTM.ViewModels.AnswerTemplate;
using OTM.ViewModels.ExerciseTemplate;
using OTM.ViewModels.Group;
using OTM.ViewModels.TestTemplates;
using Exercise = Data.Core.Domain.Exercise;

namespace OTM
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            #region Groups
            CreateMap<Group, CreateGroupViewModel>();
            CreateMap<Group, EditGroupViewModel>().ForMember(dest => dest.Students,
                opt => opt.Ignore());
            CreateMap<User, EditStudentInGroup>();
            CreateMap<Group, IndexGroupViewModel>();
            CreateMap<Group, DeleteGroupViewModel>();
            CreateMap<Group, DetailsGroupViewModel>();
            CreateMap<User, DetailsStudentInGroup>();
            #endregion

            #region TestTemplates

            CreateMap<Test, IndexTestTemplatesViewModel>();
            CreateMap<Test, EditTestTemplatesViewModel>();
            CreateMap<Test, DeleteTestTemplateViewModel>();

            #endregion

            #region ExeciseTemplates

            CreateMap<Exercise, IndexExercise>();
            CreateMap<Exercise, EditExercise>();
            CreateMap<Exercise, DeleteExerciseTemplateViewModel>();
            #endregion

            #region AnswerTemplates

            CreateMap<Answer, IndexAnswer>();
            CreateMap<Answer, EditAnswer>();
            CreateMap<Answer, EditAnswerTemplatesViewModel>();
            CreateMap<Answer, DeleteAnswerTemplatesViewModel>();

            #endregion
        }
    }
}
