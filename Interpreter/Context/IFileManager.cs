using System.Collections.Generic;

namespace Interpreter.Context
{
	public interface IFileManager
	{
		bool CompareFiles(string fileA, string fileB);
		void CreateFile(string file);
		void DeleteFile(string file);
		bool FileExists(string file);
		List<string> GetFileList(string path);
		string GetFileName(string file);
		int GetFileSize(string file);
		string MakeDir(string path);
		void MoveFile(string fileA, string fileB);


	}
}