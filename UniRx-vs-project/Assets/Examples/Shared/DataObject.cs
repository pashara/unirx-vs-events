using System;
using UnityEngine;

namespace Examples.Shared
{
    [Serializable]
    public class DataObject
    {
        public int numberInt;
        public string textString;
        public GameObject gameObject;

        public override string ToString()
        {
            var gameObjectName = (gameObject == null) ? string.Empty : gameObject.name;
            return $"DO: {numberInt}, {textString}, {gameObjectName}";
        }
    }
}