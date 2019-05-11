using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace TPoint
{
    public class shape
    {
        protected Graphics g;
        protected bool click_value;
        protected Pen cvetik;
        protected Brush cvet;
        public float x = 0;
        public float y = 0;
        public int r = 0;
        public float x0;
        public float y0;
        public shape()
        {
            click_value = true;
        }
        public virtual void change_click()
        {
            click_value = !click_value;
        }
        public virtual void change_color()
        {
            if (click_value != true)
                cvetik = new Pen(Color.Red);
            else cvetik = new Pen(Color.Black);
        }
        public virtual void draw(Graphics g) { }
     
        public virtual bool finder(float c, float d)
        {
            return false;
        }
        public bool output_abroad_for_the_point(float c, float d, float x, float y, Panel mypanel)
        {
            if ((c + x + 1 < mypanel.Width && d + y + 1 < mypanel.Height) &&
                (x + c - 1 > 0 && y + d - 1 > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool output_abroad(float c, float d, Panel mypanel)
        {
            return false;
        }
        public virtual Pen my_color()
        {
            return cvetik;
        }
        ~ shape()
        {
        }
    };
    class CCircle : shape
    {
        public CCircle()
        {
            cvetik = new Pen(Color.Black);
            x = 1;
            y = 1;
            r = 24;
        }
        public CCircle(float i, float j, int t, Pen new_cvetik)
        {
            cvetik = new_cvetik;
            cvet = Brushes.Bisque;
            r = t;
            x0 = i - t;
            y0 = j - t;
            x = i;
            y = j;
        }
        public override void draw(Graphics g)
        {
            g.FillEllipse(cvet, new RectangleF(x - r, y - r, 2 * r, 2 * r));
            g.DrawEllipse(cvetik, x - r, y - r, 2 * r, 2 * r);
        }
        public override bool output_abroad(float c, float d, Panel mypanel)
        {
            if (c > 0 && d > 0 &&
                (d + y + r < mypanel.Height) && (c + x + r < mypanel.Width))
                return true;
            if (c < 0 && d > 0 &&
                (d + y + r < mypanel.Height) && (c + x - r > 0))
                return true;
            if (c < 0 && d < 0 &&
                (d + y - r > 0) && (c + x - r > 0))
                return true;
            if (c > 0 && d < 0 &&
                (d + y - r > 0) && (c + x + r < mypanel.Width))
                return true;
            if (c == 0 && d == 0 &&

               (y + r < mypanel.Height && y - r > 0 && x + r < mypanel.Width && x - r > 0)
            )
                return true;
            else
            {
                MessageBox.Show("shapes cross the frontier");
                return false;
            }
        }
        public override bool finder(float c, float d)
        {
            if (((c - x) * (c - x) + (d - y) * (d - y)) <= (r * r))
                return true;
            return false;
        }
        ~CCircle()
        {
        }
    };
    class Section : shape
    {
        private float xv = 0;
        private float yv = 0;
        private float cv = 0;
        private float x1 = 0;
        private float y1 = 0;
        private float x2 = 0;
        private float y2 = 0;
        private float k;
        public Section()
        {
            cvetik = new Pen(Color.Black);
            x1 = 0;
            y1 = 0;
            x2 = 0;
            y2 = 0;
        }
        public Section(float x1, float y1, float x2, float y2, Pen new_cvetik)
        {
            cvetik = new_cvetik;
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
           
            xv = x2 - x1;
            yv = y2 - y1;
            cv = xv * y1 - yv * x1;
        }
        public override void draw(Graphics g)
        {
            g.DrawLine(cvetik, x1, y1, x2, y2);

        }
        public override bool finder(float c, float d)
        {
            if ((x2 - x1) != 0)
                k = (y2 - y1) / (x2 - x1);
            else k = 1;
            float b = y2 - k * x2;
            float f = k * c + b;
            if ((d >= f - 5 && d <= f + 5) && ((c <= x2 && c >= x1 && d <= y2 && d >= y1) || (c >= x2 && c <= x1 && d >= y2 && d <= y1) || (c <= x2 && c >= x1 && d >= y2 && d <= y1) || (c >= x2 && c <= x1 && d <= y2 && d >= y1)))
            {
                return true;
            }
            return false;
        }
        public override bool output_abroad(float c, float d, Panel mypanel)
        {
            if ((c + x2 + 1 < mypanel.Width && d + y2 + 1 < mypanel.Height) &&
                (c + x1 + 1 < mypanel.Width && d + y1 + 1 < mypanel.Height) &&
                (x2 + c - 1 > 0 && y2 + d - 1 > 0) &&
                (x1 + c - 1 > 0 && y1 + d - 1 > 0))
            {
                return true;
            }
            else
            {
                MessageBox.Show("shapes cross the frontier");
                return false;
            }
        }
        ~Section()
        {
        }
    }
}
