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
        public TextBox TbR;
        public TextBox TbG;
        public TextBox TbB;
        public StackPanel LayerContainer;

        public ChangeableLayer(LayerTableRecord layer)
        {
            _layer = layer;
        }

        public void ApplyChange()
        {
            _layer.Name = TbName.Text;

            _layer.IsOff = (bool)!CbVisible.IsChecked;

            var colorValue = _layer.Color.ColorValue;

            byte r, g, b;

            if (!byte.TryParse(TbR.Text, out r))
                r = colorValue.R;

            if (!byte.TryParse(TbG.Text, out g))
                g = colorValue.G;

            if (!byte.TryParse(TbB.Text, out b))
                b = colorValue.B;

            _layer.Color = Color.FromRgb(r, g, b);
        }
    }
}
