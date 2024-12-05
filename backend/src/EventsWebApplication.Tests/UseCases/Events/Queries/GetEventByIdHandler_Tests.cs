using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById;
using EventsWebApplication.Application.Configs.Mappings.EventCategories;
using EventsWebApplication.Application.Configs.Mappings.Events;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Queries
{
    public class GetEventByIdHandler_Tests
    {
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly IMapper _mapper;

        public GetEventByIdHandler_Tests()
        {
            _mockEventRepository = new Mock<IEventRepository>();

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
        public async Task ReturnsEvent_WhenEventFoundInRepository()
        {
            var eventId = Guid.NewGuid();

            var eventFromRepo = new Event
            {
                Id = eventId,
                Name = "Test Event",
                Description = "Test Description",
                Date = DateTime.Now,
                Time = TimeSpan.Zero,
                Location = "Test Location",
                ImageUrl = "test_image.jpg",
                MaxParticipants = 100,
                EventRegistrations = new List<EventRegistration>(),
                Category = new EventCategory()
            };

            var eventReadDto = _mapper.Map<EventReadDto>(eventFromRepo);

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(eventId, CancellationToken.None)
            ).ReturnsAsync(eventFromRepo);

            var handler = new GetEventByIdHandler(
                _mockEventRepository.Object,
                _mapper
            );

            var result = await handler.Handle(
                new GetEventByIdQuery { EventId = eventId },
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);
            Assert.Equal("Test Event", result.Name);

            _mockEventRepository.Verify(r =>
                r.GetByIdAsync(eventId, CancellationToken.None),
                Times.Once
            );
        }

        [Fact]
        public async Task ThrowsNotFoundException_WhenEventNotFoundInRepository()
        {
            var eventId = Guid.NewGuid();

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(eventId, CancellationToken.None)
            ).ReturnsAsync((Event)null);

            var handler = new GetEventByIdHandler(
                _mockEventRepository.Object,
                _mapper
            );

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    new GetEventByIdQuery { EventId = eventId },
                    CancellationToken.None
                )
            );

            Assert.IsType<NotFoundException>(exception);
        }
    }
}