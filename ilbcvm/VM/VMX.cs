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
        /// 
        /// </summary>
        /// <param name="startingIndex"></param>
        /// <returns>Integet value</returns>
        private int Get32BitParameter(int startingIndex)
        {
            byte[] seperate = new byte[4];
            //First we need to get the bytes to convert.
            for (int i = 0; i < 4; i++)
            {
                // Get each byte
                seperate[i] = ram.memory[startingIndex + i];
            }
            bool[] b1 = new bool[8];
            bool[] b2 = new bool[8];
            bool[] b3 = new bool[8];
            bool[] b4 = new bool[8];
            b1 = BLOperations.GetBinaryValue(seperate[0]);
            b2 = BLOperations.GetBinaryValue(seperate[1]);
            b3 = BLOperations.GetBinaryValue(seperate[2]);
            b4 = BLOperations.GetBinaryValue(seperate[3]);
            b1 = Conversions.BooleanArray.JoinBooleans(b1, b2);
            b1 = Conversions.BooleanArray.JoinBooleans(b1, b3);
            b1 = Conversions.BooleanArray.JoinBooleans(b1, b4);
            return BLOperations.GetIntegerValue(b1);
        }
    }
}
