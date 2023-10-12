using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp116
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            drawingPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            drawingPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            DoubleBuffered = true; //для плавной отрисовки
        }

        private bool isMouse = false;
        private Point previousPoint; // Предыдущая позиция мыши
        private Pen drawingPen = new Pen(Color.Black, 2); // Перо для рисования

        private Bitmap bitmap = new Bitmap(100, 100);

        private Graphics square;
        private Graphics triangle;
        private Graphics ellipse;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouse)
            {
                Point currentPoint = e.Location;

                Graphics g = pictureBox1.CreateGraphics();
                g.DrawLine(drawingPen, previousPoint, currentPoint);
                
                previousPoint = currentPoint;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;

            previousPoint = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            drawingPen.Width = trackBar1.Value;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                drawingPen.Color = colorDialog1.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Button button in panel1.Controls)
            {
                button.Click += Item_Click;
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            drawingPen.Color = button.BackColor;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ellipse = pictureBox1.CreateGraphics();
            square = triangle = null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            square = pictureBox1.CreateGraphics();
            ellipse = triangle = null;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            triangle = pictureBox1.CreateGraphics();
            square = ellipse = null;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ellipse?.DrawEllipse(drawingPen, e.X, e.Y, 100, 100);
            square?.DrawRectangle(drawingPen, e.X, e.Y, 100, 100);

            if (triangle != null)
            {
                DrawTriangle(e.Location);
            }
        }

        private void DrawTriangle(Point point)
        {
            triangle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Point[] trianglePoints = new Point[] {
                new Point(point.X, point.Y),
                new Point(point.X + 100, point.Y),
                new Point(point.X + 50, point.Y + 100) };

            triangle.DrawPolygon(drawingPen, trianglePoints);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            var result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                bitmap.Save(saveFileDialog1.FileName);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, Point.Empty);
        }
    }
}