using Xunit;
using Moq;
using System;
using WDPR_A.ViewModels;

namespace test;

public class GenerateTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void RandomString_nonNegativeNumber_returnsSameLength(int length)
    {
        var sut = new Generate(new Random());
        
        var generatedString = sut.RandomString(length);

        Assert.Equal(length, generatedString.Length);
    }
    [Theory]
    [InlineData(-1)]
    [InlineData(-25)]
    [InlineData(-50)]
    public void RandomString_negativeNumber_throwsException(int negativeNumber)
    {
        var sut = new Generate(new Random());

        var exception = Assert.Throws<Exception>(() => sut.RandomString(negativeNumber));

        Assert.Equal("number must be bigger or equal to 0", exception.Message);
    }
}