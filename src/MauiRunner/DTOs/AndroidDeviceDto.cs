
namespace MauiRunner;

public class AndroidDeviceDto : IDevice
{
    public string ID { get; set; }
    public string Model { get; set; }
    public string Product { get; set; }
    public int TransportId { get; set; }
    public string Device { get; set; }
    public string Name => Model;
}