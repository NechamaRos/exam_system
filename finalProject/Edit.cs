using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace finalProject
{
    public partial class Edit : Form
    {
        string TestPath = "tests.json";
        Help help = new Help();
        public Edit()
        {
            InitializeComponent();
            ReadItems();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void ReadItems()
        {
            string read = File.ReadAllText(TestPath);
            List<SaveTest> tests = JsonConvert.DeserializeObject<List<SaveTest>>(read);
            string s = " ";
            foreach (var item in tests)
            {
                s = " ";
                if (!item.Status)
                    s = " Draff";
                comboBox1.Items.Add(item.Name + s);
            }

        }

        public void Removetest()
        {
            string read = File.ReadAllText(TestPath);
            List<SaveTest> items = JsonConvert.DeserializeObject<List<SaveTest>>(read);
            IEnumerable<SaveTest> TestToDelete = from i in items
                                                 where i.Name == (comboBox1.SelectedItem.ToString().Split(" "))[0]
                                                 select i;
            foreach (var item in TestToDelete)
            {
                items.Remove(item);
                break;
            }
            string json = JsonConvert.SerializeObject(items);
            File.WriteAllText(TestPath, json);
        }
        public void RemoveQuestions()
        {
            var q = help.ReadQuestionFromJson();
            var query = from list in q
                        where list[0].TestName == (comboBox1.SelectedItem.ToString().Split(" "))[0]
                        select list;
            foreach (var item in query)
            {
                q.Remove(item);
                break;
            }
            help.WriteQuestionToJson(q);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem!=null)
            {
                var readT=File.ReadAllText(TestPath);
                var ListT=JsonConvert.DeserializeObject<List<SaveTest>>(readT);                
                var ListQ = help.ReadQuestionFromJson();
                var t = ListT.Find((item)=>item.Name== comboBox1.SelectedItem.ToString().Split(" ")[0]);
                t.Status = false;
                var jsonT=JsonConvert.SerializeObject(ListT);
                File.WriteAllText(TestPath,jsonT);
                var lq = ListQ.Find((item) => item[0].TestName == comboBox1.SelectedItem.ToString().Split(" ")[0]);
                ListQ.Remove(lq);
                string list=JsonConvert.SerializeObject(ListQ);
                File.WriteAllText(TestPath,list);
                EditYourT edit = new EditYourT(lq);
                Hide();
                edit.Show();
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var resalt=MessageBox.Show("Are you sure?","",MessageBoxButtons.YesNo);
                if (resalt == DialogResult.No)
                    return;
                Removetest();
                RemoveQuestions();
                Close();
            }
                
        }
    }
}
