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
    /// <summary>
    /// Based of in class example by Leo Hazou
    /// </summary>
    public partial class FormGraph : Form
    {
        public List<TextBox> Edges = new List<TextBox>();
        private PointF Origin = new PointF(400, 200);
        private int EdgeLength = 100;
        private PointF[] Vertices = new PointF[0];

        public FormGraph(List<TextBox> edges)
        {
            Edges = edges.Where(n => !string.IsNullOrEmpty(n.Text)).OrderBy(tb => tb.Name).ToList();
            InitializeComponent();
        }

        private void FormGraph_Paint(object sender, PaintEventArgs e)
        {
            GetVertices((int)MathF.Sqrt(Edges.Count), EdgeLength, Origin);
            DrawGraph(e);
        }

        void DrawGraph(PaintEventArgs e)
        {
            foreach (var vert in Vertices)
            {
                DrawCircle(e, vert);
            }
            DrawPolygon(e);

            foreach (var edge in Edges)
            {
                if (edge.Text != "0")
                {
                    var col = Int32.Parse(edge.Name.Substring(5, 1));
                    var row = Int32.Parse(edge.Name.Substring(7, 1));

                    DrawEdge(e, Vertices[col], Vertices[row]);
                }
            }
        }

        // unused
        bool EvaluateDirectionality()
        {
            // I was trying to come up with a way to use the facts we learned in class to evaluate the matrix but was not able to get it working so went the other route,
            // would like to hear your thoughts on if there is a better approach

            var edgeCount = Edges.Where(x => x.Text != "0").Count();
            var degreeTotal = edgeCount * 2;
            var maxDirectionalDegrees = (Vertices.Length - 1) * Vertices.Length;

            return degreeTotal <= maxDirectionalDegrees;
        }

        private void DrawCircle(PaintEventArgs e, PointF vert)
        {
            Pen pen = new Pen(Color.Black, 4);
            e.Graphics.DrawEllipse(pen, vert.X - 2, vert.Y - 2, 4, 4);
        }

        private void DrawPolygon(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 4);
            e.Graphics.DrawPolygon(pen, Vertices);
        }

        private void DrawEdge(PaintEventArgs e, PointF source, PointF destination)
        {
            Pen pen = new Pen(Color.Green, 4);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            e.Graphics.DrawLine(pen, source, destination);
        }

        // Vertex creation sourced from in class example
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
