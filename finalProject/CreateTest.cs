using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject
{
    public partial class CreateTest : Form
    {
        public string FilePath { get; set; } = "tests.json";
        public string Name { get; set; }
        public CreateTest()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            Name = textBox1.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Name != null)
            {
                
                if(File.Exists(FilePath))
                {
                    string read = File.ReadAllText(FilePath);
                    List<SaveTest> questions = JsonConvert.DeserializeObject<List<SaveTest>>(read);
                    foreach (SaveTest s in questions)
                    {
                        if (s.Name == Name)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    List<SaveTest> l= new List<SaveTest>();
                    string json1 = JsonConvert.SerializeObject(l);
                    File.WriteAllText(FilePath, json1);
                }
                KindOfQuestion q = new KindOfQuestion(Name);
                Hide();
                q.Show();
                TestToJson();
                
            }
        }
        public void TestToJson()
        {
            string read = File.ReadAllText(FilePath);
            List<SaveTest> questions = JsonConvert.DeserializeObject<List<SaveTest>>(read);
            SaveTest saveTest = new SaveTest(Name);
            questions.Add(saveTest);
            string json = JsonConvert.SerializeObject(questions);
            File.WriteAllText(FilePath, json);
        }
    }
}
