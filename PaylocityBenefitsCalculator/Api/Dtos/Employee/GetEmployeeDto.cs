using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public class GetEmployeeDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();

    public GetEmployeeDto(
        int id,
        String firstName,
        String lastName,
        decimal salary,
        DateTime dob,
        ICollection<GetDependentDto> dependents
    ){
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Salary = salary;
        DateOfBirth = dob;
        Dependents = dependents;
    }
}
