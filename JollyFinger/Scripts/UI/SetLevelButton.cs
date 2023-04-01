using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetLevelButton : MonoBehaviour
{
    private int levelIndex;

    private Button selectLevelbtn;

    private void SetReference()
    {
        levelIndex = GetComponent<SetStarUI>().GetLevelIndex();

        GameObject buttonGO = gameObject.transform.GetChild(1).GetChild(0).gameObject;

        selectLevelbtn = buttonGO.GetComponent<Button>();
        selectLevelbtn.onClick.AddListener(() => GoToScene(1, levelIndex));    
    }

    private void GoToScene(int sceneIndex, int levelIndex)
    {
        GameManager.Instance.SelectedLevelIndex = levelIndex;

        GameManager.Instance.LoadScene(sceneIndex);
    }

    public void IsButtonActive(bool isEnable)
    {
        SetReference();

        selectLevelbtn.interactable = isEnable;
    }
}
