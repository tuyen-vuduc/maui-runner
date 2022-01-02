
namespace MauiRunner;

public partial class AppleDeviceDto : IDevice
{
    public string Udid { get; set; }

    public string Name { get; set; }

    public string OS { get; set; }

    public string ID => Udid;
}
