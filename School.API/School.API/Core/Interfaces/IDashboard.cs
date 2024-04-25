using School.API.Core.Models.DashoboardRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IDashboard
    {
        Task<DashboardResponse> getInformation(int yearId);
    }
}
