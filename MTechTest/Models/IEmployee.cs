
namespace MTechTest.Models
{
    public interface IEmployee
    {
        DateTime BornDate { get; set; }
        int ID { get; set; }
        string LastName { get; set; }
        string Name { get; set; }
        string RFC { get; set; }
        EmployeeStatus Status { get; set; }
    }
}