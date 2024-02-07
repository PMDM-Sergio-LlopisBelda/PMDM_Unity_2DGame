using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopChestHandler : MonoBehaviour
{

    public Text coinText;
    public Canvas mainCanvas;
    public Canvas shopCanvas;
    public GameObject gamePlayer;
    public AudioSource openChest;
    public AudioSource clicked;
    
    // Start is called before the first frame update
    void Start()
    {
        shopCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleOpenChest() {
        coinText.text = "Coins: " + GameManager.coindsCollected.ToString();
        if (shopCanvas != null) {
            openChest.Play();
            shopCanvas.enabled = true;
            gamePlayer.SetActive(false);
        }
        if (mainCanvas != null) {
            mainCanvas.enabled = false;
        }
    }

    public void ExitClick() {
        clicked.Play();
        if (mainCanvas != null) {
            mainCanvas.enabled = true;
        }
        if (shopCanvas != null) {
            shopCanvas.enabled = false;
        }
        gamePlayer.SetActive(true);
    }

    public void HpClick() {
        clicked.Play();
        if (GameManager.coindsCollected > 0) {
            GameManager.coindsCollected--;
            GameManager.playerMaxHp++;
            coinText.text = "Coins: " + GameManager.coindsCollected.ToString();
        }
    }

    public void DmgClick() {
        clicked.Play();
        if (GameManager.coindsCollected > 0) {
            GameManager.coindsCollected--;
            GameManager.playerDmg++;
            coinText.text = "Coins: " + GameManager.coindsCollected.ToString();
        }
    }

}
