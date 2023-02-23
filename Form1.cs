namespace GraphWinForm
{
    public partial class Form1 : Form
    {
        TextBox[,] _matrix = new TextBox[9,9];

        public Form1()
        {
            InitializeComponent();
            AddTextBoxes();
        }

        void AddTextBoxes()
        {
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    TextBox tb = new TextBox() { Name = $"Node_{i}_{j}" };
                    tableLayoutPanel1.Controls.Add(tb, i, j);
                    _matrix[i,j] = tb;
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
            if (EvaluateMatrix())
            {
                var tbs = tableLayoutPanel1.Controls.OfType<TextBox>().ToList();
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
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (string.IsNullOrEmpty(_matrix[i, j].Text) || _matrix[i, j].Text == "0") continue;

                    if (string.IsNullOrEmpty(_matrix[j, i].Text) || _matrix[j, i].Text != "0")
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}