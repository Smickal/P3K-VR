using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPourCollision : MonoBehaviour
{
    BottleWater bottleWater;
    DirtyCleaner cleaner;

    public void RegisterWaterBottle(BottleWater bottleWater, DirtyCleaner cleaner)
    {
        this.bottleWater = bottleWater;
        this.cleaner = cleaner;
    }

    private void OnParticleCollision(GameObject other)
    {
        bottleWater.PlaySplashOnCollision();


        //Checks if the ITEM IS NOT A CLEANER OR
        //THE COLLIDED OBJ IS NOT A DIRTY OBJ
        if (cleaner == null || other.GetComponent<DirtyObject>() == null) return;        
        cleaner.TryRegisterADirtyObject(other);
        cleaner.OnCleaning?.Invoke();
    }
}
