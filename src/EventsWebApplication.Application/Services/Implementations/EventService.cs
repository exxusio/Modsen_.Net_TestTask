using AutoMapper;
using EventsWebApplication.Application.DTOs.Events;
using EventsWebApplication.Application.DTOs.EventCategories;
using EventsWebApplication.Application.Services.Interfaces;
using EventsWebApplication.Domain.Interfaces;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = _unitOfWork.GetRepository<Event>();

            var events = await repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }

        public async Task<EventDetailedReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = _unitOfWork.GetRepository<Event>();

            var _event = await repository.GetByIdAsync(id, cancellationToken);
            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {id}", nameof(_event));
            }

            return _mapper.Map<EventDetailedReadDto>(_event);
        }

        public async Task<EventDetailedReadDto> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var repository = _unitOfWork.GetRepository<Event>();

            var _event = (await repository.GetByPredicateAsync(eventDto => eventDto.Name == name, cancellationToken)).FirstOrDefault();
            if (_event == null)
            {
                throw new NotFoundException($"Not found with name: {name}", nameof(_event));
            }

            return _mapper.Map<EventDetailedReadDto>(_event);
        }

        public async Task<IEnumerable<EventReadDto>> GetByCriteriaAsync(EventCriteriaDto criteriaDto, CancellationToken cancellationToken = default)
        {
            var repository = _unitOfWork.GetRepository<Event>();

            var events = await repository.GetByPredicateAsync(
                eventDto =>
                    (!criteriaDto.Date.HasValue || eventDto.Date == criteriaDto.Date.Value) &&
                    (string.IsNullOrEmpty(criteriaDto.Location) || eventDto.Location.Contains(criteriaDto.Location)) &&
                    (criteriaDto.Category == null || _mapper.Map<EventCategoryReadDto>(eventDto.Category).Equals(criteriaDto.Category)),
                cancellationToken
                );

            return _mapper.Map<IEnumerable<EventReadDto>>(events);
        }

        public async Task<IEnumerable<EventWithAvailabilityDto>> GetEventsWithAvailabilityAsync(CancellationToken cancellationToken = default)
        {
            var repository = _unitOfWork.GetRepository<Event>();

            var events = await repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventWithAvailabilityDto>>(events);
        }

        public async Task<EventReadDto> CreateAsync(EventCreateDto createDto, CancellationToken cancellationToken = default)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            var eventRepository = _unitOfWork.GetRepository<Event>();
            var categoryRepository = _unitOfWork.GetRepository<EventCategory>();

            var _event = _mapper.Map<Event>(createDto);

            var existingCategory = await categoryRepository.GetByIdAsync(createDto.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Not found with id: {createDto.CategoryId}", nameof(existingCategory));
            }

            _event.Category = existingCategory;

            await eventRepository.AddAsync(_event, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventReadDto>(_event);
        }

        public async Task<EventReadDto> UpdateAsync(EventUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            var repository = _unitOfWork.GetRepository<Event>();

            var existingEvent = await repository.GetByIdAsync(updateDto.Id, cancellationToken);
            if (existingEvent == null)
            {
                throw new NotFoundException($"Not found with id: {updateDto.Id}", nameof(existingEvent));
            }

            var newEvent = _mapper.Map(updateDto, existingEvent);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EventReadDto>(newEvent);
        }

        public async Task<EventDetailedReadDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var repository = _unitOfWork.GetRepository<Event>();

            var _event = await repository.GetByIdAsync(id, cancellationToken);

            if (_event == null)
            {
                throw new NotFoundException($"Not found with id: {id}", nameof(_event));
            }

            repository.Delete(_event);
            await repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventDetailedReadDto>(_event);
        }
    }
}