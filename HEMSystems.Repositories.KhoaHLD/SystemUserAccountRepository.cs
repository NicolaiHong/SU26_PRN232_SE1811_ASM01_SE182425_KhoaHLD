using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Repositories.KhoaHLD.Base;
using HEMSystems.Repositories.KhoaHLD.DBContext;
using Microsoft.EntityFrameworkCore;

namespace HEMSystems.Repositories.KhoaHLD
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
       public SystemUserAccountRepository() 
        {

        }
        public SystemUserAccountRepository(HackathonManagementSystemContext context) => _context = context;

        public async Task<SystemUserAccount?> GetUserAccount(string userName, string password)
        {
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(
                x => x.Email == userName &&
                    x.Password == password &&
                    x.IsActive
            );
        }
    }
}
