using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInterface : MonoBehaviour
{

    private AudioSource audioSource;
    private float currentTime = 0f;
    private float waitTime = 1f;
    private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.coindsCollected = 0;
        GameManager.currentHP = GameManager.playerMaxHp;
        GameManager.enabledShop = false;
        GameManager.canOpenShopChest = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0) {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && clicked) {
                SceneManager.LoadScene("RulesScene");
            }
        }
    }

    public void StartGameButton() {
        audioSource.Play();
        currentTime = waitTime;
        clicked = true;
    }

}
