using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverInterface : MonoBehaviour
{

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Total Coins: " + GameManager.totalCoins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgainButton() {
        SceneManager.LoadScene("SceneMenu");
    }

    public void ExitButton() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
