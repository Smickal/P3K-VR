using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class PatientBackBlowHeimlich : MonoBehaviour
{
    [SerializeField] RigBuilder _rig;

    [Header("Heimlich")]
    [SerializeField] HeimlichMovement _heimlichMove;
    [SerializeField] GameObject _heimlichContainerOBJ;
    [SerializeField] Rig _heimlichRig;
    [SerializeField] int _heimlichMaxCount = 5;

    [Header("BackBlow")]
    [SerializeField] BackBlowMovement _backBlowMove;
    [SerializeField] GameObject _backBlowContainerOBJ;
    [SerializeField] Rig _backBlowRig;
    [SerializeField] int _backBlowMaxCount = 5;


    [HideInInspector] public UnityEvent<int> OnHeimlichCountUp;
    [HideInInspector] public UnityEvent<int> OnBackBlowCountUp;


    private void Start()
    {
        OnHeimlichCountUp.AddListener(CheckHeimlichCount);
        OnBackBlowCountUp.AddListener(CheckBackBlowCount);

        ActivateBackBlowRig();
    }

    public void ActivateBackBlowRig()
    {
        _heimlichContainerOBJ.SetActive(false);
        _backBlowContainerOBJ.SetActive(true);

        _heimlichRig.weight = 0f;
        _backBlowRig.weight = 0.75f;
    }

    public void ActivateHeimlich()
    {
        _heimlichContainerOBJ?.SetActive(true);
        _backBlowContainerOBJ.SetActive(false);

        _backBlowRig.weight = 0f;
        _heimlichRig.weight = 0.75f;
    }

    private void CheckBackBlowCount(int count)
    {
        if(count >= _backBlowMaxCount)
        {
            ActivateHeimlich();
        }
    }


    private void CheckHeimlichCount(int count)
    {
        if(count >= _heimlichMaxCount)
        {
            //Trigger Done Here!
            Debug.Log("done movement");
        }
    }
}
