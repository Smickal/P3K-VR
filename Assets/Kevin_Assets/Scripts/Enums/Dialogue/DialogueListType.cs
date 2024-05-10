using System;

public enum DialogueListTypeParent
{
    None, 
    Home_Intro, Home_Quiz, Home_QuizExplanation,

    Bleeding_WrongItem,
    Choking_Intro, Bleeding_Intro,
    Choking_Explanation, Bleeding_Explanation,
    Choking_Ending, Bleeding_Ending
}
[Serializable]

public enum DialogueListType_Home_Intro
{
    None, Home_Intro_Wake, Home_Intro_AfterTakeLetter,//grab tutor
    Home_Intro_AfterWakeUpRobot, //interact button with hand
    Home_Intro_AfterQuiz, Home_Intro_TutorMove,//try to go to the place
    Home_Intro_FirstAid, //open all grab
    Home_Intro_GoToDoor
    
}
public enum DialogueListType_Home_Quiz
{
    None, Home_QuizStart, //interact with UI
    Home_QuizStart2, Home_QuizStart3, Home_QuizRight, Home_QuizWrong, Home_QuizLate, 
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
public enum DialogueListType_Choking_Intro
{
    None, 
    Choking_Intro_1, Choking_Intro_2,
    Choking_Intro_3
}
public enum DialogueListType_Bleeding_Intro
{
    None,
    Bleeding_Intro_1, Bleeding_Intro_2,
    Bleeding_Intro_3
}
public enum DialogueListType_Choking_Explanation
{
    None,
    Choking_Exp_1,
    Choking_Exp_2,
    Choking_Exp_3,
    Choking_Exp_Backblow,
    Choking_Exp_Heimlich

}
public enum DialogueListType_Bleeding_Explanation
{
    None, 
    Bleeding_WithItem_CleanHands,
    Bleeding_WithItem_Bandage,
    Bleeding_WithoutItem_CleanHands, Bleeding_WithoutItem_WearGloves, Bleeding_WithoutItem_StopBleed, Bleeding_WithoutItem_CleanBlood, Bleeding_WithoutItem_DryWater, Bleeding_WithoutItem_BandageTime, Bleeding_WithoutItem_PuttingLegOnTopSomethingTime, Bleeding_WithoutItem_Done,
    Bleeding_PatientDissatisfied
}
public enum DialogueListType_Choking_Ending
{
    None,
    Ending_Sad,
    Ending_SmallHappy,
    Ending_Happy,
    EndingAfter
}
public enum DialogueListType_Bleeding_Ending
{
    None,
    Ending_Sad,
    Ending_SmallHappy,
    Ending_Happy,
    EndingAfter
}

