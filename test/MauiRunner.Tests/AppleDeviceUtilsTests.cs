using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MauiRunner.Tests;

public class AppleDeviceUtilsTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("device")]
    public void Parse_ShouldNotReturnAnAndroidDeviceDto_WhenGivenStringDoesntFollowThePattern(string input)
    {
        // Arrange

        // Act
        var actual = AndroidDeviceUtils.Parse(input);

        // Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Parse_ShouldNotReturnAnAndroidDeviceDto_WhenAValidStringIsGiven()
    {
        // Arrange
        var input = $"iPad (9th generation) Simulator (15.2) (32882EC2-6761-4785-91B0-2EB8ADBCDA2A)";

        // Act
        var actual = AppleDeviceUtils.Parse(input);

        // Assert
        Assert.NotNull(actual);

        Assert.Equal(actual.ID, "32882EC2-6761-4785-91B0-2EB8ADBCDA2A");

        Assert.Equal(actual.Name, "iPad (9th generation) Simulator (15.2)");

        Assert.Equal(actual.OS, "ios");
    }
}
