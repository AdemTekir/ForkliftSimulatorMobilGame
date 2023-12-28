using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameMenuEdit : MonoBehaviour
{
    public GameObject gameLoseMenuUI;
    public GameObject levelCompleteMenuUI;
    public GameObject palletCountCheckMenuUI;
    public GameObject palletCountCheckMenuHeaderUI;
    public GameObject timeCountUI;
    public GameObject pauseGameBtn;
    public GameObject changeCameraBtn;

    public float palletCount;
    public int totalNumberPallet = 3;

    private float timeCountForStopGame = 2f;
    private bool gameOverState;
    private bool gameLoseControl = false;

    private void Update()
    {
        if (gameOverState)
        {
            if (timeCountForStopGame > 0)
            {
                timeCountForStopGame -= Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0f;

                gameOverState = false;
            }
        }
    }

    public void LevelCompleteMenu(string levelCompleteHeader)
    {
        if (gameLoseControl)
        {
            return;
        }

        timeCountUI.SetActive(false);

        pauseGameBtn.GetComponent<Button>().enabled = false;
        changeCameraBtn.GetComponent<EventTrigger>().enabled = false;

        levelCompleteMenuUI.SetActive(true);
        levelCompleteMenuUI.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "" + levelCompleteHeader;

        gameOverState = true;

    }

    public void PalletCountCheckMenu(float add)
    {
        if (gameLoseControl)
        {
            return;
        }

        palletCount += add;

        if (palletCount < totalNumberPallet)
        {
            palletCountCheckMenuUI.SetActive(true);
            palletCountCheckMenuHeaderUI.GetComponent<TextMeshProUGUI>().text = palletCount + " / " + totalNumberPallet + " PALET KOYULDU";
        }
        else if (palletCount == totalNumberPallet)
        {
            palletCountCheckMenuUI.SetActive(true);
            palletCountCheckMenuHeaderUI.GetComponent<TextMeshProUGUI>().text = palletCount + " / " + totalNumberPallet + " PALET KOYULDU";
            LevelCompleteMenu("BÖLÜM TAMAMLANDI!");
        }
    }

    public void SetTimer(float time, bool state = false) 
    {
        if (state && palletCount != totalNumberPallet)
        {
            timeCountUI.SetActive(true);
            timeCountUI.GetComponent<TextMeshProUGUI>().text = time.ToString("F0");
        }
        else
        {
            timeCountUI.SetActive(false);
            timeCountUI.GetComponent<TextMeshProUGUI>().text = "";
            palletCountCheckMenuUI.SetActive(false);
        }
    }

    public void GameLoseMenu(string gameLoseMenuHeader)
    {
        if (gameOverState)
        {
            return;
        }

        pauseGameBtn.GetComponent<Button>().enabled = false;
        changeCameraBtn.GetComponent<EventTrigger>().enabled = false;

        gameLoseMenuUI.SetActive(true);
        gameLoseMenuUI.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "" + gameLoseMenuHeader;

        gameOverState = true;
        gameLoseControl = true;
    }
}
