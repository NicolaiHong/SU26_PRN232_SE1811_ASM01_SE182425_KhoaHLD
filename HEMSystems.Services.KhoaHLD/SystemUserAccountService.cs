using HEMSystems.Repositories.KhoaHLD;
using HEMSystems.Services.KhoaHLD.DTOs;

namespace HEMSystems.Services.KhoaHLD;

public class SystemUserAccountService(SystemUserAccountRepository userRepo) : ISystemUserAccountService
{
    private readonly SystemUserAccountRepository _userRepo = userRepo;

    public async Task<GetUserAccountResponse?> GetUserAccount(GetUserAccountRequest request)
    {
        try
        {
            var user = await _userRepo.GetUserAccount(request.UserName, request.Password);

            if (user == null)
                return null;

            if (!user.IsActive)
                return null;

            return new GetUserAccountResponse
            {
                UserAccountId = user.UserAccountId,
                UserName = user.UserName ?? string.Empty,
                Password = user.Password ?? string.Empty,
                FullName = user.FullName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Phone = user.Phone ?? string.Empty,
                EmployeeCode = user.EmployeeCode ?? string.Empty,
                RoleId = user.RoleId,
                RequestCode = user.RequestCode ?? string.Empty,
                ApplicationCode = user.ApplicationCode ?? string.Empty,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                CreatedBy = user.CreatedBy ?? string.Empty,
                ModifiedDate = user.ModifiedDate,
                ModifiedBy = user.ModifiedBy ?? string.Empty
            };
        }
        catch (Exception)
        {
        }

        return null;
    }
}
