using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetTimelineType : MonoBehaviour
{
    [Header("Di atas 0")]
    public int enumNumber;
    public TimelineType timelineType
    {
        get 
        { 
            if(enumNumber > timelineTypes.Length)return timelineTypes[0];
            return timelineTypes[enumNumber];
        }
    }

    private TimelineType[] timelineTypes = (TimelineType[])Enum.GetValues(typeof(TimelineType));
}
