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
        string[] dlls = ParsePaths(dllsToConvert);
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

    static string[] ParsePaths(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<string>();

            var paths = new List<string>();
            bool inQuotes = false;
            StringBuilder currentPath = new StringBuilder();
            char quoteChar = '\0';

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if ((c == '"' || c == '\'') && (i == 0 || input[i-1] != '\\'))
                {
                    if (inQuotes && c == quoteChar)
                    {
                        inQuotes = false;
                        quoteChar = '\0';
                    }
                    else if (!inQuotes)
                    {
                        inQuotes = true;
                        quoteChar = c;
                    }
                    continue;
                }

                if (c == ' ' && !inQuotes)
                {
                    if (currentPath.Length > 0)
                    {
                        paths.Add(currentPath.ToString().Trim('"', '\''));
                        currentPath.Clear();
                    }
                    continue;
                }

                currentPath.Append(c);
            }

            if (currentPath.Length > 0)
            {
                paths.Add(currentPath.ToString().Trim('"', '\''));
            }

            return paths.ToArray();
        }

    static void MoveDlls(string? dllsToMove, string movePath)
    {
        string[] dllPaths = ParsePaths(dllsToMove);
        string validMovePath = Path.GetFullPath(movePath);

        foreach (string dllPath in dllPaths)
        {  
            string fullFileName = Path.GetFileName(dllPath);
            if (!fullFileName.EndsWith(".dll"))
            {
                Console.WriteLine("""This "dll" file has a problem: {0}""", fullFileName);
            }
            else
            {
                if (Path.Exists(dllPath) && Path.Exists(validMovePath))
                {
                    try
                    {
                        string asdasd = Path.Combine(movePath, Path.GetFileName(dllPath));
                        File.Copy(dllPath, asdasd);
                        Console.WriteLine($"Moved {fullFileName}!");
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to move dll {dllPath} to {movePath}\n{e}");
                        Thread.Sleep(1000);
                        Main(new string[] { });
                    }
                }
                else
                {
                    Console.WriteLine(dllPath);
                    Console.WriteLine(validMovePath);
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