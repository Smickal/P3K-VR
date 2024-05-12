using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        [Header("Dialogue Line")]
        [SerializeField] private DialogueSystem.DialogueLine _dialogueLineContainer;

        [Header("Reference")]
        [SerializeField] private GameObject _bgContainer, _namatextContainer;
        [SerializeField] private SODialogueTalker dialogueTalkerSO;

        [Tooltip("If true, langsung hide, if not true, tunggu Action apa baru tutup dr sana")]
        [SerializeField]private bool isCloseAfterFinished, hasSceneDialogueFinish;

        [SerializeField]IEnumerator dialogSeq;
        private void Awake() 
        {
            HideDialogue();
        }

        private IEnumerator dialogueSequence(SODialogue SceneDialogue)
        {
            for(int i=0;i<SceneDialogue.Scene_Dialogues.Count;i++)
            {
                // Deactivate();
                // transform.GetChild(i).gameObject.SetActive(true);
                // DialogueSystem.DialogueLine line = _dialogueLineContainer.GetComponent<DialogueLine>();
                // Deactivate();
                if(!_dialogueLineContainer.gameObject.activeSelf)_dialogueLineContainer.gameObject.SetActive(true);
                
                
                // line.GoLineText();
                // yield return new WaitUntil(()=> line.finished);
                // line.ChangeFinished_false();
                DialoguePerLine dialogueNow = SceneDialogue.Scene_Dialogues[i];
                int dialogueTalkerNow = (int)dialogueNow.charaName;
                _dialogueLineContainer.GoLineText(dialogueNow, dialogueTalkerSO.dialogueTalkers[dialogueTalkerNow]);

                yield return new WaitUntil(()=> _dialogueLineContainer.finished);
                _dialogueLineContainer.ChangeFinished_false();
                
            }
            hasSceneDialogueFinish = true;

            if(SceneDialogue.isCloseAfterFinished) HideDialogue();
            DialogueManager.DoSomethingAfterFinish();
        }
        private void Deactivate()
        {
            _dialogueLineContainer.gameObject.SetActive(false);
        }
        public void ShowDialogue(SODialogue SceneDialogue)
        {
            StopCourotineNow();
            gameObject.SetActive(true);
            dialogSeq = dialogueSequence(SceneDialogue);
            StartCoroutine(dialogSeq);
        }
        public void HideDialogue()
        {
            if(hasSceneDialogueFinish)hasSceneDialogueFinish = false;
            _namatextContainer.SetActive(false);
            _bgContainer.SetActive(false);
            gameObject.SetActive(false);
            
        }
        public void StopCourotineNow()
        {
            if(!_dialogueLineContainer.finished)_dialogueLineContainer.StopLineText();
            if(dialogSeq == null)return;
            if(!hasSceneDialogueFinish)StopCoroutine(dialogSeq);
            dialogSeq = null;
            
        }

        public void StopCoroutineAbruptly()
        {
            StopCourotineNow();
            HideDialogue();

            //gaperlu bawah krn ini bakal cuma terjadi di A suruh ngapain, yg biasanya di akhirnya bakal disuru hal lain yg bukan manggil dialog ke dialog, ex : suruh ambil barang, kalo brgnya ud diambil si brg ini yg bakal ngelakuin itu, bukan dr sininya, biasanya ini kalo dr dialog abis itu lanjut dialog, ato dialog abis itu lanjut timeline, krn emg gabisa diskip lol
            
            // DialogueManager.DoSomethingAfterFinish();
        }

        public bool HasSceneDialogueFinish() {return hasSceneDialogueFinish;}
    }
}

