using UnityEngine;

/// <summary>
/// корневой класс для контроллера конца игрового уровня.
/// </summary>
public class UIGameOverRoot : UIRoot
{
    // Reference to game over view class.
    [SerializeField]
    private UIGameOverView gameOverView;
    public UIGameOverView GameOverView => gameOverView;

    public override void ShowRoot()
    {
        base.ShowRoot();

        gameOverView.ShowView();
    }

    public override void HideRoot()
    {
        gameOverView.HideView();

        base.HideRoot();
    }
}
