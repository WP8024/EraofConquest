using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    static readonly Data instance = new Data();

    public static Data Instance => instance;

    //모든 오브젝트 정보 저장 자료구조
    //<오브젝트 이름,오브젝트 정보>
    private Dictionary<string, ObjectBody> objectDictionary;

    public void Setup()
    {
        objectDictionary = new Dictionary<string, ObjectBody>();
    }

    public void RegisterObject(ObjectBody newObj)
    {
        objectDictionary.Add(newObj.name, newObj);
    }

    public ObjectBody GetObjectFromID(string objName)
    {
        foreach (KeyValuePair<string, ObjectBody> obj in objectDictionary)
        {
            if (obj.Key == objName)
            {
                return obj.Value;
            }
        }

        return null;
    }

    public void RemoveObject(ObjectBody removeObj)
    {
        objectDictionary.Remove(removeObj.name);
    }
}
