using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDto>
        {
            new GetEmployeeDto(1, "LeBron", "James", 75420.99m, new DateTime(1984, 12, 30), new List<GetDependentDto>()),
            // {
            //     Id = 1,
            //     FirstName = "LeBron",
            //     LastName = "James",
            //     Salary = 75420.99m,
            //     DateOfBirth = new DateTime(1984, 12, 30)
            // },
            new GetEmployeeDto(2, "Ja", "Morant", 92365.22m, new DateTime(1999, 8, 10),new List<GetDependentDto>
            {
                new GetDependentDto(1,"Spouse", "Morant", new DateTime(1998, 3, 3),Relationship.Spouse),
                new GetDependentDto(2,"Child1", "Morant", new DateTime(2020, 6, 23),Relationship.Child),
                new GetDependentDto(3,"Child2", "Morant",new DateTime(2021, 5, 18),Relationship.Child)
            }),
            // {
            //     Id = 2,
            //     FirstName = "Ja",
            //     LastName = "Morant",
            //     Salary = 92365.22m,
            //     DateOfBirth = new DateTime(1999, 8, 10),
            //     Dependents = new List<GetDependentDto>
            //     {
            //         new()
            //         {
            //             Id = 1,
            //             FirstName = "Spouse",
            //             LastName = "Morant",
            //             Relationship = Relationship.Spouse,
            //             DateOfBirth = new DateTime(1998, 3, 3)
            //         },
            //         new()
            //         {
            //            Id = 2,
            //             FirstName = "Child1",
            //             LastName = "Morant",
            //             Relationship = Relationship.Child,
            //             DateOfBirth = new DateTime(2020, 6, 23)
            //         },
            //         new()
            //         {
            //             Id = 3,
            //             FirstName = "Child2",
            //             LastName = "Morant",
            //             Relationship = Relationship.Child,
            //             DateOfBirth = new DateTime(2021, 5, 18)
            //         }
            //     }
            // },
            new GetEmployeeDto(3, "Michael","Jordan",143211.12m,new DateTime(1963, 2, 17),new List<GetDependentDto>{
                new GetDependentDto(4,"DP", "Jordan", new DateTime(1974, 1, 2),Relationship.DomesticPartner)
            })
            // {
            //     Id = 3,
            //     FirstName = "Michael",
            //     LastName = "Jordan",
            //     Salary = 143211.12m,
            //     DateOfBirth = new DateTime(1963, 2, 17),
            //     Dependents = new List<GetDependentDto>
            //     {
            //         new()
            //         {
            //             Id = 4,
            //             FirstName = "DP",
            //             LastName = "Jordan",
            //             Relationship = Relationship.DomesticPartner,
            //             DateOfBirth = new DateTime(1974, 1, 2)
            //         }
            //     }
            // }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }
 
    [Fact]
    //task: make test pass
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto(1, "LeBron", "James", 75420.99m, new DateTime(1984, 12, 30),new List<GetDependentDto>());
        // {
        //     Id = 1,
        //     FirstName = "LeBron",
        //     LastName = "James",
        //     Salary = 75420.99m,
        //     DateOfBirth = new DateTime(1984, 12, 30)
        // };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
    
    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenAskedForMonthlyPaycheck_ShouldReturnCorrectPayCheck(){
        var response = await HttpClient.GetAsync($"https://localhost:7124/api/v1/Employees/getPayCheck?id=2");
        await response.ShouldReturn(HttpStatusCode.OK ,2214.9967538461538);
    }
    [Fact]
    public async Task WhenAskedForNonExistentPaycheck_ShouldReturn404(){
        var response = await HttpClient.GetAsync($"https://localhost:7124/api/v1/Employees/getPayCheck?id=7");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

