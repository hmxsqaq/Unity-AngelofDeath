using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace tool
{
    public class DialogueManager : SingletonMonoDDOL<DialogueManager>
    {
        [SerializeField] private Image leftHead,rightHead,dialoguePanel;
        [SerializeField]private Text text,AName,BName;
        [SerializeField]private List<Sprite> leftHeads = new();
        [SerializeField]private List<Sprite> rightHeads = new();
        private readonly List<string> texts = new();
        [SerializeField]private TextAsset[] textAsset;
        private int index;
        private bool canDialogue,isDialoguing;
        [SerializeField] private List<BranchButton> branchButtons = new();
        private bool _isBranch;

        void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !canDialogue||_isBranch) return;
            if (isDialoguing)
            {
                StopCoroutine(nameof(ChangeText));
                isDialoguing = false;
                text.text = texts[index];
            }else
                UpdateDialoguePanel();
        }
        
        public void StartDialogue(int textIndex)
        {
            Debug.Log($"{textIndex}对话开始");
            if (textAsset[textIndex] ==null)
            {
                Debug.Log("缺少对话");
                return;
            }
            var lineDate = textAsset[textIndex].text.Split("\n");
            foreach (var line in lineDate)
            {
                texts.Add(line);
            }
            text.text = texts[0];
            UpdateDialoguePanel();
            canDialogue = true;
            dialoguePanel.gameObject.SetActive(true);
        }

        public void StartDialogue(string textName)
        {
            int textIndex=0;
            bool hasFound= false;
            for (int i = 0; i < textAsset.Length; i++)
            {
                Debug.Log(textAsset[i].name);
                if (textName==textAsset[i].name)
                {
                    textIndex = i;
                    hasFound = true;
                    break;
                }
            }
            if (!hasFound)
            {
                Debug.Log($"未找到对话{textName}");
                return;
            }
            Debug.Log($"{textIndex}对话开始");
            if (textAsset[textIndex] ==null)
            {
                Debug.Log("缺少对话");
                return;
            }
            var lineDate = textAsset[textIndex].text.Split("\n");
            foreach (var line in lineDate)
            {
                texts.Add(line);
            }
            text.text = texts[0];
            UpdateDialoguePanel();
            canDialogue = true;
            dialoguePanel.gameObject.SetActive(true);
        }
    
        public void UpdateDialoguePanel()
        {
            index++;
            
            
            switch (texts[index])
            {
                case "END":
                case "END\r":
                    CloseDialoguePanel();
                    return;
                case "A\r":
                case "A":
                    leftHead.sprite = leftHeads[int.Parse(texts[index+1])];
                    AName.text = texts[index + 2]=="_"||texts[index + 2]=="_\r"?"":texts[index + 2];
                    leftHead.gameObject.SetActive(true);
                    rightHead.gameObject.SetActive(false);
                    index+=3;
                    break;
                case "B\r":
                case "B":
                    rightHead.sprite = rightHeads[int.Parse(texts[index+1])];
                    BName.text = texts[index + 2]=="_"||texts[index + 2]=="_\r"?"":texts[index + 2];
                    leftHead.gameObject.SetActive(false);
                    rightHead.gameObject.SetActive(true);
                    index+=3;
                    break;
                case "@":
                case "@\r":
                    index+=1;
                    UpdateDialoguePanel();
                    return;
                case "BRANCH":
                case "BRANCH\r":
                    var branchs = new List<string>();
                    if (texts[index+3] is "BRANCHEND" or "BRANCHEND\r")
                    {
                        branchs.Add(texts[index+1]);
                        branchs.Add(texts[index+2]);
                        index += 3;
                    }
                    if (texts[index+4] is "BRANCHEND" or "BRANCHEND\r")
                    {
                        branchs.Add(texts[index+1]);
                        branchs.Add(texts[index+2]);
                        branchs.Add(texts[index+3]);
                        index += 4;
                    }
                    for (int i = 0; i < branchs.Count; i++)
                    {
                        branchButtons[i].ChangeText(branchs[i]);
                    }
                    _isBranch = true;
                    return;
                case "GOTO":
                case "GOTO\r":
                    RecognitionGoTo(texts[index+1]);
                    UpdateDialoguePanel();
                    return;
                case "EVENT":
                case "EVENT\r":
                    Debug.Log(texts[index+1]);
                    index++;
                    UpdateDialoguePanel();
                    return;
            }
            StartCoroutine(nameof(ChangeText));
        }
    
        private void CloseDialoguePanel()
        {
            index = 0;
            texts.Clear();
            canDialogue = false;
            leftHead.gameObject.SetActive(false);
            rightHead.gameObject.SetActive(false);
            dialoguePanel.gameObject.SetActive(false);
            Debug.Log("对话结束");
        }

        private IEnumerator ChangeText()
        {
            text.text = "";
            isDialoguing = true;
            for (int i = 0; i < texts[index].Length; i++)
            {
                text.text += texts[index][i];
                yield return new WaitForSeconds(0.05f);
            }
            isDialoguing = false;
        }

        public void RecognitionGoTo(string GoToName)
        {
            bool hasFound = false;
            for (int i = 0; i < texts.Count; i++)
            {
                if ((texts[i] == "@" || texts[i] == "@\r")&& (texts[i+1] == GoToName || texts[i+1] == GoToName + "\r"))
                {
                    index = i+1;
                    Debug.Log($"index为{index}");
                    hasFound = true;    
                    break;
                }
            }
            if (!hasFound)
            {
                Debug.Log($"未找到字符:{GoToName}");
            }
            foreach (var branchButton in branchButtons)
            {
                branchButton.gameObject.SetActive(false);
            }
            _isBranch = false;
        }
    }
}


