using GylleneDroppen.Admin.Api.Models;
using GylleneDroppen.Admin.Api.Utilities;

namespace GylleneDroppen.Admin.Api.Services.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<bool>> CreateUserAsync(string email, string password);
}