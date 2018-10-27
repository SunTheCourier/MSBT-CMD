using System;
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

            string file = args[0];
            string entryname = args[1];
            string label = args[2];

            MsbtAdapter CMD = new MsbtAdapter();
            CMD.Load(file, true);

            bool found = false;
            foreach (MsbtEntry entry in CMD.Entries)
            {
                if (entry.Name == entryname)
                {
                    entry.EditedText = label;
                    found = true;
                    break;
                }
            }
            CMD.Save();
            if (found == false) Console.WriteLine("Entry name not found.");
            Console.WriteLine("Done!");
        }
    }
}
