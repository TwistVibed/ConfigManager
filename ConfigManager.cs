using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Config
{
    public class ConfigManager
    {

        public string nameOfDirectory;

        public ConfigManager(string nameOfDirectory) { this.nameOfDirectory = nameOfDirectory; }
        public void CreateConfigDirectory(string fileName)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeName = System.AppDomain.CurrentDomain.FriendlyName;
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

        public void writeValue<T>(string nameValue, T value, string fileName)
        {
            File.AppendAllText(GetDataFile(fileName), nameValue + ":" + value + "#");
        }

        public string ReadString(string nameValue, string fileName) 
        {
            string data = File.ReadAllText(GetDataFile(fileName));
            string[] limiter = data.Split("#");
            foreach (var line in limiter)
            {
                string[] value = line.Split(":");
                if (value[0] == nameValue) 
                {
                    return value[1];
                }
                
            }
            return string.Empty;
        }

        public int ReadInt(string nameValue, string fileName)
        {
            string data = File.ReadAllText(GetDataFile(fileName));
            string[] limiter = data.Split("#");
            foreach (var line in limiter)
            {
                string[] value = line.Split(":");
                if (value[0] == nameValue) 
                {
                    return int.Parse(value[1]);
                }
            }
            return 0;
        }


        public float ReadFloat(string nameValue, string fileName)
        {
            string data = File.ReadAllText(GetDataFile(fileName));
            string[] limiter = data.Split("#");
            foreach (var line in limiter)
            {
                string[] value = line.Split(":");
                if (value[0] == nameValue)
                {
                    return float.Parse(value[1]);
                }
            }
            return 0;
        }
        
        public Vector3 ReadVector(string nameValue, string fileName)
        {
            string data = File.ReadAllText(GetDataFile(fileName));
            string[] limiter = data.Split("#");
            foreach (var line in limiter)
            {
                string[] value = line.Split(":");
                if (value[0] == nameValue)
                {
                    string vec = value[1].Replace("<", "");
                    string parse = vec.Replace(">", "");
                    string[] vec3 = parse.Split(",");
                    Vector3 vector = new Vector3();
                    vector.X = float.Parse(vec3[0]);
                    vector.Y = float.Parse(vec3[1]);
                    vector.Z = float.Parse(vec3[2]);
                    return vector;
                }
            }
            return new Vector3();
        }

    }
}
