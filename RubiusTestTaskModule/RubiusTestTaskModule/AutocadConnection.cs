using System;
using System.Collections;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace RubiusTestTaskModule
{
    internal class AutocadConnection:IDisposable
    {
        private Transaction _transaction;
        private Database _database;

        public AutocadConnection()
        {
            _database = Application.DocumentManager.MdiActiveDocument.Database;
            _transaction = _database.TransactionManager.StartTransaction();
        }

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


        public void Dispose()
        {
            _transaction.Dispose();
        }

        public DataSet FindObjects()
        {
            var objects = new DataSet();

            objects.Layers = FindLayers();

            BlockTableRecord ms = (BlockTableRecord)_transaction.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(_database), OpenMode.ForRead);

            // "пробегаем" по всем объектам в пространстве модели
            foreach (ObjectId id in ms)
            {
                // приводим каждый из них к типу Entity
                Entity entity = (Entity)_transaction.GetObject(id, OpenMode.ForRead);
                
                if (entity.GetType() == typeof(Line))
                   objects.Lines.Add(entity as Line);
                
                if (entity.GetType() == typeof(Circle))
                    objects.Circles.Add(entity as Circle);

                if (entity.GetType() == typeof(DBPoint))
                    objects.Points.Add(entity as DBPoint);
            }

            return objects;

        }
    }
}