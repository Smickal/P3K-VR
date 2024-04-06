using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauzeWipesCollision : MonoBehaviour
{
    DirtyCleaner cleaner;
    GauzeWipes gauzeWipes;

    public void RegisterCleaner(GauzeWipes gauzeWipes, DirtyCleaner cleaner)
    {
        this.cleaner = cleaner;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (cleaner == null || collision.gameObject.GetComponent<DirtyObject>() == null) return;
        cleaner.TryRegisterADirtyObject(collision.gameObject);
     
    }

    private void OnCollisionStay(Collision collision)
    {
        if (cleaner == null || collision.gameObject.GetComponent<DirtyObject>() == null) return;
        cleaner.OnCleaning?.Invoke();
    }
}
