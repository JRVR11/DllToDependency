using System;
using System.Text;
using TextCopy;

namespace customDepsConverter.Converter;

class Program
{
    static void Convert(string? dllsToConvert, string? dllsPath)
    {
        Console.Clear();
        StringBuilder sB = new StringBuilder();
        string[] dlls = dllsToConvert.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string indent = new string(' ', 2 * 2);

        string skippedDlls = "";

        sB.AppendLine("<ItemGroup>");
        foreach (string dllPath in dlls)
        {
            string dllName = Path.GetFileNameWithoutExtension(dllPath).Replace("'", "");
            string dllFullName = Path.GetFileName(dllPath).Replace("'", "");

            if (!dllFullName.Contains(".dll"))
            {
                skippedDlls += $"\n{dllName}";
            }
            else
            {
                Console.WriteLine($"Converted {dllFullName}");
                string realPath = dllsPath.Replace("/", "\\");

                sB.AppendLine($"{indent}<Reference Include='{dllName}'>");
                sB.AppendLine($"{indent}<HintPath>{realPath}\\{dllFullName}</HintPath>");
                sB.AppendLine($"{indent}</Reference>");
            }
        }
        sB.AppendLine("</ItemGroup>");

        ClipboardService.SetText(sB.ToString());
        Console.WriteLine("Copied converted dlls to clipboard!");
        Console.WriteLine($"Copy converted dlls to {dllsPath}? (y or n): ");
        var yesno = Console.ReadLine();

        if (!string.IsNullOrEmpty(yesno))
        {
            switch (yesno)
            {
                case "y":
                    MoveDlls(dllsToConvert, dllsPath);
                    break;
                case "n":
                    Console.WriteLine("Ok.");
                    Thread.Sleep(1000);
                    Main(new string[] { });
                    break;
            }
        }

        //Console.WriteLine($"""Skipped "dlls" were...{skippedDlls}""");
    }

    static void MoveDlls(string? dllsToMove, string movePath)
    {
        string[] dllPaths = dllsToMove.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string dllPath in dllPaths)
        {
            string validDllPathMaybePLease = dllPath.Replace("'", "");
            string validMovePathMaybePlease = movePath.Replace("'", "");
            if (!validDllPathMaybePLease.EndsWith(".dll"))
            {

            } else {
                if (Path.Exists(validDllPathMaybePLease) && Path.Exists(validMovePathMaybePlease))
                {
                    try
                    {
                        string fullFileName = Path.GetFileName(dllPath);
                        if (!fullFileName.EndsWith(".dll"))
                        {

                        }
                        else
                        {

                            string asdasd = Path.Combine(movePath, Path.GetFileName(dllPath));
                            File.Copy(dllPath, asdasd);
                            Console.WriteLine($"Moved {fullFileName}!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to move dll {dllPath} to {movePath}\n{e}");
                        Thread.Sleep(1000);
                        Main(new string[] { });
                    }
                }else
                {
                    Console.WriteLine(validDllPathMaybePLease);
                    Console.WriteLine(validMovePathMaybePlease);
                    Console.WriteLine("Failed to move dlls, invalid path(s)");
                    Thread.Sleep(10000);
                    Main(new string[] { });
                }
            }
            
        }
        Thread.Sleep(1000);
        Main(new string[] { });
    }
    static void Main(string[] args)
    {
        Console.Clear();
        Console.Write("Dlls to Convert: ");
        var dllsToConvert = Console.ReadLine();
        Console.Write("Dlls' Path: ");
        var dllsPath = Console.ReadLine();

        if (!string.IsNullOrEmpty(dllsToConvert) && !string.IsNullOrEmpty(dllsPath))
        {
            Convert(dllsToConvert, dllsPath);
        }
    }
}