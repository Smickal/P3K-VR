using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    const string QuizKey = "QuizKeyDAta";

    [SerializeField] bool _isActivated = false;
    [SerializeField] float _maxTimeSlider = 10f;

    [Header("Reference")]
    [SerializeField] KitUiManager _kitUiManager;

    [SerializeField] GameObject _questionContainer;
    [SerializeField] GameObject _resultContainer;
    [SerializeField] GameObject _explainContainer;
 
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

    [Space(5)]
    [Header("Results")]
    [SerializeField] TMP_Text _resultAnswer;
    [SerializeField] TMP_Text _answerText;
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
    private void Start()
    {
        if(!_isActivated) { return; }


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

        CreateQuestion();
    }

    private void Update()
    {
        if (isTimerActivated)
        {
            curTime -= Time.deltaTime;

            _timerSlider.value = curTime / _maxTimeSlider;

            if(curTime < 0)
            {
                //Activate
                RunOutOfTime();
            }    
        }
    }

    public void CreateQuestion()
    {
        if (questionQ.Count == 0)
        {
            //NO MORE QUESTION
            _kitUiManager.DeactivateBaseUI();
            return;
        }

        curQuestion = questionQ.Dequeue();

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
        curTime = _maxTimeSlider;

        _questionContainer.SetActive(false);
        _resultContainer.SetActive(true);

        if (answerType == curQuestion.AnswerType)
        {
            //Right Answer!!!
            _answerText.SetText("RIGHT ANSWER!!!");

        }
        else
        {
            //Wrong Answer!!
            _answerText.SetText("Wrong Answer!! /n The correct answer is:");

        }
        _resultAnswer.SetText(curQuestion.GetAnswer());
    }    

    private void RunOutOfTime()
    {
        isTimerActivated = false;
        curTime = _maxTimeSlider;

        _questionContainer.SetActive(false);
        _resultContainer.SetActive(true);
        _answerText.SetText("You didn't answer in time!");
        _resultAnswer.SetText(string.Empty);

    }

    private void OpenExplanationPage()
    {
        _nextExButton.onClick.RemoveAllListeners();

        _resultContainer.SetActive(false);
        _explainContainer.SetActive(true);

        _currentExplanationImages = curQuestion.ExplanationSprites;

        _exImage.sprite = _currentExplanationImages[curExplainImageIdx];
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
}
