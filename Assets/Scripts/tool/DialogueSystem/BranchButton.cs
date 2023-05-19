using tool;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BranchButton : MonoBehaviour
    {
        private string _branchName;
        [SerializeField] private Text branchNameUI;
        
        public void ChangeText(string branchName)
        {
            _branchName = branchName;
            branchNameUI.text = branchName;
            gameObject.SetActive(true);
        }

        public void ChooseBranch()
        {
            DialogueManager.Instance.RecognitionGoTo(_branchName);
            DialogueManager.Instance.UpdateDialoguePanel();
            Debug.Log(_branchName);
        }
    }
}