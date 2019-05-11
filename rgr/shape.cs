using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using _storage;

namespace _shape
{


   public class shape
    {
        protected Graphics g;
        protected Graphics sg;
        protected bool click_value;
        protected Pen cvetik;
        protected Brush cvet;
        public float x = 0;
        public float y = 0;
        public int r = 0;
        public float x0;
        public float y0;
        public float sx = 0, sy = 0, sx2 = 0, sy2 = 0;
        public shape()
        {
            click_value = true;
        }
        //public virtual void Update(MyStorage obj) { }
        public string my_cvet(Brush d)
        {
            if (d == Brushes.IndianRed)
                return "IndianRed";
            if (d == Brushes.Green)
                return "Green";
            if (d == Brushes.DarkBlue)
                return "DarkBlue";
            if (d == Brushes.Yellow)
                return "Yellow";
            return "White";
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
        public virtual void draw(Panel mypage) { }
        public virtual void deformation1()
        {
            r++;

        }
        public virtual void deformation2()
        {
            if (r > 0)
                r--;
        }
        public virtual void move(float x, float y)
        {
            this.x += x;
            this.y += y;
        }
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
        public virtual void coloration(Keys v, Panel mypage)
        {
            if (v == Keys.G)
            {
                cvet = Brushes.Green;
            }
            if (v == Keys.Y)
            {
                cvet = Brushes.Yellow;
            }
            if (v == Keys.B)
            {
                cvet = Brushes.DarkBlue;
            }
            if (v == Keys.R)
            {
                cvet = Brushes.IndianRed;
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
        public virtual void save(StreamWriter stream) { }
        public virtual void Load(StreamReader stream) { }
        public virtual string classname() { return "shape"; }
        public virtual void shadow(float x, float y) { }
        public virtual void draw_shadow(Panel mypanel) { }
        public virtual bool shadow_output_abroad(float x, float y, Panel mypanel) { return false; }
        public virtual void obnul() {
            sx = 0;
            sy = 0;
            sx2 = 0;
            sy2 = 0;
        }
        ~shape()
        {
        }
    };
unsafe sealed class CGroup : shape
    {
       private MyStorage _shapes=new MyStorage(0);
        public CGroup(int _count)
        {
            MyStorage _shapes = new MyStorage(_count);
        }
        ~CGroup()
        {
        }
        public void addShape(shape shape)
        {
            int s = _shapes.getCount();
            shape[] _shapes1;
            _shapes1 = new shape[++s];
            for (int i = 0; i < s - 1; i++)
            {
                _shapes1[i] = _shapes.GetObject(i);
            }
            _shapes1[s - 1] = shape;
            _shapes = new MyStorage(s);
            for (int i = 0; i < s; i++)
            {
                _shapes.SetObject(i,_shapes1[i]);
            }
        }
        public shape GetObject(int index)
        {

            return _shapes.GetObject(index);

        }
        public int getCount()
        {
            return _shapes.getCount();
        }
        public override void move(float dx, float dy)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
                _shapes.GetObject(i).move(dx, dy);
        }
        public override void draw(Panel mypage)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
                _shapes.GetObject(i).draw(mypage);
        }
        public override bool finder(float c, float d)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                if (_shapes.GetObject(i).finder(c, d))
                {
                    return true;
                }
            }
            return false;
        }
        public override Pen my_color()
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                if (_shapes.GetObject(i).my_color().Color != Color.Red)
                    return Pens.Black;
            }
            return Pens.Red;
        }
        public override bool output_abroad(float c, float d, Panel mypanel)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                if (!_shapes.GetObject(i).output_abroad(c, d, mypanel))
                {
                    return false;
                }
            }
            return true;
        }
        public override void deformation1()
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).deformation1();
            }
        }
        public override void deformation2()
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).deformation2();
            }
        }
        public override void change_click()
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).change_click();
            }
        }
        public override void change_color()
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).change_color();
            }
        }
        public override void coloration(Keys v, Panel mypage)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).coloration(v,mypage);
            }
        }
        public override void shadow(float x, float y)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).shadow(x,y);
            }
        }
        public override void draw_shadow(Panel mypanel)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).draw_shadow(mypanel);
            }
        }
        public override bool shadow_output_abroad(float c, float d, Panel mypanel)
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                if (_shapes.GetObject(i).shadow_output_abroad(c, d, mypanel))
                {
                    return true;
                }
            }
            return false;
        }
        public override void obnul()
        {
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).obnul();
            }
        }
        public override void Load(StreamReader stream)
        {
            int c = Convert.ToInt32(stream.ReadLine());
            for (int i = 0; i <c; i++)
            {
                _shapes.Add(op_8.Form1.createShape(stream.ReadLine()));
                _shapes.GetObject(i).Load(stream);
            }
        }
        public override void save(StreamWriter stream)
        {
            stream.WriteLine("G");
            stream.WriteLine(_shapes.getCount().ToString());
            for (int i = 0; i < _shapes.getCount(); i++)
            {
                _shapes.GetObject(i).save(stream);
            }
        }
        public override string classname() { return "Group"; }
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
            cvet = Brushes.White;
            r = t;
            x0 = i - t;
            y0 = j - t;
            x = i;
            y = j;
        }
        public override void draw(Panel mypage)
        {
            g = mypage.CreateGraphics();
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
        public override void shadow(float x, float y)
        {
            sx = this.x + x;
            sy = this.y + y;

        }
        public override void draw_shadow(Panel mypanel)
        {
            if (sx != 0 || sy != 0)
            {
                sg = mypanel.CreateGraphics();
                sg.DrawEllipse(cvetik, sx - r, sy - r, 2 * r, 2 * r);
            }
        }
        public override bool shadow_output_abroad(float x, float y, Panel mypanel)
        {
            if (this.x + x + r < mypanel.Width && this.x + x - r > 0)
                if (this.y + y + r < mypanel.Height && this.y + y - r > 0)
                    return false;
            return true;
        }
     
        public override void Load(StreamReader stream)
        {
            string s = stream.ReadLine();
            string[] info = s.Split(' ');
            x = float.Parse(info[0]);
            y = float.Parse(info[1]);
            r = int.Parse(info[2]);
            if (info[3] == "White")
                cvet = Brushes.White;
            if (info[3] == "Yellow")
                cvet = Brushes.Yellow;
            if (info[3] == "Green")
                cvet = Brushes.Green;
            if (info[3] == "IndianRed")
                cvet = Brushes.IndianRed;
            if (info[3] == "DarkBlue")
                cvet = Brushes.DarkBlue;
        }
        public override void save(StreamWriter stream)
        {
            stream.WriteLine("C");
            stream.WriteLine(x.ToString() + " " + y.ToString() + " " + r.ToString() + " " + my_cvet(cvet));
        }

        public override string classname() { return "Circle"; }
    };
    class ppoint : shape
    {

        public ppoint()
        {
            cvetik = new Pen(Color.Black);
            x = 1;
            y = 1;
            r = 1;
        }
        public ppoint(float i, float j, int t, Pen new_cvetik)
        {
            cvetik = new_cvetik;
            cvet = Brushes.DarkGray;
            r = t;
            x0 = i - r;
            y0 = j - r;
            x = i;
            y = j;
        }
        public override void draw(Panel mypage)
        {
            g = mypage.CreateGraphics();
            g.FillEllipse(cvet, new RectangleF(x - r, y - r, 2 * r, 2 * r));
            g.DrawEllipse(cvetik, x - r, y - r, 2 * r, 2 * r);
        }
        public override bool finder(float c, float d)
        {
            if (((c - x) * (c - x) + (d - y) * (d - y)) <= (r * r))
                return true;
            return false;
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
        public override void shadow(float x, float y)
        {
            sx = this.x + x;
            sy = this.y + y;

        }
        public override void draw_shadow(Panel mypanel)
        {
            if (sx != 0 || sy != 0)
            {
                sg = mypanel.CreateGraphics();
                sg.DrawEllipse(cvetik, sx - r, sy - r, 2 * r, 2 * r);
            }
        }
        public override bool shadow_output_abroad(float x, float y, Panel mypanel)
        {
            if (this.x + x + r < mypanel.Width && this.x + x - r > 0)
                if (this.y + y + r < mypanel.Height && this.y + y - r > 0)
                    return false;
            return true;
        }
        public override void Load(StreamReader stream)
        {
            string s = stream.ReadLine();
            string[] info = s.Split(' ');
            x = float.Parse(info[0]);
            y = float.Parse(info[1]);
            r = int.Parse(info[2]);
            if (info[3] == "White")
                cvet = Brushes.White;
            if (info[3] == "Yellow")
                cvet = Brushes.Yellow;
            if (info[3] == "Green")
                cvet = Brushes.Green;
            if (info[3] == "IndianRed")
                cvet = Brushes.IndianRed;
            if (info[3] == "DarkBlue")
                cvet = Brushes.DarkBlue;
        }
        public override void save(StreamWriter stream)
        {
            stream.WriteLine("P");
            stream.WriteLine(x.ToString() + " " + y.ToString() + " " + r.ToString() + " " + my_cvet(cvet));

        }
        public override string classname() { return "Point"; }
    };
    class RRectangle : shape
    {

        public RRectangle()
        {
            cvetik = new Pen(Color.Black);
            x = 1;
            y = 1;
            r = 24;
        }
        public RRectangle(float i, float j, int t, Pen new_cvetik)
        {
            cvetik = new_cvetik;
            cvet = Brushes.White;
            r = t;
            x0 = i - r;
            y0 = j - r;
            x = i;
            y = j;

        }
        public override void draw(Panel mypage)
        {
            g = mypage.CreateGraphics();
            g.FillRectangle(cvet, new RectangleF(x - r, y - r, 2 * r, 2 * r));
            g.DrawRectangle(cvetik, x - r, y - r, 2 * r, 2 * r);
        }
        public override bool finder(float c, float d)
        {
            if (c >= x - r && d >= y - r && c <= x + r && d <= y + r)
                return true;
            return false;
        }
        public override bool output_abroad(float c, float d, Panel mypanel)
        {
            if (output_abroad_for_the_point(c, d, x - r, y - r, mypanel) && output_abroad_for_the_point(c, d, x + r, y - r, mypanel)
                && output_abroad_for_the_point(c, d, x - r, y + r, mypanel) && output_abroad_for_the_point(c, d, x + r, y + r, mypanel))
                return true;
            else
            {
                MessageBox.Show("shapes cross the frontier");
                //sx = 0; sy = 0; sx2 = 0; sy2 = 0;
                return false;
            }
        }
        public override void shadow(float x, float y)
        {
            sx = this.x + x;
            sy = this.y + y;


        }
        public override void draw_shadow(Panel mypanel)
        {
            if (sx != 0 || sy != 0)
            {
                sg = mypanel.CreateGraphics();
                sg.DrawRectangle(cvetik, sx - r, sy - r, 2 * r, 2 * r);
            }
        }
        public override bool shadow_output_abroad(float x, float y, Panel mypanel)
        {
            if (this.x + x + r < mypanel.Width && this.x + x - r > 0)
                if (this.y + y + r < mypanel.Height && this.y + y - r > 0)
                    return false;
            return true;
        }
        public override void Load(StreamReader stream)
        {
            string s = stream.ReadLine();
            string[] info = s.Split(' ');
            x = float.Parse(info[0]);
            y = float.Parse(info[1]);
            r = int.Parse(info[2]);
            if (info[3] == "White")
                cvet = Brushes.White;
            if (info[3] == "Yellow")
                cvet = Brushes.Yellow;
            if (info[3] == "Green")
                cvet = Brushes.Green;
            if (info[3] == "IndianRed")
                cvet = Brushes.IndianRed;
            if (info[3] == "DarkBlue")
                cvet = Brushes.DarkBlue;
        }
        public override void save(StreamWriter stream)
        {
            stream.WriteLine("R");
            stream.WriteLine(x.ToString() + " " + y.ToString() + " " + r.ToString() + " " + my_cvet(cvet) );

        }
        public override string classname() { return "Rectangle"; }
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
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            xv = x2 - x1;
            yv = y2 - y1;
            cv = xv * y1 - yv * x1;
        }
        public override void draw(Panel my_panel)
        {
            g = my_panel.CreateGraphics();
            //cvetik.Width = 5;
            g.DrawLine(cvetik, x1, y1, x2, y2);

        }
        public override bool finder(float c, float d)
        {
            float k = (y2 - y1) / (x2 - x1);
            float b = y2 - k * x2;
            float f = k * c + b;
            if ((d >= f - 5 && d <= f + 5) && ((c <= x2 && c >= x1 && d <= y2 && d >= y1)|| (c >= x2 && c <= x1 && d >= y2 && d <= y1)|| (c <= x2 && c >= x1 && d >= y2 && d <= y1)|| (c >= x2 && c <= x1 && d <= y2 && d >= y1)))
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
        public override void deformation1()
        {
            float k = (y2 - y1) / (x2 - x1);
            float b = y2 - k * x2;
            if (x1 > x2)
            {
                x1++;
                x2--;
                y1 = k * x1 + b;
                y2 = k * x2 + b;
            }
            else
            {
                x1--;
                x2++;
                y1 = k * x1 + b;
                y2 = k * x2 + b;
            }


        }
        public override void deformation2()
        {
            float k = (y2 - y1) / (x2 - x1);
            float b = y2 - k * x2;
            if (x1 > x2)
            {
                x1--;
                x2++;
                y1 = k * x1 + b;
                y2 = k * x2 + b;
            }
            else
            {
                x1++;
                x2--;
                y1 = k * x1 + b;
                y2 = k * x2 + b;
            }


        }
        public override void move(float x, float y)
        {
            this.x1 += x;
            this.y1 += y;
            this.x2 += x;
            this.y2 += y;
        }
        public override void shadow(float x, float y)
        {
            sx = this.x1 + x;
            sy = this.y1 + y;
            sx2 = this.x2 + x;
            sy2 = this.y2 + y;
        }
        public override void draw_shadow(Panel mypanel)
        {
            if (sx != 0 || sy != 0)
            {
                sg = mypanel.CreateGraphics();
                sg.DrawLine(cvetik, sx, sy, sx2, sy2);
            }
        }
        public override bool shadow_output_abroad(float c, float d, Panel mypanel)
        {
            if ((c + x2 + 1 < mypanel.Width && d + y2 + 1 < mypanel.Height) &&
                 (c + x1 + 1 < mypanel.Width && d + y1 + 1 < mypanel.Height) &&
                 (x2 + c - 1 > 0 && y2 + d - 1 > 0) &&
                 (x1 + c - 1 > 0 && y1 + d - 1 > 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void Load(StreamReader stream)
        {
            string s = stream.ReadLine();
            string[] info = s.Split(' ');
            x1 = float.Parse(info[0]);
            y1 = float.Parse(info[1]);
            x2 = float.Parse(info[2]);
            y2 = float.Parse(info[3]);
        }
        public override void save(StreamWriter stream)
        {
            stream.WriteLine("S");
            stream.WriteLine(x1.ToString() + " " + y1.ToString() + " " + x2.ToString() + " " + y2.ToString() + " " + cvetik.Color.ToString() );
        }
        public override string classname() { return "Section"; }
    }
};
