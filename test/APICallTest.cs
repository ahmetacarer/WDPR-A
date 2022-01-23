using Xunit;
using Moq;
using System;
using WDPR_A.ViewModels;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Controllers;
using Microsoft.AspNetCore.Identity;
using src.Controllers;
using System.Threading.Tasks;

namespace test;

public class APICallTest {

    [Fact]
    public async Task GetClientFile_EmptyDateWithNegativeBSN_Error () {
        string birthDate = "10-06-2010";
        int BSN = -1;
        var test = await APIcall.GetClientFile(birthDate, BSN);
        Assert.Equal("Error", test);
    }    
    
}