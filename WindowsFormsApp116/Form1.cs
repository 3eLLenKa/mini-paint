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

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            drawingPen.StartCap = System.Drawing.Drawing2D.LineCap.Round; //Для плавной отрисовки линий
            drawingPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);

            DoubleBuffered = true; //для плавной отрисовки
        }

        private bool isMouse = false;
        private Point previousPoint; // Предыдущая позиция мыши

        private Pen drawingPen = new Pen(Color.Black, 2); // Перо для рисования
        private Pen figuresPen = new Pen(Color.Black, 2); //Перо для фигур

        private Bitmap bitmap;

        private Graphics g;

        private bool square;
        private bool triangle;
        private bool ellipse;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouse)
            {
                Point currentPoint = e.Location;

                g.DrawLine(drawingPen, previousPoint, currentPoint);

                previousPoint = currentPoint;
                pictureBox1.Invalidate(); // Перерисовываем pictureBox1
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (square | triangle | ellipse)
            {
                isMouse = false;
            }
            else
            {
                isMouse = true;
                previousPoint = e.Location;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            square = triangle = ellipse = false;
            pictureBox1.Invalidate();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            drawingPen.Width = trackBar1.Value;
            figuresPen.Width = trackBar1.Value;
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
            ellipse = true;
            square = triangle = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            square = true;
            ellipse = triangle = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            triangle = true;
            square = ellipse = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (square)
            {
                g.DrawRectangle(figuresPen, e.X, e.Y, 100, 100);
                pictureBox1.Invalidate(); 
            }

            if (ellipse)
            {
                g.DrawEllipse(figuresPen, e.X, e.Y, 100, 100);
                pictureBox1.Invalidate();
            }

            if (triangle)
            {
                DrawTriangle(e.Location);
            }
        }

        private void DrawTriangle(Point point)
        {
            Point[] trianglePoints = new Point[] {
                new Point(point.X, point.Y),
                new Point(point.X + 100, point.Y),
                new Point(point.X + 50, point.Y + 100) };

            g.DrawPolygon(figuresPen, trianglePoints);
            pictureBox1.Invalidate();
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