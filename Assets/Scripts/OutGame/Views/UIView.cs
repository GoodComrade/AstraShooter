using UnityEngine;

/// <summary>
/// Базовый класс для всех представлений UI.
/// </summary>
public class UIView : MonoBehaviour
{
    /// <summary>
    /// Метод для отображения представления или эллемента.
    /// </summary>
    public virtual void ShowView()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Метод для сокрытия представления или эллемента.
    /// </summary>
    public virtual void HideView()
    {
        gameObject.SetActive(false);
    }
}

