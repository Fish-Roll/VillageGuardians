using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<KeyPairValue<TKey, TValue>> _dictonary = new List<KeyPairValue<TKey, TValue>>();

    public void OnBeforeSerialize() => Clear();
    public void OnAfterDeserialize()
    {
        Clear();
        for (int i = 0; i < _dictonary.Count; i++)
        {
            var kvp = _dictonary[i];
            if (!ContainsKey(kvp.Key))
                Add(kvp.Key, kvp.Value);
            else
                throw new Exception($"Ключ элемента под индексом {i} является копией");
        }
    }
}