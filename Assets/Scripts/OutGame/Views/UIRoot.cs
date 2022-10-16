using UnityEngine;

/// <summary>
/// Базовый класс для корневых классов разных контроллеров.
/// </summary>
public class UIRoot : MonoBehaviour
{
    /// <summary>
    /// Метод для отображения UI.
    /// </summary>
    public virtual void ShowRoot()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Метод для сокрытия UI.
    /// </summary>
    public virtual void HideRoot()
    {
        gameObject.SetActive(false);
    }
}
