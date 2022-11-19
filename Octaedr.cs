using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Octaedr : Primitive
    {
		public List<Point3D> Vertexes = new List<Point3D>();
		public List<Face> Faces = new List<Face>();
		public Octaedr()
		{

			Vertexes = new List<Point3D>();
			double scale = 0.5;

			Vertexes.Add(new Point3D(-scale / 2, 0, 0));
			Vertexes.Add(new Point3D(0, -scale / 2, 0));
			Vertexes.Add(new Point3D(0, 0, -scale / 2));
			Vertexes.Add(new Point3D(scale / 2, 0, 0));
			Vertexes.Add(new Point3D(0, scale / 2, 0));
			Vertexes.Add(new Point3D(0, 0, scale / 2));


			Faces.Add(new Face(new List<Point3D> { Vertexes[0], Vertexes[2], Vertexes[4] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[2], Vertexes[4], Vertexes[3] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[4], Vertexes[5], Vertexes[3] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[0], Vertexes[5], Vertexes[4] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[0], Vertexes[5], Vertexes[1] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[5], Vertexes[3], Vertexes[1] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[0], Vertexes[2], Vertexes[1] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[2], Vertexes[1], Vertexes[3] }));
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
			if (Vertexes.Count != 6) return;

			foreach (var Face in Faces)
				Face.Draw(g, projection, width, height);
		}
	}
}
