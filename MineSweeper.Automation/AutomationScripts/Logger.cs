using System;
using System.IO;
using static MineSweeper.Automation.Tests;

namespace MineSweeper.Automation
{
    class Logger
    {
        public void Log(string message, AutomationTestType testType)
        {
            string filePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Outputs", testType.ToString() + ".txt");
                 
            using StreamWriter streamWriter = new StreamWriter(filePath, append: true);

            streamWriter.WriteLine(message);
            streamWriter.Close();
        }
    }
}
