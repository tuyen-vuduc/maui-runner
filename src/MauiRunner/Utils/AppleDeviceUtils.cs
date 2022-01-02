using System.Text.RegularExpressions;

namespace MauiRunner;

public static class AppleDeviceUtils {
    /// <summary>
    /// Parse an Apple device from given single line string
    /// e.g.
    /// ```
    /// MacBook Pro (B8ECB2E2-1846-57B2-B009-AB8A00155F7C)
    /// iPhone (15.1) (00008030-000164421E91402E)
    /// Apple TV Simulator (15.2) (9CE65E57-208C-4A20-B01A-C19BC6B1BC2D)
    /// iPad (9th generation) Simulator (15.2) (32882EC2-6761-4785-91B0-2EB8ADBCDA2A)
    /// iPhone 11 Simulator (15.2) (E41E8E09-A950-4012-AD88-49AA8F8F99E4)
    /// iPod touch (7th generation) Simulator (15.2) (CEAC6F67-2E4D-4A22-88A5-971D6CFDB4F5)
    /// ```
    /// <summary>
    public static AppleDeviceDto? Parse(string input) {
        var regex = new Regex(@"^(.+)\s+\(([A-F0-9\-]+)\)$", RegexOptions.Singleline);

        var matches = regex.Matches(input);

        if (matches.Count != 1 || matches[0].Groups.Count != 3) return null;

        var name = matches[0].Groups[1].Value;
        var udid = matches[0].Groups[2].Value;
        var os = name.Contains("Macbook")
                    ? "maccatalyst"
                    : name.Contains("Apple TV")
                    ? "tvos"
                    : "ios";

        return new AppleDeviceDto {
            Udid = udid,
            Name = name,
            OS = os
        };
    }
}