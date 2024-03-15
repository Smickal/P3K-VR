using System.Collections;
using System.Collections.Generic;
using BNG;
using Oculus.Interaction;
using UnityEngine;

public class InteractToolsController : MonoBehaviour
{
    [SerializeField]private HandActiveState leftHand, rightHand;
    [SerializeField]private GameObject leftControllerGameObject, rightControllerGameObject;
    [SerializeField]private PointableCanvasModule pointableCanvasModule;
    [SerializeField]private VRUISystem vRUISystem;
    private int checker = 0; //1 hands, 2 = controller

    // Update is called once per frame
    private void Update()
    {
        if(leftHand.Active || rightHand.Active)
        {
            // Debug.Log("???");
            if(checker != 1)
            {
                ControlSetActive(false);
                if(vRUISystem)vRUISystem.enabled = false;
                // pointableCanvasModule.Enable()
                
            }
            
        }
        else
        {
            // Debug.Log("what???");
            if(OVRInput.IsControllerConnected(OVRInput.Controller.LTouch) || OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
            {
                    leftControllerGameObject.SetActive(true);
                    rightControllerGameObject.SetActive(true);
                    
                    // pointableCanvasModule.Disable();
                    // if(checker != 2)
                    // {
                        if(vRUISystem)vRUISystem.enabled = true;
                        if(vRUISystem)vRUISystem.ReAddCameratoCanvas();
                        checker = 2;
                    // }
                    
                    
                
                
            }
        }
        Debug.Log(checker);
        
    }

    private void ControlSetActive(bool changeBool)
    {
        Debug.Log("Haloo??");
        leftControllerGameObject.SetActive(changeBool);
        rightControllerGameObject.SetActive(changeBool);
        if(!changeBool) checker = 1;
        else checker = 2;
    }
}
