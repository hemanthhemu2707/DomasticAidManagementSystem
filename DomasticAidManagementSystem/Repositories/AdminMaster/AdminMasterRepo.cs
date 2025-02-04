using IIITS.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomasticAidManagementSystem
{
    public class AdminMasterRepo : IAdminMasterRepo
    {
        private readonly LMSMasterServiceDBContext _dbContext;

        public AdminMasterRepo(LMSMasterServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<DashBoard> GetDashboardCountAdminDash()
        {
            throw new NotImplementedException();
        }

        //public async Task<DashBoard> GetDashboardCountAdminDash()
        //{
        //    DashBoard dashBoard=new DashBoard();
        //    dashBoard.ApprovalCount = 0;

        //    var DataesultDash = await _dbContext.FamilyConfig.ToListAsync();

        //    return dashBoard;
        //}
    }
}
