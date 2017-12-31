using AutoMapper;
using Data.Core.Domain;
using OTM.Models.GroupViewModels;
using OTM.Models.TestTemplatesViewModels;

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

            #endregion
        }
    }
}
