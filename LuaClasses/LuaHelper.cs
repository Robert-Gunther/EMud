using System;
using NLua;

namespace LuaClasses
{
	public class LuaHelper
	{
		public Lua State;

		public LuaHelper ()
		{
			State = new Lua();
			State.LoadCLRPackage ();
		}

		public void AddGlobal(string name, object o)
		{
			State [name] = o;
		}

		public void RegisterFunction(string name, object b, System.Reflection.MethodInfo method)
		{
			State.RegisterFunction (name, b, method);
		}

		public object[] Call(string name, params object[] args) 
		{
			LuaFunction func = State [name] as LuaFunction;
			return func.Call (args);
		}

		public void RunFile(string path)
		{
			State.DoFile (path);
		}
	}
}

