using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleApp
{
	class UserHelper
	{
		static String[] Names = new String[5] { "Jero", "Ohad", "Wanhing", "Garrick", "Dave" };
		static int userNumber = 0;
		public override String ToString()
		{
			userNumber = (userNumber + 1) % Names.Length;
			return Names[userNumber];
		}
	}
}
