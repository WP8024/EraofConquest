using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInstancer : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Color color;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = Instantiate(meshRenderer.material);
        meshRenderer.material.SetColor("color", color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
