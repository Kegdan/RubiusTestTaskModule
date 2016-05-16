using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule
{
    // вспосогательный класс, осуществляющий работу с обьектами автокада, их закрузку и сохранение
    internal class AutocadConnection:IDisposable
    {
        private Transaction _transaction;
        private Database _database;

        public AutocadConnection()
        {
            _database = Application.DocumentManager.MdiActiveDocument.Database;
            _transaction = _database.TransactionManager.StartTransaction();
        }

        // медот, вытаскивающий все интересующий нес обьекты. и помещающий их в класс-контейнер
        public DataSet FindObjects()
        {
            var objects = new DataSet();

            objects.Layers = FindLayers();

            BlockTableRecord ms = (BlockTableRecord)_transaction.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(_database), OpenMode.ForWrite);

            // "пробегаем" по всем объектам в пространстве модели
            foreach (ObjectId id in ms)
            {
                // приводим каждый из них к типу Entity
                Entity entity = (Entity)_transaction.GetObject(id, OpenMode.ForWrite);
                
                if (entity.GetType() == typeof(Line))
                   objects.Lines.Add(entity as Line);
                
                if (entity.GetType() == typeof(Circle))
                    objects.Circles.Add(entity as Circle);

                if (entity.GetType() == typeof(DBPoint))
                    objects.Points.Add(entity as DBPoint);
            }

            return objects;

        }

        // метод, вытаскивающий обьекты-слои
        private List<LayerTableRecord> FindLayers()
        {
            var layers = new List<LayerTableRecord>();

            LayerTable lt = _transaction.GetObject(_database.LayerTableId, OpenMode.ForWrite) as LayerTable;
            foreach (ObjectId layerId in lt)
            {
                var layer = _transaction.GetObject(layerId, OpenMode.ForWrite) as LayerTableRecord;
                layers.Add(layer);
            }

            return layers;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        
    }
}