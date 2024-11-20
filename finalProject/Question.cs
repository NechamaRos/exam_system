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
    public partial class Question : Form
    {
        public event EventHandler<MyEventArgs> OnAddQuestion;

        public string Description { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public List<string> Answer { get; set; } = new List<string>();
        public int score { get; set; }
        public string Type { get; set; }
        public Test test { get; set; }
        public Question(Test t, string type)
        {
            InitializeComponent();
            test = t;
            Type = type;
            switch (Type)
            {
                case "Yes/No question":
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = true;
                    break;
                case "American question":
                    groupBox3.Visible = false;
                    groupBox2.Visible = false;
                    break;
                case "More then one answer":
                    groupBox3.Visible = false;
                    groupBox1.Visible = false;
                    break;
                default:
                    break;
            }
        }
        private void EnterToList()
        {
            Questions q = new Questions(Description, score, test.Name, Options, Answer, Type);
            test.questions.Add(q);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Description = textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private void YesNo()
        {
            Answer.Add(radioButton5.Checked ? "Yes" : "No");

        }
        private void American()
        {
            Options.Add(textBox4.Text);
            Options.Add(textBox5.Text);
            Options.Add(textBox6.Text);
            Options.Add(textBox7.Text);
            Answer.Add(radioButton1.Checked ? textBox4.Text : radioButton2.Checked ? textBox5.Text : radioButton3.Checked ? textBox6.Text : radioButton4.Checked ? textBox7.Text : "");
        }
        private void More()
        {
            checkedListBox1.Items[0] = (textBox2.Text);
            checkedListBox1.Items[1] = (textBox3.Text);
            checkedListBox1.Items[2] = (textBox8.Text);
            checkedListBox1.Items[3] = (textBox9.Text);
            var a = checkedListBox1.CheckedItems;
            foreach (var item in a)
            {
                Answer.Add(item.ToString());
            }
            Options.Add(textBox2.Text);
            Options.Add(textBox3.Text);
            Options.Add(textBox8.Text);
            Options.Add(textBox9.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(Description!=null)
            {
                EnterToList();
                OnAddQuestion(this, new MyEventArgs(score));
                switch (Type)
                {
                    case "Yes/No question":
                        YesNo();
                        break;
                    case "American question":
                        American();
                        break;
                    case "More then one answer":
                        More();
                        break;
                    default:
                        break;
                }
                Close();
            }
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            score = (int)numericUpDown1.Value;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            Description = textBox1.Text;

        }
    }
}
