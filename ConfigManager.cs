using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Newtonsoft.Json.Linq;

namespace Config
{
    public class ConfigManager
    {
        public string nameOfDirectory;

        public ConfigManager(string nameOfDirectory) { this.nameOfDirectory = nameOfDirectory; }
        public void CreateConfigDirectory(string fileName)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeName = AppDomain.CurrentDomain.FriendlyName;
            string filePath = strExeFilePath.Replace(exeName + ".dll", string.Empty);
            string configDir = filePath + nameOfDirectory;
            if (!Directory.Exists(configDir)) 
            {
                Directory.CreateDirectory(configDir);
                var file = File.Create(configDir + @"\" + fileName + ".txt");
                file.Close();
            }
        }

        public string GetDirectory()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeName = System.AppDomain.CurrentDomain.FriendlyName;
            string filePath = strExeFilePath.Replace(exeName + ".dll", string.Empty);
            string configDir = filePath + nameOfDirectory;
            return configDir;
        }

        public string GetDataFile(string fileName)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeName = AppDomain.CurrentDomain.FriendlyName;
            string filePath = strExeFilePath.Replace(exeName + ".dll", string.Empty);
            string configDir = filePath + nameOfDirectory + @"\" + fileName + ".txt";
            return configDir;
        }
        
        public void ClearData(string fileName)
        {
            File.WriteAllText(GetDataFile(fileName), string.Empty);
        }

        public void WriteValue<T>(string nameValue, T value, string fileName)
        {
            File.AppendAllText(GetDataFile(fileName), nameValue + ":" + value + "#");
        }

        public T ReadValue<T>(string nameValue, string fileName)
        {
            string data = File.ReadAllText(GetDataFile(fileName));
            string[] limiter = data.Split("#");
            foreach (var line in limiter)
            {
                string[] value = line.Split(":");
                if (value[0] == nameValue)
                {
                    switch (typeof(T).Name)
                    {
                        case "Int32":
                            return (T)Convert.ChangeType(int.Parse(value[1].Trim()), typeof(T));
                        case "String":
                            return (T)Convert.ChangeType(value[1].Trim(), typeof(T));
                        case "Single": // this is a float, C# why do you call floats SINGLES ?
                            return (T)Convert.ChangeType(float.Parse(value[1].Trim()), typeof(T));
                        case "Vector3":
                            // scuffed but works 
                            string vec = value[1].Replace("<", "");
                            string parse = vec.Replace(">", "");
                            string[] vec3 = parse.Split(",");
                            Vector3 vector = new Vector3();
                            vector.X = float.Parse(vec3[0]);
                            vector.Y = float.Parse(vec3[1]);
                            vector.Z = float.Parse(vec3[2]);
                            return (T)Convert.ChangeType(vector, typeof(T));
                        case "ArrayList": // TODO: Implement arrays
                            break;
                        default:
                            throw new Exception("Invalid type value: " + typeof(T).Name);
                    }
                }
            }
            return default(T);
        }
    }
}
