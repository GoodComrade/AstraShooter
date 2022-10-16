using UnityEngine;

// UI root for Game controller.
public class UIGameRoot : UIRoot
{
    // Reference to game view class.
    [SerializeField]
    private UIGameView gameView;
    public UIGameView GameView => gameView;

    public override void ShowRoot()
    {
        base.ShowRoot();

        gameView.ShowView();
    }

    public override void HideRoot()
    {
        gameView.HideView();

        base.HideRoot();
    }
}
