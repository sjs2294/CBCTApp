using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelTransparency : MonoBehaviour
{
   // Start is called before the first frame update

    public Material mat; 
    public float alpha = 0.15f; 
    public Slider slider;

    void Start()
    {
        
    }

    void Update() {
        ChangeTransparency(slider.value);
        
    }

    void ChangeTransparency(float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
