using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab6
{
    class Edge : Primitive{
        public Point3D A;
        public Point3D B;

        public Edge(Point3D a, Point3D b)
{
            A = a;
            B = b;
        }

        public void Change(Transform t)
        {
            A.Change(t);
            B.Change(t);
        }

        public void Draw(Graphics g, Transform projection, int width, int height)
        {
            var c = A.Transform(projection).ViewTransform(width, height);
            var d = B.Transform(projection).ViewTransform(width, height);
            g.DrawLine(new Pen(Color.Black, 1), (float)c.X, (float)c.Y, (float)d.X, (float)d.Y);
        }

        public void Draw(Graphics g, Transform projection, int width, int height, Color color)
        {
            var c = A.Transform(projection).ViewTransform(width, height);
            var d = B.Transform(projection).ViewTransform(width, height);
            g.DrawLine(new Pen(color, 1), (float)c.X, (float)c.Y, (float)d.X, (float)d.Y);
        }
    }
}
