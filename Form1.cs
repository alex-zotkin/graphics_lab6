using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        private Graphics perspective_g;
        private Bitmap perspective_bmp;

        private Graphics orthographic_g;
        private Bitmap orthographic_bmp;

        private Primitive cur_primitive;


        private Transform get_perpective_transform()
        {
            switch (PerspectiveComboBox.SelectedItem.ToString())
            {
                case "Перспективная":
                    {
                        return Transform.PerspectiveProjection();
                    }
                case "Изометрическая":
                    {
                        return Transform.IsometricProjection();
                    }
                default:
                    {
                        return Transform.PerspectiveProjection();
                    }
            }
        }

        private Transform get_orthographic_transform()
        {
            switch (OrthographicComboBox.SelectedItem.ToString())
            {
                case "Ортографическая XY":
                    {
                        return Transform.OrthographicXYProjection();
                    }
                case "Ортографическая XZ":
                    {
                        return Transform.OrthographicXZProjection();
                    }
                case "Ортографическая YZ":
                    {
                        return Transform.OrthographicYZProjection();
                    }
                default:
                    {
                        return Transform.OrthographicXYProjection();
                    }
            }
        }

        //Рисует координатные оси 
        private void DrawAxis(Graphics g, Transform t, int width, int height)
        {
            List<Primitive> primitives = new List<Primitive>();
            Point3D o = new Point3D(0, 0, 0);
            Point3D x = new Point3D(1, 0, 0);
            Point3D y = new Point3D(0, 1, 0);
            Point3D z = new Point3D(0, 0, 1);

            primitives.Add(o);
            primitives.Add(x);
            primitives.Add(y);
            primitives.Add(z);

            new Edge(o, x).Draw(g, t, width, height, Color.Green);
            new Edge(o, y).Draw(g, t, width, height, Color.Blue);
            new Edge(o, z).Draw(g, t, width, height, Color.Red);

            primitives.Add(cur_primitive);

            foreach (Primitive p in primitives)
            {
                p.Draw(g, t, width, height);
            }
        }

        private void GetPrimitive()
        {
            switch (PrimitiveComboBox.SelectedItem.ToString())
            {
                case "Тетраэдр":
                    {
                        cur_primitive = new Tetraedr();
                        break;
                    }
                case "Гексаэдр":
                    {
                        cur_primitive = new Heksaedr();
                        break;
                    }
                case "Октаэдр":
                    {
                        cur_primitive = new Octaedr();
                        break;
                    }
                case "Икосаэдр":
                    {
                        cur_primitive = new Icosaedr();
                        break;
                    }
                /*case "Додекаэдр":
                    {
                        cur_primitive = new Dodecaedr(0.5);
                        break;
                    }*/
                default:
                    {
                        cur_primitive = new Tetraedr();
                        break;
                    }
            }
        }

        private void Clear()
        {
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;

            orthographic_bmp = new Bitmap(OrthographicBox.Width, OrthographicBox.Height);
            orthographic_g = Graphics.FromImage(orthographic_bmp);
            OrthographicBox.Image = orthographic_bmp;
        }
        public Form1()
        {
            InitializeComponent();

            //Создаем Bitmap и Graphics для двух PictureBox
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            PerspectiveBox.Image = perspective_bmp;

            orthographic_bmp = new Bitmap(OrthographicBox.Width, OrthographicBox.Height);
            orthographic_g = Graphics.FromImage(orthographic_bmp);
            OrthographicBox.Image = orthographic_bmp;

            perspective_g.SmoothingMode = SmoothingMode.AntiAlias;
            orthographic_g.SmoothingMode = SmoothingMode.AntiAlias;

            //Инициализируем ComboBox для отображения проекций
            PerspectiveComboBox.SelectedItem = PerspectiveComboBox.Items[1];
            OrthographicComboBox.SelectedItem = OrthographicComboBox.Items[0];
            PrimitiveComboBox.SelectedItem = PrimitiveComboBox.Items[0];
            ReflectionComboBox.SelectedItem = ReflectionComboBox.Items[0];

            GetPrimitive();
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ChangePerspective_Click(object sender, EventArgs e)
        {
            perspective_bmp = new Bitmap(PerspectiveBox.Width, PerspectiveBox.Height);
            perspective_g = Graphics.FromImage(perspective_bmp);
            perspective_g.SmoothingMode = SmoothingMode.AntiAlias;
            PerspectiveBox.Image = perspective_bmp;
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
        }

        private void ChangeOrthographic_Click(object sender, EventArgs e)
        {
            orthographic_bmp = new Bitmap(OrthographicBox.Width, OrthographicBox.Height);
            orthographic_g = Graphics.FromImage(orthographic_bmp);
            orthographic_g.SmoothingMode = SmoothingMode.AntiAlias;
            OrthographicBox.Image = orthographic_bmp;
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ChangePrimitive_Click(object sender, EventArgs e)
        {
            Clear();
            GetPrimitive();
            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        //Смещение
        private void Translate()
        {
            double X = (double)numericUpDown1.Value;
            double Y = (double)numericUpDown2.Value;
            double Z = (double)numericUpDown3.Value;
            cur_primitive.Change(Transform.Translate(X, Y, Z));
        }

        //Поворот
        private void Rotate()
        {
            double X = (double)numericUpDown4.Value / 180 * Math.PI;
            double Y = (double)numericUpDown5.Value / 180 * Math.PI;
            double Z = (double)numericUpDown6.Value / 180 * Math.PI;
            cur_primitive.Change(Transform.RotateX(X) * Transform.RotateY(Y) * Transform.RotateZ(Z));
        }

        //Масштаб
        private void Scale()
        {
            double X = (double)numericUpDown7.Value;
            double Y = (double)numericUpDown8.Value;
            double Z = (double)numericUpDown9.Value;
            cur_primitive.Change(Transform.Scale(X, Y, Z));

        }

        //Отражение
        private void Reflect()
        {
            switch (ReflectionComboBox.SelectedItem.ToString())
            {
                case "Отражение по X":
                    {
                        cur_primitive.Change(Transform.ReflectX());
                        break;
                    }
                case "Отражение по Y":
                    {
                        cur_primitive.Change(Transform.ReflectY());
                        break;
                    }
                case "Отражение по Z":
                    {
                        cur_primitive.Change(Transform.ReflectZ());
                        break;
                    }
                default:
                    {
                        cur_primitive.Change(Transform.ReflectX());
                        break;
                    }
            }
        }

        //Масштабирование относительно центра
        private void ScaleCenter()
        {
            double C = (double)numericUpDown10.Value;
            cur_primitive.Change(Transform.Scale(C, C, C));
        }

        private void RotateCenter()
        {
            double X = (double)numericUpDown11.Value / 180 * Math.PI;
            double Y = (double)numericUpDown12.Value / 180 * Math.PI;
            double Z = (double)numericUpDown13.Value / 180 * Math.PI;
            cur_primitive.Change(Transform.RotateX(X) * Transform.RotateY(Y) * Transform.RotateZ(Z));
        }

        private void RotateLine()
        {
            double X1 = (double)numericUpDown14.Value;
            double Y1 = (double)numericUpDown15.Value;
            double Z1 = (double)numericUpDown16.Value;

            double X2 = (double)numericUpDown17.Value;
            double Y2 = (double)numericUpDown18.Value;
            double Z2 = (double)numericUpDown19.Value;

            Edge l = new Edge(new Point3D(X1, Y1, Z1), new Point3D(X2, Y2, Z2));

            double ang = (double)numericUpDown20.Value / 180 * Math.PI;

            cur_primitive.Change(Transform.RotateLine(l, ang));
        }

        private void ChangeAffin_Click(object sender, EventArgs e)
        {
            Clear();
            Translate();
            Rotate();
            Scale();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ChangeReflection_Click(object sender, EventArgs e)
        {
            Clear();
            Reflect();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ChangeScaleCenter_Click(object sender, EventArgs e)
        {
            Clear();
            ScaleCenter();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ChangeRotationCenter_Click(object sender, EventArgs e)
        {
            Clear();
            RotateCenter();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }

        private void ChangeLineRotation_Click(object sender, EventArgs e)
        {
            Clear();
            RotateLine();

            DrawAxis(perspective_g, get_perpective_transform(), PerspectiveBox.Width, PerspectiveBox.Height);
            DrawAxis(orthographic_g, get_orthographic_transform(), OrthographicBox.Width, OrthographicBox.Height);
        }
    }
}
