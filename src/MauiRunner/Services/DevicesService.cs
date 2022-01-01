using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MauiRunner;

public class DevicesManager : IDeviceSource
{
    IList<IDeviceSource> deviceSources;

    public DevicesManager()
    {
        deviceSources = new List<IDeviceSource>
        {
            new AndroidDeviceSource(),
            new IOSSimulatorSource(),
        };
    }

    public IList<IDevice> GetDevices()
    {
        var devices = new List<IDevice>();

        foreach (var deviceSource in deviceSources) {
            devices.AddRange(deviceSource.GetDevices());
        }

        return devices;
    }
}

