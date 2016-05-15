using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule
{
    // оболочка для DBPoint, содеражащяя связанные с ним элементы вьюшки
    internal class ChangeablePoint : IChangeable
    {
        private DBPoint _point;
        public ChangeablePoint(DBPoint point)
        {
            _point = point;
        }

        public void ApplyChange()
        {
            throw new System.NotImplementedException();
        }
    }
}
