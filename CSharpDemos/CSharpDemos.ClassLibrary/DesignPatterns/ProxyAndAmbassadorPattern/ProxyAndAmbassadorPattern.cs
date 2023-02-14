using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.ProxyAndAmbassadorPattern
{
    /*
    * The Proxy design pattern is a structural pattern that provides a substitute or placeholder for another object to control access to it. 
    * This is useful when you want to add a level of abstraction between the client and the real object that it wants to access.
    * 
    * For example, imagine you have a slow, resource-intensive service, such as a database, 
    * that you want to access. Instead of accessing the database directly, you can create a proxy that acts as an intermediary between the client and the database. 
    * The proxy can perform various tasks, such as caching, logging, or security checks, before accessing the database.
    * 
    * In this example, the Client class accesses the Database through a DatabaseProxy. 
    * The DatabaseProxy class implements the same interface as the Database class and acts as an intermediary, 
    * logging a message to the console before accessing the database.
    */

    public class InvokeProxyAndAmbassadorPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            IDatabase database = new DatabaseProxy();
            database.GetData().Dump();
        }
    }

    public interface IDatabase
    {
        string GetData();
    }

    public class Database : IDatabase
    {
        public string GetData() => "Data from the database";
    }

    public class DatabaseProxy : IDatabase
    {
        private Database _database = new Database();

        public string GetData()
        {
            Console.WriteLine("Accessing the database...");
            return _database.GetData();
        }
    }

    public class Client
    {
        static void Main()
        {
            IDatabase database = new DatabaseProxy();
            Console.WriteLine(database.GetData());
        }
    }
}