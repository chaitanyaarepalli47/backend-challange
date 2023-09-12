namespace Api.Models;

public class Dependent
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public Dependent(){}
    public Dependent(
        int id,
        String firstName,
        String lastName,
        DateTime dob,
        Relationship relationship,
        int empId
    ){
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        Relationship = relationship;
        EmployeeId = empId;
    }
}
