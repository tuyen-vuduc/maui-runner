using System;

namespace MauiRunner;

public static class AndroidDeviceUtils
{
    const string ARG_MODEL = "model";
    const string ARG_PRODUCT = "product";
    const string ARG_DEVICE = "device";
    const string ARG_TRANSPORT_ID = "transport_id";
    const string ARG_VALUE_DELIMITER = ":";

    /// <summary>
    /// Convert a line from `adb devices -l` command into an AndroidDeviceDto
    ///
    /// E.g.
    /// ```
    /// emulator-5554          device product:sdk_gphone_x86 model:sdk_gphone_x86 device:generic_x86_arm transport_id:3
    /// ```
    /// </summary>
    /// <param name="input">A line of device info from `adb devices -l`</param>
    /// <returns>An instance of AndroidDeviceDto if given string is a valid Android info string</returns>
    public static AndroidDeviceDto? Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || !input.Contains(ARG_VALUE_DELIMITER)) return null;

        var argsInString = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // The first part of the input is considered the device ID
        var deviceId = argsInString[0];

        // Args are parts with a colon in the middle
        var args = GetArgs(argsInString);

        return new AndroidDeviceDto
        {
            ID = deviceId,
            Model = GetArgValue(args, ARG_MODEL),
            Product = GetArgValue(args, ARG_PRODUCT),
            Device = GetArgValue(args, ARG_DEVICE),
            TransportId = GetArgValue(args, ARG_TRANSPORT_ID)
        };
    }

    private static string GetArgValue(IDictionary<string, string> args, string argName)
    {
        return args.TryGetValue(argName, out var arg)
            ? arg
            : string.Empty;
    }

    private static IDictionary<string, string> GetArgs(string[] argsInString)
    {
        var kvPairs = argsInString.Where(x => x.Contains(ARG_VALUE_DELIMITER))
                        .Select(x => x.Split(ARG_VALUE_DELIMITER))
                        .Select(x => new KeyValuePair<string, string>(x[0], x[1]));

        return new Dictionary<string, string>(kvPairs);
    }
}

