using DocaSub.Controllers.API;
using DocaSub.Data;
using DocaSub.Models;
using DocaSub.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocaSub.Tests
{
    public class SubventionsTest
    {

        private readonly TestDbContext dbContext;

        public SubventionsTest()
        {
            dbContext = getInMemoryDbContext();
        }
        private TestDbContext getInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: $"DocaSubTest_{Guid.NewGuid().ToString()}")
                .Options;
            return new TestDbContext(options);
        }


        [Fact]
        public async Task GetSubvention_SubventionExiste_ReturnOk()
        {
           
            /*var subvention = new Subvention
            {
                Id = 1,
                Name = "Test Subvention",
                Description = "Test Description",
                Category = 5,
                Partner = "Region",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(30)
            };
            dbContext.Subventions.Add(subvention);
            dbContext.SaveChanges();*/


            var contollerAPI = new SubventionsController(dbContext, null);
            var result = await contollerAPI.GetSubvention(1);

            
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            
            var subvetionResult = Assert.IsType<Subvention>(okResult.Value);
            Assert.Equal("Test Subvention", subvetionResult.Name);

        }

        [Fact]
        public async Task GetSubvention_SubventionExiste_Return404()
        {
           
            var contollerAPI = new SubventionsController(dbContext);
            var result = await contollerAPI.GetSubvention(856);

            //Assert
            var okResult = Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}