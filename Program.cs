using System;
using System.ComponentModel;
using System.Numerics;

namespace Config
{
    class Config
    {
       
        public static void Main(string[] args)
        {
            ConfigManager config = new ConfigManager("saves");
            Vector3 vector = new Vector3();
            vector.X = 211;
            vector.Y = 512;
            vector.Z = 100;
            config.CreateConfigDirectory("data");
            Thread.Sleep(100);
            Console.WriteLine("Press ENTER to write\nPress SPACE to load\nPress BACKSPACE to clear");
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                config.writeValue("hello", 256.52821, "data");
                config.writeValue("lol", "this is a string", "data");
                config.writeValue("vector", vector, "data");
                Console.WriteLine("Wrote 3  values to: " + config.GetDataFile("data"));
            }
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                Console.WriteLine(config.ReadFloat("hello", "data"));
                Console.WriteLine(config.ReadString("lol", "data"));
                Console.WriteLine(config.ReadVector("vector", "data"));
                Console.WriteLine("Read 3 values!");
            }
            if (Console.ReadKey(true).Key == ConsoleKey.Backspace) 
            {
                config.ClearData("data");
                Console.WriteLine("Cleared all data!");
            }
            Console.Read();
        }
    }
}