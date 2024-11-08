using Moq;
using AutoMapper;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Repositories;
using EventsWebApplication.Application.UseCases.Admins.EventCases.Commands.DeleteEvent;
using EventsWebApplication.Application.Abstractions.Caching;
using EventsWebApplication.Application.Configs.Mappings;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Tests.UseCases.Events.Commands
{
    public class DeleteEventHandler_Tests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly Mock<IEventRepository> _mockRepository;
        private readonly IMapper _mapper;

        public DeleteEventHandler_Tests()
        {
            _mockCacheService = new Mock<ICacheService>();
            _mockRepository = new Mock<IEventRepository>();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EventMappingConfig());
                cfg.AddProfile(new EventCategoryMappingConfig());
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

            _mockCacheService.Setup(c =>
                c.DeleteAsync<EventReadDto>(eventReadDto.Id.ToString())
            ).Returns(Task.CompletedTask)
            .Verifiable();

            var handler = new DeleteEventHandler(
                _mockCacheService.Object,
                _mockRepository.Object,
                _mapper
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
            _mockCacheService.Verify(c =>
                c.DeleteAsync<EventReadDto>(eventReadDto.Id.ToString()),
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

            var handler = new DeleteEventHandler(
                _mockCacheService.Object,
                _mockRepository.Object,
                _mapper
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

            _mockCacheService.Setup(c =>
                c.DeleteAsync<EventReadDto>(eventReadDto.Id.ToString())
            ).Returns(Task.CompletedTask)
            .Verifiable();

            var handler = new DeleteEventHandler(
                _mockCacheService.Object,
                _mockRepository.Object,
                _mapper
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