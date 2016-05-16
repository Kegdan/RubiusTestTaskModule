using System;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace RubiusTestTaskModule.ChangableElements
{
    // оболочка для Line, содеражащяя связанные с ним элементы вьюшки
    internal class ChangeableLine : IChangeable
    {
        private Line _line;

        public Tuple<TextBox, TextBox, TextBox> TbStartPoint;
        public Tuple<TextBox, TextBox, TextBox> TbEndPoint;
        public TextBox TbThickness { get; set; }

        public ChangeableLine(Line line)
        {
            _line = line;
        }

        // метод просматривает связанные с обьектом вью-компоненты, извлекает из них данные, и обновляет объект
        public void ApplyChange()
        {
            Double spx, epx, spy, epy, spz, epz, thickness;

            if (double.TryParse(TbStartPoint.Item1.Text, out spx) && double.TryParse(TbStartPoint.Item2.Text, out spy) &&
                double.TryParse(TbStartPoint.Item3.Text, out spz))
                _line.StartPoint = new Point3d(spx, spy, spz);

            if (double.TryParse(TbEndPoint.Item1.Text, out epx) && double.TryParse(TbEndPoint.Item2.Text, out epy) &&
                double.TryParse(TbEndPoint.Item3.Text, out epz))
                _line.EndPoint = new Point3d(epx, epy, epz);

            if (double.TryParse(TbThickness.Text, out thickness))
                _line.Thickness = thickness;
        }
    }
}
