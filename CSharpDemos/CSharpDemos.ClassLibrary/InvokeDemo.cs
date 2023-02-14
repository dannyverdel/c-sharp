using System;
namespace CSharpDemos.ClassLibrary.DatabaseDemo
{
	public class InvokeDemo
	{
		public async Task InvokeMethod()
		{
			Database db1 = Database.GetDatabase();
			string data = await db1.GetData();
			data.Dump();
		}
	}

	public class Database
	{
		private static Database? _database;
		private static readonly object _lock_obj = new object();

		private Database() { }

		public static Database GetDatabase()
		{
			lock (_lock_obj)
				if (_database == null)
					_database = new Database();

			return _database;
		}

		public async Task<string> GetData()
		{
			await Task.Delay(10000);
			return "data from database";
		}
	}
}

