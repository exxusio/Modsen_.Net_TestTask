using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Abstractions.Notify;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent;
using EventsWebApplication.Application.Configs.Mappings.EventCategories;
using EventsWebApplication.Application.Configs.Mappings.Events;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Commands
{
    public class DeleteEventHandler_Tests
    {
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly Mock<INotificationService> _mockNotifyService;
        private readonly IMapper _mapper;

        public DeleteEventHandler_Tests()
        {
            _mockRepository = new Mock<IEventRepository>();
            _mockNotifyService = new Mock<INotificationService>();

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
        public async Task DeletesEvent_Successfully()
        {
            var eventId = Guid.NewGuid();

            var eventFromRepo = new Event { Id = eventId, Name = "Event to delete" };
            var eventReadDto = _mapper.Map<EventReadDto>(eventFromRepo);

            _mockRepository.Setup(r =>
                r.GetByIdAsync(eventId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(eventFromRepo);

            _mockRepository.Setup(r =>
                r.Delete(It.IsAny<Event>())
            ).Verifiable();

            _mockRepository.Setup(r =>
                r.SaveChangesAsync(It.IsAny<CancellationToken>())
            );

            _mockNotifyService.Setup(r =>
                r.SendToAllEventChange(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())
            );

            var handler = new DeleteEventHandler(
                _mockRepository.Object,
                _mapper,
                _mockNotifyService.Object
            );

            var result = await handler.Handle(
                new DeleteEventCommand { EventId = eventId },
                CancellationToken.None
            );

            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);

            _mockRepository.Verify(r =>
                r.Delete(It.IsAny<Event>()),
                Times.Once
            );
        }

        [Fact]
        public async Task ThrowsNotFoundException_WhenEventNotFound()
        {
            var eventId = Guid.NewGuid();

            _mockRepository.Setup(r =>
                r.GetByIdAsync(eventId, It.IsAny<CancellationToken>())
            ).ReturnsAsync((Event)null);

            _mockNotifyService.Setup(r =>
                r.SendToAllEventChange(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())
            );

            var handler = new DeleteEventHandler(
                _mockRepository.Object,
                _mapper,
                _mockNotifyService.Object
            );

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(
                    new DeleteEventCommand { EventId = eventId },
                    CancellationToken.None
                )
            );

            Assert.IsType<NotFoundException>(exception);
        }

        [Fact]
        public async Task SavesChanges_AfterEventDeleted()
        {
            var eventId = Guid.NewGuid();

            var eventFromRepo = new Event { Id = eventId, Name = "Event to delete" };
            var eventReadDto = _mapper.Map<EventReadDto>(eventFromRepo);

            _mockRepository.Setup(r =>
                r.GetByIdAsync(eventId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(eventFromRepo);

            _mockRepository.Setup(r =>
                r.Delete(It.IsAny<Event>())
            ).Verifiable();

            _mockRepository.Setup(r =>
                r.SaveChangesAsync(It.IsAny<CancellationToken>())
            );

            _mockNotifyService.Setup(r =>
                r.SendToAllEventChange(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())
            );

            var handler = new DeleteEventHandler(
                _mockRepository.Object,
                _mapper,
                _mockNotifyService.Object
            );

            await handler.Handle(
                new DeleteEventCommand { EventId = eventId },
                CancellationToken.None
            );

            _mockRepository.Verify(r =>
                r.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                    Times.Once
                );
        }
    }
}