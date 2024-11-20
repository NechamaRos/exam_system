namespace finalProject
{
    public partial class Enter : Form
    {
        public Enter()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseTest c = new ChooseTest();
            Hide();
            c.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Teacher t = new Teacher();
            Hide();
            t.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ChooseTest c = new ChooseTest();
            Hide();
            c.Show();
        }
    }
}