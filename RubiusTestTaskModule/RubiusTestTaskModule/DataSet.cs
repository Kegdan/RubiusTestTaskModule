using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule
{
    // вспомогательный класс-контейнер
    internal class DataSet
    {
        public List<Line> Lines;
        public List<Circle> Circles;
        public List<DBPoint> Points;
        public List<LayerTableRecord> Layers;
        

        public DataSet()
        {
            Lines = new List<Line>();
            Circles = new List<Circle>();
            Points = new List<DBPoint>();
            Layers = new List<LayerTableRecord>();
        }

    }
}
