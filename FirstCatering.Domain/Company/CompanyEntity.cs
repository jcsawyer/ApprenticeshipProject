using System.Collections.Generic;

namespace FirstCatering.Domain
{
    /// <summary>
    /// A company entity
    /// </summary>
    public class CompanyEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of company employees
        /// </summary>
        public virtual ICollection<EmployeeEntity> Employees { get; set; }

        /// <summary>
        /// Initialises a <see cref="CompanyEntity"/> with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Company id</param>
        public CompanyEntity(long id)
            => Id = id;

        /// <summary>
        /// Initialises a <see cref="CompanyEntity"/> with the specified <paramref name="id"/>
        /// and <paramref name="name"/>
        /// </summary>
        /// <param name="id">Company id</param>
        /// <param name="name">Company name</param>
        public CompanyEntity(
            long id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}