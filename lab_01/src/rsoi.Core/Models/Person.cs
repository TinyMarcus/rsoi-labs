using System;

namespace rsoi.Core.Models
{
    /// <summary>
    /// Person entity.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Primary key of entity.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Name of person.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Age of person.
        /// </summary>
        public int Age { get; }
        
        /// <summary>
        /// Address of person.
        /// </summary>
        public string Address { get; }
        
        /// <summary>
        /// Workplace of person. 
        /// </summary>
        public string Work { get; }

        /// <summary>
        /// Creates person entity.
        /// </summary>
        /// <param name="id">Primary key of entity.</param>
        /// <param name="name">Name of person.</param>
        /// <param name="age">Age of person.</param>
        /// <param name="address">Address of person.</param>
        /// <param name="work">Workplace of person.</param>
        public Person(int id,
            string name,
            int age,
            string address,
            string work)
        {
            Id = id;
            Name = name;
            Age = age;
            Address = address;
            Work = work;
        }
    }
}