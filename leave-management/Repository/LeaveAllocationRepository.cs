﻿using leave_management.Contracts;
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

        public bool CheckAllocation(int leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(q => q.EmployeeId == employeeId &&
                q.LeaveTypeId == leaveTypeId && q.Period == period).Any();
        }

        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public bool Exists(int id)
        {
            return _db.LeaveAllocations.Any(l => l.Id == id);
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _db.LeaveAllocations
                    .Include( q => q.LeaveType)
                    .Include( e => e.Employee)
                    .ToList();
        }

        public LeaveAllocation FindById(int id)
        {
            return _db.LeaveAllocations
                    .Include(q => q.LeaveType)
                    .Include(e => e.Employee)
                    .FirstOrDefault( q => q.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id) {
            var period = DateTime.Now.Year;
            return FindAll().Where(q => q.EmployeeId == id && q.Period == period).ToList();
        }

        public LeaveAllocation GetLeaveAllocationByEmployeeAndType(string id, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            return FindAll().FirstOrDefault(q => q.EmployeeId == id &&
                q.Period == period && q.LeaveTypeId == leaveTypeId);
        }
    }
}
