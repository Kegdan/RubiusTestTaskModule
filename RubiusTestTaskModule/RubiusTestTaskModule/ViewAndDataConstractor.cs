using System;
using System.Windows;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using RubiusTestTaskModule.ChangableElements;

namespace RubiusTestTaskModule
{
    // Свпомогательный класс для постоение вью
    internal static class ViewAndDataConstractor
    {

        // создаем вьюшку для слоя и попутно собираем обьект 
        public static ChangeableLayer CreateChangeableLayer(LayerTableRecord layer, StackPanel layerContainer)
        {
            var changeableLayer = new ChangeableLayer(layer);

            var stackPanel = CreateLevel(layer.Name, layerContainer);

            changeableLayer.SpLayerContainer = stackPanel;

            changeableLayer.TbName = AddSingleParameter(TextConst.NameLabelText, layer.Name, stackPanel);

            changeableLayer.CbVisible = AddLayerVisibleParameter(layer.IsOff, stackPanel);

            changeableLayer.TbRGB = AddPointParameter(TextConst.ColorLabelText, layer.Color.ColorValue.R.ToString(),
                layer.Color.ColorValue.G.ToString(), layer.Color.ColorValue.B.ToString(), stackPanel);

            return changeableLayer;
        }

        // создаем вьюшку для линии и попутно собираем обьект 
        public static IChangeable CreateChangeableLine(Line line, StackPanel layerContainer)
        {
            ChangeableLine changeableLine = new ChangeableLine(line);

            var stackPanel = CreateLevel(TextConst.LineLevelText, layerContainer);

            changeableLine.TbStartPoint = AddPointParameter(TextConst.StartPointText, line.StartPoint.X.ToString(),
                line.StartPoint.Y.ToString(), line.StartPoint.Z.ToString(), stackPanel);

            changeableLine.TbEndPoint = AddPointParameter(TextConst.EndPointText, line.EndPoint.X.ToString(),
                line.EndPoint.Y.ToString(), line.EndPoint.Z.ToString(), stackPanel);

            changeableLine.TbThickness = AddSingleParameter(TextConst.ThicknessText, line.Thickness.ToString(),
                stackPanel);

            return changeableLine;
        }

        // создаем вьюшку для круга и попутно собираем обьект 
        public static IChangeable CreateChangeableCircle(Circle circle, StackPanel layerContainer)
        {
            ChangeableCircle changeableCircle = new ChangeableCircle(circle);

            var stackPanel = CreateLevel(TextConst.CircleLayerText, layerContainer);

            changeableCircle.TbCenter = AddPointParameter(TextConst.CenterText, circle.Center.X.ToString(),
                circle.Center.Y.ToString(), circle.Center.Z.ToString(), stackPanel);

            changeableCircle.TbRadius = AddSingleParameter(TextConst.RadiusText, circle.Radius.ToString(), stackPanel);

            changeableCircle.TbThickness = AddSingleParameter(TextConst.ThicknessText, circle.Thickness.ToString(),
                stackPanel);

            return changeableCircle;
        }

        // создаем вьюшку для точки и попутно собираем обьект 
        public static IChangeable CreateChangeablePoint(DBPoint point, StackPanel layerContainer)
        {
            ChangeablePoint changeablePoint = new ChangeablePoint(point);

            var stackPanel = CreateLevel(TextConst.PointLevelText, layerContainer);

            changeablePoint.TBPosition = AddPointParameter(TextConst.EndPointText, point.Position.X.ToString(),
                point.Position.Y.ToString(), point.Position.Z.ToString(), stackPanel);

            changeablePoint.TbThickness = AddSingleParameter(TextConst.ThicknessText, point.Thickness.ToString(),
                stackPanel);

            return changeablePoint;
        }

#region Auxiliary_methods

        // Метод, создающий вью для точки (лейбл и 3 текстбокса) и возвращающий текстбоксы
        private static Tuple<TextBox, TextBox, TextBox> AddPointParameter(string name, string xValue, string yValue, string zValue, StackPanel stackPanel)
        {
            Grid grid = new Grid();

            Label lColor = new Label();
            lColor.Content = name;
            grid.Children.Add(lColor);

            var X = GetValue(xValue, grid, LayoutConst.LittleTBOffset[0]);
            var Y = GetValue(yValue, grid, LayoutConst.LittleTBOffset[1]);
            var Z = GetValue(zValue, grid, LayoutConst.LittleTBOffset[2]);

            stackPanel.Children.Add(grid);
            return new Tuple<TextBox, TextBox, TextBox>(X,Y,Z);
        }

        // создаем новый уровень в виде раскрывающейся вкладки и возвращаем его внутренний контейнер
        private static StackPanel CreateLevel(string levelName, StackPanel layerContainer)
        {
            Expander expander = new Expander();
            expander.Header = levelName;
            layerContainer.Children.Add(expander);

            StackPanel stackPanel = new StackPanel();
            expander.Content = stackPanel;
            stackPanel.Margin = LayoutConst.LevelMargin;
            return stackPanel;
        }

        // Создаем лейбл и текст бокс для отображения пареметра с одним тестовым полем
        private static TextBox AddSingleParameter(string name, string value, StackPanel stackPanel)
        {
            Grid grid = new Grid();
            Label label = new Label();
            label.Content = name;
            grid.Children.Add(label);

            TextBox tb = new TextBox();
            tb.Text = value;
            tb.Margin = LayoutConst.StandartMargin;
            tb.Width = LayoutConst.StandartTextBoxWidth;
            tb.HorizontalAlignment = HorizontalAlignment.Left;

            grid.Children.Add(tb);

            stackPanel.Children.Add(grid);

            return tb;
        }

        // Добавляем чекбокс для параметра видимости слоя
        private static CheckBox AddLayerVisibleParameter(bool layerIsOff, StackPanel stackPanel)
        {
            Grid gVisible = new Grid();

            CheckBox cbVisible = new CheckBox();

            cbVisible.Content = TextConst.VisibleCheckBoxText;
            cbVisible.IsChecked = !layerIsOff;

            gVisible.Children.Add(cbVisible);

            stackPanel.Children.Add(gVisible);

            return cbVisible;
        }

       

        // создаем маленький текстбокс
        private static TextBox GetValue(string colorComponentValue, Grid gColor, Thickness offset)
        {
            TextBox tb = new TextBox();
            tb.Text = colorComponentValue;
            tb.Margin = offset;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.Width = LayoutConst.LittleTextBoxWigth;
            gColor.Children.Add(tb);
            return tb;
        }

#endregion

        // вспомогательный класс констант разметки
        static class LayoutConst
        {
            public static Thickness LevelMargin = new Thickness(20, 0, 0, 0);
            public static Thickness StandartMargin = new Thickness(70, 5, 0, 0);
            public static int StandartTextBoxWidth = 100;
            public static Thickness[] LittleTBOffset = { new Thickness(70, 5, 0, 0), new Thickness(130, 5, 0, 0), new Thickness(190, 5, 0, 0) };
            public static int LittleTextBoxWigth = 50;
        }

        // вспомогательный класс текстовых констант
        static class TextConst
        {
            public static string ColorLabelText = "Color RGB";
            public static string NameLabelText = "Name";
            public static string VisibleCheckBoxText = "IsVisible";
            public static string LayerSpecialName = "0";
            public static string LineLevelText = "Line";
            public static string StartPointText = "Start point";
            public static string EndPointText = "End point";
            public static string ThicknessText = "Thickness";
            public static string CenterText = "Center";
            public static string RadiusText = "Radius";
            public static string CircleLayerText = "Circle";
            public static string PointLevelText = "Point";
        }

    }
}