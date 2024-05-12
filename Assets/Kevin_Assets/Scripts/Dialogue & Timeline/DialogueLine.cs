using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



namespace DialogueSystem
{
    public class DialogueLine : DialogueBase
    {
        
        [Header("Option")]
        [SerializeField]private DialogueTalkerType dialogueType;
        [Header("All text")]
        private string dialogueText;
        private TMP_Text _textContainer;
        // [SerializeField]private Color _textColor;
        [Tooltip ("Kalo off berarti dia ambil dr yg ada di desc text")]
        // [SerializeField]private bool isInputText_FromOutside = true;

        [Header("Time Delay ")]
        [SerializeField]private float delayTypeText;
        [SerializeField]private float delayBetweenLines;
        [Header("Name")]
        [SerializeField]private string charaName;
        [SerializeField]private TMP_Text _nametextContainer;
        [SerializeField]private GameObject _bgContainer;

        private IEnumerator lineText;

        private void Awake() 
        {
            _textContainer = GetComponent<TextMeshProUGUI>();
            
        }
        public void ChangeInputText(string inputTexts)
        {
            dialogueText = inputTexts;
        }
        public void ChangeDelayTypeText(float delay)
        {
            delayTypeText = delay;
        }

        public void GoLineText(DialoguePerLine dialogueInput, DialogueTalker Talker)
        {
            _bgContainer.gameObject.SetActive(true);

            // /if(!isInputText_FromOutside)
            // {
            //     dialogueText = _textContainer.text;
            // }
            // else
            // {
            //     dialogueText = dialogueInput.dialogueText;
            // }
            _textContainer.text = "";

            if(dialogueInput.dialogueTalkerType == DialogueTalkerType.character) // cek list enum dialoguetype dr dialoguetalker
            {
                _nametextContainer.color = Talker._textColor;
                // string _talkerName = Talker.charaName.ToString();
                // _talkerName = _talkerName.Replace('_',' ');
                _nametextContainer.text = Talker.name + " :";
                _nametextContainer.gameObject.SetActive(true);
            }
            else
            {
                _nametextContainer.color = Talker._textColor;
                _nametextContainer.text = "*INSTRUCTION*";
                _nametextContainer.gameObject.SetActive(true);
            }
            lineText = typeText(dialogueInput.dialogueText, _textContainer, delayTypeText, delayBetweenLines, Talker._textColor);
            StartCoroutine(lineText);

        }
        public void StopLineText()
        {
            if(lineText == null)return;
            StopCoroutine(lineText);
            lineText = null;
            finished = false;
            // Debug.Log("Ke sini ada orang dul");
        }
    }
}

