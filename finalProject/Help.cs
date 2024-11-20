
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Help
    {
        string pathOfQuestion = "data.json";
        public List<List<Questions>> ReadQuestionFromJson()
        {
            string read=File.ReadAllText(pathOfQuestion);
            var list= JsonConvert.DeserializeObject<List<List<Questions>>>(read);
            return list;
        }
        public void WriteQuestionToJson(List<List<Questions>> list)
        {
            string write=JsonConvert.SerializeObject(list);
            File.WriteAllText(pathOfQuestion, write);
        }
    }
}
