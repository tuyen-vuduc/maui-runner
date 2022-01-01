using System.Diagnostics;

namespace MauiRunner;

public class AndroidDeviceSource : IDeviceSource
{
    public IList<IDevice> GetDevices()
    {
        var output = CmdUtils.ReadOutput("adb", "devices -l");
        var lines = output.Split(Environment.NewLine);

        return lines
                .Select(x => (IDevice)AndroidDeviceUtils.Parse(x))
                .Where(x => x != null)
                .ToList();
    }
}