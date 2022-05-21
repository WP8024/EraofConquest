using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Area
{
    public static Color Red, Blue, Cur;

}

public class AreaColor : MonoBehaviour
{
    //[SerializeField] Area[] areas;
    //[SerializeField] GameObject Tile;
    //[SerializeField] Material tileMaterial;
    //[SerializeField] int code;
    //Dictionary<int, Material> materialCodeTable = new Dictionary<int, Material>();

    public Color blue, red;
    private Renderer render;
    private Color startColor;
    public bool isChanged = false;

    private void Awake()
    {
        render = transform.GetComponent<MeshRenderer>();
        startColor = render.material.color;
    }

    //public Material GetMaterial()
    //{
    //    if (materialCodeTable.ContainsKey(code))
    //    {
    //        return materialCodeTable[code];
    //    }
    //    else
    //    {
    //        Material material = new Material(tileMaterial);
    //        material.mainTexture = Array.Find(areas, x => x.code == code).texture;
    //        materialCodeTable.Add(code, material);
    //        return material;
    //    }
    //}


    void OnTriggerEnter(Collider other)
    {
        if (!isChanged)
        {
            if (other.CompareTag("Blue"))
            {
                render.material.color = blue;
                isChanged = true;
               

            }
            else if (other.CompareTag("Red"))
            {
                render.material.color = red;
                isChanged = true;
             
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (isChanged)
        {
            
            isChanged = false;
  
            render.material.color=startColor;
        }
    }
}
