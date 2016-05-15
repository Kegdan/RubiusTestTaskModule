using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;


namespace RubiusTestTaskModule
{
    public class RubiusTestAutocadExtension : IExtensionApplication
    {
        private PaletteSet _paletteSet;
        private Dialog _dialog;
        private AutocadConnection _autocadConnection;


        // задаем команду автокада для вызова данной функции
        [CommandMethod("RubiusDialog")]
        public void OpenDialog()
        {
            // инициализация полей
            if (_paletteSet==null)
               FieldInitialization();


            _autocadConnection = new AutocadConnection();

            FindAndShowObjects();

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

            // привязываем вьюшку к paletteSet
            _paletteSet.AddVisual("RubiusDialog", _dialog);
        }

        // функция, вызываемая при нажатии на кнопку подтверждения
        private void ConformActionFormDialog()
        {
            _dialog.Clear();
            _autocadConnection.Dispose();
            // Делаем диалоговое окно невидимым
            _paletteSet.KeepFocus = false;
            _paletteSet.Visible = false;
        }


        public void FindAndShowObjects()
        {

            var objects = _autocadConnection.FindObjects();

            foreach (var layer in objects.Layers)
            {
                var layerContainer = _dialog.CreateLayerContainer(layer);


                foreach (var line in objects.Lines)
                {
                    if (line.Layer == layer.Name)
                    {
                        Expander expander2 = new Expander();
                        expander2.Header = "Line";
                        layerContainer.Children.Add(expander2);
                    }
                }

                foreach (var circle in objects.Circles)
                {
                    if (circle.Layer == layer.Name)
                    {
                        Expander expander2 = new Expander();
                        expander2.Header = "Circle";
                        layerContainer.Children.Add(expander2);
                    }
                }

                foreach (var point in objects.Points)
                {
                    if (point.Layer == layer.Name)
                    {
                        Expander expander2 = new Expander();
                        expander2.Header = "Point";
                        layerContainer.Children.Add(expander2);
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
