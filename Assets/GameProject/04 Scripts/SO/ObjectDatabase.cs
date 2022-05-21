using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDatabase
{
    static readonly ObjectDatabase instance = new ObjectDatabase();
    
    public static ObjectDatabase Instance => instance;

    //��� ������Ʈ ���� ���� �ڷᱸ��
    //<������Ʈ �̸�,������Ʈ ����>
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
        foreach(KeyValuePair<string,ObjectBody> obj in objectDictionary)
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
