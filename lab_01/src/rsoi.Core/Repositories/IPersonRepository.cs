using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using rsoi.Core.Models;

namespace rsoi.Core.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPersonsAsync();

        Task<Person?> FindPersonAsync(int id);

        Task AddPersonAsync(int? id,
            string? name,
            int? age,
            string? address,
            string? work);
        
        Task<Person?> UpdatePersonAsync(int id,
            string? name,
            int? age,
            string? address,
            string? work);

        Task<Person?> DeletePersonAsync(int id);
    }
}