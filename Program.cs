using System;
using text_msbt;

namespace MSBT_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args[0];
            string entryname = args[1];
            string label = args[2];

            if (args.Length != 3)
            {
                Console.WriteLine("MSBT_cmd.exe {file path} {entry name} {label text}");
                return;
            }

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
