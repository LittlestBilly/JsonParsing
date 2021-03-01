﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace JsonParsing
{

    //Step priority Enum
    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2

    }

    //Step progress Enum
    public enum stepstate
    {
        Pending = 0,
        InProgress = 1,
        Done = 2
    }



    public class Step
    {   //Task Step parameters



        public string stepname { get; set; }
        public stepstate stepstate { get; set; }
        public Priority priority { get; set; }
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
                    //var ss = (stepstate)taskList[i].steps[x].stepstate;

                    Console.WriteLine(taskList[i].steps[x].stepstate);
                  //  var p = (Priority)taskList[i].steps[x].priority;
                    Console.WriteLine(taskList[i].steps[x].priority);
                }
                Console.WriteLine("___________________________________");

            }
        }

        //Method to help make new steps and returns the step as a list.
        public Step makeStep(string makeStepname, Priority makePriority)
        {
            
            //Creates the Step object
            Step newStep = new Step
            {
                stepname = makeStepname,
                stepstate = stepstate.Pending,
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
        public void addSteps(String taskName, string addStepname, Priority addPriority)
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
            int i = 0;
            foreach (Task task in taskList)
            {
                if (task.name.Equals(taskName))
                {
                    foreach (Step step in taskList[i].steps)
                    {
                        if (step.stepname.Equals(stepname))
                        {
                            taskList[i].steps.Remove(step);
                            break;
                        }
                    }
                    break;
                }
                i++;
            }

            reWrite(serializeTask(taskList));

        }

        //Method to edit step
        public void editStep(string taskName, string stepName, string newStepName, Priority newStepPriority, stepstate newStepState)
        {
            int i = 0;

            Step editedStep = new Step
            {
                stepname = newStepName,
                stepstate = newStepState,
                priority = newStepPriority

            };

            List<Task> taskList = deserializeTask(readJson());
            foreach (Task task in taskList)
            {
                if (task.name.Equals(taskName))
                {
                    foreach (Step step in taskList[i].steps)
                    {
                        if (step.stepname.Equals(stepName))
                        {
                            int si = taskList[i].steps.IndexOf(step);
                            taskList[i].steps[si] = editedStep;

                            break;
                        }
                    }
                    break;
                }
                i++;
            }

            reWrite(serializeTask(taskList));


        }

        //Method to edit task
        public void editTask(string taskName, string newTaskName, string newTaskNotes)
        {
            List<Task> taskList = deserializeTask(readJson());
            
            int i = 0;

            foreach(Task task in taskList)
            {
                if (task.name.Equals(taskName))
                {
                    i = taskList.IndexOf(task);

                    break;
                    
                }
            }
            List<Step> stepList = taskList[i].steps;
             
            Task newTask = new Task
            {
                name = newTaskName,
                notes = newTaskNotes,
                steps = stepList
            };

            taskList[i] = newTask;

            reWrite(serializeTask(taskList));


        }

        //Method to delete task
        public void removeTask(string taskname)
        {
            List<Task> taskList = deserializeTask(readJson());
            for (int i = 0; i < taskList.Count; i++)
            {
                Task task = taskList[i];
                if (task.name.Equals(taskname))
                {
                    taskList.Remove(task);
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
