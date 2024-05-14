using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleWater : MonoBehaviour
{
    [Range(0,1)][SerializeField] float _waterSpillRotationThreshold = 1f;


    [Header("Reference")]
    [SerializeField] SnapZone _bottleCapSnapZone;
    [SerializeField] Transform _dropWaterTransform;
    [SerializeField] ParticleSystem _waterLeakParticlePrefab;
    [SerializeField] ParticleSystem _waterSplashParticlePrefab;
    [SerializeField] IsBeingGrabHandTrack isBeingGrabHandTrack;
    [SerializeField] Rigidbody rb;
    [SerializeField] bool _wasGrab;
 

    [Header("FILL THIS IF THIS ITEM IS A CLEANER")]
    [SerializeField] DirtyCleaner _cleaner;

    [Header("DONT TOUCH! DEBUG PURPOSE")]
    [SerializeField] bool isBottleOpened = false;

    ParticleSystem curSpawnedWLeak;
    ParticleSystem curSpawnedWSplash;


    private void Start()
    {
        //Instantiate WaterLeak And Splash GO
        curSpawnedWLeak = Instantiate(_waterLeakParticlePrefab);
        curSpawnedWLeak.transform.SetParent(_dropWaterTransform);
        curSpawnedWLeak.transform.localPosition = Vector3.zero;
        curSpawnedWLeak.GetComponent<WaterPourCollision>().RegisterWaterBottle(this, _cleaner);

        curSpawnedWSplash = Instantiate(_waterSplashParticlePrefab);


        isBottleOpened = false;
    }

    public void OpenedCapBottle()
    {
        isBottleOpened = true;
    }

    public void ClosedCapBottle()
    {
        isBottleOpened = false;
    }

    private void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(_dropWaterTransform.position, Vector3.down, out hitInfo);
        Debug.DrawRay(_dropWaterTransform.position, Vector3.down * 5, Color.red);

        //Check if is bottled cap is opened
        if (!isBottleOpened) 
        {
            if(curSpawnedWLeak.isPlaying || curSpawnedWSplash.isPlaying)
            {
                curSpawnedWLeak.Stop();
                curSpawnedWSplash.Stop();
            }
            return;
        }
        

        //Check if the bottle is upside down
        if (transform.up.y > -_waterSpillRotationThreshold)
        {
            if(curSpawnedWLeak.isPlaying || curSpawnedWSplash.isPlaying)
            {
                curSpawnedWLeak.Stop();
                curSpawnedWSplash.Stop();
            }

           
            return;
        }
        
        
        if(!curSpawnedWLeak.isPlaying)
        {
            curSpawnedWLeak.Play();
        }

        //Update Splash Position
        Vector3 hitPos = hitInfo.point;
        Debug.Log("Drop Water Posnya adalah " + _dropWaterTransform.position);
        curSpawnedWSplash.transform.position = hitPos;

    }


    public void PlaySplashOnCollision()
    {
        if(!_waterSplashParticlePrefab.isEmitting || !_waterSplashParticlePrefab.isPlaying)
        {
            curSpawnedWSplash.Play();
        }
    }
    public void Handtrack_isKinematicWhenGrab(bool change)
    {
        if(change && isBeingGrabHandTrack.IsBeingGrab())
        {
            _wasGrab = true;
            rb.isKinematic = true;
        }
        else if(!change && !isBeingGrabHandTrack.IsBeingGrab() && _wasGrab)
        {
            _wasGrab = false;
            rb.isKinematic = false;
        }
    }


}
