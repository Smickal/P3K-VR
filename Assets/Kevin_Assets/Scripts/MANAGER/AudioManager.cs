using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    const string SaveStateKeyMaster = "AudioData_Key";
    const string SaveStateKeyBGM = "BGMAudioData_Key";
    const string SaveStateKeySFX = "SFXAudioData_Key";
    const string BGM_Tag = "BGM";
    [SerializeField]private IChangeVolume _BGMIChange;
    [SerializeField]private List<IChangeVolume> _SFXIChange;
    [SerializeField]private float startMasterVol, startBGMVol, startSFXVol;

    [Header("Slider")]
    [SerializeField]private Slider masterSlider, BGMSlider, SFXSlider;
    [SerializeField]GameObject[] BGM;
    [SerializeField]GameObject theTrueBGM;
    [Header("Mixer")]
    [SerializeField]private AudioMixer audioMixer;
    const string Mixer_Master = "Master";
    const string Mixer_BGM = "BGM";
    const string Mixer_SFX = "SFX";

    private float masterVol, bgmVol, sfxVol;
    private void Awake() 
    {
        IChangeVolume[] _SFXIChangeArray = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IChangeVolume>().ToArray();
        _SFXIChange = new List<IChangeVolume>(_SFXIChangeArray);

        theTrueBGM = null;
        BGM = GameObject.FindGameObjectsWithTag(BGM_Tag);
        if(BGM.Length == 1)
        {
            theTrueBGM = BGM[0];
        }
        else if(BGM.Length > 1)
        {
            Debug.Log("di sini");
            foreach(GameObject gameObject in BGM)
            {
                Debug.Log(gameObject);
                if(gameObject != null && gameObject.GetComponent<BGMManager>().IsInstance)
                {
                    
                    theTrueBGM = gameObject;
                    break;
                }
            }
        }

        _BGMIChange = theTrueBGM.GetComponent<BGMManager>();
        _SFXIChange.Remove(_BGMIChange);

        BGMManager bgmMgr = theTrueBGM.GetComponent<BGMManager>();

        LoadVolume();
        bgmMgr.PlayBGM();

        masterSlider.onValueChanged.AddListener(
            (float value)=>
            {
                ChangeMasterVol(value);
            }
        );
        BGMSlider.onValueChanged.AddListener(
            (float value)=>
            {
                // Debug.Log(value);
                ChangeBGMVol(value);
            }
        );
        SFXSlider.onValueChanged.AddListener(
            (float value)=>
            {
                ChangeSFXVol(value);
            }
        );
    }
    private void Start() 
    {
        audioMixer.SetFloat(Mixer_Master, Mathf.Log10(masterVol)*20);
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmVol)*20-1);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxVol)*20);
    }
    public void LoadVolume()
    {
        float masterVolLoad = PlayerPrefs.GetFloat(SaveStateKeyMaster, startMasterVol);
        masterSlider.value = masterVolLoad;
        ChangeMasterVol(masterVolLoad);

        float bgmVolLoad = PlayerPrefs.GetFloat(SaveStateKeyBGM, startBGMVol);
        BGMSlider.value = bgmVolLoad;
        ChangeBGMVol(bgmVolLoad);

        float sfxVolLoad = PlayerPrefs.GetFloat(SaveStateKeySFX, startSFXVol);
        SFXSlider.value = sfxVolLoad;
        ChangeSFXVol(sfxVolLoad);
    }

    public void ChangeMasterVol(float vol)
    {   
        masterVol = vol;
        audioMixer.SetFloat(Mixer_Master, Mathf.Log10(masterVol)*20);
        PlayerPrefs.SetFloat(SaveStateKeyMaster, vol);
    }
    public void ChangeBGMVol(float vol)
    {
        bgmVol = vol;
        audioMixer.SetFloat(Mixer_BGM, Mathf.Log10(bgmVol)*20-1);
        Debug.Log(bgmVol + "waht");
        _BGMIChange.ChangeVolume(vol);
        PlayerPrefs.SetFloat(SaveStateKeyBGM, vol);
    }
    public void ChangeSFXVol(float vol)
    {
        sfxVol = vol;
        audioMixer.SetFloat(Mixer_SFX, Mathf.Log10(sfxVol)*20);
        foreach(IChangeVolume _SFXICh in _SFXIChange)
        {
            _SFXICh.ChangeVolume(vol);
        }
        PlayerPrefs.SetFloat(SaveStateKeySFX, vol);
    }
    
    
}
