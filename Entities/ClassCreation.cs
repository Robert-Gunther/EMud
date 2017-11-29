using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//This class governs the creation of player character classes including name and stats
namespace EMudEntityCreationMenus
{
	public class ClassCreation
	{
		string AnotherClass = null;

		private string nmspc;

		private void writeClass(List<string> stat)
		{
			System.IO.File.WriteAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", "using System;\nusing NLua;\nnamespace " + nmspc + "\n{\n\tpublic class Player\n\t{\n\t\tprivate Lua luaState;\n\t\tprivate int _level;\n\t\tpublic string playerName;\n\t\tpublic string className;\n\t\tpublic int level{\n\t\t\tget{\n\t\t\t\treturn _level;\n\t\t\t}\n\t\t\tprivate set{\n\t\t\t\tif (value > 0)\n\t\t\t\t\t_level = value;\n\t\t\t\tcalculateStats ();\n\t\t\t}\n\t\t}");

			foreach (string element in stat)
			{
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", "private int _" + element + ";\n\t\tpublic int " + element + "{\n\t\t\tget{\n\t\t\t\treturn _" + element + ";\n\t\t\t}\n\t\t\tprivate set {\n\t\t\t\tstring statName = \"stat\" + " + element + ";\n\t\t\t\tLuaFunction luaFunct = luaState [\"stat" + element + "\"] as LuaFunction;\n\t\t\t\t_" + element);
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", " = (int)(double)luaFunct.Call (level)[0];\n\t\t\t}\n\t\t}");
			}
			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", "\n\t\t//End loop\n\t\tpublic Player (string playerName, string className, int level){\n\t\t\tthis.playerName = playerName;\n\t\t\tthis.className = className;\n\n\t\t\t//Need to account for different OS for directory\n\t\t\tstring fileName = System.IO.Directory.GetCurrentDirectory () + \"/Classes/\" + className + \".lua\";");
			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", "luaState = new Lua ();\n\t\t\tluaState.LoadCLRPackage();\n\t\t\tluaState.DoFile (fileName);\n\n\t\t\tthis.level = level;\n\t\t}\n\t\tprivate void calculateStats(){\n\t\t\t//Just to trigger the lua functions\n\t\t");

			foreach (string element in stat)
			{
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", element + " = 0;\n\t\t");

			}
			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", "}");
			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/Player.cs", "public int actionAttack(ref Player adversary){\n\t\t\tLuaFunction luaFunct = luaState [\"setAdversary\"] as LuaFunction;luaFunct.Call (adversary.attack, adversary.magic, adversary.defence);\n\t\t\tluaFunct = luaState [\"actionAttack\"] as LuaFunction;return (int)(double)luaFunct.Call ()[0];\n\t\t}\n\t\tpublic void levelUp(){\n\t\t\tlevel = level + 1;\n\t\t\tcalculateStats ();\n\t\t}\n\t}\n}");
		}

		private void writeLua(List<string> classes, List<string> stats)
		{
			foreach (string element in classes)
			{
				//Player Table
				System.IO.File.WriteAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "Player = {");
				foreach (string elementS in stats)
				{
					System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "\n\t" + elementS + " = 0,");
				}
				//Removes the last comma
				System.IO.FileStream fs = new System.IO.FileStream(System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
				fs.SetLength(fs.Length - 1);
				fs.Close();
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "\n}\n\n");

				//Adversary Table
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "Adversary = {");
				foreach (string elementS in stats)
				{
					System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "\n\t" + elementS + " = 0,");
				}
				//Removes the last comma
				fs = new System.IO.FileStream(System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
				fs.SetLength(fs.Length - 1);
				fs.Close();
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "\n}\n\n");
				//Set Adversary Function
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "function setAdversary (");
				foreach (string elementS in stats)
				{
					System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", elementS + ", ");
				}
				fs = new System.IO.FileStream(System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
				fs.SetLength(fs.Length - 2);
				fs.Close();
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", ")");
				foreach (string elementS in stats)
				{
					System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "\n\tAdversary." + elementS + " = " + elementS);
				}
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "\nend\n\n");
				foreach (string elementS in stats)
				{
					System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "function stat" + elementS + "(lvl)\n\tPlayer." + elementS + " = (3 * lvl)\n\treturn Player." + elementS + "\nend\n\n");
				}

				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/" + element + ".lua", "function actionAttack ()\n\treturn (2 * Player." + stats[0] + ") - Adversary." + stats[1] + "\nend");
			}

			/*
			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/class.lua", "Player = {\n--Loop here\n\t" + element + "= 0,\n\t" + element + "= 0\n--End loop\n}\n");
			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/class.lua", "Adversary = {\n--Loop here\n\t" + element + "= 0, \n\t" + element + " = 0\n--End loop\n}\n\n--Loop here\n'stat'base = 0;\n--End loop");

			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/class.lua", "function setAdversary (element[0], element[1])\n--Look into getting those the same way we get Player stats, maybe through something like dofile()\n--This would allow for greater number of stats\n\tAdversary.attack = attack\n\tAdversary.magic = magic\n\tAdversary.defence = defence\nend");

			foreach (string element in classes)
			{
				System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/class.lua", "--Loop here\nfunction stat element (lvl)\n\tPlayer.element = elementBase + (3 * lvl)\n\treturn Player.element\nend\n--End loop");
			}
			System.IO.File.AppendAllText(@System.IO.Directory.GetCurrentDirectory() + "/Game/class.lua", "function actionAttack ()\n\treturn (2 * Player.attack) - Adversary.defence\nend");
			*/
		}
		public ClassCreation(string nmspc)
		{
			this.nmspc = nmspc;
		}
		public void create()
		{
			//string ClassName = null;
			List<string> Class = new List<string>();
			List<string> Stat = new List<string>();
			//string Stat1 = null;
			//string Stat2 = null;
			//string Stat3 = null;

			Console.WriteLine("Welcome to Class Creation. For a list of commands for this module, input 'CreatureHelp'. \n");

			string ModuleChoice = Console.ReadLine();
			//string AnotherClass = null;
			//cin >> ModuleChoice;

			while (ModuleChoice != "Return")
			{
				if (ModuleChoice == "CreateClass")
				{
					Console.WriteLine("How many classes do you want to create? ");
					int ClassNumber = Convert.ToInt32(Console.ReadLine());
					Console.WriteLine("How many stats do you want your class(es) to have? ");
					int StatNumber = Convert.ToInt32(Console.ReadLine());

					for (int cnt = 0; cnt < StatNumber; ++cnt)
					{
						Console.WriteLine("What do you want to name stat number " + (cnt + 1) + "? ");
						Stat.Add(Console.ReadLine());
					}
					for (int cnt = 0; cnt < ClassNumber; ++cnt)
					{
						Console.WriteLine("What do you want to name class number " + (cnt + 1) + "? ");
						Class.Add(Console.ReadLine());
					}
					writeClass(Stat);
					writeLua(Class, Stat);

					Console.WriteLine("As you are done creating classes, input 'Return' to return to the main menu.");
					ModuleChoice = Console.ReadLine();
				}
			}
		}
	}
}
