using System.Collections;
using System.Collections.Generic;
using BNG;
using Unity.VisualScripting;
using UnityEngine;

public class GlassShardsCollide : MonoBehaviour
{
    [SerializeField]Bleeding_QuestManager bleeding_QuestManager;
    [SerializeField]private List<Collider> _ignoredColliders;
    [SerializeField]private SnapZone[] snapZones;
    private void OnCollisionEnter(Collision other) 
    {
        if(GameManager.CheckGameStateNow() != GameState.InGame || GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid)return;
        
        Debug.Log(other.gameObject + " Collideee ");
        if(!CheckIgnoredCollidersList(other.collider))
        {
            if(!CheckSnapZoneHeldItem(other.gameObject))
            {
                Debug.Log("Patient Dissatisfied");
                if(bleeding_QuestManager)bleeding_QuestManager.PatientDissatisfy();
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.CheckGameStateNow() != GameState.InGame || GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid)return;
        
        
        if(!CheckIgnoredCollidersList(other))
        {
            if(!CheckSnapZoneHeldItem(other.gameObject))
            {
                Debug.Log(other.gameObject + " Collideee ");
                Debug.Log("Patient Dissatisfied");
                if(bleeding_QuestManager)bleeding_QuestManager.PatientDissatisfy();
            }
        }
    }
    private bool CheckIgnoredCollidersList(Collider collide)
    {
        foreach(Collider collision in _ignoredColliders)
        {
            if(collide == collision)
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckSnapZoneHeldItem(GameObject collide)
    {
        foreach(SnapZone snapZone in snapZones)
        {
            if(snapZone.HeldItem != null)
            {
                if(collide == snapZone.HeldItem)
                {
                    return true;
                }
            }
        }
        return false;
    }
    

}
