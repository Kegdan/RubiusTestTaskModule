using System;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace RubiusTestTaskModule
{
    // оболочка для Circle, содеражащяя связанные с ним элементы вьюшки
    internal class ChangeableCircle : IChangeable
    {
        private Circle _circle;
        public Tuple<TextBox, TextBox, TextBox> TbCenter;
        public TextBox TbRadius;
        public TextBox TbThickness;

        public ChangeableCircle(Circle circle)
        {
            _circle = circle;
        }

        // метод просматривает связанные с обьектом вью-компоненты, извлекает из них данные, и обновляет объект
        public void ApplyChange()
        {
            Double px, py, pz, radius, thickness;

            if (double.TryParse(TbCenter.Item1.Text, out px) && double.TryParse(TbCenter.Item2.Text, out py) &&
                double.TryParse(TbCenter.Item3.Text, out pz))
                _circle.Center = new Point3d(px, py, pz);

            if (double.TryParse(TbRadius.Text, out radius))
                _circle.Radius = radius;

            if (double.TryParse(TbThickness.Text, out thickness))
                _circle.Thickness = thickness;
        }
    }


}
