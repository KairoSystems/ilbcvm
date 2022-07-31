using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ilbcvm;
using ilbcvm.Globals;

namespace ilbcvm_RT
{
    public class AR_Console : VConsole
	{
		public override void WriteLine(string text)
		{
			Console.WriteLine(text);
		}
		public override void Write(char ch)
		{
			Console.Write(ch);
		}
		public override void Write(string text)
		{
			Console.Write(text);
		}
		public override byte Read()
		{
			char t = Console.ReadKey(false).KeyChar;
            byte[] b = { (byte)t };
			ASCIIEncoding.Convert(new UnicodeEncoding(), new ASCIIEncoding(), b);
			return b[0];
		}
		public override string ReadLine()
		{
			string? s = Console.ReadLine();
			if (s != null)
			{
				return s;
			}
			else
			{
				return "";
			}
		}
	}
}
