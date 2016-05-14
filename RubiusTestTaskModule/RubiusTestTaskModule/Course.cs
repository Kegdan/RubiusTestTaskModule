using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;


namespace RubiusTestTaskModule
{
    public class test : IExtensionApplication
    {

        private PaletteSet _paletteSet;


        [CommandMethod("RubiusTestDialog")]
        public void Helloworld()
        {
            var editor = Application.DocumentManager.MdiActiveDocument.Editor;
            editor.WriteMessage("Привет из Autocad плагина, username!");

            if (_paletteSet == null)
            {
                _paletteSet = new PaletteSet("RubiusDialog")
                {
                    Size = new Size(300,300),
                    DockEnabled = (DockSides) ((int) DockSides.Left + (int) DockSides.Right)
                };

                Dialog dialog = new Dialog();
                _paletteSet.AddVisual("AddVisual", dialog);

            }
            // Display our palette set

            _paletteSet.KeepFocus = true;
            _paletteSet.Visible = true;
        }

        public void Initialize()
        {
            var editor = Application.DocumentManager.MdiActiveDocument.Editor;
            editor.WriteMessage("Инициализация плагина.." + Environment.NewLine);

           
        }

        public void Terminate()
        {

        }

    }
}
