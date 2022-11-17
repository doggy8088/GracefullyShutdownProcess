using System.Diagnostics;

var data = from p in Process.GetProcesses()
            where p.MainWindowHandle > 0 && p.ProcessName == "msedge"
           select p;

foreach (Process edge in data)
{
    GracefullyShutdownProcess(edge);
    
    Console.WriteLine(edge.ProcessName + " closed.");
}

void GracefullyShutdownProcess(Process p, int waitSeconds = 9)
{
    if (!p.HasExited)
    {
        p.CloseMainWindow();

        if (waitSeconds == 0)
        {
            p.Kill();
            return;
        }

        Console.WriteLine($"Closing {p.ProcessName} process. Counting down: {waitSeconds}");

        Thread.Sleep(1_000);

        p.Refresh();
        GracefullyShutdownProcess(p, waitSeconds - 1);
    }
}