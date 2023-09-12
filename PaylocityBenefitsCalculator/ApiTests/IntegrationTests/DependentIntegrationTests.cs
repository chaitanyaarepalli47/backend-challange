using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class DependentIntegrationTests : IntegrationTest
{
    [Fact]
    //task: make test pass
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        var response = await HttpClient.GetAsync("/api/v1/dependents");
        var dependents = new List<GetDependentDto>
        {
            new GetDependentDto(1,"Spouse", "Morant", new DateTime(1998, 3, 3), Relationship.Spouse),
            // {
            //     Id = 1,
            //     FirstName = "Spouse",
            //     LastName = "Morant",
            //     Relationship = Relationship.Spouse,
            //     DateOfBirth = new DateTime(1998, 3, 3)
            // },
            new GetDependentDto(2,"Child1", "Morant", new DateTime(2020, 6, 23), Relationship.Child),
            // {
            //     Id = 2,
            //     FirstName = "Child1",
            //     LastName = "Morant",
            //     Relationship = Relationship.Child,
            //     DateOfBirth = new DateTime(2020, 6, 23)
            // },
            new GetDependentDto(3,"Child2", "Morant", new DateTime(2021, 5, 18), Relationship.Child),
            // {
            //     Id = 3,
            //     FirstName = "Child2",
            //     LastName = "Morant",
            //     Relationship = Relationship.Child,
            //     DateOfBirth = new DateTime(2021, 5, 18)
            // },
            new GetDependentDto(4,"DP", "Jordan", new DateTime(1974, 1, 2), Relationship.DomesticPartner)
            // {
            //     Id = 4,
            //     FirstName = "DP",
            //     LastName = "Jordan",
            //     Relationship = Relationship.DomesticPartner,
            //     DateOfBirth = new DateTime(1974, 1, 2)
            // }
        };
        await response.ShouldReturn(HttpStatusCode.OK, dependents);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        var response = await HttpClient.GetAsync("/api/v1/dependents/1");
        var dependent = new GetDependentDto(1,"Spouse","Morant", new DateTime(1998, 3, 3), Relationship.Spouse);
        // {
        //     Id = 1,
        //     FirstName = "Spouse",
        //     LastName = "Morant",
        //     Relationship = Relationship.Spouse,
        //     DateOfBirth = new DateTime(1998, 3, 3)
        // };
        await response.ShouldReturn(HttpStatusCode.OK, dependent);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/dependents/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

