using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    #region variables

    private static GameManager instance;

    [SerializeField]
    private GameObject gameOverPanelGO;

    [SerializeField]
    public GameLevel gameLevel;

    private GameObject canvasGO;

    private GameObject countDownGO;

    private GameObject adManagerGO;

    public GameObject selectedLevelGO;
  
    private int selectedLevelIndex;

    private float phoneDPI;

    private bool isGameOver;

    #endregion

    #region Properties

    private bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public int SelectedLevelIndex
    {
        get { return selectedLevelIndex; }
        set { selectedLevelIndex = value; }
    }

    public float PhoneDPI
    {
        get { return phoneDPI; }
        set { phoneDPI = value; }
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {     
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetCleanGOCollider(bool isEnable)
    {
        selectedLevelGO.GetComponent<MeshCollider>().enabled = isEnable;
    }

    public float GetCleanTargetAmount()
    {
        return CleanDetails.GetTargetAmount(selectedLevelIndex);
    }

    public void SetGameOver()
    {
        if (!isGameOver)
        {
            AudioManager.instance.SetCleaningSound(AudioState.State.Stop);

            AudioManager.instance.SetLevelCompletedSound(AudioState.State.Play);

            SetCleanGOCollider(false);

            float finishedTime = GetComponent<CleanTimer>().Timer;

            EnableTimer(false);

            GameOverScore(finishedTime);

            isGameOver = true;
        }
    }

    private void GameOverScore(float playerTime)
    {
        int starWon = CleanDetails.GetStarWon(selectedLevelIndex, playerTime);

        GetComponent<GetJSONData>().UpdateLevelStar(selectedLevelIndex, starWon);

        SetStarScoreUI(starWon, playerTime);
    }

    public void RestartGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetStarScoreUI(int playerStarScore, float playerTime)
    {
        gameOverPanelGO.SetActive(true);

        GameObject starPanel = gameOverPanelGO.transform.GetChild(0).gameObject;

        GameObject timePanel = gameOverPanelGO.transform.GetChild(1).gameObject;

        starPanel.GetComponent<SetGameOverStar>().SetStarWon(playerStarScore);

        timePanel.GetComponent<TextMeshProUGUI>().text = playerTime.ToString();
    }

    private void DisplayLevelDetails()
    {
        GameObject[] levelInfosGO;

        int nextLevelToUnlock = gameLevel.NextLevelToUnlock;

        List<LevelDetails> levelDetails = gameLevel.LevelDetails;

        levelInfosGO = GameObject.FindGameObjectsWithTag("LevelInfoContainer");

        for (int i = 0; i < levelInfosGO.Length; i++)
        {
            int levelIndex = levelInfosGO[i].GetComponent<SetStarUI>().GetLevelIndex();

            if (levelIndex <= nextLevelToUnlock)
            {
                int starCount = levelDetails[levelIndex - 1].Stars;

                if (starCount > 0)
                    levelInfosGO[i].GetComponent<SetStarUI>().SetStarDetails(starCount);

                levelInfosGO[i].GetComponent<SetLevelButton>().IsButtonActive(true);
            }

            else
                continue;
        }
    }

    private void SetAdManagerGO()
    {
        adManagerGO = GameObject.Find("AdManager");
    }

    private void SetGameOverPanelGOReference()
    {
        gameOverPanelGO = canvasGO.transform.GetChild(0).gameObject;
    }

    private void SetCanvasGO()
    {
        canvasGO = GameObject.Find("/Canvas");
    }

    private void SetCountDownGOReference()
    {
        countDownGO = canvasGO.transform.GetChild(1).GetChild(0).gameObject;
    }

    private void SetCleanGOReference()
    {
        GameObject mainGO = GameObject.Find("MainGO");

        selectedLevelGO = mainGO.transform.GetChild(selectedLevelIndex - 1).gameObject;
    }

    public void EnableTimer(bool isEnable)
    {
        GetComponent<CleanTimer>().enabled = isEnable;
    }

    public float GetTimer()
    {
        return GetComponent<CleanTimer>().Timer;
    }

    public void LoadScene(int sceneIndex)
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneIndex);

        if (sceneIndex == 1)
        {
            loadingOperation.completed += (asyncOperation) =>
            {
                isGameOver = false;

                SetCanvasGO();

                SetGameOverPanelGOReference();

                SetCountDownGOReference();

                SetCleanGOReference();

                selectedLevelGO.SetActive(true);

                countDownGO.SetActive(true);
            };
        }           
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameLevel == null)
            SetGameLevelData(GetComponent<GetJSONData>().GetLevelData());

        DisplayLevelDetails();    
    }

    public void SetGameLevelData(GameLevel gameLevel)
    {
        this.gameLevel = gameLevel;
    }

    public GameLevel GetSavedGameData()
    {
        return gameLevel;
    }

    #endregion

}
