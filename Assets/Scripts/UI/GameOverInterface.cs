using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverInterface : MonoBehaviour
{
    public AudioSource clickSound;
    private float currentTime = 0f;
    private float waitTime = 1f;
    private bool exit = false;
    private bool playAgain = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0) {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && playAgain) {
                SceneManager.LoadScene("SceneMenu");
            }
            if (currentTime <= 0 && exit) {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
        }
    }

    public void PlayAgainButton() {
        clickSound.Play();
        currentTime = waitTime;
        playAgain = true;
        exit = false;
    }

    public void ExitButton() {
        clickSound.Play();
        currentTime = waitTime;
        exit = true;
        playAgain = true;
    }
}
