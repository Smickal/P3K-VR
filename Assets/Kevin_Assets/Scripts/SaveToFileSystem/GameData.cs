using System;

[Serializable]
public class GameData
{
    //Regular Saved-GameData
    public string PlayerID;

    //Datas_Choking
    public string Level1Name;
    public string Level1Score;
    public float level1ProgressFinal;
    public float  Level1TimeToFinish;
    public bool IsLevel1Done;

    //datas_Bleeding
    public string Level2Name;
    public string Level2Score;
    public float Level2TimeToFinish;
    public float Level2TimeToFinish_WithoutItem;
    public float Level2TimeToFinish_WithItem;
    public int TotalSmallTrashCount;
    public int TotalDissastifaction;
    public bool IsLevel2Done;

    public GameData(string playerID)
    {
        this.PlayerID = playerID;

        IsLevel1Done = false;
        IsLevel2Done = false;
    }

}
