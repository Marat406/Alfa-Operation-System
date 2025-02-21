using Cosmos.System.ExtendedASCII;
using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel: Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs;
        string Curentdirectory = @"0:\";
        

        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.WriteLine("Welcome to ALPHA OS. To view the list of commands, use Help");
        }
        
        protected override void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Input: ");
            Console.ForegroundColor = ConsoleColor.White;
            var input = Console.ReadLine();
            string filename = "";
            string dirname = "";
            switch (input)
            {
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(input + ": unknow comand");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "dir":
                    var directory_list = fs.GetDirectoryListing(Curentdirectory);
                    try
                    {
                        foreach (var file in directory_list)
                        {
                            var content = File.ReadAllText(filename);
                            Console.WriteLine("File name: " + file);
                            Console.WriteLine("File size: " + content.Length);
                            Console.WriteLine("Content: " + content);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    break;
                case "New file":
                    try
                    {
                        var file_stream = File.Create(@"0:\testing.txt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    break;
                case "Write to file":
                    try
                    {
                        File.WriteAllText(@"0:\testing.txt", "Learning how to use VFS!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    break;
                case "Help":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nShutdown \n1.Reboot \n2.sysainfo \n3.Resource monitoring \n4.calc");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Shutdown":
                    Cosmos.System.Power.Shutdown();
                    break;
                case "Reboot":
                    Cosmos.System.Power.Reboot();
                    break;
                case "sysainfo":
                    string CPUmodel = Cosmos.Core.CPU.GetCPUBrandString();
                    string CPUvender = Cosmos.Core.CPU.GetCPUVendorName();
                    uint amount_of_ram = Cosmos.Core.CPU.GetAmountOfRAM();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(@"CPU: {0}
CPU Vendor {1}
Amount of RAM {2} MB", CPUmodel, CPUvender, amount_of_ram);
                    break;
                case "Resource Monitoring":
                    ulong available_of_ram = Cosmos.Core.GCImplementation.GetAvailableRAM();
                    uint UsedRam = Cosmos.Core.GCImplementation.GetUsedRAM();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(@"available_of_ram: {0} MB
UsedRam {1} MB", available_of_ram, UsedRam);
                    break;
                case "File system":
                    filename = Console.ReadLine();
                    fs.CreateFile(Curentdirectory + filename);
                    break;
                case"mkdir":
                    dirname = Console.ReadLine();
                    fs.CreateDirectory(Curentdirectory + dirname);
                    break;
                case "delfile":
                    filename = Console.ReadLine();
                    Sys.FileSystem.VFS.VFSManager.DeleteFile(Curentdirectory + filename);
                    break;
                case"deldir":
                    dirname = Console.ReadLine();
                    Sys.FileSystem.VFS.VFSManager.DeleteFile(Curentdirectory + dirname);
                    break;
                case "cd":
                    Curentdirectory = Console.ReadLine();
                    break;

            }


        }
    }
}
