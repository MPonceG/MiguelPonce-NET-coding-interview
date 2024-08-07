using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Moq;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using SecureFlight.Infrastructure.Repositories;
using Xunit;

namespace SecureFlight.Test
{
    public class AirportTests
    {
        public static IEnumerable<object[]> GetAirportToUpdate()
        {
            yield return new object[] { new Airport() { City = "Lima",
                Country = "Peru",
                Code = "AAQ",
                Name = "Anapa Vityazevo"} };
        }


        [Theory]
        [MemberData(nameof(GetAirportToUpdate))]
        public async Task Update_Succeeds_GetAirportToUpdate(Airport airport)
        {
            var testingContext = new SecureFlightDatabaseTestContext();
            testingContext.CreateDatabase();
            var repository = new BaseRepository<Airport>(testingContext);

            var originEntity = await repository.FilterAsync(a => a.Code == airport.Code);
            

            repository.Update(airport);
            //await repository.SaveChangesAsync();
            var newEntity = await repository.FilterAsync(a => a.Code == airport.Code);

            Assert.NotEqual<Airport>(newEntity.FirstOrDefault(), originEntity.FirstOrDefault());
            
            //Assert
            
            testingContext.DisposeDatabase();
        }
    }
}
