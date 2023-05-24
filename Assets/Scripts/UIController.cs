using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text jumpText;
    [SerializeField] private GameObject StorePanel;
    [SerializeField] private GameObject StarterPanel;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StarterPanel.SetActive(true);
            StorePanel.SetActive(false);
        }
            
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
            PlayerPrefs.SetInt("JumpPower", 0);
        }
        jumpText.text = PlayerPrefs.GetInt("JumpPower").ToString();
        CoinTextDisplay();

        EventManager.CoinCollectEvent += CoinTextDisplay;
        EventManager.PlayerDieEvent += DeleteCoinText;
        EventManager.PlayerDieEvent += DeleteJumpPowerText;
        EventManager.JumpPowerCollectEvent += JumpPowerTextDisplay;
    }
    private void CoinTextDisplay()
    {
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    private void DeleteCoinText()
    {
        PlayerPrefs.DeleteAll();
        coinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    private void JumpPowerTextDisplay()
    {
        jumpText.text = PlayerPrefs.GetInt("JumpPower").ToString();
    }
    private void DeleteJumpPowerText()
    {
        PlayerPrefs.DeleteAll();
        jumpText.text = PlayerPrefs.GetInt("JumpPower").ToString();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void StoreButton()
    {
        StarterPanel.SetActive(false);
        StorePanel.SetActive(true);
    }
    public void BackButton()
    {
        StarterPanel.SetActive(true);
        StorePanel.SetActive(false);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

}
