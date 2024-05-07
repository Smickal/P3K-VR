using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;

public class Robot : GrabbableEvents
{
    [Header("Reference")]
    [SerializeField]private GameManager gameManager;
    [SerializeField]private PlayerManager playerManager;
    [SerializeField] float _minDistanceToPlayer = 2.0f;
    [SerializeField] float _maxDistanceToPlayer = 3.0f;
    [SerializeField] float _followVelocity = 500f;
    [SerializeField] float _yOffset = 1f;
    [SerializeField] float _xOffset = 1.25f;


    RobotAnimationController _controller;
    ReturnRobotToStartingPos returnPos;
    Camera mainCam;
    Rigidbody myRb;
    [Header("For Handtrack")]
    [SerializeField]private HandGrabInteractable[] handGrabs;
    [SerializeField]private DistanceHandGrabInteractable[] distanceHandGrabs;
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;
    [SerializeField]private DistanceHandGrabInteractor leftDistanceGrabberHT, rightDistanceGrabberHT;
    private HandGrabInteractor currHand;
    private DistanceHandGrabInteractor currDistanceHand;
    IsBeingGrabHandTrack isBeingGrabHandTrack;

    

    [Header("Debug")]
    [SerializeField] bool isActivated = false;
    [SerializeField] bool isFollowingPlayer = false;

    public bool IsFollowingPlayer {  get { return isFollowingPlayer; } }
    public void ActivateLookAt()
    {
        isActivated = true;
    }

    public void DeactivateLookAt()
    {
        isActivated = false;
    }


    private void Start()
    {
        // Debug.Log("Ini lewat sinikann??" + handGrabs);
        // Debug.Log("ya ?" + distanceHandGrabs);
        handGrabs = GetComponentsInChildren<HandGrabInteractable>().ToArray();
        distanceHandGrabs = GetComponentsInChildren<DistanceHandGrabInteractable>().ToArray();
        
        isBeingGrabHandTrack = GetComponent<IsBeingGrabHandTrack>();
        _controller = GetComponent<RobotAnimationController>();
        myRb = GetComponent<Rigidbody>();
        returnPos = GetComponent<ReturnRobotToStartingPos>();
        mainCam = Camera.main;
        

        if(gameManager.LevelModeNow() == LevelMode.Home)
        {
            if(!playerManager.IsFinish_TutorialMain())
            {
                _controller.TriggerFrozeAnim();
                
            }
            else
            {
                ActivateLookAt();
            }
        }
        else if(gameManager.LevelModeNow() == LevelMode.Level)
        {
            if(!playerManager.IsFinish_IntroLevel((int)gameManager.LevelTypeNow()))
            {
                
            }
            else
            {
                ActivateLookAt();
                if(playerManager.PlayerLastInGameMode() == InGame_Mode.NormalWalk)
                {
                    ActivateFollowPlayer();
                }
                else
                {
                    transform.position = returnPos.StartingPos.position;
                }
            }
        }

    }

    private void Update()
    {

        //Robot will lookat camera when positioned in his snapzone.
        if(isActivated)
        {
            transform.LookAt(mainCam.transform);
        }

        //Checks if the player Stops grabbing in the middle of animation.
        if (grab.RemoteGrabbing)
        {
            DeactivateLookAt();
            returnPos.StopMoveToSnapZone();
        }


        //Tries to stay in front of the camera
        if (!isFollowingPlayer || grab.RemoteGrabbing || grab.BeingHeld || isBeingGrabHandTrack.IsBeingGrab()) return;
        ActivateLookAt();

        myRb.velocity = Vector3.zero;
        myRb.angularVelocity = Vector3.zero;


        Vector3 targetPos = mainCam.transform.position +
                (mainCam.transform.forward * _minDistanceToPlayer) +
                (mainCam.transform.right * _xOffset) +
                    (mainCam.transform.up * _yOffset);
        // Debug.Log(targetPos + " target ");

        if(Vector3.Distance(mainCam.transform.position, transform.position) > _minDistanceToPlayer &&
            Vector3.Distance(mainCam.transform.position, transform.position) < _maxDistanceToPlayer)
        {
            Vector3 robotCameraDistance = transform.position - mainCam.transform.position;
            float dot = Vector3.Dot(robotCameraDistance, mainCam.transform.forward);
            if(dot <= 0)
            {
                // Debug.Log("Ga di dalem kamera woi");
                transform.position += (targetPos - transform.position) * 0.025f;
            }
            _controller.TriggerIdleAnim();
            
        }

        else if (Vector3.Distance(mainCam.transform.position, transform.position) > _maxDistanceToPlayer)
        {
            transform.position += (targetPos - transform.position) * 0.025f;
            _controller.TriggerForwardAnim();
        }

        else if (Vector3.Distance(mainCam.transform.position, transform.position) < _minDistanceToPlayer)
        {
            transform.position += (targetPos - transform.position) * 0.025f;
            _controller.TriggerBackwardAnim();

        }


    }


    public override void OnGrab(Grabber grabber)
    {
        _controller.TriggerFrozeAnim();

        thisGrabber = grabber;
        DeactivateLookAt();
        returnPos.StopMoveToSnapZone();
    }


    public override void OnRelease()
    {
        _controller.TriggerBackwardAnim();

        ActivateLookAt();
        returnPos.MoveToSnapZone();
    }
    public void OnGrabHT()
    {
        HandGrabInteractor curHand = CheckHandGrabInteractor();
        DistanceHandGrabInteractor curDistanceHand = CheckDistanceHandGrabInteractor();
        if(curHand == null && curDistanceHand == null)return;
        if(curHand != null)
        {
            currHand = curHand;
        }
        else if(curDistanceHand != null)
        {
            currDistanceHand = curDistanceHand;
        }

        _controller.TriggerFrozeAnim();
        DeactivateLookAt();
        returnPos.StopMoveToSnapZone();
    }
    public void OnReleaseHT()
    {
        if(currHand == null && currDistanceHand == null)return;
        currHand = null;
        currDistanceHand = null;

        _controller.TriggerBackwardAnim();

        ActivateLookAt();
        returnPos.MoveToSnapZone();
    }


    public void ReleaseRobot()
    {
        if(GameManager.CheckGameStateNow() == GameState.Cinematic)
        {
            return;
        }
        ActivateLookAt();
        if(grab)grab.DropItem(thisGrabber, true, false);
        if(currHand != null)
        {
            currHand.ForceRelease();
            isBeingGrabHandTrack.ChangeIsBeingGrab(false);
        }
        else if(currDistanceHand != null)
        {
            // curr.RemoveSelectingInteractor(currDistanceHand);
            // curr.RemoveInteractor(currDistanceHand);
            currDistanceHand.Unselect();
            isBeingGrabHandTrack.ChangeIsBeingGrab(false);
            // Debug.Log("Halooo ???");
        }
        isBeingGrabHandTrack.ChangeIsBeingGrab(false);
        currHand = null;
        currDistanceHand = null;
        returnPos.MoveToSnapZone();
    }

    public void ActivateFollowPlayer()
    {
        isFollowingPlayer = true;
    }

    public void DeactivateFollowPlayer()
    {
        isFollowingPlayer = false;
    }


        private HandGrabInteractor CheckHandGrabInteractor()
    {
        foreach(HandGrabInteractable handGrabInteractable in handGrabs)
        {
            // Debug.Log("handgrab" + handGrabInteractable);
            if(handGrabInteractable.HasSelectingInteractor(leftGrabberHT))
            {
                // Debug.Log(handGrabInteractable + " This is the chosen onee");
                return leftGrabberHT;
            }
            else if (handGrabInteractable.HasSelectingInteractor(rightGrabberHT))
            {
                // Debug.Log(handGrabInteractable + " This is the chosen onee");
                return rightGrabberHT;
            }
        }
        return null;
    }
    private DistanceHandGrabInteractor CheckDistanceHandGrabInteractor()
    {
        foreach(DistanceHandGrabInteractable distanceHandGrabInteractable in distanceHandGrabs)
        {
            // Debug.Log("handgrab" + handGrabInteractable);
            if(distanceHandGrabInteractable.HasSelectingInteractor(leftDistanceGrabberHT))
            {
                // Debug.Log(distanceHandGrabInteractable + " This is the chosen onee distancee" + leftGrabberHT);
                return leftDistanceGrabberHT;
            }
            else if (distanceHandGrabInteractable.HasSelectingInteractor(rightDistanceGrabberHT))
            {
                // Debug.Log(distanceHandGrabInteractable + " This is the chosen onee distancee" + rightGrabberHT);

                return rightDistanceGrabberHT;
            }
        }
        return null;
    }
}
