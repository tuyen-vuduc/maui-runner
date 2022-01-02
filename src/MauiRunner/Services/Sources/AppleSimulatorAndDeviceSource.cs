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

        return lines
                .Select(x => (IDevice)AppleDeviceUtils.Parse(x))
                .Where(x => x != null)
                .ToList();
    }
}