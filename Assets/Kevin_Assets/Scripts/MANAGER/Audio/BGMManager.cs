using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM_Type
{
    main, tense
}
public class BGMManager : MonoBehaviour, IChangeVolume
{
    public static BGMManager Instance{get; private set;}
    [SerializeField]private AudioSource BGM;
    private float volumeBGM;
    [SerializeField]private float fadeInDurationMax = 0.5f, fadeOutDurationMax = 0.5f;
    private float fadeDuration;
    private IEnumerator BGMFadeIn, BGMFadeOut;

    [Header("Kalo mau ubah audio clip pas first aid")]
    [SerializeField]private AudioClip BGM_Main;
    [SerializeField]private AudioClip BGM_Tense;
    BGM_Type bgm_TypeNow = BGM_Type.main;
    public static Action<BGM_Type> ChangeBGMAudio;

    [SerializeField]private bool isInstance;
    public bool IsInstance { get { return isInstance; } }

    private void Awake() 
    {
        if(!Instance)
        {
            ChangeBGMAudio += ChangeBGMClip;
            BGM_Main = BGM.clip;
            Instance = this;
            isInstance = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator fadeIn()
    {
        while(fadeDuration < fadeInDurationMax )
        {
            fadeDuration += 0.01f;
            // Debug.Log("fadein " + fadeDuration + " " + Mathf.Lerp(0f, volumeBGM, fadeDuration/fadeInDurationMax));
            BGM.volume = Mathf.Lerp(0f, volumeBGM, fadeDuration/fadeInDurationMax);  
            // Debug.Log(BGM.volume);     
            yield return new WaitForSeconds(0.1f);
        }
        BGM.volume = volumeBGM;
        BGMFadeIn = null;
    }
    private IEnumerator fadeOut(BGM_Type bGM_Type, bool isChangingBGM)
    {
        while(fadeDuration < fadeOutDurationMax )
        {
            fadeDuration += 0.01f;
            // Debug.Log("fadeout " + fadeDuration + " " + Mathf.Lerp(volumeBGM, 0f, fadeDuration/fadeOutDurationMax));
            BGM.volume = Mathf.Lerp(volumeBGM, 0f, fadeDuration/fadeInDurationMax);       
            yield return new WaitForSeconds(0.1f);
        }
        BGM.volume = 0;
        BGM.Stop();
        BGMFadeOut = null;
        if(isChangingBGM)PlayBGM();
    }
    public void PlayBGM()
    {
        if(BGM.isPlaying)return;

        fadeDuration = 0;
        BGM.volume = 0f;
        BGM.Play();
        if(BGMFadeIn!= null)
        {
            StopCoroutine(BGMFadeIn);
            BGMFadeIn = null;
        }
        BGMFadeIn = fadeIn();
        StartCoroutine(BGMFadeIn);
    }
    public void StopBGM(BGM_Type bGM_Type, bool isChangingBGM)
    {
        if(!BGM.isPlaying)return;

        fadeDuration = 0;
        if(BGMFadeOut!= null)
        {
            StopCoroutine(BGMFadeOut);
            BGMFadeOut = null;
        }
        BGMFadeOut = fadeOut(bGM_Type,isChangingBGM);
        StartCoroutine(BGMFadeOut);
    }
    public void ChangeVolume(float volume)
    {
        volumeBGM = 1;
        if(BGM)BGM.volume = 1;
    }
    public void DestroyInstance(){
        ChangeBGMAudio -= ChangeBGMClip;
        Destroy(gameObject);
    }

    public void ChangeBGMClip(BGM_Type bGM_Type)
    {
        if(bgm_TypeNow == bGM_Type)return;
        StopBGM(bGM_Type, true);
        
        bgm_TypeNow = bGM_Type;
        if(bGM_Type == BGM_Type.main)
        {
            BGM.clip = BGM_Main;
            // Debug.Log("change to main");
        }
        else if(bGM_Type == BGM_Type.tense)
        {
            BGM.clip = BGM_Tense;
            // Debug.Log("change to tense");
        }
        
    }
}


