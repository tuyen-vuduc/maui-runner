using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MauiRunner;

public class IOSSimulatorSource : IDeviceSource
{
    public IList<IDevice> GetDevices()
    {
        var json = CmdUtils.ReadOutput("xcrun", "simctl list -j devices booted");

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var data = JsonSerializer.Deserialize<SimulatorGroupDto>(json, serializeOptions);

        return data.Devices.SelectMany(x => x.Value).Cast<IDevice>().ToList();
    }
}