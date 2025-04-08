using Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueController : MonoBehaviour
{
    public TextMeshProUGUI captionText;

    [SerializeField] private GameObject dialogueOptionsPanel;   //reference to the dialogue options panel game object
    [SerializeField] private Slider dialogueSlider;             //reference to the time slider component
    [SerializeField] private TextMeshProUGUI[] dialogueOptions; //list of dialogue options texts

    private int _dialogueIndex;                                 //number of possible options
    private int _selectedOption;                                //points to the scrolled to option
    private float _dialogueTimer;                               //time before dialogue options disappear
    
    private bool _isChoiceActive;                               //is the dialogue active
    private bool _isChoicesVisible;                             //can the player see the choices
    private bool _wasScrollUsed;                                //did the player use the scroll to select an option

    private void Update()
    {
        if (!_isChoicesVisible)
        {
            _dialogueTimer -= 0.1f * Time.deltaTime;
            dialogueSlider.value = _dialogueTimer;
        }
        if (_dialogueTimer <= 0)
        {
            UIEndOptions();
        }
    }

    public void UIInitiateOptions(Dialogue[] list)
    {
        //Subscribe to opening and closing dialogue options input signals
        InputSignals.Instance.OnOpenDialogueOptions = OpenDialogueChoices;
        InputSignals.Instance.OnCloseDialogueOptions = CloseDialogueChoices;

        _isChoiceActive = true;
        _isChoicesVisible = false;
        _wasScrollUsed = false;
        _dialogueTimer = 1;
        _selectedOption = 0;
        _dialogueIndex = list.Length;
        dialogueOptionsPanel.SetActive(true);

        for (int i = 0; i < _dialogueIndex; i++)
        {
            dialogueOptions[i].text = $"{i + 1}. {list[i].title}";
        }
    }
    
    public void UIEndOptions()
    {
        InputSignals.Instance.OnOpenDialogueOptions = null;
        InputSignals.Instance.OnCloseDialogueOptions = null;
        dialogueOptionsPanel.SetActive(false);
        _isChoiceActive = false;
        HideOptions();
    }

    private void OpenDialogueChoices()
    {
        dialogueOptionsPanel.SetActive(false);

        for (int i = 0; i < _dialogueIndex; i++)
        {
            dialogueOptions[i].gameObject.SetActive(true);
        }

        _isChoicesVisible = true;
        InputSignals.Instance.OnChooseNumber = ChooseByNumberInput;
        InputSignals.Instance.OnInputScrolling = ChooseByScrollInput;
    }
    private void CloseDialogueChoices()
    {
        HideOptions();

        if (!_isChoiceActive) return;

        if (_wasScrollUsed)
        {
            DialogueManager.Instance.RequestPlayDialogue(DialogueManager.Instance.currentDialogue.responses[_selectedOption]);
            dialogueOptions[_selectedOption].color = Color.white;
            return;
        }

        dialogueOptionsPanel.SetActive(true);
    }

    private void ChooseByNumberInput(uint n)
    {
        if (n > _dialogueIndex || !_isChoicesVisible) return;

        DialogueManager.Instance.RequestPlayDialogue(DialogueManager.Instance.currentDialogue.responses[n - 1]);
        dialogueOptions[n - 1].color = Color.white;

        UIEndOptions();

        if (!_wasScrollUsed) return;
        dialogueOptions[_selectedOption].color = Color.white;
    }

    private void ChooseByScrollInput(float val)
    {
        Debug.Log("Scroll value: " + val);
        if (!_wasScrollUsed)
        {
            _wasScrollUsed = true;
            _selectedOption = 0;
            dialogueOptions[_selectedOption].color = Color.yellow;
            return;

        }

        dialogueOptions[_selectedOption].color = Color.white;
        if (val > 0)
        {
            _selectedOption++;
            if (_selectedOption > _dialogueIndex - 1)
            {
                _selectedOption = 0;
            }
        }
        else if (val < 0)
        {
            _selectedOption--;
            if (_selectedOption < 0)
            {
                _selectedOption = _dialogueIndex - 1;
            }
        }
        dialogueOptions[_selectedOption].color = Color.yellow;
    }

    private void HideOptions()
    {
        for (int i = 0; i < _dialogueIndex; i++)
        {
            dialogueOptions[i].gameObject.SetActive(false);
        }

        _isChoicesVisible = false;
        InputSignals.Instance.OnChooseNumber = null;
        InputSignals.Instance.OnInputScrolling = null;
    }
}