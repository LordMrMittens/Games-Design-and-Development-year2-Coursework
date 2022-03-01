using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    [SerializeField] Color firstColor;
    [SerializeField] Color secondColor;
    Color lerpedColor = Color.red;
    Material material;
    private void Start()
    {
        
        material = GetComponent<Renderer>().material;
        
    }
    void Update()
    {
        material.EnableKeyword("_EMISSION");
        lerpedColor = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time, .6f));
        material.SetColor("_EmissionColor", lerpedColor);
       
    }
}
