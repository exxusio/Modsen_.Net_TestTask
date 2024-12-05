using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Queries.GetAllEvents;
using EventsWebApplication.Application.Configs.Mappings.EventCategories;
using EventsWebApplication.Application.Configs.Mappings.Events;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Queries
{
    public class GetAllEventsHandler_Tests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly IMapper _mapper;

        public GetAllEventsHandler_Tests()
        {
            _mockRepository = new Mock<IEventRepository>();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CreateEventCommandToEventProfile());
                cfg.AddProfile(new EventToEventReadDtoProfile());
                cfg.AddProfile(new UpdateEventCommandToEventProfile());
                cfg.AddProfile(new CreateCategoryCommandToEventCategoryProfile());
                cfg.AddProfile(new EventCategoryToEventCategoryReadDtoProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async Task ReturnsEvents_List()
        {
            var eventsFromRepo = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Name = "Event 1" },
                new Event { Id = Guid.NewGuid(), Name = "Event 2" },
            };

            var eventReadDtos = _mapper.Map<IEnumerable<EventReadDto>>(eventsFromRepo);

            _mockRepository.Setup(r =>
                r.GetAllAsync(
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(eventsFromRepo);

            var handler = new GetAllEventsHandler(
                _mockRepository.Object,
                _mapper
            );

            var result = await handler.Handle(
                new GetAllEventsQuery(),
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Event 1", result.First().Name);
        }

        [Fact]
        public async Task ReturnsEmptyList_WhenNoEvents()
        {
            var eventsFromRepo = new List<Event>();

            _mockRepository.Setup(r =>
                r.GetAllAsync(
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(eventsFromRepo);

            var handler = new GetAllEventsHandler(
                _mockRepository.Object,
                _mapper
            );

            var result = await handler.Handle(
                new GetAllEventsQuery(),
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}