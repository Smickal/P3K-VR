using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG {

    /// <summary>
    /// If this object is not being held, return to a snap zone
    /// </summary>
    public class ReturnToSnapZone : MonoBehaviour {

        [Tooltip("The SnapZone to return to if not being held")]
        public SnapZone ReturnTo;

        [Tooltip("How fast to Lerp Towards the SnapZone")]
        public float LerpSpeed = 15f;

        [Tooltip("How long to wait before starting to Lerp the object back towards the SnapZone. In Seconds.")]
        public float ReturnDelay = 0.1f;
        
        // How long we've been waiting
        float currentDelay = 0;

        Grabbable grab;
        Rigidbody rigid;
        bool useGravityInitial;
        [SerializeField]private IsBeingGrabHandTrack isBeingGrabHand;

        [Tooltip("Initiate snap if distance between the Grabbable and SnapZone is <= SnapDistance")]
        public float SnapDistance = 0.05f;
        [Tooltip("Kalau mager taro di starting item snapzone - true; tp suara ada")]
        [SerializeField]private bool returnFirstTime = false;
        [SerializeField]private bool onlyReturnOnce = true;
        public bool OnlyReturnOnce { get { return onlyReturnOnce; } }

        [Header("isi ini kalo mo balik berdasarkan briefcase buka ato tutup")]
        [SerializeField]private Briefcase briefCase;

        void Start() {
            grab = GetComponent<Grabbable>();
            rigid = GetComponent<Rigidbody>();
            if(isBeingGrabHand == null)isBeingGrabHand = GetComponent<IsBeingGrabHandTrack>();
            useGravityInitial = rigid.useGravity;
        }

        void Update() {

            // Reset the counter if we're holding the item
            if(grab.BeingHeld || (isBeingGrabHand && isBeingGrabHand.IsBeingGrab())) {
                currentDelay = 0;
            }

            bool validReturn = grab != null && ReturnTo != null && ReturnTo.HeldItem == null && (!grab.BeingHeld || (isBeingGrabHand && !isBeingGrabHand.IsBeingGrab()));

            // Increment how long we've been waiting
            if (validReturn) {
                if(returnFirstTime) currentDelay = ReturnDelay;
                else currentDelay += Time.deltaTime;
            }

            // Start moving towards the SnapZone
            if(validReturn && currentDelay >= ReturnDelay) {
                if(returnFirstTime)
                {
                    moveToSnapZone();
                    returnFirstTime = false;
                }
                else
                {
                    
                    //kalo butu dia balik pas briefcase kebuka aja
                    if(briefCase)
                    {
                        if(briefCase.IsOpen)moveToSnapZone();
                    }
                    else
                    {
                        moveToSnapZone();
                    }
                }
                
            }
        }

        void moveToSnapZone() {
            rigid.useGravity = false;
        
            transform.position = Vector3.MoveTowards(transform.position, ReturnTo.transform.position, Time.deltaTime * LerpSpeed);

            if (Vector3.Distance(transform.position, ReturnTo.transform.position) < SnapDistance) {
                rigid.useGravity = useGravityInitial;
                Debug.Log("is this your fault");
                ReturnTo.GrabGrabbable(grab);
                if(onlyReturnOnce)this.enabled = false;
            }
                
            
        }
    }
}