namespace FPPPractice.API.Repositories.Customer.Models.Dtos
{
    public class InsertCustomerDto
    {
        public InsertCustomerDto(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
