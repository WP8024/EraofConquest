using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjModel",menuName = "Custom/Obj Data")]
public class ObjectDatabase:ScriptableObject
{
    public List<ObjectData> ObjectDB = new List<ObjectData>();
}
