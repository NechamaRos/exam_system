﻿using System;
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
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CreateTest newTest = new CreateTest();
            Hide();
            newTest.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Edit edit= new Edit();
            Hide();
            edit.Show();
        }
    }
}
