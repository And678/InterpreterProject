namespace Interpreter.Context
{
	public interface IInputManager
	{
		string GetLineFromUser();
		void PrintLine(string line);
	}
}