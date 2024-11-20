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
    public partial class finishTest : Form
    {
        public finishTest(int mark)
        {
            InitializeComponent();
            label1.Text=mark.ToString();
        }

        private void finishTest_Load(object sender, EventArgs e)
        {

        }
    }
}
