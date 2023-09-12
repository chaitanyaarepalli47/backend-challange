namespace Api.Services.Dependents;

using Api.Dtos.Dependent;
using Api.Models;

public class DependentService: IDependentService
{
    private static readonly Dictionary<int, Dependent> _dpendents =  new ();
    private static int depId = 1;
    
    public Dependent CreateDependent(String firstName, String lastName, DateTime dob, Relationship r, int empId){
        Dependent dependent = new Dependent(depId, firstName, lastName,dob, r, empId);
        _dpendents.Add(depId, dependent);
        depId +=1;
        return dependent;
    }

    public ICollection<GetDependentDto> GetDependentDtos(){
        ICollection<GetDependentDto> dependentDtos = new List<GetDependentDto>();

        foreach(KeyValuePair<int, Dependent> dependent in _dpendents)
            dependentDtos.Add(DependentToDto(dependent.Value));

        return dependentDtos;
    }

    public GetDependentDto GetDependent(int id){
        if(!_dpendents.ContainsKey(id)) return null;
        Dependent d = _dpendents[id];
        return new GetDependentDto(d.Id, d.FirstName,d.LastName,d.DateOfBirth, d.Relationship);
    }

    public GetDependentDto DependentToDto(Dependent dependent){
        return new GetDependentDto(dependent.Id, dependent.FirstName,dependent.LastName,dependent.DateOfBirth,dependent.Relationship);
    }
}