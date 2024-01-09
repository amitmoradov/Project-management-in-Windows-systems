namespace DalTest;
using DalApi;
using DO;
using Dal;
using System;
using System.Data.Common;

/// <summary>
/// 
/// </summary>
public static class Initialization
{
    private static IDependency? e_dalDependency;
    private static ITask? e_dalTask;
    private static IEngineer? e_dalEngineer;
    private static readonly Random e_rand = new Random();

    private static void createEngineer()
    {
        const int MIN = 200000000;
        const int MAX = 400000000;

        // Array containing engineers' names
        string[] engineerNames = { "John", "Alice", "Bob", "Eva", "David", "Sophia", "Michael", "Olivia", "Daniel",
        "Emma", "William", "Ava", "Matthew", "Emily", "Christopher", "Mia",
        "Nicholas", "Grace", "Andrew", "Lily" };

        // Create an email address for the engineer
        string _gmailSuffix = "@gmail.com";

        // convert the number mail example: amit12@gmail.com
        string _uniqueMail = e_rand.Next(11, 99).ToString();
        string _emailAddress = engineerNames + _uniqueMail + _gmailSuffix;
        // Loop through each engineer in the array

        foreach (var engineerName in engineerNames)
        {
            // Generate a random ID for the engineer
            int _id = e_rand.Next(MIN, MAX);

            // chack if the id is not exsist
            while (e_dalEngineer?.Read(_id) is not null)
            {
                _id = e_rand.Next(MIN, MAX);
            }

            // Randomly choose an experience level for the engineer
            EngineerExperience _level = new EngineerExperience();

            // Sets a random number in the amount of enum EngineerExperience items
            _level = (EngineerExperience)e_rand.Next(0, 4);

            // Variable for the engineer's salary, to be updated based on experience level
            double? _cost = 0;

            // Assign salary based on the experience level
            switch (_level)
            {
                case EngineerExperience.Beginner:
                    _cost = 120;
                    break;
                case EngineerExperience.AdvancedBeginner:
                    _cost = e_rand.Next(150, 200);
                    break;
                case EngineerExperience.Intermediate:
                    _cost = e_rand.Next(200, 250);
                    break;
                case EngineerExperience.Advanced:
                    _cost = e_rand.Next(250, 300);
                    break;
                case EngineerExperience.Expert:
                    _cost = e_rand.Next(300, 350);
                    break;
            }

            Engineer new_engineer = new(_id, _cost, _level, _emailAddress, engineerName);
            e_dalEngineer?.Create(new_engineer);
        }
    }

    private static void createTaks()
    {
        string[] description = new string[]
        {
            "Research and Analysis of Vehicle Traffic",
            "Evaluation of Potential Charging Station Locations",
            "Development of Smart Communication System",
            "Assessment of Project Impact on Air Pollution Levels",
            "Planning and Establishment of Advanced Energy System",
            "Construction of Charging Stations",
            "Testing and Verification of Safety Standards",
            "Collaboration with Electric Vehicle Manufacturers",
            "Development of Budgetary and Implementation Plan",
            "Establishment of Data Management System",
            "Implementation of Smart Traffic Management System",
            "Creation of Roadway Maintenance and Monitoring System",
            "Provision of Driver Support Services",
            "Development of Smart Traffic Control System",
            "Installation of Advanced Energy Stations",
            "Building a Communication System Between Vehicles",
            "Creation of Project Management and Oversight Plan",
            "Budgetary Compliance and Planned Work Scope Review",
            "Development of Vehicle Traffic Management System",
            "Assessment of Project Budget Adherence",
            "Development of Infrastructure Support for Electric Vehicle Offices",
            "Examination of Infrastructure Support for Electric Vehicle Offices"
        };
        string[] alias = new string[]
      {
           "TRA",
            "CSE",
            "SCS",
            "APIA",
            "AESP",
            "CSC",
            "SST",
            "EVMC",
            "BIP",
            "DMS",
            "STMI",
            "RMS",
            "DSP",
            "STC",
            "AESI",
            "VCSB",
            "PMPC",
            "BCSR",
            "VTSM",
            "PBAA",
            "EOID",
            "EOISE"
      };
        string[] tasksremarks = new string[]
        {
            "Gathering data and analyzing vehicular movement.",
            "Inspecting and assessing suitable charging station positions.",
            "Creating an intelligent communication infrastructure.",
            "Examining the project's effect on air quality.",
            "Planning and implementing an advanced energy structure.",
            "Building facilities for electric vehicle charging.",
            "Conducting testing procedures to verify safety measures.",
            "Engaging in collaboration with electric vehicle manufacturers.",
            "Creating a comprehensive budget and execution strategy.",
            "Setting up a system for efficient data management.",
            "Integrating intelligent traffic control systems.",
            "Establishing a protocol for road maintenance.",
            "Offering assistance to drivers during charging processes.",
            "Designing an intelligent traffic control mechanism.",
            "Setting up installations for advanced energy sources.",
            "Creating a communication platform for vehicles.",
            "Formulating a detailed project management strategy.",
            "Evaluating adherence to budget and project scope.",
            "Overseeing the operation of vehicular traffic systems.",
            "Evaluating compliance with the project's budget.",
            "Creating infrastructure for electric vehicle office spaces.",
            "Reviewing support systems for electric vehicle office spaces."
        };

        EngineerExperience _copmliexity = new EngineerExperience();
        for (int i = 0; i < description.Length; i++)
        {

            // Get the difficulty of the task
            _copmliexity = (EngineerExperience)e_rand.Next(0, 4);

            DateTime _startDate = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - _startDate).Days;
            DateTime _randomDate = _startDate.AddDays(e_rand.Next(range));

            DateTime _completeDate = new DateTime(2024, 1, 1);
            TimeSpan _requiredEffortTime = _completeDate - _startDate;

            Task item = new(_randomDate, _requiredEffortTime, _copmliexity,_startDate, null, _completeDate, null, alias[i], description[i], null, tasksremarks[i]);
            e_dalTask?.Create(item);
        }
    }

    private static void createDependency()
    {
        int _dependentTask = 0;
        int _dependsOnTask = 0;

        // Have a 40 Tasks .
        for (int i = 1; i <= 40; i++)
        {
            // We must that have at least 2 task with same dependent (3 & 1 -> 1) .
            if (i % 2 != 0 && i < 4)
            {
                _dependentTask = i + 2;
                _dependsOnTask = i % 2;
            }
            else
            {
                do
                {
                    _dependentTask = e_rand.Next(1, 23);
                    _dependsOnTask = e_rand.Next(1, 22);

                }
                // The dependentTask must be bigger from dependsOnTask .
                while (_dependsOnTask >= _dependentTask);
                
            }
            Dependency item = new(_dependentTask , _dependsOnTask);
            e_dalDependency?.Create(item);
        }

            
    }

    public static void Do(IDependency? dalDependency, ITask? dalTask , IEngineer? dalEngineer)
    {
        e_dalDependency = dalDependency ?? throw new NullReferenceException("Dependency can not be null!");
        e_dalEngineer = dalEngineer ?? throw new NullReferenceException("Engineer can not be null!");
        e_dalTask = dalTask ?? throw new NullReferenceException("Task can not be null!");
        createDependency();
        createEngineer();
        createTaks();
    }
}

