using System.Collections.Generic;

namespace CarRentingSystem.Areas.Admin.Service
{
    public interface IAdminService
    {
        IEnumerable<ScheduleModel> Schedule(int records);
    }
}
