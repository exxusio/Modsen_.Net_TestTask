using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Filters;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Application.UseCases.Users.EventCases.Queries.GetEventsByFilter;
using EventsWebApplication.Application.Configs.Mappings.EventCategories;
using EventsWebApplication.Application.Configs.Mappings.Filters;
using EventsWebApplication.Application.Configs.Mappings.Events;
using EventsWebApplication.Application.Configs.Mappings.Paged;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Queries
{
    public class GetEventsByFilterHandler_Tests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly IMapper _mapper;

        public GetEventsByFilterHandler_Tests()
        {
            _mockRepository = new Mock<IEventRepository>();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CreateEventCommandToEventProfile());
                cfg.AddProfile(new EventToEventReadDtoProfile());
                cfg.AddProfile(new UpdateEventCommandToEventProfile());
                cfg.AddProfile(new CreateCategoryCommandToEventCategoryProfile());
                cfg.AddProfile(new EventCategoryToEventCategoryReadDtoProfile());
                cfg.AddProfile(new EventCategoryToEventCategoryReadDtoProfile());
                cfg.AddProfile(new GetEventsByFilterQueryToEventFilterProfile());
                cfg.AddProfile(new PagedQueryToPagedFilterProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async Task ReturnsEvents_WhenEventsMatchFilter()
        {
            var query = new GetEventsByFilterQuery
            {
                EventName = "Test Event",
                FromDate = DateTime.Now.AddDays(-1),
                ToDate = DateTime.Now.AddDays(1),
                FromTime = TimeSpan.FromHours(10),
                ToTime = TimeSpan.FromHours(20),
                Location = "Test Location",
                CategoryId = Guid.NewGuid(),
                PageNumber = 1,
                PageSize = 10
            };

            var eventsFromRepo = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Event",
                    Date = DateTime.Now,
                    Time = TimeSpan.FromHours(12),
                    Location = "Test Location",
                    CategoryId = query.CategoryId.Value
                }
            };

            var eventReadDtos = _mapper.Map<IEnumerable<EventReadDto>>(eventsFromRepo);

            _mockRepository.Setup(r =>
                r.GetByFilterAsync(
                    It.IsAny<PagedFilter>(),
                    It.IsAny<EventFilter>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync((eventsFromRepo, eventsFromRepo.Count));

            var handler = new GetEventsByFilterHandler(
                _mockRepository.Object,
                _mapper
            );

            var result = await handler.Handle(
                query,
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Single(result.Events);
            Assert.Equal(result.TotalCount, eventsFromRepo.Count);
            Assert.Equal(query.EventName, result.Events.First().Name);

            _mockRepository.Verify(r =>
                r.GetByFilterAsync(
                    It.IsAny<PagedFilter>(),
                    It.IsAny<EventFilter>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task ReturnsEmpty_WhenNoEventsMatchFilter()
        {
            var query = new GetEventsByFilterQuery
            {
                EventName = "Nonexistent Event",
                FromDate = DateTime.Now.AddDays(-1),
                ToDate = DateTime.Now.AddDays(1),
                Location = "Nonexistent Location",
                CategoryId = Guid.NewGuid(),
                PageNumber = 1,
                PageSize = 10
            };

            _mockRepository.Setup(r =>
                r.GetByFilterAsync(
                    It.IsAny<PagedFilter>(),
                    It.IsAny<EventFilter>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync((new List<Event>(), 0));

            var handler = new GetEventsByFilterHandler(
                _mockRepository.Object,
                _mapper
            );

            var result = await handler.Handle(
                query,
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Empty(result.Events);

            _mockRepository.Verify(r =>
                r.GetByFilterAsync(
                    It.IsAny<PagedFilter>(),
                    It.IsAny<EventFilter>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task ReturnsEvents_Paginated()
        {
            var query = new GetEventsByFilterQuery
            {
                PageNumber = 1,
                PageSize = 2
            };

            var eventsFromRepo = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Name = "Event 1" },
                new Event { Id = Guid.NewGuid(), Name = "Event 2" },
                new Event { Id = Guid.NewGuid(), Name = "Event 3" },
                new Event { Id = Guid.NewGuid(), Name = "Event 4" },
                new Event { Id = Guid.NewGuid(), Name = "Event 5" }
            };

            var eventReadDtos = _mapper.Map<IEnumerable<EventReadDto>>(eventsFromRepo);

            _mockRepository.Setup(r =>
                r.GetByFilterAsync(
                    It.Is<PagedFilter>(f =>
                        f.PageNumber == query.PageNumber &&
                        f.PageSize == query.PageSize
                    ),
                    It.IsAny<EventFilter>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(
                (eventsFromRepo
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize),
                5)
            );

            var handler = new GetEventsByFilterHandler(
                _mockRepository.Object,
                _mapper
            );

            var result = await handler.Handle(
                query,
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Equal(2, result.Events.Count());
            Assert.Equal("Event 1", result.Events.First().Name);
        }

        [Fact]
        public async Task ReturnsEvents_FilteredByLocation()
        {
            var location = "Test Location";

            var query = new GetEventsByFilterQuery
            {
                Location = location,
                PageNumber = 1,
                PageSize = 2
            };

            var eventsFromRepo = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Name = "Event 1", Location = location },
                new Event { Id = Guid.NewGuid(), Name = "Event 2", Location = location },
                new Event { Id = Guid.NewGuid(), Name = "Event 3", Location = "Location" },
            };

            var eventReadDtos = _mapper.Map<IEnumerable<EventReadDto>>(eventsFromRepo);

            _mockRepository.Setup(r =>
                r.GetByFilterAsync(
                    It.IsAny<PagedFilter>(),
                    It.IsAny<EventFilter>(),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(
                (eventsFromRepo
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize),
                2)
            );

            var handler = new GetEventsByFilterHandler(
                _mockRepository.Object,
                _mapper
            );

            var result = await handler.Handle(
                query,
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Equal(2, result.Events.Count());
            Assert.All(result.Events, e =>
                Assert.Equal(location, e.Location)
            );
        }
    }
}