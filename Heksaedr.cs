using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Heksaedr : Primitive{
		public List<Point3D> Vertexes = new List<Point3D>();
		public List<Face> Faces = new List<Face>();
		public Heksaedr()
		{
			Vertexes = new List<Point3D>();
			double scale = 0.5;

			Vertexes.Add(new Point3D(-scale / 2, -scale / 2, -scale / 2));
			Vertexes.Add(new Point3D(-scale / 2, -scale / 2, scale / 2));
			Vertexes.Add(new Point3D(-scale / 2, scale / 2, -scale / 2));
			Vertexes.Add(new Point3D(scale / 2, -scale / 2, -scale / 2));
			Vertexes.Add(new Point3D(-scale / 2, scale / 2, scale / 2));
			Vertexes.Add(new Point3D(scale / 2, -scale / 2, scale / 2));
			Vertexes.Add(new Point3D(scale / 2, scale / 2, -scale / 2));
			Vertexes.Add(new Point3D(scale / 2, scale / 2, scale / 2));

			Faces.Add(new Face(new List<Point3D> { Vertexes[0], Vertexes[1], Vertexes[5], Vertexes[3] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[2], Vertexes[6], Vertexes[3], Vertexes[0] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[4], Vertexes[1], Vertexes[0], Vertexes[2] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[7], Vertexes[5], Vertexes[3], Vertexes[6] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[2], Vertexes[4], Vertexes[7], Vertexes[6] }));
			Faces.Add(new Face(new List<Point3D> { Vertexes[4], Vertexes[1], Vertexes[5], Vertexes[7] }));

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
			foreach (var v in Vertexes)
				v.Change(t);
		}

		public void Draw(Graphics g, Transform projection, int width, int height)
		{
			if (Vertexes.Count != 8) return;

			foreach (var Face in Faces)
				Face.Draw(g, projection, width, height);
		}
	}
}
