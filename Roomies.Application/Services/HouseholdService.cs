using System.Net;
using System.Threading.Tasks;
using Roomies.Application.Constants;
using Roomies.Application.Enums;
using Roomies.Application.Interfaces;
using Roomies.Application.Interfaces.Repositories;
using Roomies.Application.Interfaces.Services;
using Roomies.Application.Models;
using Roomies.Domain.Entities;
using Roomies.Domain.Enums;

namespace Roomies.Application.Services
{
    public class HouseholdService
    {
        private readonly IHouseholdRepository _householdRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public HouseholdService(
            IHouseholdRepository householdRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _householdRepository = householdRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<Guid, Enum>> CreateHouseholdAsync(
            CreateHouseholdRequestDto request)
        {
            var validationError = await ValidateHouseholdAsync(request);
            if (validationError != null)
            {
                return ServiceResponse<Guid, Enum>.Failure(
                    validationError, HttpStatusCode.BadRequest);
            }

            var household = new Household
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                Members = new List<HouseholdMember>()
                {
                    new HouseholdMember
                    {
                        Id = Guid.NewGuid(),
                        UserId = _currentUserService.UserId,
                        MemberTypeId = MemberType.Admin,
                        JoinedAt = DateTime.UtcNow
                    }
                }
            };

            await _householdRepository.AddHouseholdAsync(household);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResponse<Guid, Enum>.Success(household.Id);
        }

        private async Task<Enum> ValidateHouseholdAsync(CreateHouseholdRequestDto request)
        {
            if (request == null)
            {
                return ErrorCodes.MissingHouseholdData;
            }

            if (string.IsNullOrWhiteSpace(request.Name)
                || request.Name.Length > CommonConstants.MaxNameLength)
            {
                return ErrorCodes.InvalidNameFormat;
            }

            if (!string.IsNullOrWhiteSpace(request.Description)
                && request.Description.Length > CommonConstants.MaxDescriptionLength)
            {
                return ErrorCodes.InvalidDescriptionFormat;
            }

            var hasHouseholdWithTheSameName = await _householdRepository
                .HasSameNameUserHouseholdAsync(request.Name, _currentUserService.UserId);
            if (hasHouseholdWithTheSameName)
            {
                return ErrorCodes.HouseholdAlreadyExists;
            }

            return null;
        }
    }
}
