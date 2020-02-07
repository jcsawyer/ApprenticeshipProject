namespace FirstCatering.Models.Employee
{
    public class RegisterRequestModel
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public long CompanyId { get; set; }
        public string PIN { get; set; }
    }
}