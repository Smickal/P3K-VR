using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class AlcoholWipesHT : MonoBehaviour
{
    [SerializeField]private HandGrabInteractor leftGrabberHT;
    [SerializeField]private HandGrabInteractor rightGrabberHT;

    AlcoholCleanManager alcoholManager;

    private void Start()
    {
        alcoholManager = AlcoholCleanManager.Instance;
    }

    // public override void OnGrab(Grabber grabber)
    // {
    //     base.OnGrab(grabber);

    //     curGrabber = grabber;
    //     alcoholManager?.RegisterGrabber(grabber);
    // }

    // public override void OnGrip(float gripValue)
    // {
    //     base.OnGrip(gripValue);

    //     if (!curGrabber) return;
    //     alcoholManager.IsHolding = true;

    // }

    // public override void OnRelease()
    // {
    //     base.OnRelease();
    //     curGrabber = null;

    //     alcoholManager.IsHolding = false;
    //     alcoholManager.SaveCurrentTimeProgress();
    // }

    public void DestroyTrash()
    {
        Destroy(this.gameObject);
    }
}
