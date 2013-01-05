﻿using System.Collections.Generic;
using Sledge.DataStructures.MapObjects;
using Sledge.Editor.Documents;

namespace Sledge.Editor.History
{
    /// <summary>
    /// A history create adds objects to the map.
    /// </summary>
    public class HistoryCreate : IHistoryItem
    {
        public string Name { get; private set; }
        private readonly List<MapObject> _createdObjects;

        public HistoryCreate(string name, IEnumerable<MapObject> createdObjects)
        {
            Name = name;
            _createdObjects = new List<MapObject>(createdObjects);
        }

        public void Undo(Document document)
        {
            _createdObjects.ForEach(x => x.Parent.Children.Remove(x));
        }

        public void Redo(Document document)
        {
            _createdObjects.ForEach(x => x.Parent.Children.Add(x));
        }

        public void Dispose()
        {
            _createdObjects.Clear();
        }
    }
}