using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Представление меню с ивентами для кнопок.
/// </summary>
public class UIMenuView : UIView
{
    public UnityAction OnPlayClicked;

    public UnityAction OnQuitClicked;

    [HideInInspector]
    public int currentLevelIndex = 0;

    /// <summary>
    /// Метод, вызываемый кнопкой игрового уровня..
    /// </summary>
    /// <param name="index">Индекс выбранного уровня</param>
    public void PlayClicked(int index)
    {
        currentLevelIndex = index;
        OnPlayClicked?.Invoke();
    }

    /// <summary>
    /// Метод, вызываемый кнопкой выхода из игры.
    /// </summary>
    public void QuitClicked()
    {
        OnQuitClicked?.Invoke();
    }
}
