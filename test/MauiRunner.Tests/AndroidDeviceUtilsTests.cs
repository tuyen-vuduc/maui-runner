using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MauiRunner.Tests;

public class AndroidDeviceUtilsTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("emulator-5544")]
    public void Parse_ShouldNotReturnAnAndroidDeviceDto_WhenGivenStringDoesntContainAnyColons(string input)
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
        var id = Guid.NewGuid().ToString();
        var model = "ANDROID-TEST";
        var product = "AOSP";
        var device = "ARM128";
        var transport_id = "1";
        var args = new Dictionary<string, string>
        {
            { nameof(model), model },
            { nameof(product), product },
            { nameof(device), device },
            { nameof(transport_id), transport_id },
        };
        var input = $"{id}          device {string.Join(" ", args.Select(x => $"{x.Key}:{x.Value}"))}";

        // Act
        var actual = AndroidDeviceUtils.Parse(input);

        // Assert
        Assert.NotNull(actual);

        Assert.Equal(actual.ID, id);

        Assert.Equal(actual.Model, model);

        Assert.Equal(actual.Product, product);

        Assert.Equal(actual.Device, device);
    }
}
