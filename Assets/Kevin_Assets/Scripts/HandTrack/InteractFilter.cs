using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class InteractFilter : MonoBehaviour, IGameObjectFilter
{
    [Tooltip("If banObject = false, hanya yang di list gameobject yang bisa masuk; sebaliknya jika true, object di list ke ban")]
    [SerializeField]bool banObject = true;
    [SerializeField] private List<string> gameObjectNamesList;
    public bool Filter(GameObject gameObject)
    {
        if(gameObjectNamesList != null)
        {
            foreach (string currList in gameObjectNamesList)
            {
                // Debug.Log(gameObject + " " + currList);
                if(gameObject.transform.parent.gameObject.name == currList)
                {
                    if(banObject)return false;
                    else return true;
                }
            }
            if(banObject)return true;
            else return false;

        }
        
        return true;
        
    }
}
