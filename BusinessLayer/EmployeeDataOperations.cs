﻿using EmployeeDeactivation.Data;
using EmployeeDeactivation.Interface;
using EmployeeDeactivation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDeactivation.BusinessLayer
{
    public class EmployeeDataOperations : IEmployeeDataOperations
    {
        private readonly EmployeeDeactivationContext _context;
        public EmployeeDataOperations(EmployeeDeactivationContext context)
        {
            _context = context;
        }
        public List<Teams> RetrieveEmployeeData()
        {
            List<Teams> teamDetails = new List<Teams>();
            var details = _context.Teams.ToList();
            foreach (var item in details)
            {
                teamDetails.Add(new Teams
                {
                    SponsorGID = item.SponsorGID,
                    TeamName = item.TeamName,
                    SponsorFirstName = item.SponsorFirstName,
                    SponsorLastName = item.SponsorLastName,
                    SponsorEmailID = item.SponsorEmailID,
                    Department = item.Department
                });
            }
            return teamDetails;
        }
      public async Task<bool> AddEmployeeData(string firstName, string lastName, string gId, string email, DateTime lastWorkingDate)
        {
            Employee employee = new Employee()
            {
                Firstname = firstName,
                Lastname = lastName,
                GId = gId,
                Email = email,
                Date = lastWorkingDate
            };
            _context.Add(employee);
            var databaseUpdateStatus= await _context.SaveChangesAsync() == 1?true:false;
            return databaseUpdateStatus;
        }

        public Employee RetrieveEmployeeDataBasedOnGid(string gId)
        {
            var details = _context.Employee.ToList();
            foreach (var item in details)
            {
                if (item.GId == gId)
                {
                    return item;
                }
            }
            return new Employee();

        }
    }

}
