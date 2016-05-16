using System;
using System.Collections.Generic;
using System.Drawing;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;


namespace RubiusTestTaskModule
{
    public class RubiusTestAutocadExtension : IExtensionApplication
    {
        private PaletteSet _paletteSet;
        private Dialog _dialog;
        private AutocadConnection _autocadConnection;
        private List<IChangeable> _Changebleobjects;

            // задаем команду автокада для вызова данной функции
        [CommandMethod("RubiusDialog")]
        public void OpenDialog()
        {
            // инициализация полей
            if (_paletteSet==null)
               FieldInitialization();

            //создаем новую коллекцию и соединение
            _Changebleobjects = new List<IChangeable>();
            _autocadConnection = new AutocadConnection();

            // создаем вьюшку и изменяемые оболочки для обьектов
            CreateChangeableObject();

            // Делаем диалоговое окно видимым
            _paletteSet.KeepFocus = true;
            _paletteSet.Visible = true;
        }

        // инициализация полей
        public void FieldInitialization()
        {
            // создаем экземпляр wpf-вьюшки
            _dialog = new Dialog();

            // создаем PaletteSet
            _paletteSet = new PaletteSet("RubiusDialog")
            {
                Size = new Size(350, 400)
            };

            // назначаем функцию для кнопки подтверждения
            _dialog.SetConformButtonClick(ConformActionFormDialog);

            // привязываем вьюшку к paletteSet
            _paletteSet.AddVisual("RubiusDialog", _dialog);
        }

        // функция, вызываемая при нажатии на кнопку подтверждения
        private void ConformActionFormDialog()
        {
            // Записываем изменения пользователя в обьекты
            foreach (var co in _Changebleobjects)
                co.ApplyChange();
            // очищаем диалог, записываем изменения в автокад и закрываем соединение
            _dialog.Clear();
            _autocadConnection.Commit();
            _autocadConnection.Dispose();
            
            // Делаем диалоговое окно невидимым
            _paletteSet.KeepFocus = false;
            _paletteSet.Visible = false;
        }

        // Метод получает оболочки для обьектов
        public void CreateChangeableObject()
        {
            // получаем обьекты из автокада
            var objects = _autocadConnection.FindObjects();

            // идем по всем уровням
            foreach (var layer in objects.Layers)
            {
                // получаем и записываем изменияемую оболочку уровня
                var changeableLayer = ViewAndDataConstractor.CreateChangeableLayer(layer, _dialog.spMain);

                _Changebleobjects.Add(changeableLayer);

                foreach (var line in objects.Lines)
                {
                    // группируем по уровням
                    if (line.Layer == layer.Name)
                    {
                        IChangeable changeableLine = ViewAndDataConstractor.CreateChangeableLine(line,
                            changeableLayer.SpLayerContainer);
                        // получаем и записываем изменияемую оболочку
                        _Changebleobjects.Add(changeableLine);
                    }
                }

                foreach (var circle in objects.Circles)
                {
                    // группируем по уровням
                    if (circle.Layer == layer.Name)
                    {
                        IChangeable changeableCircle = ViewAndDataConstractor.CreateChangeableCircle(circle,
                            changeableLayer.SpLayerContainer);
                        // получаем и записываем изменияемую оболочку
                        _Changebleobjects.Add(changeableCircle);
                    }
                }

                foreach (var point in objects.Points)
                {
                    // группируем по уровням
                    if (point.Layer == layer.Name)
                    {
                        IChangeable changeablePoint = ViewAndDataConstractor.CreateChangeablePoint(point,
                            changeableLayer.SpLayerContainer);
                        // получаем и записываем изменияемую оболочку
                        _Changebleobjects.Add(changeablePoint);
                    }
                }
            }


        }

        public void Initialize()
        {

        }

        public void Terminate()
        {

        }

    }
}
