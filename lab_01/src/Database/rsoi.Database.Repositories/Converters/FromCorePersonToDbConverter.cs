using CoreModels = rsoi.Core.Models;
using DbModels = rsoi.Database.Models;

namespace rsoi.Database.Repositories.Converters
{
    public class FromCorePersonToDbConverter
    {
        public static DbModels.Person? Convert(CoreModels.Person? corePerson)
        {
            if (corePerson is null)
                return null;

            return new DbModels.Person(
                corePerson.Id,
                corePerson.Name,
                corePerson.Age,
                corePerson.Address,
                corePerson.Work);
        }
        
        public static CoreModels.Person? ConvertBack(DbModels.Person? dbPerson)
        {
            if (dbPerson is null)
                return null;

            return new CoreModels.Person(
                dbPerson.Id,
                dbPerson.Name,
                dbPerson.Age,
                dbPerson.Address,
                dbPerson.Work);
        }
    }
}