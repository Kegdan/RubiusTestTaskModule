using System;
using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule.ChangableElements
{
    // оболочка для Line, содеражащяя связанные с ним элементы вьюшки
    internal class ChangeableLine : IChangeable
    {
        private Line _line;
        public ChangeableLine(Line line)
        {
            _line = line;
        }

        public void ApplyChange()
        {
            throw new System.NotImplementedException();
        }
    }
}
