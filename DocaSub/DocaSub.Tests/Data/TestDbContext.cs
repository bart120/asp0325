using DocaSub.Data;
using DocaSub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocaSub.Tests.Data
{
    public class TestDbContext : DocaDbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            if (!Subventions.Any())
            {
                var subvention = new Subvention
                {
                    Id = 1,
                    Name = "Test Subvention",
                    Description = "Test Description",
                    Category = 5,
                    Partner = "Region",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(30)
                };
                this.Subventions.Add(subvention);
                this.SaveChanges();
            }
        }
    }
}
