namespace Api.Services.Employee;

using Api.Models;
using Api.Dtos.Employee;
using Api.Dtos.Dependent;
public interface IEmployeeService
{
    public Employee CreateEmployee(int id, String firstName, String lastName, decimal salary, DateTime dateofBirth, ICollection<Dependent> dependent);

    public GetEmployeeDto GetEmployee(int id);
    public ICollection<GetEmployeeDto> GetEmployeetoDto();
    public decimal GetPayCheck(int id);
    public Boolean AddDependent(AddDependentApiRequest dependent);
}