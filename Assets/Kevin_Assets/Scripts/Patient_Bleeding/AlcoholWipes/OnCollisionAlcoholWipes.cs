using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionAlcoholWipes : MonoBehaviour
{
    [SerializeField] GameObject _leftGrabberHT;
    [SerializeField] GameObject _rightGrabberHT;
    AlcoholCleanManager cleanManager;

    private void Start()
    {
        cleanManager = AlcoholCleanManager.Instance;
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     Grabber grabber = collision.gameObject.GetComponent<Grabber>();
        

    //     if (grabber == null && (collision.gameObject != _leftGrabberHT && collision.gameObject != _rightGrabberHT) ) return;
    //     Debug.Log("Yang kena sini adalah EnterCol" + gameObject);
    //     cleanManager.RegisterCurrentNeedToCleanGrabber(collision.gameObject);
    // }
    // private void OnCollisionExit(Collision other) {
    //     Grabber grabber = other.gameObject.GetComponent<Grabber>();
        

    //     if (grabber == null && (other.gameObject != _leftGrabberHT && other.gameObject != _rightGrabberHT)) return;
    //     Debug.Log("Yang kena sini adalah ExitCol" + gameObject);
    //     cleanManager.UnRegisterCurrentNeedToCleanGrabber(other.gameObject);
    // }

    private void OnTriggerEnter(Collider other)
    {
        if((GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || 
            GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid))return;

        Grabber grabber = other.gameObject.GetComponent<Grabber>();


        if (grabber == null && (other.gameObject != _leftGrabberHT && other.gameObject != _rightGrabberHT)) return;
        // Debug.Log("Yang kena sini adalah EnterTrig" + other.gameObject);
        cleanManager.RegisterCurrentNeedToCleanGrabber(other.gameObject);
    }
    private void OnTriggerExit(Collider other) {
        if((GameManager.CheckLevelTypeNow() != LevelP3KType.Bleeding || 
            GameManager.CheckInGameModeNow() != InGame_Mode.FirstAid))return;

        Grabber grabber = other.gameObject.GetComponent<Grabber>();
        

        if (grabber == null && (other.gameObject != _leftGrabberHT && other.gameObject != _rightGrabberHT)) return;
        // Debug.Log("Yang kena sini adalah ExitTrig" + other.gameObject);
        cleanManager.UnRegisterCurrentNeedToCleanGrabber(other.gameObject);
    }

}
