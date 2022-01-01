using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MauiRunner;

public class DevicesService
{
	public DevicesService()
	{
	}

	public IList<IDevice> GetAvailableDevices()
    {
		var bootedSimulators = GetBootedSimulators();
		var androidDevices = GetAndroidDevices();

		return bootedSimulators
			.Concat(androidDevices)
			.ToList();
	}

    List<IDevice> GetAndroidDevices()
    {
        var processStartInfo = new ProcessStartInfo
        {
            Arguments = "devices -l",
            FileName = "adb",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        var process = new Process()
        {
            StartInfo = processStartInfo,
        };
        process.Start();

        var devices = new List<IDevice>();
        while (!process.StandardOutput.EndOfStream)
        {
            string line = process.StandardOutput.ReadLine();

            var device = AndroidDeviceUtils.Parse(line);

            if (device == null) continue;

            devices.Add(device);
        }

        return devices;
    }

    List<IDevice> GetBootedSimulators()
    {
        var processStartInfo = new ProcessStartInfo
        {
            Arguments = "simctl list -j devices booted",
            FileName = "xcrun",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        var process = new Process()
        {
            StartInfo = processStartInfo,
        };
        process.Start();

        var stringBuilder = new StringBuilder();
        while (!process.StandardOutput.EndOfStream)
        {
            string line = process.StandardOutput.ReadLine();

            stringBuilder.Append(line);
        }

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var json = stringBuilder.ToString();
        var data = JsonSerializer.Deserialize<SimulatorGroupDto>(json, serializeOptions);
        return data.Devices.SelectMany(x => x.Value).Cast<IDevice>().ToList();
    }
}

