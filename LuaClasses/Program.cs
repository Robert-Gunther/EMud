using System;
using NLua;

namespace LuaClasses
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//This is test code for Player
			/*
			Player play1 = new Player ("Player1", "Warrior", 10);
			Player play2 = new Player ("Player2", "Mage", 11);
			Player play3 = new Player ("Player3", "Defender", 10);

			System.Console.WriteLine (play1.getStats () + "\n\n");
			System.Console.WriteLine (play2.getStats () + "\n\n");
			System.Console.WriteLine (play3.getStats () + "\n\n");

			System.Console.WriteLine ("This is the damage done when " + play1.playerName + " attacks " + play2.playerName + ": " + (play1.actionAttack (ref play2)).ToString());
			System.Console.WriteLine ("This is the damage done when " + play3.playerName + " attacks " + play2.playerName + ": " + (play3.actionAttack (ref play2)).ToString());
			System.Console.WriteLine ("This is the damage done when " + play2.playerName + " attacks " + play1.playerName + ": " + (play2.actionAttack (ref play1)).ToString() + "\n\n");

			play2.levelUp ();
			System.Console.WriteLine ("This is " + play2.playerName + " after leveling up:\n");
			System.Console.WriteLine (play2.getStats() + "\n\n");

			System.Console.WriteLine ("This is the damage done when " + play2.playerName + " attacks " + play1.playerName + ": " + (play2.actionAttack (ref play1)).ToString ());
			/* */

			//These two lines are how you setup a lua state (Similar to a game state save)
			Lua luaState = new Lua();
			luaState.LoadCLRPackage ();

			//This line selects which lua file you wish to use
			luaState.DoFile ("test.lua");

			//This is how you access a lua variable. An integer must be cast as a double before it is cast as an int
			//You must use the ? after the variable name for numbers to allow them to be null
			//The string in square brackets is the name of the variable in the lua file
			// If you try to case a string as an int or etc an InvalidCastException will be thrown.
			int? value = (int?)(luaState ["test"] as double?);
			//If the value does not exist it will return a null
			if (value == null)
				value = -1;
			//If it does exist it now acts like a normal integer
			System.Console.WriteLine ("The value is: " + value.ToString ());
			/* */
			/*
			Lua luaState = new Lua ();
			luaState.LoadCLRPackage ();
			luaState.DoFile ("test.lua");

			//The same idea with a string
			string value = luaState ["testString"] as string;
			System.Console.WriteLine (value);
			/* */

			/*
			Lua luaState = new Lua ();
			luaState.LoadCLRPackage ();
			luaState.DoFile ("test.lua");

			//This is how you access a lua function
			//The string is the name of the function in the lua file
			LuaFunction luaFunct = luaState ["testFunction"] as LuaFunction;
			//A lua function will return a table, so to get a single value from it you must use the square brackets
			//and treat it as an array.
			//Anything you put in the parenthesis will be passed as parameters to the lua function
			//The return value must be cast correctly
			//If you try to return a table from a lua function I have not yet determined the type of variable
			//that is. You should be able to use the var keyword and then treat that as an array, but that is untested a/o now
			int num = (int)(double)luaFunct.Call (level, modifiers)[0];
			System.Console.WriteLine(num.ToString());

			/* */
		}
	}
}
