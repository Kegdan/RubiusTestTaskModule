
using System;
using System.Windows.Controls;


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
    }
}
