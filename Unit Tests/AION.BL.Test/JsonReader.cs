using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test
{
    public class JsonReader<OutputType>
    {
        public static List<OutputType> GetData(string fileName,string appPath = "System.AppContext.BaseDirectory")
        {
            try
            {
                string readContents;
                string filepath;
                if (appPath == "System.AppContext.BaseDirectory")
                    filepath = System.AppContext.BaseDirectory + "\\" + fileName;
                else
                    filepath = fileName;

                //   File.SetAttributes(AppContext.BaseDirectory + ConfigurationManager.AppSettings["MappingFile"], FileAttributes.Normal);
                using (StreamReader streamReader = new StreamReader(filepath, Encoding.UTF8))
                {
                    readContents = streamReader.ReadToEnd();
                }

                List<OutputType> mMMFProjects = JsonConvert.DeserializeObject<List<OutputType>>(readContents);
                if (mMMFProjects == null)
                    mMMFProjects = new List<OutputType>();
                return mMMFProjects;
            }
            catch
            {
                return new List<OutputType>();
            }
        }
    }
}
