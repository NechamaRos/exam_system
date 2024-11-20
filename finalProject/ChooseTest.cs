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
    public partial class ChooseTest : Form
    {
        string pathT = "tests.json";
        public ChooseTest()
        {
            InitializeComponent();
            tests();
        }
        private void tests()
        {
            string read = File.ReadAllText(pathT);
            var t = JsonConvert.DeserializeObject<List<SaveTest>>(read);
            foreach (var item in t)
            {
                if (item.Status == true)
                    listBox1.Items.Add(item.Name+"  ✍");
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string ss = listBox1.SelectedItem.ToString();
            string s = listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().Length-3);
           
            DoTest d = new DoTest(s);
            Hide();
            d.Show();
        }
    }
}
