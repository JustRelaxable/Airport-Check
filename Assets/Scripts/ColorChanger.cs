using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private MaterialPropertyBlock block;
    private SkinnedMeshRenderer meshRenderer;
    public ID id;

    private void Awake()
    {
        block = new MaterialPropertyBlock();    
        meshRenderer = GetComponent<SkinnedMeshRenderer>();      
    }

    void Start()
    {
        block.SetColor("_Color", id.bodyColor);
        meshRenderer.SetPropertyBlock(block, 0);
        meshRenderer.SetPropertyBlock(block, 1);
    }

    
    void Update()
    {
        
    }
}
