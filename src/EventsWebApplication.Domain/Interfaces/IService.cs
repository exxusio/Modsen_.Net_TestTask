namespace EventsWebApplication.Domain.Interfaces
{
    public interface IService<TEntity, TReadDto, TDetailedReadDto, TCreateDto, TUpdateDto>
        where TEntity : class
    {
        Task<IEnumerable<TReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TDetailedReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TReadDto> CreateAsync(TCreateDto dto, CancellationToken cancellationToken = default);
        Task<TReadDto> UpdateAsync(TUpdateDto dto, CancellationToken cancellationToken = default);
        Task<TDetailedReadDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}