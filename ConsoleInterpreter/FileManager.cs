using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interpreter.Context;

namespace ConsoleInterpreter
{
	public class FileManager : IFileManager
	{
		public bool CompareFiles(string fileA, string fileB)
		{
			byte[] file1 = File.ReadAllBytes(fileA);
			byte[] file2 = File.ReadAllBytes(fileB);
			if (file1.Length == file2.Length)
			{
				for (int i = 0; i < file1.Length; i++)
				{
					if (file1[i] != file2[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public void CreateFile(string file)
		{
			File.Create(file);
		}

		public void DeleteFile(string file)
		{
			File.Delete(file);
		}

		public bool FileExists(string file)
		{
			return File.Exists(file);
		}

		public List<string> GetFileList(string path)
		{
			return Directory.GetFiles(path).ToList();
		}

		public string GetFileName(string file)
		{
			return Path.GetFileName(file);
		}

		public int GetFileSize(string file)
		{
			return (int) new FileInfo(file).Length;
		}

		public string MakeDir(string path)
		{
			return Directory.CreateDirectory(path).FullName;
		}

		public void MoveFile(string fileA, string fileB)
		{
			File.Move(fileA, fileB);
		}
	}
}