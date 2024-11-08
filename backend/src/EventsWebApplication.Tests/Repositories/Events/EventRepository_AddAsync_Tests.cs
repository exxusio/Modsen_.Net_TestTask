using Microsoft.EntityFrameworkCore;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Infrastructure.Data.Repositories;

namespace EventsWebApplication.Tests.Repositories.Events
{
    public class EventRepository_AddAsync_Tests
    {
        private readonly EventRepository _eventRepository;
        private readonly AppDbContext _context;

        public EventRepository_AddAsync_Tests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _eventRepository = new EventRepository(_context);
        }

        [Fact]
        public async Task AddsEventSuccessfully()
        {
            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = "New Event",
                Description = "New Event Description",
                Date = DateTime.Now,
                Time = TimeSpan.Zero,
                Location = "New Location",
                MaxParticipants = 100
            };

            await _eventRepository.AddAsync(newEvent);
            await _context.SaveChangesAsync();

            var addedEvent = await _context.Set<Event>().FindAsync(newEvent.Id);
            Assert.NotNull(addedEvent);
            Assert.Equal("New Event", addedEvent.Name);
        }
    }
}