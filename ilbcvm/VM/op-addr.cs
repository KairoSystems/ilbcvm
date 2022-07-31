using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilbcvm
{
    public partial class VM
    {
        /// <summary>
        /// enum of the Address Modes
        /// </summary>
        private enum AddressMode
        {
            RegReg,
            ValReg,
            RegVal,
            ValVal
        }

        /// <summary>
        /// Address mode for current operation
        /// </summary>
        private AddressMode opMode;

        /// <summary>
        /// Address mode initially set to 0, for Register:Register
        /// </summary>
        private int AdMode = 0;

        /// <summary>
        /// Gets the current address mode from the specified byte
        /// </summary>
        /// <param name="b">Address mode byte</param>
        public void GetAddressMode(byte b)
        {
            AdMode = GetLastTwo(b);
            if (AdMode == 0)
            {
                opMode = AddressMode.RegReg;
            }
            else if (AdMode == 1)
            {
                opMode = AddressMode.ValReg;
            }
            else if (AdMode == 2)
            {
                opMode = AddressMode.RegVal;
            }
            else if (AdMode == 3)
            {
                opMode = AddressMode.ValVal;
            }
            else
            {
                throw new Exception("[CRITICAL ERROR] Invalid address mode at " + IP + " (" + AdMode + ").");
            }
        }

        /// <summary>
        /// Execute the operand at the given address
        /// </summary>
        /// <param name="addr"></param>
        public void ExecuteAtAddress(byte addr)
        {
            IP = addr;
            Execute();
        }

        /// <summary>
        /// Halt all execution
        /// </summary>
        public void Halt()
        {
            ram.memory[IP] = 0x00;
            Running = false;
        }
    }
}
