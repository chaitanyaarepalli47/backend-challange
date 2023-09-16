namespace Api.Models;

//A.V.K created a constructor
public class Employee
{
    public int Id { get; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
    public Employee(int id)
    {
        Id = id;
    }
    public Employee(
        int id,
        String firstName,
        String lastName,
        decimal salary,
        DateTime dob,
        ICollection<Dependent> dependents)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Salary = salary;
        DateOfBirth = dob;
        Dependents = dependents;
    }
}
