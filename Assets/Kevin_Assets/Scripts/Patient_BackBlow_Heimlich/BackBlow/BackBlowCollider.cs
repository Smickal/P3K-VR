using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBlowCollider : MonoBehaviour
{
    [SerializeField]private Vector3 colliderSize;
    private float sizeBack = 0.215f;
    private void Awake() 
    {
        colliderSize = GetComponent<BoxCollider>().size;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        bool hitShoulderBlades = false;
        ContactPoint[] contactPoints = new ContactPoint[5];

        for(int i=0;i<contactPoints.Length;i++)
        {
            ContactPoint currContact = contactPoints[i];
            // Debug.Log(currContact.point + "Cekk");
            Vector3 localPoint = transform.InverseTransformPoint(currContact.point);

            float halfCollWidth = colliderSize.x/2f;
            float shoulderBladesSize = (colliderSize.x - sizeBack)/2f;

            if(localPoint.x > ((-halfCollWidth + shoulderBladesSize)*-1 + transform.position.x)|| localPoint.x < -1*(halfCollWidth - shoulderBladesSize) + transform.position.x )
            {
                
                // Debug.Log(localPoint.x + " " + i);
                hitShoulderBlades = true;
            }
        }
        BackBlowMovement.OnBackBlow.Invoke(other, hitShoulderBlades);

    }


    private void OnTriggerExit(Collider other)
    {
        BackBlowMovement.OnReleaseBackBlow.Invoke(other);
    }
}

