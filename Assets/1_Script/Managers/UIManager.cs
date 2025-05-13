using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<Panel> panelList = new List<Panel>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStart += StartGame;
        GameManager.Instance.OnGameFail += Fail;
        GameManager.Instance.OnGameSuccess += Success;

        HideAll();
        Show<MenuPanel>();
    }

    public void Show<T>() where T : Panel
    {
        panelList.OfType<T>().FirstOrDefault().Show();
    }
    public void Hide<T>() where T : Panel
    {
        panelList.OfType<T>().FirstOrDefault().Hide();
    }
    public T GetPanel<T>() where T : Panel
    {
        return panelList.OfType<T>().FirstOrDefault();
    }

    public void Fail()
    {
        DOVirtual.DelayedCall(1.5f, () =>
        {
            HideAll();
            Show<GamePanel>();
            Show<FailPanel>();
        }, false);
    }

    public void Success()
    {
        DOVirtual.DelayedCall(1.5f, () =>
        {
            HideAll();
            Show<GamePanel>();
            Show<SuccesPanel>();
        }, false);
    }

    public void StartGame()
    {
        HideAll();
        Show<GamePanel>();
    }

    void HideAll()
    {
        panelList.ForEach(panel => panel.Hide());
    }
}