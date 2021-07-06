using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalSO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SJF();
            //FIFO();
        }

        private void FIFO()
        {
            MatchingSquares ms = new MatchingSquares();
            MaurerRose mr = new MaurerRose();
            HilbertCurve hc = new HilbertCurve();

            Circles cc = new Circles();

            ms.Dock = DockStyle.Fill;
            ms.BackColor = Color.Gray;


            mr.Dock = DockStyle.Fill;
            mr.BackColor = Color.Black;

            hc.Dock = DockStyle.Fill;
            hc.BackColor = Color.Black;

            cc.Dock = DockStyle.Fill;
            cc.BackColor = Color.Black;


            layoutPanel.Controls.Add(ms);
            layoutPanel.Controls.Add(mr);
            layoutPanel.Controls.Add(hc);
            layoutPanel.Controls.Add(cc);

        }

        private void SJF()
        {
            MatchingSquares ms = new MatchingSquares();
            MaurerRose mr = new MaurerRose();
            HilbertCurve hc = new HilbertCurve();
            Circles cc = new Circles();

            ms.Dock = DockStyle.Fill;
            ms.BackColor = Color.Gray;
            ms.peso = 4;

            mr.Dock = DockStyle.Fill;
            mr.BackColor = Color.Black;
            mr.peso = 8;

            hc.Dock = DockStyle.Fill;
            hc.BackColor = Color.Black;
            hc.peso = 3;

            cc.Dock = DockStyle.Fill;
            cc.BackColor = Color.Black;
            cc.peso = 10;

            List<PanelFiguras> clases = new List<PanelFiguras>
            {
                ms,
                mr,
                hc,
                cc
            };

            clases = clases.OrderBy(o => o.peso).ToList();


            foreach (PanelFiguras e in clases)
            {
                layoutPanel.Controls.Add(e);
            }

        }

    }

    class Val
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class PanelFiguras : Panel
    {
        public int peso { get; set; }
    }

    class MatchingSquares : PanelFiguras
    {


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Paint_MatchingSquares(e);

        }

        public void Paint_MatchingSquares(PaintEventArgs e)
        {
            Pen p = new Pen(Brushes.DarkMagenta, (float)0.5);
            Graphics l = e.Graphics;
            int cols, rows;
            int len = 10;

            rows = this.Height / len;
            cols = this.Width / len;

            float[,] field = new float[cols, rows];

            Random random = new Random();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = random.Next(2);
                }
            }
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (field[i, j] == 1)
                    {
                        Pen pen1 = new Pen(Color.Black, (float)0.5);
                        l.DrawEllipse(pen1, i * len, j * len, 2, 2);
                    }
                    else
                    {
                        Pen pen = new Pen(Color.White, (float)0.5);
                        l.DrawEllipse(pen, i * len, j * len, 2, 2);
                    }

                }
            }
            for (int i = 0; i < cols - 1; i++)
            {
                for (int j = 0; j < rows - 1; j++)
                {
                    float x = i * len;
                    float y = j * len;

                    Val a = new Val
                    {
                        X = x + len * (float)0.5,
                        Y = y
                    };

                    Val b = new Val
                    {
                        X = x + len,
                        Y = y + len * (float)0.5
                    };

                    Val c = new Val
                    {
                        X = x + len * (float)0.5,
                        Y = y + len
                    };

                    Val d = new Val
                    {
                        X = x,
                        Y = y + len * (float)0.5
                    };

                    int bin = get_Num((int)field[i, j], (int)field[i + 1, j], (int)field[i + 1, j + 1], (int)field[i, j + 1]);

                    switch (bin)
                    {
                        case 1:
                            drawLine(l, c, d);
                            break;
                        case 2:
                            drawLine(l, b, c);
                            break;
                        case 3:
                            drawLine(l, b, d);
                            break;
                        case 4:
                            drawLine(l, a, b);
                            break;
                        case 5:
                            drawLine(l, a, d);
                            drawLine(l, b, c);
                            break;
                        case 6:
                            drawLine(l, a, c);
                            break;
                        case 7:
                            drawLine(l, a, d);
                            break;
                        case 8:
                            drawLine(l, a, d);
                            break;
                        case 9:
                            drawLine(l, a, c);
                            break;
                        case 10:
                            drawLine(l, a, b);
                            drawLine(l, c, d);
                            break;
                        case 11:
                            drawLine(l, a, b);
                            break;
                        case 12:
                            drawLine(l, b, d);
                            break;
                        case 13:
                            drawLine(l, b, c);
                            break;
                        case 14:
                            drawLine(l, c, d);
                            break;
                    }
                }
            }
        }

        private int get_Num(int a, int b, int c, int d)
        {
            return a * 8 + b * 4 + c * 2 + d * 1;
        }

        private void drawLine(Graphics l, Val v1, Val v2)
        {
            Pen pen = new Pen(Color.White, (float)0.5);
            l.DrawLine(pen, v1.X, v1.Y, v2.X, v2.Y);
        }
    }

    class MaurerRose : PanelFiguras
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Paint_MaurerRose(e);

        }

        private void Paint_MaurerRose(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.DarkCyan, (float)0.5);

            float initialx = this.Width / 2;
            float initialy = this.Height / 2;
            float startx = this.Width / 2;
            float starty = this.Height / 2;

            double d = 74;
            double n = 4;

            int scale = 150;

            for (int i = 0; i < 361; i++)
            {
                double k = i * d * Math.PI / 180;
                double r = scale * Math.Sin(n * k);
                double x = r * Math.Cos(k);
                double y = r * Math.Sin(k);
                g.DrawLine(p, startx, starty, (float)x + initialx, (float)y + initialy);
                startx = (float)x + initialx;
                starty = (float)y + initialy;
                System.Threading.Thread.Sleep(5);
            }
        }
    }

    class HilbertCurve : PanelFiguras
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Paint_HilbertCurve(e);
        }

        private void Paint_HilbertCurve(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen3 = new Pen(Color.SpringGreen, (float)0.5);

            int N = 128;
            int scale = 10;

            Val curr;
            Val prev = new Val { X = 0, Y = 0 };

            for (int i = 0; i < N * N; i += 1)
            {
                curr = index2xy(i, N);

                g.DrawLine(pen3, prev.X * scale, prev.Y * scale, curr.X * scale, curr.Y * scale);

                prev = curr;
            }
        }

        private int last2bits(int x)
        {
            return x & 3;
        }

        private Val index2xy(int x, int N)
        {

            Val[] positions = { new Val{ X = 0, Y = 0},
                                 new Val{ X = 0, Y = 1},
                                 new Val{ X = 1, Y = 1},
                                 new Val{ X = 1, Y = 0} };

            Val tmp = positions[last2bits(x)];

            float tmp1;

            x >>= 2;

            float x1 = tmp.X;
            float y1 = tmp.Y;

            for (int n = 4; n <= N; n *= 2)
            {
                int n2 = n / 2;

                switch (last2bits(x))
                {
                    case 0:
                        tmp1 = x1;
                        x1 = y1;
                        y1 = tmp1;
                        break;

                    case 1:
                        y1 += n2;
                        break;

                    case 2:
                        x1 += n2;
                        y1 += n2;
                        break;

                    case 3:
                        tmp1 = y1;
                        y1 = (n2 - 1) - x1;
                        x1 = (n2 - 1) - tmp1;
                        x1 += n2;
                        break;
                }

                x >>= 2;
            }

            return new Val { X = x1, Y = y1 };
        }

    }

    class Circles : PanelFiguras
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawCircles(e);
        }

        private void drawCircles(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen4 = new Pen(Color.BlueViolet, (float)0.5);

            int r = 80;
            int grow = 5;
            int scale = 10;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < (r * 2); i += grow)
                {
                    if (j == 0)
                    {
                        g.DrawEllipse(pen4, ((((this.Width) / 2) + scale) - i), (((this.Height) / 2) + scale), r * 2, r * 2);
                    }
                    else if (j == 1)
                    {
                        g.DrawEllipse(pen4, (((this.Width) / 2) + scale), (((this.Height) / 2) + scale) - i, r * 2, r * 2);
                    }
                    else if (j == 2)
                    {
                        g.DrawEllipse(pen4, (((this.Width) / 2) + scale) - i, ((((this.Height) / 2) + scale) - (r * 2)), r * 2, r * 2);
                    }
                    else if (j == 3)
                    {
                        g.DrawEllipse(pen4, ((((this.Width) / 2) + scale) - (r * 2)), (((this.Height) / 2) + scale) - i, r * 2, r * 2);
                    }
                    System.Threading.Thread.Sleep(5);
                }
            }
        }
    }
}
