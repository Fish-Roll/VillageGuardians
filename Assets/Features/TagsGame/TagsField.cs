using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.TagsGame
{
    [Serializable]
    public class TagsField
    {
        [SerializeField] private SerializableDictionary<TagsPoint, Knuckle> field;
        
        public SerializableDictionary<TagsPoint, Knuckle> TagsGameField => field;

        private List<int> _possibleMoves;
        
        public List<int> PossibleMoves => _possibleMoves;
        
        public TagsPoint GetEmptyPoint()
        {
            foreach (var point in field)
                if (point.Value == null)
                    return point.Key;
            return null;
        }
        
        
    }
}