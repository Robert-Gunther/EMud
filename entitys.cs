//used mysql tutorial from the mono site
//used http://www.dotnettricks.com/learn/sqlserver/get-field-name-data-type-and-size-of-database-table
using System;
 using System.Data;
 using MySql.Data.MySqlClient;
using System.Collections.Generic;
 public class Test
 {
    public static void Main(string[] args){
		List<string>cmdlist=new List<string>();
		string command;
		string connectionString =
          "Server=localhost;" +
          "Database=entitys;" +
          "User ID=root;" +
          "Password= ;" +
          "Pooling=false";
       IDbConnection dbcon;
       dbcon = new MySqlConnection(connectionString);
       dbcon.Open();
       IDbCommand dbcmd = dbcon.CreateCommand();
	   
       string sql ="SELECT * FROM `EntityList`";
       dbcmd.CommandText = sql;
       IDataReader reader = dbcmd.ExecuteReader();
       while(reader.Read()) {
            cmdlist.Add( (string) reader["Name"]);
       }
	   reader.Close();
       reader = null;
       dbcmd.Dispose();
       dbcmd = null;
       dbcon.Close();
       dbcon = null;
		bool Continue=true;
		while(Continue==true){
			Console.WriteLine("->");
			command=Console.ReadLine();
			if (command=="help"){
				help();
			}
			else if (command=="CreateTable"){
				NewTable();
			}
			string[] cmd=command.Split();
			 if(cmd[0]=="new"){
					foreach (string item in cmdlist){
						if (item==cmd[1]){
							NewEntry(cmd[1]);
							break;
						}
					}
			 }
			if (command=="quit"){
				break;
			}
			
		}
	}
	public static void NewTable(){
		Console.WriteLine("Enter the New Entity");
		string Table=Console.ReadLine();
		string temp;
		string sql ="CREATE TABLE "+Table+"(";
		while(true){
			Console.WriteLine("Enter an attribute");
			temp=Console.ReadLine();
			sql=sql+temp+" varchar(100)";
			Console.WriteLine("Add more attributes?");
			temp=Console.ReadLine();
			if(temp=="no"){
				break;
			}
			sql=sql+",";
		}
	sql=sql+");";
		string connectionString =
          "Server=localhost;" +
          "Database=entitys;" +
          "User ID=root;" +
          "Password= ;" +
          "Pooling=false";
       IDbConnection dbcon;
       dbcon = new MySqlConnection(connectionString);
       dbcon.Open();
       IDbCommand dbcmd = dbcon.CreateCommand();
       
       dbcmd.CommandText = sql+" INSERT INTO `entitylist` (`Name`) VALUES ('"+Table+"');";
	   dbcmd.ExecuteReader();
       dbcmd.Dispose();
       dbcmd = null;
       dbcon.Close();
       dbcon = null;
	}
	public static void help(){
		//help code goes here
	}
	 public static void NewEntry(string Table)  {
		List<string> columns = new List<string>();
		List<string> columnsdata = new List<string>();
       string connectionString =
          "Server=localhost;" +
          "Database=entitys;" +
          "User ID=root;" +
          "Password= ;" +
          "Pooling=false";
       IDbConnection dbcon;
       dbcon = new MySqlConnection(connectionString);
       dbcon.Open();
       IDbCommand dbcmd = dbcon.CreateCommand();
       string sql =
           "SELECT column_name as 'Column_Name' "+"FROM information_schema.columns "+"WHERE table_name = '"+Table+"'";
       dbcmd.CommandText = sql;
       IDataReader reader = dbcmd.ExecuteReader();
       while(reader.Read()) {
            columns.Add( (string) reader["Column_Name"]);
       }
	   int x= new int();
	   x=0; 
	   string UserInput="";
	   sql="INSERT INTO `"+Table+"` (";
	   while(x<columns.Count){
		   Console.WriteLine("PLease enter"+ columns[x]);
		   UserInput=Console.ReadLine();
		   columnsdata.Add(UserInput);
		   sql=sql+"`"+columns[x]+"`";
		   
		   if(columns.Count>x+1){
			   sql=sql+", ";
		   }
		   x=x+1;
		   
	   }
	   x=0;
		sql=sql+") VALUES (";
		while(x<columnsdata.Count){
		   sql=sql+"'"+columnsdata[x]+"'";
		   if(columns.Count>x+1){
			   sql=sql+", ";
		   }
		   x=x+1;
	   }
		sql=sql+");";
       IDbCommand edit = dbcon.CreateCommand();
		edit.CommandText =sql;
       reader.Close();
		edit.ExecuteReader();
       // clean up
       reader = null;
       dbcmd.Dispose();
       dbcmd = null;
       dbcon.Close();
       dbcon = null;
    }
	public ModifyTable(){
		Console.WriteLine("How do you want to edit "+Table+"?");
		string Command=Console.ReadLine();
		string[] cmd=Command.Split();
		if (cmd[0]=="delete"){
			Delete(Table);
		}
		else if(cmd[0]=="remove"){
			Remove(cmd[1]);
		}
		else if(cmd[0]=="help"){
			MHelp();
		}
	}
	
}
 