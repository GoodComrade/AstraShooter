﻿using UnityEngine;

/// <summary>
/// Уорневой UI класс для контроллеров меню.
/// </summary>
public class UIMenuRoot : UIRoot
{
    // Reference to menu view class.
    [SerializeField]
    private UIMenuView menuView;
    public UIMenuView MenuView => menuView;

    public override void ShowRoot()
    {
        base.ShowRoot();

        menuView.ShowView();
    }

    public override void HideRoot()
    {
        menuView.HideView();

        base.HideRoot();
    }
}
