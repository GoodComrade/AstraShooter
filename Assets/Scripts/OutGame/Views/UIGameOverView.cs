using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Представление конца игры с ивентами для кнопок и отображением счета игрока.
/// </summary>
public class UIGameOverView : UIView
{
    [SerializeField]
    private TextMeshProUGUI scoreLabel;

    public UnityAction OnReplayClicked;

    public UnityAction OnMenuClicked;


    /// <summary>
    /// Метод, вызываемой кнопкой Replay.
    /// </summary>
    public void ReplayClick()
    {
        OnReplayClicked?.Invoke();
    }

    /// <summary>
    /// Метод, вызываемой кнопкой Menu.
    /// </summary>
    public void MenuClicked()
    {
        OnMenuClicked?.Invoke();
    }

    /// <summary>
    /// Методб используемый для отображения финального счета игрока в UI.
    /// </summary>
    /// <param name="gameData">Game data.</param>
    public void ShowScore(PlayerData data)
    {
        scoreLabel.text = data.playerScore.ToString();
    }
}
