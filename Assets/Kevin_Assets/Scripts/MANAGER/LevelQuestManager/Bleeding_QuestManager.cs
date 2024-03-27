using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding_QuestManager : QuestManager
{
    protected override void Quest()
    {
        Debug.Log("Bleed");
    }
    protected override void ScoreCounter()
    {
        // Debug.Log("Bleeding");
        // if()
        // {
        //     score = ScoreName.Big_Happy_Face;
            
        // }
        // else if()
        // {
        //     score = ScoreName.Small_Happy_Face;
        // }
        // else
        // {
        //     score = ScoreName.Sad_Face;
        // }
        // PlayerManager.HasBeatenLvl((int)levelP3KTypeNow, score);
    }
}
