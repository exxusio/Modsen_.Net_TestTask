using AutoMapper;
using EventsWebApplication.Application.DTOs.Roles;
using EventsWebApplication.Application.Services.Interfaces;
using EventsWebApplication.Domain.Interfaces.Repositories;
using EventsWebApplication.Domain.Exceptions;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var roles = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<RoleReadDto>>(roles);
        }

        public async Task<RoleDetailedReadDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var role = await _repository.GetByIdAsync(id, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Not found with id: {id}", nameof(role));
            }

            return _mapper.Map<RoleDetailedReadDto>(role);
        }

        public async Task<RoleReadDto> CreateAsync(RoleCreateDto createDto, CancellationToken cancellationToken = default)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            var role = _mapper.Map<Role>(createDto);

            await _repository.AddAsync(role, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<RoleReadDto> UpdateAsync(RoleUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            var existingRole = await _repository.GetByIdAsync(updateDto.Id, cancellationToken);
            if (existingRole == null)
            {
                throw new NotFoundException($"Not found with id: {updateDto.Id}", nameof(existingRole));
            }

            var newRole = _mapper.Map(updateDto, existingRole);
            await _repository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<RoleReadDto>(newRole);
        }

        public async Task<RoleDetailedReadDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var role = await _repository.GetByIdAsync(id, cancellationToken);

            if (role == null)
            {
                throw new NotFoundException($"Not found with id: {id}", nameof(role));
            }

            _repository.Delete(role);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoleDetailedReadDto>(role);
        }
    }
}