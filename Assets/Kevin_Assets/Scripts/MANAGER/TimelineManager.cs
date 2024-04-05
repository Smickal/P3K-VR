using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System;

public class TimelineManager : MonoBehaviour, ITurnOffStatic
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private SOTimelineList tImelineList;
    private TimelineType timelineTypeNow;
    public static Action<TimelineType> StartTimeline;

    private void Awake() 
    {
        StartTimeline += PlayTimeline;
    }
    
    private void Start() 
    {
        director.stopped += OnTimelineStopped;
    }
    public void TurnOffStatic()
    {
        StartTimeline -= PlayTimeline;
        director.stopped -= OnTimelineStopped;
    }
    
    private void OnTimelineStopped(PlayableDirector director)
    {
        if(timelineTypeNow == TimelineType.Home_Cutscene1)
        {
            DialogueManager.PlaySceneDialogue(DialogueListType.Home_Introduction2);
        }
    }
    private void PlayTimeline(TimelineType typeTimeline)
    {
        timelineTypeNow = typeTimeline;
        if(timelineTypeNow == TimelineType.Home_Cutscene1)
        {
            director.playableAsset = tImelineList.Home_Cutscene1;
        }
        director.Play();
    }
}
