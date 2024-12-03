
using AutoMapper;
using EventsWebApplication.Application.UseCases.Admins.EventCategoryCases.Commands.CreateCategory;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Configs.Mappings.EventCategories
{
    public class CreateCategoryCommandToEventCategoryProfile
    : Profile
    {
        public CreateCategoryCommandToEventCategoryProfile()
        {
            CreateMap<CreateCategoryCommand, EventCategory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore());
        }
    }
}