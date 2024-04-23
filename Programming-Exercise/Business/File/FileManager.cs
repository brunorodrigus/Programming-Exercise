using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programming_Exercise.Model;

namespace Programming_Exercise.Business.File
{
    public class FileManager
    {
        private readonly string filePath = @"D:\Endpoints.txt";

        public void CreateFile(string content, bool newFile)
        {
            using (TextWriter tw = new StreamWriter(filePath, append: newFile))
            {
                tw.WriteLine(content);
            }
        }

        public bool DeleteFile()
        {
            System.IO.File.Delete(filePath);
            return true;
        }

        public List<EndPoint> GetFileContent()
        {
            if (System.IO.File.Exists(filePath))
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    try
                    {
                        string json = file.ReadToEnd();

                        var serializerSettings = new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        };

                        return JsonConvert.DeserializeObject<List<EndPoint>>(json, serializerSettings);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Problem reading file");

                        return null;
                    }

                }
            }
            else 
                return null;
        }
    }
}
