using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Abstractions.Data;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.CreateEvent;
using EventsWebApplication.Application.Configs.Mappings.EventCategories;
using EventsWebApplication.Application.Configs.Mappings.Events;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Commands
{
    public class CreateEventHandler_Tests
    {
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly Mock<IEventCategoryRepository> _mockCategoryRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public CreateEventHandler_Tests()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockCategoryRepository = new Mock<IEventCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

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
        public async Task CreateEvent_Success()
        {
            var command = new CreateEventCommand
            {
                Name = "New Event",
                Description = "Description of new event",
                Date = DateTime.UtcNow.AddDays(1),
                Time = TimeSpan.FromHours(5),
                Location = "Event Location",
                ImageUrl = "",
                MaxParticipants = 100,
                CategoryId = Guid.NewGuid()
            };

            var category = new EventCategory { Id = command.CategoryId, Name = "Category 1" };
            var eventFromRepo = new Event
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                Date = command.Date,
                Time = command.Time,
                Location = command.Location,
                ImageUrl = command.ImageUrl,
                MaxParticipants = command.MaxParticipants,
                Category = category
            };

            var eventReadDto = _mapper.Map<EventReadDto>(eventFromRepo);

            _mockEventRepository.Setup(r =>
                r.GetByNameAsync(command.Name, It.IsAny<CancellationToken>())
            ).ReturnsAsync((Event)null);

            _mockCategoryRepository.Setup(r =>
                r.GetByIdAsync(command.CategoryId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(category);

            _mockUnitOfWork.Setup(u =>
                u.Events
            ).Returns(_mockEventRepository.Object);

            _mockUnitOfWork.Setup(u =>
                u.EventCategories
            ).Returns(_mockCategoryRepository.Object);

            var handler = new CreateEventHandler(
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
        }

        [Fact]
        public async Task CreateEvent_AlreadyExists()
        {
            var name = "Existing Event";

            var command = new CreateEventCommand
            {
                Name = name,
                Description = "Description of existing event",
                Date = DateTime.UtcNow.AddDays(1),
                Time = TimeSpan.FromHours(5),
                Location = "Event Location",
                ImageUrl = "",
                MaxParticipants = 100,
                CategoryId = Guid.NewGuid()
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
                Category = new EventCategory { Id = command.CategoryId, Name = "Category 1" }
            };

            _mockEventRepository.Setup(r =>
                r.GetByNameAsync(name, It.IsAny<CancellationToken>())
            ).ReturnsAsync(existingEvent);

            _mockUnitOfWork.Setup(u =>
                u.Events
            ).Returns(_mockEventRepository.Object);

            var handler = new CreateEventHandler(
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
        public async Task ThrowsNotFoundException_WhenCategoryDoesNotExist()
        {
            var nonExistentCategoryId = Guid.NewGuid();

            var command = new CreateEventCommand
            {
                Name = "New Event",
                Description = "Description",
                Date = DateTime.Now,
                Time = TimeSpan.FromHours(1),
                Location = "Location",
                ImageUrl = "",
                MaxParticipants = 100,
                CategoryId = nonExistentCategoryId
            };

            _mockEventRepository.Setup(r =>
                r.GetByNameAsync(command.Name, It.IsAny<CancellationToken>())
            ).ReturnsAsync((Event)null);

            _mockCategoryRepository.Setup(r =>
                r.GetByIdAsync(nonExistentCategoryId, It.IsAny<CancellationToken>())
            ).ReturnsAsync((EventCategory)null);

            _mockUnitOfWork.Setup(u =>
                u.Events
            ).Returns(_mockEventRepository.Object);

            _mockUnitOfWork.Setup(u =>
                u.EventCategories
            ).Returns(_mockCategoryRepository.Object);

            var handler = new CreateEventHandler(
                _mockUnitOfWork.Object,
                _mapper)
            ;

            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => handler.Handle(command, CancellationToken.None)
            );

            Assert.IsType<NotFoundException>(exception);
            Assert.Equal(nameof(EventCategory), exception.Resource);
            Assert.Equal(nameof(command.CategoryId), exception.Field);
            Assert.Equal(nonExistentCategoryId.ToString(), exception.Value);
        }
    }
}