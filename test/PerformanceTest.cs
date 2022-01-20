using Xunit;
using Moq;
using System;
using WDPR_A.ViewModels;
using WDPR_A.Models;
using WDPR_A.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace test;

public class PerformanceTest
{

    [Fact]
    public void AverageResponseTime_HomepageTest_True()
    {
        var allResponseTimes = new List<(DateTime Start, DateTime End)>();
        for (var i = 0; i < 1000; i++)
        {
            using (var client = new HttpClient())
            {
                var start = DateTime.Now;
                var response = client.GetAsync("https://zmdh-hhs.azurewebsites.net/");
                var end = DateTime.Now;
                allResponseTimes.Add((start, end));
            }
        }
        var expected = 1;
        var actual = (int)allResponseTimes
        .Select(r => (r.End - r.Start).TotalMilliseconds).Average();
        Assert.True(actual <= expected);
    }

    [Fact]
    public void AverageResponseTime_AppointmentTest_True()
    {
        var allResponseTimes = new List<(DateTime Start, DateTime End)>();
        for (var i = 0; i < 1000; i++)
        {
            using (var client = new HttpClient())
            {
                var start = DateTime.Now;
                var response = client.GetAsync("https://localhost:7181/Appointment");
                var end = DateTime.Now;
                allResponseTimes.Add((start, end));
            }
        }
        var expected = 1;
        var actual = (int)allResponseTimes
        .Select(r => (r.End - r.Start).TotalMilliseconds).Average();
        Assert.True(actual <= expected);
    }


}