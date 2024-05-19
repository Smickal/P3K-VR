using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    const string QuizKey = "QuizKeyDAta";

    [SerializeField] bool _isActivated = false;
    [SerializeField] float _maxTimeSlider = 10f;
    [SerializeField] float _maxTimerSliderNormal = 10f;

    [Header("Reference")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] KitUiManager _kitUiManager;
    public AudioClip SoundOnRight, SoundOnWrong;
    [SerializeField] GameObject _questionContainer;
    [SerializeField] GameObject _resultContainer;
    [SerializeField] GameObject _explainContainer;
    [SerializeField] GameObject _toolTipContainer;
 
    [Space(5)]
    [Header("Questions")]
    [SerializeField] TMP_Text _questionText;

    [Space(5)]
    [SerializeField] TMP_Text _choiceAText;
    [SerializeField] TMP_Text _choiceBText;
    [SerializeField] TMP_Text _choiceCText;
    [SerializeField] TMP_Text _choiceDText;

    [Space(5)]
    [SerializeField] Button _buttonA;
    [SerializeField] Button _buttonB;
    [SerializeField] Button _buttonC;
    [SerializeField] Button _buttonD;

    [Space(5)]
    [SerializeField] Slider _timerSlider;
    [SerializeField] Image _sliderFill;
    [SerializeField] Color[] _sliderColor;
    [SerializeField] float[] _sliderColorLimit;

    [Space(5)]
    [Header("Results")]
    [SerializeField] TMP_Text _resultAnswer;
    [SerializeField] TMP_Text _answerText;
    [TextArea(5,7)][SerializeField] string _answerRightText, _answerWrongText, _answerNotInTimeText;
    [SerializeField] Color _answerRightColor, _answerWrongColor;
    [SerializeField] Button _resultBtn;

    [Space(5)]
    [SerializeField] Image _exImage;
    [SerializeField] Button _nextExButton;

    [Header("Scriptable")]
    [SerializeField] SOQuizQuestions[] _scriptableData;

    Queue<SOQuizQuestions> questionQ = new Queue<SOQuizQuestions>();
    
    SOQuizQuestions curQuestion;

    Sprite[] _currentExplanationImages;
    int curExplainImageIdx = 0;

    float curTime;
    bool isTimerActivated;
    public UnityEvent OnFinishQuiz;
    private void Awake() 
    {
        if(dialogueManager == null)dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
    }
    private void Start()
    {
        // if(!_isActivated) { return; }


        // foreach(var question in _scriptableData)
        // {
        //     questionQ.Enqueue(question);
        // }

        // _resultBtn.onClick.AddListener(() =>
        // {
        //     OpenExplanationPage();
        // });

        // curTime = _maxTimeSlider;

        // _buttonA.onClick.AddListener(() => { AnswerCheck(EAnswerType.A); });
        // _buttonB.onClick.AddListener(() => { AnswerCheck(EAnswerType.B); });
        // _buttonC.onClick.AddListener(() => { AnswerCheck(EAnswerType.C); });
        // _buttonD.onClick.AddListener(() => { AnswerCheck(EAnswerType.D); });


        // _kitUiManager.ActivateBaseUI("Quiz");
        // _kitUiManager.OpenQuizUI();

        // CreateQuestion();

        // if(PlayerManager.ChangeInGame_Mode_Now)
    }
    public void StartQuiz()
    {
        foreach(var question in _scriptableData)
        {
            questionQ.Enqueue(question);
        }

        _resultBtn.onClick.AddListener(() =>
        {
            OpenExplanationPage();
        });

        curTime = _maxTimeSlider;

        _buttonA.onClick.AddListener(() => { AnswerCheck(EAnswerType.A); });
        _buttonB.onClick.AddListener(() => { AnswerCheck(EAnswerType.B); });
        _buttonC.onClick.AddListener(() => { AnswerCheck(EAnswerType.C); });
        _buttonD.onClick.AddListener(() => { AnswerCheck(EAnswerType.D); });


        _kitUiManager.ActivateBaseUI("Quiz");
        _kitUiManager.OpenQuizUI();
        _toolTipContainer.SetActive(true);

        CreateQuestion();
    }

    private void Update()
    {
        if(GameManager.CheckGameStateNow == null)return;
        if (isTimerActivated && GameManager.CheckGameStateNow() == GameState.Cinematic)
        {
            curTime -= Time.deltaTime;

            _timerSlider.value = curTime / _maxTimeSlider;
            Update_TimerVisual();
            if(curTime < 0)
            {
                //Activate
                _maxTimeSlider = _maxTimerSliderNormal;
                RunOutOfTime();
                
            }    
        }
    }
    private void Update_TimerVisual()
    {
        if(_timerSlider.value >= _sliderColorLimit[0])_sliderFill.color = _sliderColor[0]; 
        else if(_timerSlider.value >= _sliderColorLimit[1])_sliderFill.color = _sliderColor[1];
        else _sliderFill.color = _sliderColor[2];
    }

    public void CreateQuestion()
    {
        if(DialogueManager.HideFinishedDialogue_AfterFinishingTask != null)DialogueManager.HideFinishedDialogue_AfterFinishingTask();
        if (questionQ.Count == 0)
        {
            //NO MORE QUESTION;
            _toolTipContainer.SetActive(false);
            _kitUiManager.DeactivateBaseUI();
            
            OnFinishQuiz?.Invoke();
            return;
        }
        
        curQuestion = questionQ.Dequeue();
        PlayDialogueNeeded(DialogueListTypeParent.Home_Quiz, curQuestion.dialogueListTypeQuestionStart);
        

        //Create Question UI
        _questionContainer.SetActive(true);
        _resultContainer.SetActive(false);
        _explainContainer.SetActive(false);

        _questionText.SetText(curQuestion.Question);

        _choiceAText.SetText(curQuestion.AnswerA);
        _choiceBText.SetText(curQuestion.AnswerB);
        _choiceCText.SetText(curQuestion.AnswerC);
        _choiceDText.SetText(curQuestion.AnswerD);


        //Set Timer Here!
        isTimerActivated = true;
        curExplainImageIdx = 0;
    }

    public void AnswerCheck(EAnswerType answerType)
    {
        //1. Trigger Next Page
        //2. Check Answer
        //3. 
        isTimerActivated = false;
        _maxTimeSlider = _maxTimerSliderNormal;
        curTime = _maxTimeSlider;

        _questionContainer.SetActive(false);
        _resultContainer.SetActive(true);

        if (answerType == curQuestion.AnswerType)
        {
            //Right Answer!!!
            _answerText.SetText(_answerRightText);
            _answerText.color = _answerRightColor;
            PlaySoundRight();
            PlayDialogueNeeded(DialogueListTypeParent.Home_Quiz,DialogueListType_Home_Quiz.Home_QuizRight);
        }
        else
        {
            //Wrong Answer!!
            _answerText.SetText(_answerWrongText);
            _answerText.color = _answerWrongColor;
            PlaySoundWrong();
            PlayDialogueNeeded(DialogueListTypeParent.Home_Quiz,DialogueListType_Home_Quiz.Home_QuizWrong);

        }
        _resultAnswer.SetText(curQuestion.GetAnswer());
    }    

    private void RunOutOfTime()
    {
        isTimerActivated = false;
        curTime = _maxTimeSlider;

        _questionContainer.SetActive(false);
        _resultContainer.SetActive(true);
        _answerText.SetText(_answerNotInTimeText);
        PlaySoundWrong();
        PlayDialogueNeeded(DialogueListTypeParent.Home_Quiz, DialogueListType_Home_Quiz.Home_QuizLate);
        _resultAnswer.SetText(curQuestion.GetAnswer());

    }

    private void OpenExplanationPage()
    {
        _nextExButton.onClick.RemoveAllListeners();
        //close nextbutton here if you want

        _resultContainer.SetActive(false);
        _explainContainer.SetActive(true);

        _currentExplanationImages = curQuestion.ExplanationSprites;

        _exImage.sprite = _currentExplanationImages[curExplainImageIdx];
        PlayDialogueNeeded(DialogueListTypeParent.Home_QuizExplanation,curQuestion.dialogueListTypeExplanations[curExplainImageIdx]);
        curExplainImageIdx++;


        if(curExplainImageIdx >= _currentExplanationImages.Length)
        {         
            //Show Next Question
            _nextExButton.onClick.AddListener(() =>
            {
                CreateQuestion();               
            });
        }
        else
        {
            //Show Next Image
            _nextExButton.onClick.AddListener(() =>
            {
                OpenExplanationPage();
            });
        }

    }

    private void PlayDialogueNeeded<T>(DialogueListTypeParent parent, T enumValue)where T : struct, System.Enum
    {
        // Debug.Log("Masuk sinikan?");
        if(DialogueManager.HideFinishedDialogue_AfterFinishingTask != null)DialogueManager.HideFinishedDialogue_AfterFinishingTask();
        dialogueManager.PlayDialogueScene(parent, enumValue);
        Debug.Log("Saat ini adalah " + parent + "aa" + enumValue);
        // DialogueManager.PlaySceneDialogue(dialogueListType);
    }
    private void PlaySoundRight()
    {
        if (SoundOnRight) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                BNG.VRUtils.Instance.PlaySpatialClipAt(SoundOnRight, transform.position, 0.75f);
            }
        }
    }
    private void PlaySoundWrong()
    {
        if (SoundOnWrong) {
            // Only play the sound if not just starting the scene
            if (Time.timeSinceLevelLoad > 0.1f) {
                BNG.VRUtils.Instance.PlaySpatialClipAt(SoundOnWrong, transform.position, 0.75f);
            }
        }
    }
}
