using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace finalProject
{
    public partial class KindOfQuestion : Form
    {
        string TestPath = "tests.json";
        Help help=new Help();
        public Test test { get; set; } = new Test();
        public int TestScore { get; set; }
        public KindOfQuestion(string name)
        {
            InitializeComponent();
            test.Name = name;
        }
        public KindOfQuestion(List<Questions> l)
        {
            InitializeComponent();
            test.questions = l;
            test.Name = l[0].TestName;
            int s = 0;
            foreach (Questions q in test.questions)
            {
                s += q.Score;
            }
            test.TestScore = s;
            TestScore = s;
            progressBar1.Value = TestScore;
            label1.Text = TestScore.ToString() + "%";
        }
        private void Add(object? sender, MyEventArgs e)
        {
            if (test.TestScore + e.Message <= 100)
            {
                test.TestScore += e.Message;
                progressBar1.Value = test.TestScore;
                label1.Text = test.TestScore.ToString() + "%";
                label2.Text = "";
            }
            else
            {
                label2.Text = "The question didn't saved because the precents of the test became more then 100";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (test.TestScore == 100)
            {
                label3.Text = "The test saved in status test";
                string read = File.ReadAllText(TestPath);
                List<SaveTest> questions = JsonConvert.DeserializeObject<List<SaveTest>>(read);

                IEnumerable<SaveTest> query = from q in questions
                                              where q.Name == test.Name
                                              select q;

                foreach (var item in query)
                {
                    item.Status = true;
                }
                string json1 = JsonConvert.SerializeObject(questions);
                File.WriteAllText(TestPath, json1);
            }
            else
                label3.Text = "The test saved in status draff";

            if (!File.Exists("data.json"))
            {
                List<List<Questions>> qq = new List<List<Questions>>();
                help.WriteQuestionToJson(qq);
            }
            
            var questions1 = help.ReadQuestionFromJson();
            if (questions1.Find((item) => item[0].TestName == test.Name) != null)
            {
                questions1.RemoveAll((item) => item[0].TestName == test.Name);
            }
            questions1.Add(test.questions);
            help.WriteQuestionToJson(questions1);
            //wait
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Question yn = new Question(test, comboBox1.Text);
            yn.OnAddQuestion += Add;
            Hide();
            yn.Show();
        }
    }
}

