using MediatR;
using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Domain.Interfaces.Repositories;

namespace EventsWebApplication.Application.UseCases.Admins.RoleCases.Queries.GetAll
{
    public class GetAllRolesHandler(
        IRoleRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleReadDto>>
    {
        public async Task<IEnumerable<RoleReadDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<RoleReadDto>>(roles);
        }
    }
}