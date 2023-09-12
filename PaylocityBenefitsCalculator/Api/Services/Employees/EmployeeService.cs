namespace Api.Services.Employee;

using Api.Dtos.Employee;
using Api.Dtos.Dependent;
using Api.Services.Dependents;
using Api.Models;
using Microsoft.AspNetCore.Http.Features;

public class EmployeeService : IEmployeeService
{
    private readonly IDependentService _dependentService;
    private static readonly Dictionary<int,Employee> _employees = new ();

        public EmployeeService(IDependentService dependentService){
        _dependentService = dependentService;
    }

    public Employee CreateEmployee(int id, String firstName, String lastName, decimal salary, DateTime dateofBirth, ICollection<Dependent> dependents){
        Employee employee = new Employee(id, firstName, lastName, salary, dateofBirth, dependents);
        _employees.Add(id,employee);
        return employee;
    }
    public ICollection<GetEmployeeDto> GetEmployeetoDto(){
        ICollection<GetEmployeeDto> employeeDtos = new List<GetEmployeeDto>();
        
        foreach(KeyValuePair<int, Employee> employee in _employees)
        {
            ICollection<Dependent> dependents = employee.Value.Dependents;
            ICollection<GetDependentDto> dependentDtos = new List<GetDependentDto>();
            foreach (var item in dependents)
                dependentDtos.Add(_dependentService.DependentToDto(item));
            employeeDtos.Add(new GetEmployeeDto(employee.Value.Id,employee.Value.FirstName,employee.Value.LastName,employee.Value.Salary,employee.Value.DateOfBirth,dependentDtos));
        }
        return employeeDtos;
    }

    public GetEmployeeDto GetEmployee(int id){
        if(!_employees.ContainsKey(id)) return null;
        Employee e = _employees[id];
        ICollection<Dependent> dependents = e.Dependents;
        ICollection<GetDependentDto> dependentDtos = new List<GetDependentDto>();
        foreach (var item in dependents)
            dependentDtos.Add(_dependentService.DependentToDto(item));
        return new GetEmployeeDto(e.Id, e.FirstName,e.LastName,e.Salary,e.DateOfBirth,dependentDtos);
    }

    public decimal GetPayCheck(int id){
        if(!_employees.ContainsKey(id)) return -1;
        decimal payCheck = _employees[id].Salary;
        Boolean additional = (payCheck>80000m)? true:false;
        decimal deductionperMonth = 1000;
        decimal dependentDeduction = 600*_employees[id].Dependents.Count;
        decimal deductionForOlderDependents = 0;
        var today = DateTime.Today;
        foreach(Dependent dependent in _employees[id].Dependents){
            var age = today.Year - dependent.DateOfBirth.Year;
            if(age > 50){
                deductionForOlderDependents+= 200;
            }
        }
        decimal totalDeductionperMonth = deductionperMonth+dependentDeduction+deductionForOlderDependents;
        decimal YearlyDeduction = payCheck - (totalDeductionperMonth*12);
        if(additional) YearlyDeduction = 0.98m*YearlyDeduction;
        return (YearlyDeduction)/26m;
    }

    public Boolean AddDependent(AddDependentApiRequest dependent){
        if(dependent.Relationship == Relationship.Spouse || dependent.Relationship == Relationship.DomesticPartner){

            foreach(Dependent dependent1 in _employees[dependent.EmployeeId].Dependents){
                if(dependent1.Relationship == dependent.Relationship) return false;
            }
        }
        Dependent d = _dependentService.CreateDependent(dependent.FirstName,dependent.LastName,dependent.DateOfBirth,dependent.Relationship,dependent.EmployeeId);
        _employees[dependent.EmployeeId].Dependents.Add(d);
        return true;
    }
}