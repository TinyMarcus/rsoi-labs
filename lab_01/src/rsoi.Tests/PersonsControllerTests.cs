using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rsoi.Controllers;
using rsoi.Core.Repositories;
using rsoi.Database.Repositories.Converters;
using rsoi.Dto.Http;
using Xunit;
using DbModels = rsoi.Database.Models;
using CoreModels = rsoi.Core.Models;

namespace rsoi.Tests
{
    public class PersonsControllerTests
    {
        [Fact]
        public async void GetPersonsOkTest()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            var personsCore = new List<CoreModels.Person>();
            var personsDb = new List<DbModels.Person>
            {
                new DbModels.Person { Id = 0, Name = "Idris", Age = 22, Address = "Moscow", Work = "Programmer" },
                new DbModels.Person { Id = 1, Name = "Olga", Age = 25, Address = "Moscow", Work = "Teacher" },
                new DbModels.Person { Id = 2, Name = "Stacie", Age = 21, Address = "Moscow", Work = "Manager" }
            };

            foreach (var person in personsDb)
                personsCore.Add(FromCorePersonToDbConverter.ConvertBack(person));

            personRepository.Setup(p => p.GetPersonsAsync())
                .ReturnsAsync(personsCore)
                .Verifiable();
            var controller = new PersonsController(personRepository.Object);

            // Act
            var res = await controller.GetPersons();
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(res);
            var response = Assert.IsAssignableFrom<List<PersonDto>>(okResult.Value);
            foreach (var person in Enumerable.Zip(personsCore, response))
                AssertPersonEqual(person.First, person.Second);
        }
        
        [Fact]
        public async void FindPersonOkTest()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            var personCore = new CoreModels.Person(0, "Idris", 22, "Moscow", "Programmer");

            personRepository.Setup(p => p.FindPersonAsync(0))
                .ReturnsAsync(personCore)
                .Verifiable();
            var controller = new PersonsController(personRepository.Object);

            // Act
            var res = await controller.FindPerson(0);
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(res);
            var response = Assert.IsAssignableFrom<PersonDto>(okResult.Value);
            AssertPersonEqual(personCore, response);
        }
        
        [Fact]
        public async void FindPersonNotFoundTest()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            var personCore = new CoreModels.Person(0, "Idris", 22, "Moscow", "Programmer");

            personRepository.Setup(p => p.FindPersonAsync(0))
                .ReturnsAsync(personCore)
                .Verifiable();
            var controller = new PersonsController(personRepository.Object);

            // Act
            var res = await controller.FindPerson(1);
            
            // Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(res);
            Assert.IsAssignableFrom<string>(okResult.Value);
        }
        
        [Fact]
        public async void UpdatePersonNotFoundTest()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            var personDto = new PersonDto(0, "Idris", 22, "Moscow", "Programmer");

            personRepository.Setup(p => p.UpdatePersonAsync(1, personDto.Name,
                    personDto.Age, personDto.Address, personDto.Work))
                .Verifiable();
            var controller = new PersonsController(personRepository.Object);

            // Act
            var res = await controller.UpdatePerson(1, personDto);
            
            // Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(res);
        }
        
        private void AssertPersonEqual(CoreModels.Person expected, PersonDto actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Age, actual.Age);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Work, actual.Work);
        }
        
        private void AssertPersonEqual(PersonDto expected, PersonDto actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Age, actual.Age);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Work, actual.Work);
        }
    }
}