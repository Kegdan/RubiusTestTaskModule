using System;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace RubiusTestTaskModule
{
    // оболочка для DBPoint, содеражащяя связанные с ним элементы вьюшки
    internal class ChangeablePoint : IChangeable
    {
        private DBPoint _point;
        public Tuple<TextBox, TextBox, TextBox> TBPosition;
        public TextBox TbThickness { get; set; }

        public ChangeablePoint(DBPoint point)
        {
            _point = point;
        }

        // метод просматривает связанные с обьектом вью-компоненты, извлекает из них данные, и обновляет объект
        public void ApplyChange()
        {
            Double px, py, pz, thickness;

            if (double.TryParse(TBPosition.Item1.Text, out px) && double.TryParse(TBPosition.Item2.Text, out py) &&
                double.TryParse(TBPosition.Item3.Text, out pz))
                _point.Position = new Point3d(px, py, pz);

            if (double.TryParse(TbThickness.Text, out thickness))
                _point.Thickness = thickness;
        }
    }
}
