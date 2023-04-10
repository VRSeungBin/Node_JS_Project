using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SerializableDictionary<TKey, TVaule> : Dictionary<TKey,TVaule>, ISerializationCallbackReceiver
{
    public List<TKey> InspectorKeys;
    public List<TVaule> InspectorValues;

    public SerializableDictionary()
    {
        InspectorKeys = new List<TKey>();
        InspectorValues = new List<TVaule>();
        SyncInspectorFromDictionary();
    }

    public new void Add(TKey key , TVaule value)
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
    public void SyncInspectorFromDictionary()               //딕셔너리에 있는것을 인스펙터 창과 싱크를 맞추는 함수
    {
        InspectorKeys.Clear();
        InspectorValues.Clear();

        foreach(KeyValuePair<TKey, TVaule> pair in this)
        {
            InspectorKeys.Add(pair.Key);
            InspectorValues.Add(pair.Value);
        }
    }

    public void SyncDictionaryFromInspector()                   //인스펙터에 있는것을 딕셔너리와 싱크를 맞춤
    {
        foreach (var key in Keys.ToList())
        {
            base.Remove(key);
        }

        for(int i = 0;i < InspectorKeys.Count; i++)
        {
            if(this.ContainsKey(InspectorKeys[i]))
            {
                Debug.LogError("중복 키가 있습니다. ");
                break;
            }

            base.Add(InspectorKeys[i], InspectorValues[i]); 
        }        
    }
    
    public void OnAfterDeserialize()
    {
        if (InspectorKeys.Count == InspectorValues.Count)
        {
            SyncDictionaryFromInspector();
        }
    }
}
