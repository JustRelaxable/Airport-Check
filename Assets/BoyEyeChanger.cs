using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoyEyeChanger : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedRenderer;
    private MaterialPropertyBlock block;
    public Image greenIndicator;
    public Image redIndicator;
    private ID id;

    private void Awake()
    {
        greenIndicator = GameObject.FindGameObjectWithTag("Green").GetComponent<Image>();
        redIndicator = GameObject.FindGameObjectWithTag("Red").GetComponent<Image>();
        block = new MaterialPropertyBlock();
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        id = GetComponentInParent<ID>();
    }

    void Start()
    {
        
        
    }

    void Update()
    {
        if(greenIndicator.color.a > 0)
        {
            block.SetVector("_MainTex_ST", new Vector4(1, 1, 0.2f, 0.7f));
        }
        if(redIndicator.color.a > 0)
        {
            block.SetVector("_MainTex_ST", new Vector4(1, 1, 0.4f, 0.3f));
        }

        if(GameManager.currentId == id)
            skinnedRenderer.SetPropertyBlock(block);
        block.SetVector("_MainTex_ST", new Vector4(1, 1, 0f, 0f));
    }
}
