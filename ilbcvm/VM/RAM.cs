using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilbcvm
{
    public class RAM
	{
		/// <summary>
		/// Fills the specified location in memory with the specified byte content
		/// </summary>
		/// <param name="location"></param>
		/// <param name="content"></param>
		public void SetSection(int location, byte[] content)
		{
			for (int i = 0; i < content.Length; i++)
			{
				Globals.RuntimeOptions.console.WriteLine("Set Location: " + location.ToString("X2") + " Content: " + content[i].ToString("X2"));
				memory[(i + location)] = content[i];
			}
		}

		/// <summary>
		/// Returns a section of memory at the specified location of the specified length
		/// </summary>
		/// <param name="location"></param>
		/// <param name="length"></param>
		/// <returns>Section of memory</returns>
		public byte[] GetSection(int location, int length)
		{
			byte[] ret = new byte[length];
			for (int i = 0; i < length; i++)
			{
				Globals.RuntimeOptions.console.WriteLine("Get Location: " + location.ToString("X2") + " Content: " + memory[(location + i)].ToString("X2"));
				ret[i] = memory[(location + i)];
			}
			return ret;
		}
		/// <summary>
		/// Sets a specified location in memory to a specified byte
		/// </summary>
		/// <param name="location"></param>
		/// <param name="content"></param>
		public void SetByte(int location, byte content)
		{
			Globals.RuntimeOptions.console.WriteLine("Set Location: " + location.ToString("X2") + " Content: " + content.ToString("X2"));
			if (location > RAMLimit)
			{
				memory[location] = content;
			}
			else
			{
#if DEBUG
				//Globals.RuntimeOptions.console.WriteLine("FATAL RAM ERROR AT " + memory[location]);
				Globals.RuntimeOptions.console.WriteLine("FATAL RAM ERROR");
#else
				throw new Exception("The application tried to modify itself and was terminated. Consult the application vendor for more support.");
#endif
			}
		}
		/// <summary>
		/// Memory as a byte array
		/// </summary>
		public byte[] memory;
		/// <summary>
		/// Constructor for the RAM
		/// </summary>
		/// <param name="amount"></param>
		public RAM(int amount)
		{
			memory = new byte[amount];
		}
		/// <summary>
		/// Position of where the executed binary ends + 1
		/// </summary>
		public int RAMLimit;

	}
}


