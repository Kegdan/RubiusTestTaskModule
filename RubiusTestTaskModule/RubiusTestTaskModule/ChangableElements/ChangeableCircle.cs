using System;
using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule
{
    // оболочка для Circle, содеражащяя связанные с ним элементы вьюшки
    internal class ChangeableCircle : IChangeable
    {
        private Circle _circle;

        public ChangeableCircle(Circle circle)
        {
            _circle = circle;
        }

        public void ApplyChange()
        {
            throw new System.NotImplementedException();
        }
    }


}
