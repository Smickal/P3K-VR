using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandsMaterial
{
    Normal, Gloves
}
public class ChangeHandMaterial : MonoBehaviour
{
    [SerializeField]private Material[] materialHands;
    [SerializeField]private Material normalMaterial, glovesMaterial;
    [Header("Debug ONLY")]
    public bool normalOn, glovesOn;
    private void Awake() 
    {
        ChangeHandsMaterial(HandsMaterial.Normal);
    }
    private void Update() 
    {
        if(normalOn)
        {
            normalOn = false;
            ChangeHandsMaterial(HandsMaterial.Normal);
        }
        if(glovesOn)
        {
            glovesOn = false;
            ChangeHandsMaterial(HandsMaterial.Gloves);
        }
    }

    public void ChangeHandsMaterial(HandsMaterial handsMaterial)
    {
        for(int i=0;i<materialHands.Length;i++)
        {
            if(handsMaterial == HandsMaterial.Normal)
            {
                materialHands[i].shader = normalMaterial.shader;
                // materialHands[i].CopyPropertiesFromMaterial(normalMaterial);
                // materialHands[i].parent = normalMaterial;
            }
            else if(handsMaterial == HandsMaterial.Gloves)
            {
                materialHands[i].shader = glovesMaterial.shader;
                // materialHands[i].CopyPropertiesFromMaterial(glovesMaterial);
                // materialHands[i].parent = glovesMaterial;
            }
        }
        
    }
}
