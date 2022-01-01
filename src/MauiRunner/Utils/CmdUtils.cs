using System.Diagnostics;

namespace MauiRunner;

public static class CmdUtils {
    public static string ReadOutput(string cmd, string args) {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = cmd,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        var process = new Process()
        {
            StartInfo = processStartInfo,
        };
        process.Start();

        var result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return result;
    }
}