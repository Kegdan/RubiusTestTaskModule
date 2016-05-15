using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule
{
    // Свпомогательный класс для постоение вью
    internal class ViewAndDataConstractor
    {
        // панель, на которую привязываются созданные вьюшки
        private StackPanel _mainPanel;

        public ViewAndDataConstractor(StackPanel mainPanel)
        {
            _mainPanel = mainPanel;
        }
        // создаем вьюшку для слоя и попутно собираем обьект 
        public ChangeableLayer CreateLayerContainer(LayerTableRecord layer)
        {
            var changeableLayer = new ChangeableLayer(layer);

            var stackPanel = CreateLevel(layer.Name);

            changeableLayer.LayerContainer = stackPanel;

            changeableLayer.TbName = AddLayerNameParameter(layer.Name, stackPanel);

            changeableLayer.CbVisible = AddLayerVisibleParameter(layer.IsOff, stackPanel);

            AddLayerColorParameterAndSetRGB(layer.Color.ColorValue, stackPanel, changeableLayer);

            return changeableLayer;
        }

        // создаем новый уровень в виде раскрывающейся вкладки и возвращаем его внутренний контейнер
        private StackPanel CreateLevel(string levelName)
        {
            Expander expander = new Expander();
            expander.Header = levelName;
            _mainPanel.Children.Add(expander);

            StackPanel stackPanel = new StackPanel();
            expander.Content = stackPanel;
            stackPanel.Margin = LayoutConst.LevelMargin;
            return stackPanel;
        }

        // Создаем лейбл и текст бок для отображения имени слоя
        private TextBox AddLayerNameParameter(string layerName, StackPanel stackPanel)
        {
            Grid gName = new Grid();
            Label lName = new Label();
            lName.Content = TextConst.NameLabelText;
            gName.Children.Add(lName);

            TextBox tbName = new TextBox();
            tbName.Text = layerName;
            tbName.Margin = LayoutConst.StandartMargin;
            tbName.Width = LayoutConst.StandartTextBoxWidth;
            tbName.HorizontalAlignment = HorizontalAlignment.Left;

            // костыль, не позловяющий сменить имя главного слоя с "0" (автокад против)
            if (tbName.Text == TextConst.LayerSpecialName)
                tbName.IsEnabled = false;

            gName.Children.Add(tbName);

            stackPanel.Children.Add(gName);

            return tbName;
        }

        // Добавляем чекбокс для параметра видимости слоя
        private CheckBox AddLayerVisibleParameter(bool layerIsOff, StackPanel stackPanel)
        {
            Grid gVisible = new Grid();

            CheckBox cbVisible = new CheckBox();

            cbVisible.Content = TextConst.VisibleCheckBoxText;
            cbVisible.IsChecked = !layerIsOff;

            gVisible.Children.Add(cbVisible);

            stackPanel.Children.Add(gVisible);

            return cbVisible;
        }

        // Добавляет отображение полей цвета в виде RGB и попутно заполняет поля цветовых компонент
        // цвет отоброжается в виде RGB так как не всем цветам соответствуют имена (можно было бы красивый выпадающий список, но увы)
        private static void AddLayerColorParameterAndSetRGB(Color color, StackPanel stackPanel, ChangeableLayer changeableLayer)
        {
            Grid gColor = new Grid();

            Label lColor = new Label();
            lColor.Content = TextConst.ColorLabelText;
            gColor.Children.Add(lColor);

            changeableLayer.TbR = GetValue(color.R.ToString(), gColor, LayoutConst.ColorTBWidth[0]);
            changeableLayer.TbG = GetValue(color.G.ToString(), gColor, LayoutConst.ColorTBWidth[1]);
            changeableLayer.TbB = GetValue(color.B.ToString(), gColor, LayoutConst.ColorTBWidth[2]);

            stackPanel.Children.Add(gColor);
        }

        // создаем текстбокс для конкретной цветовой компоненты 
        private static TextBox GetValue(string colorComponentValue, Grid gColor, int offset)
        {
            TextBox mtbR = new TextBox();
            mtbR.Text = colorComponentValue;
            mtbR.Margin = new Thickness(offset, 0, 0, 0);
            mtbR.HorizontalAlignment = HorizontalAlignment.Left;
            mtbR.Width = 40;

            gColor.Children.Add(mtbR);
            return mtbR;
        }

        // вспомогательный класс констант разметки
        static class LayoutConst
        {
            public static Thickness LevelMargin = new Thickness(20, 0, 0, 0);
            public static Thickness StandartMargin = new Thickness(70, 0, 0, 0);
            public static int StandartTextBoxWidth = 100;
            public static int[] ColorTBWidth = { 70, 120, 170 };
        }

        // вспомогательный класс текстовых констант
        static class TextConst
        {
            public static string ColorLabelText = "Color RGB";
            public static string NameLabelText = "Name";
            public static string VisibleCheckBoxText = "IsVisible";
            public static string LayerSpecialName = "0";
        }
    }
}