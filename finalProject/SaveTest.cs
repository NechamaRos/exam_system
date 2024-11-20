using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class SaveTest
    {
        public string Name { get; set; }
        public bool Status { get; set; } = false;
        public SaveTest(string name) 
        {
            Name = name;
        }
    }
}
