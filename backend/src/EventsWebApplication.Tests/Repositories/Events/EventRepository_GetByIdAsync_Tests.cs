using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Infrastructure.Data.Repositories;
using EventsWebApplication.Infrastructure.Data;

namespace EventsWebApplication.Tests.Repositories.Events
{
    public class EventRepository_GetByIdAsync_Tests
    {
        private readonly IEventRepository _eventRepository;
        private readonly AppDbContext _context;

        public EventRepository_GetByIdAsync_Tests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _eventRepository = new EventRepository(_context);
        }

        [Fact]
        public async Task ReturnsEvent_WhenEventExists()
        {
            var eventId = Guid.NewGuid();
            var mockEvent = new Event
            {
                Id = eventId,
                Name = "Sample Event",
                Description = "Sample Description",
                Date = DateTime.Now,
                Time = TimeSpan.Zero,
                Location = "Sample Location",
                MaxParticipants = 100
            };

            _context.Set<Event>().Add(mockEvent);
            await _context.SaveChangesAsync();

            var result = await _eventRepository.GetByIdAsync(eventId);

            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);
            Assert.Equal("Sample Event", result.Name);
        }

        [Fact]
        public async Task ReturnsNull_WhenEventDoesNotExist()
        {
            var eventId = Guid.NewGuid();

            var result = await _eventRepository.GetByIdAsync(eventId);

            Assert.Null(result);
        }
    }
}