using System;
using NLua;

namespace LuaClasses
{
	public class Player
	{
		private Lua luaState;
		private int _attack;
		private int _magic;
		private int _defence;
		private int _level;
		public string playerName;
		public string className;
		public int level{
			get{
				return _level;
			}
			private set{
				if (value > 0)
					_level = value;
				calculateStats ();
			}
		}
		public int attack {
			get{
				return _attack;
			}
			private set {
				int modifiers = 5;	//Just a place holder. Should be calculated from gear
				LuaFunction luaFunct = luaState ["statAttack"] as LuaFunction;
				_attack = (int)(double)luaFunct.Call (level, modifiers)[0];
				//InvalidCastException
				//Does not exist
			}
		}
		public int magic {
			get{
				return _magic;
			}
			private set{
				int modifiers = 5;	//Just a place holder. Should be calculated from gear
				LuaFunction luaFunct = luaState ["statMagic"] as LuaFunction;
				_magic = (int)(double)luaFunct.Call (level, modifiers)[0];
			}
		}
		public int defence {
			get{
				return _defence;
			}
			private set{
				int modifiers = 5;	//Just a place holder. Should be calculated from gear
				LuaFunction luaFunct = luaState ["statDefence"] as LuaFunction;
				_defence = (int)(double)luaFunct.Call (level, modifiers)[0];
			}
		}
		public Player (string playerName, string className, int level){
			this.playerName = playerName;
			this.className = className;

			//Need to account for different OS for directory
			string fileName = System.IO.Directory.GetCurrentDirectory () + "/Classes/" + className + ".lua";
			luaState = new Lua ();
			luaState.LoadCLRPackage();
			luaState.DoFile (fileName);

			this.level = level;
		}
		private void calculateStats(){
			//Just to trigger the lua functions
			attack = 0;
			magic = 0;
			defence = 0;
		}
		public int actionAttack(ref Player adversary){
			LuaFunction luaFunct = luaState ["setAdversary"] as LuaFunction;
			luaFunct.Call (adversary.attack, adversary.magic, adversary.defence);
			luaFunct = luaState ["actionAttack"] as LuaFunction;
			return (int)(double)luaFunct.Call ()[0];
		}
		public void levelUp(){
			level = level + 1;
			calculateStats ();
		}
		public string getStats(){
			string stats = "Name: " + playerName;
			stats += "\nClass: " + className;
			stats += "\nAttack: " + attack;
			stats += "\nMagic: " + magic;
			stats += "\nDefence: " + defence;
			return stats;
		}
	}
}

