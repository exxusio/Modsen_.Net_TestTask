// using AutoMapper;
// using EventsWebApplication.Application.DTOs.EventRegistrations;
// using EventsWebApplication.Application.Services.Interfaces;
// using EventsWebApplication.Domain.Interfaces.Repositories;
// using EventsWebApplication.Domain.Interfaces;
// using EventsWebApplication.Domain.Exceptions;
// using EventsWebApplication.Domain.Entities;

// namespace EventsWebApplication.Application.Services.Implementations
// {
//     public class EventRegistrationService : IEventRegistrationService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly IMapper _mapper;

//         public EventRegistrationService(IUnitOfWork unitOfWork, IMapper mapper)
//         {
//             _unitOfWork = unitOfWork;
//             _mapper = mapper;
//         }

//         public async Task<IEnumerable<EventRegistrationReadDto>> GetAllAsync(CancellationToken cancellationToken = default)
//         {
//             var repository = _unitOfWork.GetRepository<EventRegistration>();

//             var registrations = await repository.GetAllAsync(cancellationToken);
//             return _mapper.Map<IEnumerable<EventRegistrationReadDto>>(registrations);
//         }

//         public async Task<EventRegistrationReadDto> RegisterUserAsync(EventRegistrationCreateDto createDto, CancellationToken cancellationToken = default)
//         {
//             if (createDto == null)
//             {
//                 throw new ArgumentNullException(nameof(createDto));
//             }

//             var eventRegistrationRepository = _unitOfWork.GetRepository<EventRegistration>();

//             var existingRegistration = eventRegistrationRepository.GetByIdAsync

//             if (existingRegistration != null)
//             {
//                 throw new DuplicateRegistrationException("User is already registered for this event.");
//             }

//             var eventRepository = _unitOfWork.GetRepository<Event>();

//             var _event = await eventRepository.GetByIdAsync(createDto.EventId, cancellationToken);
//             if (_event == null)
//             {
//                 throw new NotFoundException($"Not found with id: {createDto.EventId}", nameof(_event));
//             }

//             var userRepository = _unitOfWork.GetRepository<User>();

//             var user = await userRepository.GetByIdAsync(createDto.ParticipantId, cancellationToken);
//             if (user == null)
//             {
//                 throw new NotFoundException($"Not found with id: {createDto.ParticipantId}", nameof(user));
//             }

//             var registration = _mapper.Map<EventRegistration>(createDto);

//             await eventRegistrationRepository.AddAsync(registration);
//             await _unitOfWork.SaveChangesAsync();
//         }
//         public async Task<EventRegistrationReadDto> CancelRegistrationAsync(EventRegistrationCancelDto cancelDto, CancellationToken cancellationToken = default)
//         {

//         }
//         public async Task<IEnumerable<EventRegistrationReadDto>> GetUserRegistrationsAsync(Guid userId, CancellationToken cancellationToken = default)
//         {

//         }
//         public async Task<EventRegistrationDetailedReadDto> GetRegistrationInfoAsync(EventRegistrationInfoDto infoDto, CancellationToken cancellationToken = default)
//         {

//         }
//         public async Task NotifyParticipantsAsync(Guid eventId, string message, CancellationToken cancellationToken = default)
//         {

//         }
//     }
// }