using System;
using System.Resources;
using System.Configuration;

namespace Lesson_8.Homework_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourceManager rm = new ResourceManager("Lesson_8.Homework_1.Properties.TextResources", typeof(Program).Assembly);

            var cm = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = cm.AppSettings.Settings;

            if (settings["Name"] == null)
            {
                Console.Write(rm.GetString("NamePrompt") + ": ");
                string name = Console.ReadLine();
                settings.Add(new KeyValueConfigurationElement("Name", name));
            }
            else
                Console.WriteLine(rm.GetString("Greeting"), settings["Name"].Value);

            if (settings["Age"] == null)
            {
                Console.Write(rm.GetString("AgePrompt") + ": ");
                string name = Console.ReadLine();
                settings.Add(new KeyValueConfigurationElement("Age", name));
            }
            else
                Console.WriteLine(rm.GetString("AgePrefix") + ": ", settings["Age"].Value);

            if (settings["Business"] == null)
            {
                Console.Write(rm.GetString("BusinessPrompt") + ": ");
                string name = Console.ReadLine();
                settings.Add(new KeyValueConfigurationElement("Business", name));
            }
            else
                Console.WriteLine(rm.GetString("BusinessPrefix") + ": ", settings["Business"].Value);

            cm.Save(ConfigurationSaveMode.Modified);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

}
