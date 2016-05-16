using System;
using System.Windows.Controls;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule
{
    // оболочка для LayerTableRecord, содеражащяя связанные с ним элементы вьюшки
    internal class ChangeableLayer : IChangeable
    {
        private LayerTableRecord _layer;

        public TextBox TbName;
        public CheckBox CbVisible;
        public Tuple<TextBox, TextBox, TextBox> TbRGB;
        public StackPanel SpLayerContainer;

        public ChangeableLayer(LayerTableRecord layer)
        {
            _layer = layer;
        }

        // метод просматривает связанные с обьектом вью-компоненты, извлекает из них данные, и обновляет объект
        public void ApplyChange()
        {
            // нельзя сменить имя главного слоя, поэтому мы его не трогаем
            if (_layer.Name != "0")
                _layer.Name = TbName.Text;

            _layer.IsOff = (bool)!CbVisible.IsChecked;

            var colorValue = _layer.Color.ColorValue;

            byte r, g, b;

            if (!byte.TryParse(TbRGB.Item1.Text, out r))
                r = colorValue.R;

            if (!byte.TryParse(TbRGB.Item2.Text, out g))
                g = colorValue.G;

            if (!byte.TryParse(TbRGB.Item3.Text, out b))
                b = colorValue.B;

            _layer.Color = Color.FromRgb(r, g, b);
        }
    }
}
