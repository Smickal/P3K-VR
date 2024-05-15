using System.Collections;
using System.Collections.Generic;
using SamDriver.Decal;
using UnityEngine;
using BNG;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class RobotExpressionController : MonoBehaviour
{
    [SerializeField] DecalProjector _eyeProjector;
    [SerializeField] DecalMesh _eyeMesh;
    [SerializeField]GameObject _eye;
    [SerializeField] float _fadeSpeed;
    bool isFading, isTurnOn = true;
    public UnityEvent OnFadeDone;


    [Header("Material")]
    [SerializeField] Material _normalExpressionMaterial;
    [SerializeField]PlayerManager playerManager;
    [SerializeField]GameManager gameManager;
    [SerializeField]Robot robot;
    public AudioClip SoundOnTurnOn;

    // Start is called before the first frame update
    public bool TurnOn, TurnOff;
    private void Awake() 
    {
        if(_eyeMesh != null)
        {
            if(!playerManager.IsFinish_TutorialMain() && gameManager.LevelModeNow() == LevelMode.Home)
            {
                // _eye.SetActive(false);
                _eyeMesh.Opacity = 0;
                isTurnOn = false;
            }
        }
        
    }
    void Start()
    {
        _eyeProjector.material = _normalExpressionMaterial;
    }
    private void Update() {
        if(TurnOn)
        {
            TurnOn = false;
            TurnOnEyes();
        }
        if(TurnOff)
        {
            TurnOff = false;
            TurnOffEyes();
        }
    }
    public void TurnOnEyes()
    {
        if(isFading || isTurnOn)return;
        // _eye.SetActive(true);
        PlaySoundTurnOn();
        StartCoroutine(doFade(0,1));
        robot.ActivateLookAt();
    }
    public void TurnOffEyes()
    {
        if(isFading || !isTurnOn)return;
        robot.DeactivateLookAt();
        StartCoroutine(doFade(1,0));
    }

    IEnumerator doFade(float from, float To) {
        
        isFading = true;
        float alphaStart = from;

        _eyeMesh.Opacity = alphaStart;

        while (alphaStart != To) {

            if (from < To) {
                alphaStart += Time.deltaTime * _fadeSpeed;
                if (alphaStart > To) {
                    alphaStart = To;
                }
            }
            else {
                alphaStart -= Time.deltaTime * _fadeSpeed;
                if (alphaStart < To) {
                    alphaStart = To;
                }
            }

            _eyeMesh.Opacity = alphaStart;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();

        // Ensure alpha is always applied
        _eyeMesh.Opacity = To;
        isFading = false;
        if(from < To)isTurnOn = true;
        else isTurnOn = false;

        // if(from > To)_eyeMesh.gameObject.SetActive(false);
        OnFadeDone?.Invoke();
    }
    private void PlaySoundTurnOn()
    {
        if (SoundOnTurnOn) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                VRUtils.Instance.PlaySpatialClipAt(SoundOnTurnOn, transform.position, 0.75f);
            }
        }
    }


}
