using System;
using System.IO;
using System.Linq;
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

            //organize args
            FileInfo msbt = new FileInfo(args[0]);
            string entryname = args[1];
            string label = args[2];

            //check if the file given actually exists
            if (!msbt.Exists)
            {
                Console.WriteLine("MSBT file doesnt exist.");
                return;
            }

            //load MSBT
            MsbtAdapter CMD = new MsbtAdapter();
            CMD.Load(msbt.FullName, true);

            //find entry
            var entry = CMD.Entries.FirstOrDefault(e => e.Name == entryname);
            if (entry != null) entry.EditedText = label;

            CMD.Save();
            if (entry == null) Console.WriteLine("Entry name not found.");
            File.Delete($"{msbt.FullName}.bak");
            Console.WriteLine("Done!");
        }
    }
}
