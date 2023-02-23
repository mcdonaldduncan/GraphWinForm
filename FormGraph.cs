using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphWinForm
{
    public partial class FormGraph : Form
    {
        public List<TextBox> Edges = new List<TextBox>();
        private PointF Origin = new PointF(500, 500);
        private int Length = 100;
        private PointF[] Vertices = new PointF[0];

        public FormGraph(List<TextBox> edges)
        {
            InitializeComponent();
            Edges = edges.Where(n => !string.IsNullOrEmpty(n.Text)).OrderBy(tb => tb.Name).ToList();
        }

        private void FormGraph_Paint(object sender, PaintEventArgs e)
        {
            var sideCount = MathF.Sqrt(Edges.Count);
            // validate matrix clean, no skipped column

            GetVertices((int)sideCount, Length, Origin);
            foreach (var vert in Vertices)
            {
                DrawCircle(e, vert);
            }
            DrawPoly(e);

            foreach (var edge in Edges)
            {
                if (edge.Text != "0")
                {
                    var col = Int32.Parse(edge.Name.Substring(5, 1));
                    var row = Int32.Parse(edge.Name.Substring(7, 1));

                    DrawEdge(e, Vertices[col - 1], Vertices[row - 1]);
                }
            }
        }

        private void DrawCircle(PaintEventArgs e, PointF vert)
        {
            Pen pen = new Pen(Color.Black, 4);
            e.Graphics.DrawEllipse(pen, vert.X - 2, vert.Y - 2, 4, 4);
        }

        private void DrawPoly(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 4);
            e.Graphics.DrawPolygon(pen, Vertices);
        }

        private void DrawEdge(PaintEventArgs e, PointF source, PointF destination)
        {
            Pen pen = new Pen(Color.Red, 4);
            e.Graphics.DrawLine(pen, source, destination);
        }

        private void GetVertices(int sides, int length, PointF origin)
        {
            var vertices = new PointF[sides];
            var degrees = 180 * (sides - 2) / sides;
            var degreeOffset = 360 / sides;

            var radian = degrees * (Math.PI / 180);
            var SinDegree = Math.Sin(radian);
            var CosDegree = Math.Cos(radian);

            vertices[0] = origin;

            for (int i = 1; i < vertices.Length; i++)
            {
                double x = vertices[i - 1].X - CosDegree * length;
                double y = vertices[i - 1].Y - SinDegree * length;

                vertices[i] = new PointF((int)x, (int)y);
                degrees -= degreeOffset;
                radian = degrees * (Math.PI / 180);

                SinDegree = Math.Sin(radian);
                CosDegree = Math.Cos(radian);
            }

            Vertices = vertices;
        }

    }
}
