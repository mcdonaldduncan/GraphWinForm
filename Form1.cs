namespace GraphWinForm
{
    public partial class Form1 : Form
    {
        TextBox[,] Matrix = new TextBox[9,9];

        public Form1()
        {
            InitializeComponent();
            AddTextBoxes();
        }

        void AddTextBoxes()
        {
            for (int i = 0; i < TableLayout.ColumnCount; i++)
            {
                for (int j = 0; j < TableLayout.ColumnCount; j++)
                {
                    TextBox tb = new TextBox() { Name = $"Node_{i}_{j}" };
                    TableLayout.Controls.Add(tb, i, j);
                    Matrix[i,j] = tb;
                }
            }

            foreach (var tb in TableLayout.Controls.OfType<TextBox>().ToList())
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
            if (EvaluateMatrix())
            {
                var tbs = TableLayout.Controls.OfType<TextBox>().ToList();
                FormGraph form = new FormGraph(tbs);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("The adjacency matrix you entered is not valid or not directed");
            }
        }

        bool EvaluateMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (string.IsNullOrEmpty(Matrix[i, j].Text) || Matrix[i, j].Text == "0") continue;

                    if (string.IsNullOrEmpty(Matrix[j, i].Text) || Matrix[j, i].Text != "0")
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}