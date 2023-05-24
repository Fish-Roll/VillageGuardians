using System;
using UnityEngine;

[Serializable]
public class KeyPairValue<K, V>
{
    public KeyPairValue(K key, V value)
    {
        _key = key;
        _value = value;
    }
    [SerializeField]
    private K _key;
    public K Key => _key;
    [SerializeField]
    private V _value;
    public V Value => _value;
}
