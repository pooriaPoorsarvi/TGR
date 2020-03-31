using System;
using UnityEngine;
using System.Collections;
using System.Security.Cryptography;


public class AutoTransparent : MonoBehaviour
{
    private Shader m_OldShader = null;
    private Color m_OldColor = Color.black;
    private float m_Transparency = 0.3f;
    private const float m_TargetTransparancy = 0.3f;
    private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec
    private Material m_material;

    public Material transparentMaterial;
    
    

    public void BeTransparent()
    {
        // reset the transparency;
        // m_Transparency = m_TargetTransparancy;
        // if (m_material == null)
        // {
            // Save the current shader
            // m_OldShader = GetComponent<Renderer>().material.shader;
            // m_OldColor  = GetComponent<Renderer>().material.color;
            // GetComponent<Renderer>().material = Shader.Find("Transparent/Diffuse");
        // }
        if (m_material == null)
        {
            m_material = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = transparentMaterial;
            StartCoroutine(DestroyInFuture(1f));   
        }
    }
    void Update()
    {
        // if (m_Transparency < 1.0f)
        // {
        //     // Color C = GetComponent<Renderer>().material.color;
        //     // C.a = m_Transparency;
        //     // GetComponent<Renderer>().material.color = C;
        // }
        // else
        // {
        //     // Reset the shader
        //     // GetComponent<Renderer>().material.shader = m_OldShader;
        //     // GetComponent<Renderer>().material.color = m_OldColor;
        //     if (m_material == transparentMaterial || m_material == null)
        //     {
        //         Debug.Log("Something went wrong with transparent col");
        //     }
        //     // And remove this script
        // }
        // m_Transparency += ((1.0f-m_TargetTransparancy)*Time.deltaTime) / m_FallOff;
    }

    IEnumerator DestroyInFuture(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = m_material;
        StartCoroutine(DestroyInFuture(.5f));
        Destroy(this);
    }

}