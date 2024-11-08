
using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventById;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Application.Configs.Mappings;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Queries
{
    public class GetEventByIdHandler_Tests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly IMapper _mapper;

        public GetEventByIdHandler_Tests()
        {
            _mockCacheService = new Mock<ICacheService>();
            _mockEventRepository = new Mock<IEventRepository>();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventMappingConfig());
                cfg.AddProfile(new EventCategoryMappingConfig());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async Task ReturnsEvent_WhenEventIsInCache()
        {
            var eventId = Guid.NewGuid();

            var cachedEvent = new EventReadDto
            {
                Id = eventId,
                Name = "Test Event",
                Description = "Test Description",
                Date = DateTime.Now,
                Time = TimeSpan.Zero,
                Location = "Test Location",
                ImageUrl = "test_image.jpg",
                MaxParticipants = 100,
                RegisteredCount = 50,
                HasAvailableSeats = true,
                Category = new EventCategoryReadDto()
            };

            _mockCacheService.Setup(c =>
                c.GetAsync<EventReadDto>(eventId.ToString())
            ).ReturnsAsync(cachedEvent);

            var handler = new GetEventByIdHandler(
                _mockCacheService.Object,
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

            _mockCacheService.Verify(c =>
                c.GetAsync<EventReadDto>(eventId.ToString()),
                Times.Once
            );
        }

        [Fact]
        public async Task ReturnsEvent_WhenEventNotInCache_AndFoundInRepository()
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

            _mockCacheService.Setup(c =>
                c.GetAsync<EventReadDto>(eventId.ToString())
            ).ReturnsAsync((EventReadDto)null);

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(eventId, CancellationToken.None)
            ).ReturnsAsync(eventFromRepo);

            _mockCacheService.Setup(c =>
                c.SetAsync(eventReadDto.Id.ToString(), eventReadDto)
            );

            var handler = new GetEventByIdHandler(
                _mockCacheService.Object,
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

            _mockCacheService.Setup(c =>
                c.GetAsync<EventReadDto>(eventId.ToString())
            ).ReturnsAsync((EventReadDto)null);

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(eventId, CancellationToken.None)
            ).ReturnsAsync((Event)null);

            var handler = new GetEventByIdHandler(
                _mockCacheService.Object,
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