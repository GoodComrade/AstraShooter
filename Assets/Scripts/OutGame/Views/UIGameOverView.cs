using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Game over view with events for buttons and showing data.
/// </summary>
public class UIGameOverView : UIView
{
    // Reference to score label.
    [SerializeField]
    private TextMeshProUGUI scoreLabel;

    // Reference to time label.
    //[SerializeField]
    //private TextMeshProUGUI timeLabel;

    // Event called when Replay Button is clicked.
    public UnityAction OnReplayClicked;

    /// <summary>
    /// Method called by Replay Button.
    /// </summary>
    public void ReplayClick()
    {
        OnReplayClicked?.Invoke();
    }

    // Event called when Menu Button is clicked.
    public UnityAction OnMenuClicked;

    /// <summary>
    /// Method called by Menu Button.
    /// </summary>
    public void MenuClicked()
    {
        OnMenuClicked?.Invoke();
    }

    /// <summary>
    /// Method used to show game data in UI.
    /// </summary>
    /// <param name="gameData">Game data.</param>
    public void ShowScore(PlayerData data)
    {
        scoreLabel.text = data.playerScore.ToString();
    }
}
