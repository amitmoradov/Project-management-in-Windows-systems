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
        string[] tasks = new string[]
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
        string[] ditailsTasks = new string[]
        {
            "Conduct research and analyze traffic patterns for vehicles.",
            "Evaluate potential locations suitable for charging stations.",
            "Develop a smart communication system for the project.",
            "Assess the impact of the project on air pollution levels.",
            "Plan and establish an advanced energy system.",
            "Construct charging stations according to the project plan.",
            "Test and verify safety standards applicable to the project.",
            "Collaborate with manufacturers of electric vehicles.",
            "Develop a comprehensive budgetary and implementation plan.",
            "Establish an effective data management system.",
            "Implement a smart traffic management system.",
            "Create a system for roadway maintenance and monitoring.",
            "Provide support services for drivers during charging.",
            "Develop a smart traffic control system for the project.",
            "Install advanced energy stations at selected sites.",
            "Build a communication system between electric vehicles.",
            "Create a project management and oversight plan.",
            "Review budgetary compliance and planned work scope.",
            "Develop a system for managing vehicle traffic.",
            "Assess adherence to the project's budget.",
            "Develop infrastructure support for offices dealing with electric vehicles.",
            "Examine infrastructure support for offices dealing with electric vehicles."
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
        foreach (var task in tasks)
        {
            // Get the difficulty of the task
            _copmliexity = (EngineerExperience)e_rand.Next(0, 4);

            int _engineerId = 
            DateTime? _deadLine = null;
            Task item = new(, _engineerId,);
            e_dalTask.Create(item)
        }
    }

}
