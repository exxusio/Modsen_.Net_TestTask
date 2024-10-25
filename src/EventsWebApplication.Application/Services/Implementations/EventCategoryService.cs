using AutoMapper;
using EventsWebApplication.Application.DTOs.EventCategories;
using EventsWebApplication.Application.Services.Interfaces;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Implementations
{
    public class EventCategoryService : IEventCategoryService
    {
        private readonly IEventCategoryRepository _repository;
        private readonly IMapper _mapper;

        public EventCategoryService(IEventCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventCategoryReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EventCategoryReadDto>>(categories);
        }

        public async Task<EventCategoryDetailedReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _repository.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException($"Not found with id: {id}", nameof(category));
            }

            return _mapper.Map<EventCategoryDetailedReadDto>(category);
        }

        public async Task<EventCategoryReadDto> CreateAsync(EventCategoryCreateDto createDto, CancellationToken cancellationToken = default)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            var category = _mapper.Map<EventCategory>(createDto);

            await _repository.AddAsync(category, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventCategoryReadDto>(category);
        }

        public async Task<EventCategoryReadDto> UpdateAsync(EventCategoryUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            var existingCategory = await _repository.GetByIdAsync(updateDto.Id, cancellationToken);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Not found with id: {updateDto.Id}", nameof(existingCategory));
            }

            var newCategory = _mapper.Map(updateDto, existingCategory);
            await _repository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<EventCategoryReadDto>(newCategory);
        }

        public async Task<EventCategoryDetailedReadDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _repository.GetByIdAsync(id, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException($"Not found with id: {id}", nameof(category));
            }

            _repository.Delete(category);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventCategoryDetailedReadDto>(category);
        }
    }
}