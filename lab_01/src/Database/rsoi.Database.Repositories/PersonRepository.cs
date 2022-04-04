using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rsoi.Core;
using rsoi.Core.Models;
using rsoi.Core.Repositories;
using rsoi.Database.NpgsqlContext;
using rsoi.Database.Repositories.Converters;
using CoreModels = rsoi.Core.Models;
using DbModels = rsoi.Database.Models;

namespace rsoi.Database.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly NpgSqlContext _dbContext;
        
        public PersonRepository(NpgSqlContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<Person>> GetPersonsAsync()
        {
            var persons = await _dbContext.Persons!
                .AsNoTracking()
                .Select(p => FromCorePersonToDbConverter.ConvertBack(p)!)
                .ToListAsync();

            return persons;
        }

        public async Task<Person?> FindPersonAsync(int id)
        {
            var dbPerson = await _dbContext.Persons!
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return FromCorePersonToDbConverter.ConvertBack(dbPerson);
        }

        public async Task AddPersonAsync(int? id, string? name, int? age, string? address, string? work)
        {
            if (id == null || id < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(id)}.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Parameter {nameof(name)} must be not null or whitespace.");
            
            if (age == null || age < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(age)}.");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException($"Parameter {nameof(address)} must be not null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(work))
                throw new ArgumentException($"Parameter {nameof(work)} must be not null or whitespace.");

            var person = new DbModels.Person((int) id,
                name!,
                (int) age,
                address!,
                work!);

            await _dbContext.Persons!.AddAsync(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Person?> UpdatePersonAsync(int id, string? name, int? age, string? address, string? work)
        {
            if (id < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(id)}.");

            var person = await _dbContext.Persons!
                .FirstOrDefaultAsync(p => p.Id == id);

            if (string.IsNullOrWhiteSpace(name))
                name = person.Name;
            
            if (age == null || age < 0)
                age = person.Age;
            
            if (string.IsNullOrWhiteSpace(address))
                address = person.Address;
            
            if (string.IsNullOrWhiteSpace(work))
                work = person.Work;
            
            if (person != null)
            {
                person.Name = name;
                person.Age = (int) age;
                person.Address = address;
                person.Work = work;
                
                await _dbContext.SaveChangesAsync();
                return FromCorePersonToDbConverter.ConvertBack(person);
            }
            else
                throw new ArgumentException($"Person with Id {id} does not exist.");
        }

        public async Task<Person?> DeletePersonAsync(int id)
        {
            var person = await _dbContext.Persons!
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person != null)
            {
                _dbContext.Remove(person);
                
                await _dbContext.SaveChangesAsync();
                return FromCorePersonToDbConverter.ConvertBack(person);
            }
            else
                throw new ArgumentException($"Person with Id {id} does not exist.");
        }
    }
}