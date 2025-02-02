using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace FileClipboard
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0) ShowMenu();
            else if (args.Any(arg => arg.Equals("--help", StringComparison.OrdinalIgnoreCase) || arg.Equals("-h", StringComparison.OrdinalIgnoreCase))) ShowHelp();
            else ProcessFiles(args);
        }

        static void ShowMenu()
        {
            Console.WriteLine("No file arguments were provided.");
            Console.WriteLine("Select an option:");
            Console.WriteLine("\t1. Add the program's folder to your PATH");
            Console.WriteLine("\t2. Exit");
            Console.WriteLine("Enter your choice (1/2): ");
            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1") AddProgramToPath();
            else Console.WriteLine("Exiting program.");
        }

        static void ProcessFiles(string[] fileArgs)
        {
            var validFiles = fileArgs.Where(File.Exists).ToList();
            var invalidFiles = fileArgs.Except(validFiles).ToList();

            if (invalidFiles.Any())
            {
                Console.WriteLine("The following file(s) were not found and will be skipped:");
                foreach (var file in invalidFiles)
                    Console.WriteLine("  " + file);
            }

            if (!validFiles.Any())
            {
                Console.WriteLine("No valid files to process. Exiting.");
                return;
            }

            try
            {
                StringCollection fileCollection = new StringCollection();
                foreach (var file in validFiles)
                    fileCollection.Add(Path.GetFullPath(file));

                Clipboard.SetFileDropList(fileCollection);
                Console.WriteLine("Successfully copied the following file(s) to the clipboard:");
                foreach (var file in validFiles)
                    Console.WriteLine("  " + file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error copying files to clipboard: " + ex.Message);
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("FileClipboard Usage:");
            Console.WriteLine("  FileClipboard.exe [file1] [file2] ...");
            Console.WriteLine("  FileClipboard.exe --help | -h    : Show this help message");
            Console.WriteLine();
            Console.WriteLine("If no file arguments are provided, you will be prompted with additional options.");
        }

        static void AddProgramToPath()
        {
            try
            {
                string userPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User) ?? string.Empty;
                string? exeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                if (exeDirectory == null)
                {
                    Console.WriteLine("Could not determine the executable directory.");
                    return;
                }

                var pathDirs = userPath.Split(';', StringSplitOptions.RemoveEmptyEntries);

                if (!pathDirs.Contains(exeDirectory, StringComparer.OrdinalIgnoreCase))
                {
                    string newPath = string.IsNullOrWhiteSpace(userPath) ? exeDirectory : $"{userPath};{exeDirectory}";
                    Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
                    Console.WriteLine("Program directory added to PATH. Please restart your terminal for changes to take effect.");
                }
                else
                    Console.WriteLine("Program directory is already in the PATH.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding program directory to PATH: " + ex.Message);
            }
        }
    }
}
