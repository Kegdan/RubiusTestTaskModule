using System;
using System.Windows;
using System.Windows.Controls;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;


namespace RubiusTestTaskModule
{
    public class RubiusTestAutocadExtension : IExtensionApplication
    {
        private PaletteSet _paletteSet;
        private Dialog _dialog;

        // задаем команду автокада для вызова данной функции
        [CommandMethod("RubiusDialog")]
        public void OpenDialog()
        {
            // инициализация полей
            if (_paletteSet==null)
               FieldInitialization();

            // Делаем диалоговое окно видимым
            _paletteSet.KeepFocus = true;
            _paletteSet.Visible = true;
        }

        // инициализация полей
        public void FieldInitialization()
        {
            // создаем PaletteSet
            _paletteSet = new PaletteSet("RubiusDialog")
            {
                Size = new System.Drawing.Size(300, 300)
            };

            // создаем экземпляр wpf-вьюшки
            _dialog = new Dialog();

            // назначаем функцию для кнопки подтверждения
            _dialog.SetConformButtonClick(ConformActionFormDialog);

            for (int i = 0; i < 100; i++)
            {
                Expander expander = new Expander();
                expander.Header = "Expander"+i;
                _dialog.spMain.Children.Add(expander);

                Expander expander2 = new Expander();
                expander2.Header = "Expander2";
                expander2.Margin = new Thickness(20, 0, 0, 0);
                expander.Content = (expander2);

                StackPanel stackPanel = new StackPanel();
                expander2.Content = stackPanel;

                Label label = new Label();
                label.Content = "!!!!!";
                stackPanel.Children.Add(label);

                TextBox textBox = new TextBox();
                textBox.Text = "!!!!!";
                stackPanel.Children.Add(textBox);
            }
            // привязываем вьюшку к paletteSet
            _paletteSet.AddVisual("RubiusDialog", _dialog);
        }

        // функция, вызываемая при нажатии на кнопку подтверждения
        private void ConformActionFormDialog()
        {
            // Делаем диалоговое окно невидимым
            _paletteSet.KeepFocus = false;
            _paletteSet.Visible = false;
        }

        public void Initialize()
        {

        }

        public void Terminate()
        {

        }

    }
}
