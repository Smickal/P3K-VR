using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System;
using UnityEngine.Events;

[Serializable]
public class TimelineFinishActions
{
    public TimelineType timelineType;
    public UnityEvent OnTimelineFinish;
}
public class TimelineManager : MonoBehaviour, ITurnOffStatic
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private SOTimelineList tImelineList;
    private TimelineType timelineTypeNow;
    public static Action<TimelineType> StartTimeline;
    [Header("Action To Do When TimelineFinish")]
    [SerializeField]private TimelineFinishActions[] timelineFinishActions;

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
        //kalo ga mo lwt code bs lewat inspector, kalo mo lwt code gausa tambahin di inspector, well sbnrnya tinggal ilangin return kalo mo 2-2nya
        foreach(TimelineFinishActions action in timelineFinishActions)
        {
            if(action.timelineType == timelineTypeNow)
            {
                action.OnTimelineFinish.Invoke();
                return;
            }
        }
        // if(timelineTypeNow == TimelineType.Home_Cutscene1)
        // {
        //     DialogueManager.PlaySceneDialogue(DialogueListType.Home_Introduction2);
        // }
    }
    private void PlayTimeline(TimelineType typeTimeline)
    {
        timelineTypeNow = typeTimeline;
        if(timelineTypeNow == TimelineType.BleedIntro3_Cutscene)
        {
            director.playableAsset = tImelineList.BleedIntro3_Cutscene;
        }
        director.Play();
    }
    public void PlayTimelineOnEvent(GetTimelineType typeTimeline)
    {
        timelineTypeNow = typeTimeline.timelineType;
        if(timelineTypeNow == TimelineType.BleedIntro3_Cutscene)
        {
            director.playableAsset = tImelineList.BleedIntro3_Cutscene;
        }
        director.Play();
    }
}
