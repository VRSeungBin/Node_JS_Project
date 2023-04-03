using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    public List<TKey> InspectorKeys;
    public List<TValue> InspectorValues;

    public SerializableDictionary()
    {
        InspectorKeys = new List<TKey>();
        InspectorValues = new List<TValue>();

    }

    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        SyncInspectorFromDictionary();
    }

    public new void Remove(TKey key)
    {
        base.Remove(key);
        SyncInspectorFromDictionary();
    }

    public void OnBeforeSerialize() { }
    public void SyncInspectorFromDictionary()
    {
        InspectorKeys.Clear();
        InspectorValues.Clear();

        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            InspectorKeys.Add(pair.Key);
            InspectorValues.Add(pair.Value);
        }
    }
    public void SyncDictionaryFromInspector()
    {
        foreach (var key in Keys.ToList())
        {
            base.Remove(key);
        }

        for(int i = 0; i < InspectorKeys.Count; i++)
        {
            if (this.ContainsKey(InspectorKeys[i]))
            {
                Debug.LogError("중복 키가 있습니다. ");
                break;
            }

            base.Add(InspectorKeys[i], InspectorValues[i]);
        }
    }

    public void OnAfterDeserialize()
    {
        if(InspectorKeys.Count == InspectorValues.Count)
        {
            SyncDictionaryFromInspector();
        }
    }
}
