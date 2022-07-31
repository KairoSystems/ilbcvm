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
        /// Contains the byte value of the new Instruction Pointer
        /// </summary>
        private byte NewIP;
        /// <summary>
        /// Current instructions parameters
        /// </summary>
        private int[] parameters;
        

        /// <summary>
        /// Execution state of the running virtual machine
        /// </summary>
        public bool Running = false;

        /// <summary>
        /// Loads the application as a byte array into the virtual machine's memory
        /// </summary>
        /// <param name="application"></param>
        private void LoadApplication(byte[] application)
        {
            int i = 0;
            while (i < application.Length)
            {
                ram.memory[i] = application[i];
                i++;
            }
            //Sets the RAM limit, right above the end of the executable:
            ram.RAMLimit = (i + 1);

        }
        /// <summary>
        /// This virtual machine's RAM
        /// </summary>
        public RAM ram;
        /// <summary>
        /// Constructor for a new instance of a virtual machine, specifiying the executable to run and the VM's amount of RAM.
        /// </summary>
        /// <param name="executable">The executing binary's size must be greater than the ramsize</param>
        /// <param name="ramsize">The size of virtual memory in bytes must be larger than the size of the executable binary</param>
        public VM(byte[] executable, int ramsize)
        {
            ram = new RAM(ramsize);
            // Declares the six-bit instruction to a new boolean array of length 9 
            sixbits = new bool[9];
            // Declares the two-bit parameter addressing mode to a new boolean array of length 2
            twobits = new bool[2];
            // Defines the parameters integer array as an array of length 5
            parameters = new int[5];
            // Sets the instruction pointer to 0
            IP = 0;
            // Sets the program counter to 1
            PC = 1;
            // Sets the parent virtual machine for the standard library to this instance
            stdlib.INTs.ParentVM = this;
#warning re-add software interrupts at a later stage
            //SoftwareInterrupts.ParentVM = this;
            //Loads the executable into the Virtual Machine's memory through LoadApplication()
            LoadApplication(executable);
        }

        

        /// <summary>
        /// Executes the binary loaded into the Virtual Machine's memory
        /// </summary>
        public void Execute()
        {
            Running = true;
            while (Running == true)
            {
                while (ram.memory[IP] != 0x00)
                {
                    /// <summary>
                    /// Gets the operation from the first six bytes of the instruction pointer
                    /// </summary>
                    /// <returns>operation from instruction pointer</returns>
                    byte opcode = (byte)GetFirstSix(ram.memory[IP]);
                    GetAddressMode(ram.memory[IP]);
                    ParseOpcode(opcode);
                    IP = PC;
                    PC++;
                }
            }
            Running = false;
            Halt();
        }

        // Useful for stepping in monitor or debugging modes
        public void Tick()
        {
            if (ram.memory[IP] != 0x00)
            {
                byte opcode = (byte)GetFirstSix((ram.memory[IP]));
                GetAddressMode(ram.memory[IP]);
                ParseOpcode(opcode);
                IP = PC;
                PC++;
            }
        }

       

        /// <summary>
        /// Retrieves the last two bits from a single byte (8 bits)
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int GetLastTwo(byte b)
        {
            byte c = 0;
            for (int i = 7; i > 5; i--)
            {
                twobits[c] = BLOperations.GetBit(b, i);
                c++;
            }
            return BLOperations.GetIntegerValue(twobits);
        }
        /// <summary>
        /// Stores the first six bits in a byte
        /// </summary>
        private bool[] sixbits;

        /// <summary>
        /// Retrieves the integer value of first six bits in a byte
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int GetFirstSix(byte b)
        {
            for (int i = 0; i < 6; i++)
            {
                sixbits[i] = BLOperations.GetBit(b, i);
            }
            return BLOperations.GetIntegerValue(sixbits);
        }

        /// <summary>
        /// Stores the last two bits in a byte (byte - sixbits)
        /// </summary>
        private bool[] twobits;

        
    }
}
