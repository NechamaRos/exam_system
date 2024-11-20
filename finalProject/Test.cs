using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Test
    {
        public string Name { get; set; }
        public List<Questions> questions { get; set; }= new List<Questions>();
        public int TestScore { get; set; } = 0;

        //public Test(string name)
        //{
        //    Name = name;
        //}
    }
}
