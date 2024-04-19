using System;

public enum DialogueListTypeParent
{
    None, 
    Home_Intro, Home_Quiz, Home_QuizExplanation,

    Bleeding_WrongItem 


}
[Serializable]

public enum DialogueListType_Home_Intro
{
    None, Home_Introduction1, Home_Introduction2, 
}
public enum DialogueListType_Home_Quiz
{
    None, Home_QuizStart, Home_QuizStart2, Home_QuizStart3, Home_QuizRight, Home_QuizWrong, Home_QuizLate, 
}
public enum DialogueListType_Home_QuizExplanation
{
    None, Home_QuizExplanation_1_1, Home_QuizExplanation_1_2, Home_QuizExplanation_1_3, Home_QuizExplanation_1_4, Home_QuizExplanation_1_5,
    Home_QuizExplanation_2_1, Home_QuizExplanation_2_2, Home_QuizExplanation_2_3, Home_QuizExplanation_2_4, Home_QuizExplanation_2_5,
    Home_QuizExplanation_3_1, Home_QuizExplanation_3_2, Home_QuizExplanation_3_3, Home_QuizExplanation_3_4, Home_QuizExplanation_3_5, Home_QuizExplanation_3_6, Home_QuizExplanation_3_7, Home_QuizExplanation_3_8, Home_QuizExplanation_3_9, Home_QuizExplanation_3_10, Home_QuizExplanation_3_11, Home_QuizExplanation_3_12, Home_QuizExplanation_3_13, Home_QuizExplanation_3_14 

}

public enum DialogueListType_Bleeding_WrongItem
{
    None, 
    Bleeding_WrongItem_WithItem_Bandage,
    Bleeding_WrongItem_WithoutItem_CleanHands, Bleeding_WrongItem_WithoutItem_WearGloves, Bleeding_WrongItem_WithoutItem_StopBleed, Bleeding_WrongItem_WithoutItem_CleanBlood, Bleeding_WrongItem_WithoutItem_DryWater, Bleeding_WrongItem_WithoutItem_BandageTime, Bleeding_WrongItem_WithoutItem_PuttingLegOnTopSomethingTime, Bleeding_WrongItem_WithoutItem_Done 
}
