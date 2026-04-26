// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

foreach (var p in Process.GetProcessesByName("TOSCONSOLE1"))
{
    p.Kill();
    p.WaitForExit();
}

Process n = new Process();
n.StartInfo.FileName = @"E:\RTG\Console1\TOSConsole1.exe";
n.Start();

foreach (var p in Process.GetProcessesByName("TOSCONSOLE2"))
{
    p.Kill();
    p.WaitForExit();
}

n = new Process();
n.StartInfo.FileName = @"E:\RTG\Console2\TOSConsole2.exe";
n.Start();

foreach (var p in Process.GetProcessesByName("TOSCONSOLE3"))
{
    p.Kill();
    p.WaitForExit();
}

n = new Process();
n.StartInfo.FileName = @"E:\RTG\Console3\TOSConsole3.exe";
n.Start();
