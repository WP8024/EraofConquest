using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInstancer : MonoBehaviour
{
    public static MaterialInstancer instance;
    [SerializeField]
    private GameObject Rendermodel;
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Color Deafult,Blue,Red;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = Instantiate(meshRenderer.material);
        meshRenderer.material.SetColor("color", Deafult);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
