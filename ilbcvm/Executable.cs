using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace ilbcvm
{
    public class Executable
    {
        public static void Run(byte[] application)
        {
            VM virtualMachine = new VM(application, (application.Length + 1024));
            virtualMachine.Execute();
        }
    }
}