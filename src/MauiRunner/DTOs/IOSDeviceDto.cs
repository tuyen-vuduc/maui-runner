
namespace MauiRunner;

public partial class IOSDeviceDto : IDevice
{
    public string DataPath { get; set; }
    public long DataPathSize { get; set; }
    public string LogPath { get; set; }
    public string Udid { get; set; }
    public bool IsAvailable { get; set; }
    public long LogPathSize { get; set; }
    public string DeviceTypeIdentifier { get; set; }
    public string State { get; set; }
    public string Name { get; set; }

    public string ID => Udid;
}
