namespace Api.Services.Dependents;

using Api.Dtos.Dependent;
using Api.Models;

public interface IDependentService
{
    public Dependent CreateDependent(String firstName, String lastName, DateTime dob, Relationship r, int empId);

    public GetDependentDto DependentToDto(Dependent dependent);
    public GetDependentDto GetDependent(int id);
    public ICollection<GetDependentDto> GetDependentDtos();

}