using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Formats.Asn1.AsnWriter;

namespace finalProject
{
    public partial class DoTest : Form
    {
        public event EventHandler<MyEventArgs> OnDoQuestion;
        Help help = new Help();
        string TestName;
        public int count { get; set; } = 0;
        public List<Questions> questions { get; set; } = new List<Questions>();
        public List<string>[] arr;
        
        public int numOfQuestion { get; set; } = 0;
        public DoTest(string name)
        {
            
            InitializeComponent();
            TestName = name;
            readItems();
            arr = new List<string>[questions.Count];
            for (int i = 0; i < questions.Count; i++)
            {
                arr[i] = new List<string>();
            }
            button1.Visible = false;
            if (questions.Count > 1)
                button3.Visible = false;
            else
                button2.Visible = false;
            start();
            showQ();
        }
        public void start()
        {

        }
        public void DoQ(object sender, MyEventArgs e)
        {
            count += e.Message;
        }
        private void readItems()
        {
            var q = help.ReadQuestionFromJson();
            questions = (q.Find((item) => item[0].TestName == TestName));
        }

        
        private void showQ()
        {
            //OnDoQuestion += DoQ;
            label1.Text = questions[numOfQuestion].Description;
            label2.Text = questions[numOfQuestion].Score.ToString() + "%";
            label7.Text = $"{count}\\{questions.Count}";
            int sum = arr[numOfQuestion].Count;
            switch (questions[numOfQuestion].Type)
            {
                case "Yes/No question":
                    if (sum!=0)
                    {
                        if (arr[numOfQuestion][0] == "Yes")
                            radioButton1.Checked = true;
                        else
                            radioButton2.Checked = true; 
                    }
                    else
                    {
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                    }
                    groupBox1.Visible = true;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    break;
                case "American question":
                    if (sum != 0)
                    {
                        if (arr[numOfQuestion][0] == radioButton3.Text)
                            radioButton3.Checked = true;
                        else
                        {
                            if (arr[numOfQuestion][0] == radioButton4.Text)
                                radioButton4.Checked = true;
                            else
                            {
                                if (arr[numOfQuestion][0] == radioButton5.Text)
                                    radioButton5.Checked = true;
                                else
                                    radioButton6.Checked = true;
                            }
                        }             
                    }
                    else
                    {
                        radioButton3.Checked = false;
                        radioButton4.Checked = false;
                        radioButton5.Checked = false;
                        radioButton6.Checked = false;
                    }
                    groupBox1.Visible = false;
                    groupBox2.Visible = true;
                    groupBox3.Visible = false;
                    radioButton3.Text = questions[numOfQuestion].Options[0];
                    radioButton4.Text = questions[numOfQuestion].Options[1];
                    radioButton5.Text = questions[numOfQuestion].Options[2];
                    radioButton6.Text = questions[numOfQuestion].Options[3];
                    break;
                case "More then one answer":
                    for (int i = 0; i < sum; i++)
                    {
                        if (arr[numOfQuestion][i] == label3.Text)
                            checkedListBox1.Items[0] = true;    
                        if (arr[numOfQuestion][i] == label4.Text)
                            checkedListBox1.Items[1] = true;        
                        if (arr[numOfQuestion][i] == label5.Text)
                                checkedListBox1.Items[2] = true;
                        if (arr[numOfQuestion][i] == label6.Text)
                            checkedListBox1.Items[3] = true;
                    }
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = true;
                    label3.Text = questions[numOfQuestion].Options[0];
                    label4.Text = questions[numOfQuestion].Options[1];
                    label5.Text = questions[numOfQuestion].Options[2];
                    label6.Text = questions[numOfQuestion].Options[3];
                    break;
                default:
                    break;
            }
        }

        
        public void check()
        {
            switch (questions[numOfQuestion].Type)
            {
                case "Yes/No question":
                    if (arr[numOfQuestion].Count != 0)
                    {
                        arr[numOfQuestion].Clear();
                        //OnDoQuestion(this, new MyEventArgs(-1));
                    }
                    if (radioButton1.Checked == true)
                    {
                        arr[numOfQuestion].Add("Yes");
                        radioButton1.Checked = false;
                    }   
                    else
                    {
                        if (radioButton2.Checked == true)
                        {
                            arr[numOfQuestion].Add("No");
                            radioButton2.Checked = false;
                        }
                    }
                    if (arr[numOfQuestion].Count != 0)
                    {
                        //OnDoQuestion(this, new MyEventArgs(1));
                    }
                    break;
                case "American question":
                    if (arr[numOfQuestion].Count != 0)
                    {
                        
                        //(this, new MyEventArgs(-1));
                    }
                    if (radioButton3.Checked == true)
                    {
                        arr[numOfQuestion].Clear();
                        arr[numOfQuestion].Add(radioButton3.Text);
                        radioButton3.Checked = false;
                    }
                    else
                    {
                        if (radioButton4.Checked == true)
                        {
                            arr[numOfQuestion].Clear();
                            arr[numOfQuestion].Add(radioButton4.Text);
                            radioButton4.Checked = false;
                        }
                        else
                        {
                            if (radioButton5.Checked == true)
                            {
                                arr[numOfQuestion].Clear();
                                arr[numOfQuestion].Add(radioButton5.Text);
                                radioButton5.Checked = false;
                            }
                            else
                            {
                                if (radioButton6.Checked == true)
                                {
                                    arr[numOfQuestion].Clear();
                                    arr[numOfQuestion].Add(radioButton6.Text);
                                    radioButton6.Checked = false;
                                }
                            }
                        }
                    }
                    if(arr[numOfQuestion].Count != 0)
                    {
                        //OnDoQuestion(this, new MyEventArgs(1));
                    }
                    
                    break;
                case "More then one answer":
                    if (arr[numOfQuestion].Count != 0)
                    {
                        //OnDoQuestion(this, new MyEventArgs(-1));
                    }
                    var a = checkedListBox1.CheckedItems;
                    
                    foreach (var item in a)
                    {
                        switch (item.ToString())
                        {
                            case "0": arr[numOfQuestion].Add(label3.Text);break;
                            case "1": arr[numOfQuestion].Add(label4.Text); break;
                            case "2": arr[numOfQuestion].Add(label5.Text); break;
                            case "3": arr[numOfQuestion].Add(label5.Text); break;
                            default:
                                break;
                        }  
                    }
                    checkedListBox1.ClearSelected();
                    break;
                default:
                    break;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            check();
            if (numOfQuestion < questions.Count - 1)
            {
                
                numOfQuestion++;
                button1.Visible = true;
                button3.Visible = false;
                showQ();
                if (numOfQuestion >= questions.Count - 1)
                {
                    button2.Visible = false;
                    button3.Visible = true;
                }    
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            check();
            if (numOfQuestion > 0)
            {
                
                numOfQuestion--;
                button2.Visible = true;
                button3.Visible = false;
                showQ();
                if (numOfQuestion < 1)
                    button1.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            check();
            foreach (var item in arr)
            {
                if(item.Count==0)
                {
                    var resalt = MessageBox.Show("Are you sure you want finish the test?", "", MessageBoxButtons.YesNo);
                    if (resalt == DialogResult.No)
                        return;
                    break;
                }
            }
            //לבדוק
            int mark = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if(questions[i].Type== "More then one answer")
                    if (questions[i].Answer == arr[i])
                        mark += questions[i].Score;
                else
                    if (questions[i].Answer[0] == arr[i][0])
                        mark += questions[i].Score;
            }
            finishTest f = new finishTest(mark);
            Hide();
            f.Show();
        }

        
    }
}
