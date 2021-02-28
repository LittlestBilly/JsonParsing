﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace JsonParsing
{
    public class Step
    {   //Task Step parameters
        public string stepname { get; set; }
        public int stepstate { get; set; }
        public int priority { get; set; }
    }

    public class Task
    {
        //Json parameters
        public string name { get; set; }
        public string notes { get; set; }
        public List<Step> steps { get; set; }

    }


    class Parser
    {
        //Json file path
        private string path;

        public Parser(string pathToJson)
        {
            this.path = pathToJson;
        }

        public string readJson()
        {
            return @File.ReadAllText(this.path);
        }

        //Rewrites JSON file
        public void reWrite(string newJson)
        {
            File.WriteAllText(this.path, newJson);
        }

        //Deserializes taskList
        public List<Task> deserializeTask(string raw)
        {
            List<Task> taskList = JsonConvert.DeserializeObject<List<Task>>(raw);
            return taskList;
        }
        //Serializes TaskList
        public string serializeTask(List<Task> taskList)
        {

            string serialized = JsonConvert.SerializeObject(taskList, Formatting.Indented);

            return serialized;
        }

        //Step priority Enum
        enum Priority
        {
            Low = 1,
            Medium = 2,
            High = 3

        }

        //Step progress Enum
        enum Progress
        {
            Pending = 1,
            InProgress = 2,
            Done = 3
        }



        //Method to read out Json content
        public void fromJson()
        {
            //Making a list out of the Json string
            List<Task> taskList = deserializeTask(readJson());
            //Looping trough the contents
            for (int i = 0; taskList.Count > i; i++)
            {
                Console.WriteLine(taskList[i].name);
                Console.WriteLine(taskList[i].notes);
                for (int x = 0; taskList[i].steps.Count > x; x++)
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine(taskList[i].steps[x].stepname);
                    var ss = (Progress)taskList[i].steps[x].stepstate;
                    Console.WriteLine(ss);
                    var p = (Priority)taskList[i].steps[x].priority;
                    Console.WriteLine(p);
                }
                Console.WriteLine("___________________________________");

            }
        }

        //Method to help make new steps and returns the step as a list.
        public Step makeStep(string makeStepname, int makePriority)
        {
            //Creates the Step object
            Step newStep = new Step
            {
                stepname = makeStepname,
                stepstate = 1,
                priority = makePriority
            };

            return newStep;
        }

        //Creates a list and adds the object to the List
        public List<Step> makeStepList(Step newStep)
        {

            List<Step> stepList = new List<Step>();
            stepList.Add(newStep);
            return stepList;
        }



        //Method to add steps to an existing task
        public void addSteps(String taskName, string addStepname, int addPriority)
        {

            List<Task> taskList = deserializeTask(readJson());
            int index = 0;
            for (int i = 0; taskList.Count - 1 >= i; i++)
            {
                if (taskList[i].name.Equals(taskName))
                {

                    Console.WriteLine("Item Found!!!");
                    index = i;
                    break;
                }
            }

            taskList[index].steps.Add(makeStep(addStepname, addPriority));

            reWrite(serializeTask(taskList));


        }

        //Method to delete steps from task
        public void removeStep(string taskName, string stepname)
        {
            List<Task> taskList = deserializeTask(readJson());

            foreach (Task task in taskList)
            {
                if (task.name.Equals(taskName))
                {
                    foreach (Step step in taskList[0].steps)
                    {
                        if (step.stepname.Equals(stepname))
                        {
                            taskList[0].steps.Remove(step);
                            break;
                        }
                    }
                    break;
                }
            }

            reWrite(serializeTask(taskList));

        }



        //Method to add something to json file
        public void addTask(string addName, string addNotes, List<Step> addSteps)
        {

            //Deserializing the json string as a list so we can add new task;
            List<Task> taskList = deserializeTask(readJson());
            //Creating new Task object with parameters that we want to add

            Task added = new Task
            {
                name = addName,
                notes = addNotes,
                steps = addSteps
            };
            //Adding the new object to the list
            taskList.Add(added);


            //Rewrites Json
            reWrite(serializeTask(taskList));

        }



    }
}
