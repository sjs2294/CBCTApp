using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

  /// This function sets the alpha value of the input material based on a slider's value
public class ModelTransparency : MonoBehaviour
{


    /// The material whose transparency is being changed 
    public Material mat; 

    /// defualt value for transparency
    public float alpha = 0.15f; 

    /// The slider controlling the alpha value
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
