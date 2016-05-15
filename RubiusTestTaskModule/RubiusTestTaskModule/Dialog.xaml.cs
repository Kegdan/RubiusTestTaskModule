
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using CheckBox = System.Windows.Controls.CheckBox;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Label = System.Windows.Controls.Label;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;


namespace RubiusTestTaskModule
{
    /// <summary>
    /// Логика взаимодействия для Dialog.xaml
    /// </summary>
    public partial class Dialog : UserControl
    {
        // Функция, вызываемая при нажатии кнопки подтверждения
        private Action _conformAction;

        public Dialog()
        {
            InitializeComponent();
        }

        // задаем функцию
        public void SetConformButtonClick(Action conformAction)
        {
            _conformAction = conformAction;
        }

        // При нажатии на кнопку просто вызываем привязанную функцию
        private void BConfirmClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _conformAction.Invoke();
        }

        public void Clear()
        {
            spMain.Children.Clear();
        }

        public StackPanel CreateLayerContainer(LayerTableRecord layer)
        {
            Expander expander = new Expander();
            expander.Header = layer.Name;
            spMain.Children.Add(expander);

            StackPanel stackPanel = new StackPanel();
            expander.Content = stackPanel;

            stackPanel.Margin = new Thickness(20,0,0,0);

            Grid gName = new Grid();
            gName.Name = "gName";

            Label lName = new Label();
            lName.Content = "Name";
            lName.Margin = new Thickness(0, 0, 0, 0);
            gName.Children.Add(lName);

            TextBox tbName = new TextBox();
            tbName.Name = "tbName";
            tbName.Text = layer.Name;
            tbName.Margin = new Thickness(70, 0, 0, 0);
            tbName.Width = 100;
            tbName.HorizontalAlignment = HorizontalAlignment.Left;

            if (tbName.Text == "0")
                tbName.IsEnabled = false;

            gName.Children.Add(tbName);

            stackPanel.Children.Add(gName);

            Grid gVisible = new Grid();
            gVisible.Name = "gVisible";

            CheckBox cbVisible = new CheckBox();

            cbVisible.Name = "cbVisible";
            cbVisible.Content = "IsVisible";
            cbVisible.IsChecked = !layer.IsOff;
            cbVisible.Margin = new Thickness(0, 0, 0, 0);

            gVisible.Children.Add(cbVisible);

            stackPanel.Children.Add(gVisible);

            var color = layer.Color.ColorValue;

            Grid gColor = new Grid();
            gColor.Name = "gColor";

            Label lColor = new Label();
            lColor.Content = "Color RGB";
            lColor.Margin = new Thickness(0, 0, 0, 0);
            gColor.Children.Add(lColor);

            TextBox mtbR = new TextBox();
            mtbR.Text = color.R.ToString();
            mtbR.Margin = new Thickness(70, 0, 0, 0);
            mtbR.HorizontalAlignment = HorizontalAlignment.Left;
            mtbR.Width = 40;

            gColor.Children.Add(mtbR);

            TextBox mtbG = new TextBox();
            mtbG.Text = color.G.ToString();
            mtbG.Margin = new Thickness(120, 0, 0, 0);
            mtbG.HorizontalAlignment = HorizontalAlignment.Left;
            mtbG.Width = 40;

            gColor.Children.Add(mtbG);

            TextBox mtbB = new TextBox();
            mtbB.Text = color.B.ToString();
            mtbB.Margin = new Thickness(170, 0, 0, 0);
            mtbB.HorizontalAlignment = HorizontalAlignment.Left;
            mtbB.Width = 40;

            gColor.Children.Add(mtbB);

            stackPanel.Children.Add(gColor);


            return stackPanel;
        }

       
    }
}
