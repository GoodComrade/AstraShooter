using UnityEngine;
using UnityEngine.Events;

// Menu view with events for buttons.
public class UIMenuView : UIView
{
    // Event called when Play Button is clicked.
    public UnityAction OnPlayClicked;

    // Event called when Quit Button is clicked.
    public UnityAction OnQuitClicked;

    [HideInInspector]
    public int currentLevelIndex = 0;

    // Method called by Play Button.
    public void PlayClicked(int index)
    {
        currentLevelIndex = index;
        OnPlayClicked?.Invoke();
    }

    // Method called by Quit Button.
    public void QuitClicked()
    {
        OnQuitClicked?.Invoke();
    }
}
