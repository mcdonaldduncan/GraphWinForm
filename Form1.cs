namespace GraphWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddTextBoxes();
        }

        void AddTextBoxes()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    tableLayoutPanel1.Controls.Add(new TextBox() { Name = $"Node_{i}_{j}"}, i, j);
                }
            }

            foreach (var tb in tableLayoutPanel1.Controls.OfType<TextBox>().ToList())
            {
                if (tb.Name.Substring(5,1) == tb.Name.Substring(7,1))
                {
                    tb.Text = "0";
                    tb.Enabled = false;
                    tb.ReadOnly = true;
                        
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tbs = tableLayoutPanel1.Controls.OfType<TextBox>().ToList();
            FormGraph form = new FormGraph(tbs);
            form.ShowDialog();
        }
    }
}