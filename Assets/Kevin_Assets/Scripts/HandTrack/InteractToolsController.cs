using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractToolsController : MonoBehaviour
{
    [SerializeField]private GameObject leftControllerGameObject, rightControllerGameObject;
    private int checker = 0; //1 hands, 2 = controller

    // Update is called once per frame
    private void Update()
    {
        if(OVRInput.IsControllerConnected(OVRInput.Controller.Hands))
        {
            if(checker != 1)
            {
                ControlSetActive(false);
            }
            
        }
        else
        {
            if(OVRInput.IsControllerConnected(OVRInput.Controller.LTouch) || OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
            {
                    if(OVRInput.IsControllerConnected(OVRInput.Controller.LTouch)) leftControllerGameObject.SetActive(true);
                    if(OVRInput.IsControllerConnected(OVRInput.Controller.RTouch)) rightControllerGameObject.SetActive(true);
                    checker = 2;
                
                
            }
        }
        
    }

    private void ControlSetActive(bool changeBool)
    {
        leftControllerGameObject.SetActive(changeBool);
        rightControllerGameObject.SetActive(changeBool);
        if(!changeBool) checker = 1;
        else checker = 2;
    }
}
