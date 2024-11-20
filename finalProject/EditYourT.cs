using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.Design.AxImporter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace finalProject
{
    public partial class EditYourT : Form
    {
        Help help = new Help();
        public List<Questions> ListQ { get; set; } = new List<Questions>();
        Questions current;
        public EditYourT(List<Questions> ListQuestions)
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            ListQ = ListQuestions;
            ReadQ();
        }

        private void ReadQ()
        {
            var list = help.ReadQuestionFromJson();
            var res = list.Find((item) => item[0].TestName == ListQ[0].TestName);
            if(res!=null)
            {
                list.Remove(res);
                help.WriteQuestionToJson(list);
                foreach (var item in res)
                {
                    ListQ.Add(item);
                }
            }
            foreach (var item in ListQ)
            {
                listBox1.Items.Add(item.Description);
            }
        }
        private void question()
        {
            var q = ListQ.Find((item) => item.Description == listBox1.SelectedItem.ToString());
            current = new Questions(q.Description, q.Score, q.TestName, q.Options, q.Answer, q.Type);
            textBox1.Text = q.Description;
            numericUpDown1.Value = q.Score;
            switch (q.Type)
            {
                case "Yes/No question":
                    groupBox1.Visible = true;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    if(q.Answer.Count!=0)
                        if (q.Answer[0]=="Yes")
                            radioButton1.Checked= true;
                        else
                            radioButton2.Checked = true;
                     break;
                case "American question":
                    textBox2.Text = q.Options[0];
                    textBox3.Text = q.Options[1];
                    textBox4.Text = q.Options[2];
                    textBox5.Text = q.Options[3];
                    groupBox1.Visible = false;
                    groupBox2.Visible = true;
                    groupBox3.Visible = false;
                    if (q.Answer.Count != 0)
                    {
                        if (q.Answer[0] == textBox2.Text)
                            radioButton3.Checked = true;
                        else
                        {
                            if (q.Answer[0] == textBox3.Text)
                                radioButton4.Checked = true;
                            else
                            {
                                if (q.Answer[0] == textBox4.Text)
                                    radioButton5.Checked = true;
                                else
                                    radioButton6.Checked = true;
                            }
                        }
                    }
                    break;
                case "More then one answer":
                    textBox6.Text = q.Options[0];
                    textBox7.Text = q.Options[1];
                    textBox8.Text = q.Options[2];
                    textBox9.Text = q.Options[3];
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = true;
                    for (int i = 0; i < q.Answer.Count; i++)
                    {
                        if (q.Answer[i] == textBox6.Text)
                            checkedListBox1.CheckedItems[0] = true;
                        if (q.Answer[i] == textBox7.Text)
                            checkedListBox1.CheckedItems[1] = true;
                        if (q.Answer[i] == textBox8.Text)
                            checkedListBox1.CheckedItems[2] = true;
                        if (q.Answer[i] == textBox9.Text)
                            checkedListBox1.CheckedItems[3] = true;
                    }
                    break;
                default:
                    break;
            }

        }

        private void listBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            question();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (current.Description != null)
            {
                ListQ.RemoveAll(item => item.Description == current.Description);
                current.Score = (int)numericUpDown1.Value;
                current.Description = textBox1.Text;
                switch (current.Type)
                {
                    case "Yes/No question":
                        if (radioButton1.Checked)
                            current.Answer.Equals("Yes");
                        else
                            if (radioButton2.Checked)
                            current.Answer.Equals("No");
                        break;
                    case "American question":
                        if (radioButton3.Checked)
                            current.Answer.Equals(textBox2.Text);
                        else
                        {
                            if (radioButton4.Checked)
                                current.Answer.Equals(textBox3.Text);
                            else
                            {
                                if (radioButton5.Checked)
                                    current.Answer.Equals(textBox4.Text);
                                else
                                    if (radioButton6.Checked)
                                    current.Answer.Equals(textBox5.Text);
                                else
                                    current.Answer.Clear();
                            }
                        }
                        current.Options[0] = textBox2.Text;
                        current.Options[1] = textBox3.Text;
                        current.Options[2] = textBox4.Text;
                        current.Options[3] = textBox5.Text;
                        break;
                    case "More then one answer":
                        current.Answer.Clear();
                        foreach (var item in checkedListBox1.CheckedItems)
                        {
                            int index=checkedListBox1.Items.IndexOf(item);
                            if (index==0)
                                current.Answer.Add(textBox6.Text);
                            if (index == 1)
                                current.Answer.Add(textBox7.Text);
                            if (index == 2)
                                current.Answer.Add(textBox8.Text);
                            if (index == 3)
                                current.Answer.Add(textBox9.Text);
                        }
                        current.Options[0] = textBox6.Text;
                        current.Options[1] = textBox7.Text;
                        current.Options[2] = textBox8.Text;
                        current.Options[3] = textBox9.Text;
                        break;
                    default:
                        break;
                }
                ListQ.Add(current);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            var q = help.ReadQuestionFromJson();
            var thisT = q.Find((item) => item[0].TestName == ListQ[0].TestName);
            if (thisT != null)
            {
                q.Remove(thisT);
                foreach (var item in thisT)
                {
                    ListQ.Add(item);
                }
            }
            q.Add(ListQ);

            help.WriteQuestionToJson(q);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ListQ.RemoveAll((item) => item.Description == listBox1.SelectedItem);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            KindOfQuestion k = new KindOfQuestion(ListQ);
            Hide();
            k.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ReadQ();
        }
    }
}
