using System;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;

namespace JsonParsing
{
    class Program
    {
        static void Main(string[] args)
        {







            //Testing the parsing and reading the json string from json file
            /*string path = @"C:\Users\Roope\source\repos\JsonParsing\JsonParsing\task.json";
                    string read = @File.ReadAllText(path);
            */
            Parser pobj = new Parser(@"C:\Users\Roope\source\repos\JsonParsing\JsonParsing\task.json");

            //pobj.addSteps("TaskPad", "Add editing features to Parser" , 3);
            pobj.removeStep("TaskPad", "Add editing features to Parser");
         /*   
            File.WriteAllText(path,
                pobj.addTask(read, "New test task with priority testing", "Testing taskadding after making the step method", pobj.makeStep("TestStep", false, 1))
                );
         */
            //string newerread = @File.ReadAllText(@"C:\Users\Roope\source\repos\JsonParsing\JsonParsing\task.json");

            //pobj.fromJson(newerread);
            //Console.WriteLine(pobj.addTask(read, "New Test Task", "Notes for New test Task"));

          /*
            string tempread = @File.ReadAllText(@"C:\Users\Roope\source\repos\JsonParsing\JsonParsing\task.json");
            File.WriteAllText(path, pobj.addTask(tempread, "New Test Task number 2", "Notes for New test Task number 2"));

            string newread = @File.ReadAllText(@"C:\Users\Roope\source\repos\JsonParsing\JsonParsing\task.json");
            pobj.fromJson(newread);
           */
        }
    }
}
