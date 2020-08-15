using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckAllocation(int leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;
            return (await FindAll()).Where(q => q.EmployeeId == employeeId &&
                q.LeaveTypeId == leaveTypeId && q.Period == period).Any();
        }

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.LeaveAllocations.AnyAsync(l => l.Id == id);
        }

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            return await _db.LeaveAllocations
                    .Include( q => q.LeaveType)
                    .Include( e => e.Employee)
                    .ToListAsync();
        }

        public async Task<LeaveAllocation> FindById(int id)
        {
            return await _db.LeaveAllocations
                    .Include(q => q.LeaveType)
                    .Include(e => e.Employee)
                    .FirstOrDefaultAsync( q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string id) {
            var period = DateTime.Now.Year;
            return (await FindAll()).Where(q => q.EmployeeId == id && q.Period == period).ToList();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationByEmployeeAndType(string id, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            return (await FindAll()).FirstOrDefault(q => q.EmployeeId == id &&
                q.Period == period && q.LeaveTypeId == leaveTypeId);
        }
    }
}
