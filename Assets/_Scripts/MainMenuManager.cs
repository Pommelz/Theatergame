using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Transform creditsPnl;
    public Transform tutorialPnl;
    public Transform bttnPnl;

    public enum MenuPnl
    {
        CREDITS,
        HOWTOPLAY,
    }

    public void StartGame()
    {
        SceneController.Instance.LoadScene(StringCollection.GAME_SCENE);
    }

    public void ControlCreditsPnl(bool _enable)
    {
        creditsPnl.gameObject.SetActive(_enable);
        bttnPnl.gameObject.SetActive(!_enable);
    }

    public void ControlTutorialPnl(bool _enable)
    {
        tutorialPnl.gameObject.SetActive(_enable);
        bttnPnl.gameObject.SetActive(!_enable);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
