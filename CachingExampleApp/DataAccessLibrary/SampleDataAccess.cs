using System;
using Bogus;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary;

public class SampleDataAccess
{
				private readonly IMemoryCache _memory_cache;
				public SampleDataAccess(IMemoryCache memory_cache) => _memory_cache = memory_cache;

				public async Task<List<EmployeeModel>> GetEmployeesAsync() {
								List<EmployeeModel> output = new EmployeeFaker().Generate(Random.Shared.Next(500, 10001));

								await Task.Delay(3000);

								return output;
				}

				public async Task<List<EmployeeModel>> GetEmployeesCache() {
								List<EmployeeModel>? output;

								output = _memory_cache.Get<List<EmployeeModel>>("employees");

								if ( output is null ) {
												output = await GetEmployeesAsync();
												_memory_cache.Set("employees", output, TimeSpan.FromMinutes(1));
								}

								return output;
				}
}

public class EmployeeFaker : Faker<EmployeeModel>
{
				public EmployeeFaker() {
								Random rnd = new Random();

								UseSeed(Random.Shared.Next(1, 1000001))
								.RuleFor(x => x.FirstName, ef => ef.Person.FirstName)
								.RuleFor(x => x.LastName, ef => ef.Person.LastName)
								.RuleFor(x => x.Age, _ => Random.Shared.Next(20, 76));
				}
}

