using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Questions
    {
        public string Description { get; set; }
        public int Score { get; set; }
        public string TestName { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public List<string> Answer { get; set; } = new List<string>();
        public string Type { get; set; }
         public Questions(string description, int score, string testName, List<string> options, List<string> answer, string type)
         {
            Description = description;
            Score = score;
            TestName = testName;
            Options = options;
            Answer = answer;
            Type = type;
         }
        
    }
    
}
