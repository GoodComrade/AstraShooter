using UnityEngine;

/// <summary>
/// Базовый класс для подконтроллеров с ссылкой на корневой контроллер.
/// </summary>
public abstract class SubController : MonoBehaviour
{
    [HideInInspector]
    public RootController root;

    /// <summary>
    /// Метод для активации контроллера.
    /// </summary>
    public virtual void EngageController()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Метод для деактивации контроллера.
    /// </summary>
    public virtual void DisengageController()
    {
        gameObject.SetActive(false);
    }
}

/// <summary>
/// Расширение класса подконтроллера с обобщенной ссылкой на UI Root.
/// </summary>
public abstract class SubController<T> : SubController where T : UIRoot
{
    [SerializeField]
    protected T ui;
    public T UI => ui;

    public override void EngageController()
    {
        base.EngageController();

        ui.ShowRoot();
    }

    public override void DisengageController()
    {
        base.DisengageController();

        ui.HideRoot();
    }
}
