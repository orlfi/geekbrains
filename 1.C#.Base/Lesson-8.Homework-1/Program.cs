using System;
using System.Resources;
using System.Configuration;

namespace Lesson_8.Homework_1
{
    class Program
    {
        static readonly ResourceManager rm = new ResourceManager("Lesson_8.Homework_1.Properties.TextResources", typeof(Program).Assembly);
        static readonly Configuration cm = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        static readonly KeyValueConfigurationCollection settings = cm.AppSettings.Settings;


        static void Main(string[] args)
        {
            ReadOrAdd("Name", "NamePrompt", "Greeting");

            ReadOrAdd("Age", "AgePrompt", "AgePrefix");

            ReadOrAdd("Business", "BusinessPrompt", "BusinessPrefix");

            cm.Save(ConfigurationSaveMode.Modified);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        static void ReadOrAdd(string name, string prompt, string prefix)
        {
            if (settings[name] == null)
            {
                Console.Write(rm.GetString(prompt) + ": ");
                string text = Console.ReadLine();
                settings.Add(new KeyValueConfigurationElement(name, text));
            }
            else
                Console.WriteLine(rm.GetString(prefix) + ": ", settings[name].Value);
        }
    }

}
