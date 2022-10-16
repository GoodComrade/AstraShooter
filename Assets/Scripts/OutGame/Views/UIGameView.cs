using UnityEngine;
using UnityEngine.Events;
using TMPro;
/// <summary>
/// Предстваление игрового уровня с ивентами для кнопок и отображения данных
/// </summary>
public class UIGameView : UIView
{
    [SerializeField]
    private TextMeshProUGUI playerHPLabel;

    [SerializeField]
    private TextMeshProUGUI playerScoreLabel;

    public UnityAction OnFinishClicked;

    public void FinishClick()
    {
        OnFinishClicked?.Invoke();
    }

    public UnityAction OnMenuClicked;

    public void MenuClicked()
    {
        OnMenuClicked?.Invoke();
    }

    public void UpdateHP(GameController controller)
    {
        playerHPLabel.text = "X" + controller.playerHP.ToString();
    }

    public void UpdateScore(GameController controller)
    {
        playerScoreLabel.text = controller.scoreWinCondition + "/" + controller.playerScore.ToString();
    }
}
