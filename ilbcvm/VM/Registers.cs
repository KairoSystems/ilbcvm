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
        /// Program Counter
        /// </summary>
        public byte PC;
        /// <summary>
        /// Stack Pointer
        /// </summary>
        public byte SP;
        /// <summary>
        /// Instruction Pointer
        /// </summary>
        public byte IP;
        /// <summary>
        /// Stack segment
        /// </summary>
        public byte SS;
        /// <summary>
        /// General purpose register
        /// Lower byte of the A register
        /// </summary>
        public byte AL;
        /// <summary>
        /// General purpose register
        /// Higher byte of the A register
        /// </summary>
        public byte AH;
        /// <summary>
        /// General purpose register
        /// Lower byte of the B register
        /// </summary>
        public byte BL;
        /// <summary>
        /// General purpose register
        /// Higher byte of the B register
        /// </summary>
        public byte BH;
        /// <summary>
        /// General purpose register
        /// Lower byte of the C regiser
        /// </summary>
        public byte CL;
        /// <summary>
        /// General purpose register
        /// Higher byte of the C register
        /// </summary>
        public byte CH;
        /// <summary>
		/// 32-bit general purpose register
		/// </summary>
		public Int32 X;
        /// <summary>
        /// 32-bit general purpose register
        /// </summary>
        public Int32 Y;

        /// <summary>
        /// Stores content into registers, splitting the content into the two register halves if needed
        /// </summary>
        /// <param name="Register"></param>
        /// <param name="Content"></param>
        public void SetSplit(char Register, int Content)
        {
            byte lower;
            byte higher;
            if (Content > 255)
            {
                lower = (byte)255;
                higher = (byte)(Content - 255);
            }
            else
            {
                lower = (byte)Content;
                if (Register == 'A')
                {
                    AL = lower;
                }
                else if (Register == 'B')
                {
                    BL = lower;
                }
                else if (Register == 'C')
                {
                    CL = lower;
                }
            }
        }

        /// <summary>
        /// Retrieves the data stored in each register half (AL/AH, BL/BH, CL/CH), returning the integer value
        /// If the specified register isn't A/B/C, throw a new exception.
        /// </summary>
        /// <param name="Register"></param>
        /// <returns>Two halves of the specified register combined into one integer</returns>
        public int GetSplit(char Register)
        {
            if (Register == 'A')
            {
                return BLOperations.CombineBytes(AL, AH);
            }
            else if (Register == 'B')
            {
                return BLOperations.CombineBytes(BL, BH);
            }
            else if (Register == 'C')
            {
                return BLOperations.CombineBytes(CL, CH);
            }
            throw new Exception("There was an internal error and the VM has had to close.");
        }

        /// <summary>
        /// Places content into a register, splitting if necessary
        /// </summary>
        /// <param name="Register"></param>
        /// <param name="Content"></param>
        private void SetRegister(byte Register, int Content)
        {
            if (Register == (byte)0xF0)
                PC = (byte)Content;
            else if (Register == (byte)0xF1)
                IP = (byte)Content;
            //0xF2 (Stack Pointer) is read only
            else if (Register == (byte)0xF3)
                SS = (byte)Content;
            else if (Register == (byte)0xF4)
                SetSplit('A', Content);
            else if (Register == (byte)0xF5)
                AL = (byte)Content;
            else if (Register == (byte)0xF6)
                AH = (byte)Content;
            else if (Register == (byte)0xF7)
                SetSplit('B', Content);
            else if (Register == (byte)0xF8)
                BL = (byte)Content;
            else if (Register == (byte)0xF9)
                BH = (byte)Content;
            else if (Register == (byte)0xFA)
                SetSplit('C', Content);
            else if (Register == (byte)0xFB)
                CL = (byte)Content;
            else if (Register == (byte)0xFC)
                CH = (byte)Content;
            else if (Register == (byte)0xFD)
                X = Content;
            else if (Register == (byte)0xFE)
                Y = Content;
            else
                throw new Exception("ERROR: The register " + Register + " is not a register.");
        }

        /// <summary>
        /// Returns an integer value stored in a register
        /// </summary>
        /// <param name="Reg"></param>
        /// <returns></returns>
        private int GetRegister(byte Register)
        {
            if (Register == (byte)0xF0)
                return (int)PC;
            else if (Register == (byte)0xF1)
                return (int)IP;
            else if (Register == (byte)0xF2)
                return (int)SP;
            else if (Register == (byte)0xF3)
                return (int)SS;
            else if (Register == (byte)0xF4)
                return GetSplit('A');
            else if (Register == (byte)0xF5)
                return AL;
            else if (Register == (byte)0xF6)
                return AH;
            else if (Register == (byte)0xF7)
                return GetSplit('B');
            else if (Register == (byte)0xF8)
                return BL;
            else if (Register == (byte)0xF9)
                return BH;
            else if (Register == (byte)0xFA)
                return GetSplit('C');
            else if (Register == (byte)0xFB)
                return CH;
            else if (Register == (byte)0xFC)
                return CL;
            else if (Register == (byte)0xFD)
                return X;
            else if (Register == (byte)0xFE)
                return Y;
            else
                return 0;
        }
    }
}
