using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelGameOver : MonoBehaviour
{
    private void Start()
    {
        RestartGame();

        GoToMainMenu();

        QuitGame();
    }


    public void RestartGame()
    {
        GameObject buttonGO = gameObject.transform.GetChild(2).gameObject;
        Button QuitGamebtn = buttonGO.GetComponent<Button>();
        QuitGamebtn.onClick.AddListener(() => GameManager.Instance.RestartGame());
    }

    public void GoToMainMenu()
    {
        GameObject buttonGO = gameObject.transform.GetChild(1).gameObject;
        Button QuitGamebtn = buttonGO.GetComponent<Button>();
        QuitGamebtn.onClick.AddListener(() => GameManager.Instance.LoadScene(0));
    }

    public void QuitGame()
    {
        GameObject buttonGO = gameObject.transform.GetChild(0).gameObject;
        Button QuitGamebtn = buttonGO.GetComponent<Button>();
        QuitGamebtn.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }
}
