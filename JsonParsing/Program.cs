﻿using System;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;

namespace JsonParsing
{
    class Program
    {
        static void Main(string[] args)
        {







            
            Parser pobj = new Parser(@"C:\Users\Roope\source\repos\JsonParsing\JsonParsing\task.json");

            //pobj.addTask("TaskPad2", "Notes", pobj.makeStepList(pobj.makeStep("Test1", 0)));
            pobj.addSteps("New test task with priority testing", "TestStep2", 0);

            //  pobj.removeTask("TaskPad2");
            //pobj.removeStep("TaskPad2", "Test1");
            pobj.addSteps("TASkPAd2", "TEST2", 0);
            pobj.fromJson();

           

            //pobj.editTask("TaskPad", "TaskPad", "Make an App to manage projects/work in C#");
            //pobj.removeTask("New test task with priority testing2");

            //pobj.addSteps("TaskPad", "Add editing features to Parser" , 3);
            // pobj.removeStep("New test task with priority testing", "TestStep2");

            //pobj.editStep("TaskPad", "Make new project and create github repository", "Make new project and create github repository!!!", 1, 3);


         
        }
    }
}
