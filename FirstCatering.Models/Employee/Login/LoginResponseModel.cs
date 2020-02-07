namespace FirstCatering.Models.Employee
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public LoginResponseModel(
            string token,
            string name,
            decimal balance)
        {
            Token = token;
            Name = name;
            Balance = balance;
        }
    }
}