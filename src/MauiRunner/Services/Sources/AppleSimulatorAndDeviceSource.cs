using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MauiRunner;

public class AppleSimulatorAndDeviceSource : IDeviceSource
{
    public IList<IDevice> GetDevices()
    {
        var output = CmdUtils.ReadOutput("xcrun", "xctrace list devices");
        var lines = output.Split(Environment.NewLine);

        var allDevices = lines
                .Select(x => AppleDeviceUtils.Parse(x));

        var bootedSimulators = CmdUtils.ReadOutput("xcrun", "simctl list devices booted");

        return allDevices
            .Where(
                x => x != null && 
                (
                    !x.IsSimulator || 
                    bootedSimulators.Contains(x.ID)
                )
            )
            .Cast<IDevice>()
            .ToList();
    }
}