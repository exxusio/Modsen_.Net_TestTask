using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Abstractions.Caching;
using EventsWebApplication.Domain.Abstractions.Data.Repositories;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Filters;

namespace EventsWebApplication.Infrastructure.Data.Repositories
{
    public class CachedEventRepository(
        IEventRepository _repository,
        ICacheService _cache,
        IMapper _mapper
    ) : IEventRepository
    {
        public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cacheKey = id.ToString();

            var cachedEvent = await _cache.GetAsync<EventReadDto>(cacheKey);
            if (cachedEvent != null)
            {
                var mapEvent = _mapper.Map<Event>(cachedEvent);

                Track(mapEvent);

                return _mapper.Map<Event>(mapEvent);
            }

            var _event = await _repository.GetByIdAsync(id, cancellationToken);
            if (_event != null)
            {
                await _cache.SetAsync(cacheKey, _mapper.Map<EventReadDto>(_event));
            }

            return _event;
        }

        public async Task<(IEnumerable<Event>, int)> GetByFilterAsync(PagedFilter paged, EventFilter filter, CancellationToken cancellationToken)
        {
            return await _repository.GetByFilterAsync(paged, filter, cancellationToken);
        }

        public async Task<Event?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByNameAsync(name, cancellationToken);
        }

        public async Task AddAsync(Event _event, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(_event, cancellationToken);
            await _cache.SetAsync(_event.Id.ToString(), _mapper.Map<EventReadDto>(_event));
        }

        public void Update(Event _event)
        {
            _repository.Update(_event);
            _cache.SetAsync(_event.Id.ToString(), _mapper.Map<EventReadDto>(_event)).Wait();
        }

        public void Delete(Event _event)
        {
            _repository.Delete(_event);
            _cache.DeleteAsync<EventReadDto>(_event.Id.ToString()).Wait();
        }

        public void Track(Event _event)
        {
            _repository.Track(_event);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}