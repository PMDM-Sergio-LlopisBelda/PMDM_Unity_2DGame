using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesInterface : MonoBehaviour
{

    private float waitTime = 1f;
    private float currentTime = 0f;
    private bool clicked = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentTime > 0) {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && clicked) {
                SceneManager.LoadScene("Begin");
            }
        }
        
    }

    public void PlayButton() {
        audioSource.Play();
        currentTime = waitTime;
        clicked = true;
    }
}
