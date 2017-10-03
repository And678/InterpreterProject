using System.IO;
using System.Linq;

namespace Interpreter
{
	public static class FileHelpers
	{
		public static bool IsValidPath(string path)
		{
			if (path.Any(c => Path.GetInvalidPathChars().Contains(c)))
			{
				return false;
			}
			return true;
		}

		public static string BuildAbsolute(string workingdir, string relative)
		{
			var nworkingdir = !"\\/".Contains(workingdir[workingdir.Length - 1]) ? workingdir + '\\' : workingdir;
			var nrelative = "\\/".Contains(relative[0]) ? relative.Substring(1) : relative;
			return nworkingdir + nrelative;
		}

	}
}