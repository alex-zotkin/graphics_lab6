using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Tetraedr : Primitive
    {
        public List<Point3D> Vertexes = new List<Point3D>();
        public List<Face> Faces = new List<Face>();

        public Tetraedr()
        {
            double h = Math.Sqrt(2.0 / 3.0);
            double scale = 1;
            Vertexes = new List<Point3D>();

            Vertexes.Add(new Point3D(-scale / 2, 0, h / 3));
            Vertexes.Add(new Point3D(0, 0, -h * 2 / 3));
            Vertexes.Add(new Point3D(scale / 2, 0, h / 3));
            Vertexes.Add(new Point3D(0, h, 0));

            // Основание
            Faces.Add(new Face(new List<Point3D> { Vertexes[0], Vertexes[1], Vertexes[2] }));
            Faces.Add(new Face(new List<Point3D> { Vertexes[1], Vertexes[3], Vertexes[0] }));
            Faces.Add(new Face(new List<Point3D> { Vertexes[2], Vertexes[3], Vertexes[1] }));
            Faces.Add(new Face(new List<Point3D> { Vertexes[0], Vertexes[3], Vertexes[2] }));

        }

        public Point3D Center()
        {
            Point3D p = new Point3D(0, 0, 0);

            foreach (Point3D v in Vertexes)
            {
                p.X += v.X;
                p.Y += v.Y;
                p.Z += v.Z;
            }

            p.X /= Vertexes.Count();
            p.Y /= Vertexes.Count();
            p.Z /= Vertexes.Count();
            return p;
        }

        public void Change(Transform t)
        {
            foreach (var point in Vertexes)
                point.Change(t);
        }

        public void Draw(Graphics g, Transform projection, int width, int height)
        {
            foreach (var Verge in Faces)
                Verge.Draw(g, projection, width, height);
        }
    }
}
