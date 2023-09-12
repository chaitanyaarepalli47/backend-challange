using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Api.Models;
using Api.Services.Employee;
using Api.Services.Dependents;
using System.Reflection.Metadata.Ecma335;
using System.Net;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IDependentService _dependentService;

    public EmployeesController(IEmployeeService employeeService, IDependentService dependentService){
        _employeeService = employeeService;
        _dependentService = dependentService;
    }
    // A.V.K:We will receive request with details of employee here
    [HttpPut("Create")]
    public void CreateEmployees(){
        Dependent d2_1 = _dependentService.CreateDependent("Spouse", "Morant", new DateTime(1998, 3, 3), Relationship.Spouse, 2);
        Dependent d2_2 = _dependentService.CreateDependent("Child1", "Morant", new DateTime(2020, 6, 23), Relationship.Child, 2);
        Dependent d2_3 = _dependentService.CreateDependent("Child2", "Morant", new DateTime(2021, 5, 18), Relationship.Child, 2);
        Dependent d3_1 = _dependentService.CreateDependent("DP", "Jordan", new DateTime(1974, 1, 2), Relationship.DomesticPartner, 3);
        ICollection<Dependent> l = new List<Dependent>{d2_1,d2_2,d2_3};
        ICollection<Dependent> l1 = new List<Dependent>{d3_1};
         _employeeService.CreateEmployee(1, "LeBron", "James", 75420.99m, new DateTime(1984, 12, 30), new List<Dependent>());
        Employee e2 = _employeeService.CreateEmployee(2, "Ja", "Morant", 92365.22m, new DateTime(1999, 8, 10),l);
        Employee e3 = _employeeService.CreateEmployee(3, "Michael", "Jordan", 143211.12m, new DateTime(1963, 2, 17),l1);
        d2_1.Employee = e2;
        d2_2.Employee = e2;
        d2_3.Employee = e2;
        d3_1.Employee = e3;
    }
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        GetEmployeeDto employee = _employeeService.GetEmployee(id);
        if(employee == null){
            return NotFound();
        }else {
            
        }
        var result = new ApiResponse<GetEmployeeDto>
        {
            Data = employee,
            Success = true
        };
        return  result;
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        List<GetEmployeeDto> employees = _employeeService.GetEmployeetoDto().ToList();
        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get Paycheck of an employee")]
    [HttpGet("getPayCheck")]
    public async Task<ActionResult<ApiResponse<decimal>>> GetPayCheck(int id)
    {
        decimal paycheck = _employeeService.GetPayCheck(id);
        if(paycheck < 0) return NotFound();
        var result = new ApiResponse<decimal>
        {
            Data = paycheck,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Add Dependent")]
    [HttpPost("AddDependent")]
    public async Task<ActionResult<ApiResponse<Boolean>>> AddDependent(AddDependentApiRequest dependent)
    {
        Boolean added = _employeeService.AddDependent(dependent);
        var result = new ApiResponse<Boolean>
        {
            Data = added,
            Success = true
        };

        return result;
    }
}
