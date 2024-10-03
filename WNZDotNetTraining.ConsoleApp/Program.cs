// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using WNZDotNetTraining.ConsoleApp;

//Console.WriteLine("Hello, World!");
//Console.ReadLine(); => stop enter key


// md = markdown

// C#  => database

//ADO.NET
//Dapper
//EFCore / Entity Framework

// C# => sql query => 

//nuget

//Ctrl + .

//max connection = 100
//100 = 99
//101

//ADODotNet adoDotNetRun = new ADODotNet();
//Method Call
//adoDotNetRun.Read(); 
//adoDotNetRun.Create();
//adoDotNetRun.Edit();
//adoDotNetRun.Update();
//adoDotNetRun.Delete();

DapperExample dapperRun = new DapperExample();
//dapperRun.Read();
//dapperRun.Create("aung", "din", "ha ha");
//dapperRun.Edit(4);
//dapperRun.Update(6, "Mg Aung", "Water", " Drink");
dapperRun.Delete(17);

//EFCoreExample runEFCore = new EFCoreExample();
//runEFCore.Read();
//runEFCore.Create("Ha" , "Hla" , "Blar blar");
//runEFCore.Edit(9);
//runEFCore.Update(9 , "Hein Htet" , "Chrome" , "Chrome is blar blar");
//runEFCore.Delete(9);

Console.ReadKey();
