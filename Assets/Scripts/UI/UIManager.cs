using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image fillBarEnnegy;
    Movement playerMovement;

    private void Start()
    {
        // Make sure pause menu off when start level
        if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        ShowPauseMenu();
        GetValueEnegy();
    }

    private void ShowPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void ShowPauseMenuAndroid()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenuAndroid()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void GetValueEnegy()
    {
        playerMovement = FindObjectOfType<Movement>();

        if (playerMovement == null) { return; }

        float value = playerMovement.TimeUseEnergy / playerMovement.outTimeUseEnegy;

        value = 1 - value;
        fillBarEnnegy.GetComponent<Image>().fillAmount = value;
    }
}
