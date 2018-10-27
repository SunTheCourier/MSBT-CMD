using System;
using System.IO;
using System.Linq;
using System.Threading;
using text_msbt;

namespace MSBT_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("MSBT_cmd.exe {file path} {entry name} {label text}");
                return;
            }

            if (FileOrDirectoryExists(args[0]))
            {
                //organize args
                FileAttributes msbt = File.GetAttributes(args[0]);
                string entryname = args[1];
                string label = args[2];

                MsbtAdapter CMD = new MsbtAdapter();

                if ((msbt & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    if (entryname == "*")
                    {
                        DirectoryInfo msbtdir = new DirectoryInfo(args[0]);
                        FileInfo[] msbtfiles = msbtdir.GetFiles("*.msbt", SearchOption.AllDirectories);
                        FileInfo[] bakfiles = msbtdir.GetFiles("*.bak", SearchOption.AllDirectories);

                        if (msbtfiles[0].Exists)
                        {
                            foreach (FileInfo file in msbtfiles)
                            {
                                CMD.Load(file.FullName, true);
                                foreach (MsbtEntry entry in CMD.Entries)
                                {
                                    entry.EditedText = label;
                                }
                                CMD.Save();
                                File.Delete($"{file.FullName}.bak");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No MSBT files exist in that directory.");
                        }
                    }
                    else
                    {
                        var entry = CMD.Entries.FirstOrDefault(e => e.Name == entryname);
                        if (entry != null) entry.EditedText = label;
                        else Console.WriteLine("Entry name not found.");
                    }
                }
                else
                {
                    FileInfo msbtfile = new FileInfo(args[0]);

                    //load MSBT
                    CMD.Load(msbtfile.FullName, true);

                    //find entry
                    if (entryname == "*")
                    {
                        foreach (MsbtEntry entry in CMD.Entries)
                        {
                            entry.EditedText = label;
                        }
                    }
                    else
                    {
                        var entry = CMD.Entries.FirstOrDefault(e => e.Name == entryname);
                        if (entry != null) entry.EditedText = label;
                        else Console.WriteLine("Entry name not found.");
                    }

                    CMD.Save();
                    File.Delete($"{msbtfile.FullName}.bak");
                    Console.WriteLine("Done!");
                }
            }
            else
            {
                Console.WriteLine("That file or Directory Doesn't Exist.");
            }
        }

        internal static bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }
    }
}
