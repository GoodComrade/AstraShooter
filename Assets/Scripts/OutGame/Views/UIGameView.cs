using UnityEngine;
using UnityEngine.Events;
using TMPro;

// Game view with events for buttons and showing data.
public class UIGameView : UIView
{
    // Reference to time label.
    [SerializeField]
    private TextMeshProUGUI playerHPLabel;

    [SerializeField]
    private TextMeshProUGUI playerScoreLabel;


    // Event called when Finish Button is clicked.
    public UnityAction OnFinishClicked;

    // Method called by Finish Button.
    public void FinishClick()
    {
        OnFinishClicked?.Invoke();
    }

    // Event called when Menu Button is clicked.
    public UnityAction OnMenuClicked;

    // Method called by Menu Button.
    public void MenuClicked()
    {
        OnMenuClicked?.Invoke();
    }

    //Method used to update playerHP label.
    public void UpdateHP(GameController controller)
    {
        playerHPLabel.text = "X" + controller.playerHP.ToString();
    }

    // Method used to update playerScore label.
    public void UpdateScore(GameController controller)
    {
        playerScoreLabel.text = controller.scoreWinCondition + "/" + controller.playerScore.ToString();
    }
}
