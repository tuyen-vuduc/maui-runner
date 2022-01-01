// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using MauiRunner;

Console.WriteLine("==================");
Console.WriteLine("MAUI RUNNER");
Console.WriteLine("==================");

var devicesManager = new DevicesManager();

var devices = devicesManager.GetDevices();

if (devices.Count == 0)
{
    Console.WriteLine("\n \nNo devices/simulators available.\nPlease start \n- an iOS simulator,\n- an Android emulator or \n- connect to an Android device.");
    return;
}

Console.WriteLine("\n \nAvailable devices/simulators:");
for (var i = 0; i < devices.Count; i++)
{
    Console.WriteLine($"{i + 1}. {devices[i].Name}");
}

int selectedDeviceIndex = 0;

do
{
    Console.Write("\n \nPlease select a device: ");

    if (!int.TryParse(Console.ReadLine(), out selectedDeviceIndex)) continue;

    if (selectedDeviceIndex > devices.Count || selectedDeviceIndex - 1 < 0) continue;

    break;
} while (true);

var selectedDevice = devices[selectedDeviceIndex - 1];
var process = Run(selectedDevice);

Console.WriteLine($"\n\nRunning on {selectedDevice.Name} ({selectedDevice.ID})\n\n");
Console.ReadKey();
process.Kill();

Console.WriteLine("\n \nMAUI RUNNER-->DONE");

Process Run(IDevice dto)
{
    var arguments = dto is AndroidDeviceDto
        ? $"build -t:Run -f net6.0-android -p:_DeviceName={dto.ID}"
        : $"build -t:Run -f net6.0-ios -p:_DeviceName=:v2:udid={dto.ID}";
    Console.WriteLine("dotnet " + arguments);

    var processStartInfo = new ProcessStartInfo
    {
        Arguments = arguments,
        FileName = "dotnet",
        UseShellExecute = false,
        // RedirectStandardOutput = true,
        CreateNoWindow = true
    };

    var process = new Process()
    {
        StartInfo = processStartInfo,
    };
    process.Start();

    return process;
}

