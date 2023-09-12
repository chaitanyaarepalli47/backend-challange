using Api.Models;

namespace Api.Dtos.Dependent;

public class GetDependentDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }

    public GetDependentDto(
        int id,
        String firstName,
        String lastName,
        DateTime dob,
        Relationship r
    ){
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        Relationship = r;
    }
}
