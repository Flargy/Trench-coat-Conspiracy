using System.Collections;
using Interaction;
using UnityEngine;
using UnityEngine.UI;

public class Challenge3 : MonoBehaviour
{
    private InteractionChallenge3 _interactionChallenge3;
    private ColorBlock            _orgInputColorBlock;
    private int                   _points = 0;
    private int                   _pointsToWin;
    private int                   _selectedInputButtonID = -1;

    [SerializeField]                 private Button[] inputButtons;
    [SerializeField]                 private Button[] outputButtons;
    [SerializeField]                 private Image[]  cables;
    [Space]
    [SerializeField]                 private Color    selectedButtonColor;
    //[SerializeField]                 private Color    challengeCompleteButtonColor;
    [SerializeField, Range(.1f, 2f)] private float    wrongCombinationDuration = 1f;
    //[SerializeField]                 private float    timerForWinReveal        = 1f;
    [SerializeField]                 private float    timerForClosing          = 1f;

    //private void Awake() => Init();

    public void InputButtonClicked(int buttonID) => _selectedInputButtonID = SelectInputButton(buttonID);

    public void OutputButtonClicked(int buttonID)
    {
        if (_selectedInputButtonID == buttonID)
        {
            ResetButtonColor(false, buttonID);

            inputButtons[buttonID].interactable = false;
            outputButtons[buttonID].interactable = false;
            cables[buttonID].enabled = true;
            CheckWin();
        }
        else if (_selectedInputButtonID == -1)
        {
            StartCoroutine(DisableButton(buttonID, .3f));
        }
        else
        {
            StartCoroutine(DisableButton(buttonID, wrongCombinationDuration));
        }

        ResetButtonColor(true, _selectedInputButtonID);
        _selectedInputButtonID = -1;
    }

    public void ExitButtonClicked() => StartCoroutine(CloseInstance(0.2f));

    public void Init(InteractionChallenge3 interactionChallenge3)
    {
        _interactionChallenge3 = interactionChallenge3;
        
        _pointsToWin = inputButtons.Length;

        _orgInputColorBlock = inputButtons[0].colors;

        foreach (Image cable in cables)
        {
            cable.enabled = false;
        }
    }

    private int SelectInputButton(int buttonID)
    {
        if (buttonID == -1) return -1;

        ResetButtonColor(true, _selectedInputButtonID);

        ColorButton(true, buttonID, selectedButtonColor);

        return buttonID;
    }

    private void ColorButton(bool isInputButton, int buttonID, Color color)
    {
        if (buttonID < 0) return;

        Button     button     = isInputButton ? inputButtons[buttonID] : outputButtons[buttonID];
        ColorBlock colorBlock = button.colors;

        colorBlock.normalColor = color;
        colorBlock.pressedColor = color;
        colorBlock.highlightedColor = color;
        colorBlock.disabledColor = color;
        button.colors = colorBlock;
    }

    private void ResetButtonColor(bool isInputButton, int buttonID)
    {
        if (buttonID < 0) return;

        Button button = isInputButton ? inputButtons[buttonID] : outputButtons[buttonID];

        button.colors = _orgInputColorBlock;
    }

    private void CheckWin()
    {
        _points++;

        if (_points == _pointsToWin)
        {
            _interactionChallenge3.CompleteChallenge();
            print("Yay, you won!");
            ResetButtonColor(true, _selectedInputButtonID);
            //StartCoroutine(WinTransition(timerForWinReveal));
            StartCoroutine(CloseInstance(timerForClosing));
        }
    }

    private IEnumerator DisableButton(int buttonID, float duration)
    {
        outputButtons[buttonID].interactable = false;

        yield return new WaitForSeconds(duration);

        outputButtons[buttonID].interactable = true;
    }

    // private IEnumerator WinTransition(float timer)
    // {
    //     MouseInput.Activate(true);
    //     yield return new WaitForSeconds(timer);
    //
    //     for (int i = 0; i < inputButtons.Length; i++)
    //     {
    //         ColorButton(true, i, challengeCompleteButtonColor);
    //         ColorButton(false, i, challengeCompleteButtonColor);
    //     }
    //
    //     StartCoroutine(CloseInstance(timerForClosing));
    // }

    private IEnumerator CloseInstance(float timer)
    {
        MouseInput.Activate(true);
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}