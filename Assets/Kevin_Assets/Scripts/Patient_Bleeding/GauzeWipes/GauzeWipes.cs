using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauzeWipes : MonoBehaviour, ISmallTrash
{
    [Header("Cleaner")]
    [SerializeField] DirtyCleaner _cleaner;



    private void Start()
    {
        if(_cleaner != null)
        {
            GetComponent<GauzeWipesCollision>().RegisterCleaner(this, _cleaner);
        }
    }

    public void DestroyTrash()
    {
        _cleaner.DestroyObject();
        Destroy(this.gameObject);
    }
}
