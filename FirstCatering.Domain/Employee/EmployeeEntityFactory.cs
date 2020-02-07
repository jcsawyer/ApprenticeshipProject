using FirstCatering.Models.Employee;

namespace FirstCatering.Domain
{
    /// <summary>
    /// Employe entity factory
    /// </summary>
    public static class EmployeeEntityFactory
    {
        /// <summary>
        /// Creates a <see cref="EmployeeEntity"/> from the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns><see cref="EmployeeEntity"/> employee</returns>
        public static EmployeeEntity Create(long id)
            => new EmployeeEntity(id, default, default, default, default, default, default, default);

        /// <summary>
        /// Creates a <see cref="EmployeeEntity"/> from the specified <paramref name="registerRequestModel"/>
        /// </summary>
        /// <param name="registerRequestModel"><paramref name="registerRequestModel"/> employee registration request</param>
        /// <returns><see cref="EmployeeEntity"/> employee</returns>
        public static EmployeeEntity Create(RegisterRequestModel registerRequestModel)
            => new EmployeeEntity(
                default,
                registerRequestModel.EmployeeId,
                registerRequestModel.Name,
                registerRequestModel.Email,
                registerRequestModel.MobileNumber,
                registerRequestModel.CompanyId,
                registerRequestModel.PIN,
                default);
    }
}