using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderCorrections : MonoBehaviour
{
    [SerializeField] GameObject[] iceBlocks;
    [SerializeField] GameObject[] iceBlocksTransparency;
    [SerializeField] GameObject fog;

    private void Start()
    {
        //for(int i = 0; i < iceBlocks.Length; i++)
        //{
        //    iceBlocksTransparency[i].GetComponent<Renderer>().material.renderQueue = 3000 + i + 1;
        //    iceBlocks[i].GetComponent<Renderer>().material.renderQueue = 3000 + i + 1;
        //}
    }
}
