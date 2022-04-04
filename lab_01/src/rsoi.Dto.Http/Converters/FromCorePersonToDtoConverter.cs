using CoreModels = rsoi.Core.Models;

namespace rsoi.Dto.Http.Converters
{
    public class FromCorePersonToDtoConverter
    {
        public static PersonDto Convert(CoreModels.Person corePerson)
        {
            return new PersonDto()
            {
                Id = corePerson.Id,
                Name = corePerson.Name,
                Age = corePerson.Age,
                Address = corePerson.Address,
                Work = corePerson.Work
            };
        }
    }
}