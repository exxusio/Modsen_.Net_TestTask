using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.UpdateEvent;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Application.Abstractions.Data;
using EventsWebApplication.Application.Configs.Mappings;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Commands
{
    public class UpdateEventHandler_Tests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly Mock<IEventCategoryRepository> _mockCategoryRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public UpdateEventHandler_Tests()
        {
            _mockCacheService = new Mock<ICacheService>();
            _mockEventRepository = new Mock<IEventRepository>();
            _mockCategoryRepository = new Mock<IEventCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventMappingConfig());
                cfg.AddProfile(new EventCategoryMappingConfig());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public async Task UpdateEvent_Success()
        {
            var command = new UpdateEventCommand
            {
                EventId = Guid.NewGuid(),
                Name = "Updated Event",
                Description = "Updated description",
                Date = DateTime.UtcNow.AddDays(1),
                Time = TimeSpan.FromHours(6),
                Location = "Updated Location",
                ImageUrl = "new-image-url",
                MaxParticipants = 200,
                CategoryId = Guid.NewGuid()
            };

            var category = new EventCategory { Id = command.CategoryId, Name = "Category 1" };
            var existingEvent = new Event
            {
                Id = command.EventId,
                Name = "Old Event",
                Description = "Old description",
                Date = DateTime.UtcNow,
                Time = TimeSpan.FromHours(3),
                Location = "Old Location",
                ImageUrl = "old-image-url",
                MaxParticipants = 50,
                Category = category
            };

            var updatedEvent = new Event
            {
                Id = command.EventId,
                Name = command.Name,
                Description = command.Description,
                Date = command.Date,
                Time = command.Time,
                Location = command.Location,
                ImageUrl = command.ImageUrl,
                MaxParticipants = command.MaxParticipants,
                Category = category
            };

            var eventReadDto = _mapper.Map<EventReadDto>(updatedEvent);

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(command.EventId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(existingEvent);

            _mockEventRepository.Setup(r =>
                r.GetByNameAsync(command.Name, It.IsAny<CancellationToken>())
            ).ReturnsAsync((Event)null);

            _mockCategoryRepository.Setup(r =>
                r.GetByIdAsync(command.CategoryId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(category);

            _mockUnitOfWork.Setup(u =>
                u.GetRepository<IEventRepository, Event>()
            ).Returns(_mockEventRepository.Object);

            _mockUnitOfWork.Setup(u =>
                u.GetRepository<EventCategory>()
            ).Returns(_mockCategoryRepository.Object);

            var handler = new UpdateEventHandler(
                _mockCacheService.Object,
                _mockUnitOfWork.Object,
                _mapper
            );

            var result = await handler.Handle(
                command,
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Description, result.Description);
            Assert.Equal(command.CategoryId, result.Category.Id);

            _mockCacheService.Verify(c =>
                c.SetAsync(
                    It.IsAny<string>(),
                    It.IsAny<EventReadDto>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task UpdateEvent_AlreadyExists()
        {
            var name = "Updated name";
            var eventId = Guid.NewGuid();

            var command = new UpdateEventCommand
            {
                EventId = eventId,
                Name = name,
                Description = "Updated description",
                Date = DateTime.UtcNow.AddDays(1),
                Time = TimeSpan.FromHours(6),
                Location = "Updated location",
                ImageUrl = "",
                MaxParticipants = 200,
                CategoryId = Guid.NewGuid()
            };

            var updatedEvent = new Event
            {
                Id = eventId,
                Name = "Old name",
                Description = "Old description",
                Date = DateTime.UtcNow.AddDays(2),
                Time = TimeSpan.FromHours(7),
                Location = "Old location",
                ImageUrl = "",
                MaxParticipants = 100,
                Category = new EventCategory { Id = command.CategoryId, Name = "Category 1" }
            };

            var existingEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = "Some description",
                Date = DateTime.UtcNow,
                Time = TimeSpan.FromHours(3),
                Location = "Location",
                ImageUrl = "",
                MaxParticipants = 50,
                Category = new EventCategory { Id = command.CategoryId, Name = "Category 2" }
            };

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(command.EventId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(updatedEvent);

            _mockEventRepository.Setup(r =>
                r.GetByNameAsync(name, It.IsAny<CancellationToken>())
            ).ReturnsAsync(existingEvent);

            _mockUnitOfWork.Setup(u =>
                u.GetRepository<IEventRepository, Event>()
            ).Returns(_mockEventRepository.Object);

            var handler = new UpdateEventHandler(
                _mockCacheService.Object,
                _mockUnitOfWork.Object,
                _mapper
            );

            var exception = await Assert.ThrowsAsync<AlreadyExistsException>(() =>
                handler.Handle(
                    command,
                    CancellationToken.None
                )
            );

            Assert.IsType<AlreadyExistsException>(exception);
        }

        [Fact]
        public async Task UpdateEvent_NotFound()
        {
            var command = new UpdateEventCommand
            {
                EventId = Guid.NewGuid(),
                Name = "Updated Event",
                Description = "Updated description",
                Date = DateTime.UtcNow.AddDays(1),
                Time = TimeSpan.FromHours(6),
                Location = "Updated Location",
                ImageUrl = "new-image-url",
                MaxParticipants = 200,
                CategoryId = Guid.NewGuid()
            };

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(command.EventId, It.IsAny<CancellationToken>())
            ).ReturnsAsync((Event)null);

            _mockUnitOfWork.Setup(u =>
                u.GetRepository<IEventRepository, Event>()
            ).Returns(_mockEventRepository.Object);

            var handler = new UpdateEventHandler(
                _mockCacheService.Object,
                _mockUnitOfWork.Object,
                _mapper
            );

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    command,
                    CancellationToken.None
                )
            );

            Assert.IsType<NotFoundException>(exception);
        }

        [Fact]
        public async Task UpdateEvent_CategoryNotFound()
        {
            var command = new UpdateEventCommand
            {
                EventId = Guid.NewGuid(),
                Name = "Updated Event",
                Description = "Updated description",
                Date = DateTime.UtcNow.AddDays(1),
                Time = TimeSpan.FromHours(6),
                Location = "Updated Location",
                ImageUrl = "new-image-url",
                MaxParticipants = 200,
                CategoryId = Guid.NewGuid()
            };

            var existingEvent = new Event
            {
                Id = command.EventId,
                Name = "Old Event",
                Description = "Old description",
                Date = DateTime.UtcNow,
                Time = TimeSpan.FromHours(3),
                Location = "Old Location",
                ImageUrl = "old-image-url",
                MaxParticipants = 50,
                Category = new EventCategory { Id = Guid.NewGuid(), Name = "Category 1" }
            };

            _mockEventRepository.Setup(r =>
                r.GetByIdAsync(command.EventId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(existingEvent);

            _mockCategoryRepository.Setup(r =>
                r.GetByIdAsync(command.CategoryId, It.IsAny<CancellationToken>())
            ).ReturnsAsync((EventCategory)null);

            _mockUnitOfWork.Setup(u =>
                u.GetRepository<IEventRepository, Event>()
            ).Returns(_mockEventRepository.Object);

            _mockUnitOfWork.Setup(u =>
                u.GetRepository<EventCategory>()
            ).Returns(_mockCategoryRepository.Object);

            var handler = new UpdateEventHandler(
                _mockCacheService.Object,
                _mockUnitOfWork.Object,
                _mapper
            );

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    command,
                    CancellationToken.None
                )
            );

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal(nameof(EventCategory), exception.Resource);
            Assert.Equal(nameof(command.CategoryId), exception.Field);
        }
    }
}