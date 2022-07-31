using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ilbcvm.Globals;

namespace ilbcvm.stdlib
{
    public static class INTs
    {
        public static VM ParentVM = null!;
        public static void HandleInterrupt(int command)
        {
            // stdio Commands
            #region stdio
            if (command == 0x01)
            {
                if (RuntimeOptions.DebugMode == true)
                {
                    RuntimeOptions.console.Write("KEI 0x01: ");
                }
                if (ParentVM.AL == 0x01)
                {
                    RuntimeOptions.console.Write((char)ParentVM.AH);
                    if (RuntimeOptions.DebugMode == true)
                        RuntimeOptions.console.WriteLine(" 0x01");
                }
                else if (ParentVM.AL == 0x02)
                {
                    string toPrint = "";
                    byte[] toConvert = new byte[ParentVM.GetSplit('B')];
                    toConvert = ParentVM.ram.GetSection(ParentVM.X, ParentVM.GetSplit('B'));
                    for (int i = 0; i < toConvert.Length; i++)
                    {
                        toPrint += (char)toConvert[i];
                    }
                    if (RuntimeOptions.DebugMode == true)
                    {
                        RuntimeOptions.console.WriteLine(" 0x02");
                    }
                    RuntimeOptions.console.Write(toPrint);
                    
                }
                else if (ParentVM.AL == 0x03)
                {
                    ParentVM.AH = (byte)RuntimeOptions.console.Read();
                }
                else if (ParentVM.AL == 0x04)
                {
                    string toConvert = RuntimeOptions.console.ReadLine();
                    byte[] toWrite = new byte[toConvert.Length];
                    for (int i = 0; i < toWrite.Length; i++)
                    {
                        toWrite[i] = (byte)toConvert[i];
                    }
                    ParentVM.SetSplit('B', toWrite.Length);
                    ParentVM.ram.SetSection(ParentVM.X, toWrite);
                }
            }
            else if (command == 0x02)
            {
                if (RuntimeOptions.DebugMode == true)
                {
                    RuntimeOptions.console.Write("KEI 0x02: ");
                }
                RuntimeOptions.console.WriteLine("Halting!");
                ParentVM.Halt();
            }
            else
            {
                RuntimeOptions.console.WriteLine("Undocumented function: " + command + "\nHalting for protection of data");
                ParentVM.Halt();
            }
            #endregion
        }
    }
}