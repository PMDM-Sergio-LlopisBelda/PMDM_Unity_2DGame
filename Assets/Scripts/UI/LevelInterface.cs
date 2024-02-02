using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInterface : MonoBehaviour
{
    public Text coinText;
    public PlayerController playerController;
    public SwordAttack swordAttack;
    private bool movesLeft = false;
    private bool movesRight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = GameManager.coindsCollected.ToString();
    }

    public void SpawnParticles(Transform particleTransform, ParticleSystem particle) {
        Destroy(Instantiate(particle, particleTransform.position, Quaternion.identity), 2.0f);
    }

    public void MoveLeftButtonDown() {
        print("ANAL SEX");
        movesLeft = true;
        StartCoroutine(MoveLeft());
    }

    public void MoveLeftButtonUp() {
        playerController.Speed = 0;
        playerController.animator.SetFloat("Speed", 0);
        movesLeft = false;
    }

    public void MoveRightButtonDown() {
        movesRight = true;
        StartCoroutine(MoveRight());
    }

    public void MoveRightButtonUp() {
        playerController.Speed = 0;
        playerController.animator.SetFloat("Speed", 0);
        movesRight = false;
    }

    IEnumerator MoveLeft() {
        while (movesLeft) {
            playerController.MoveLeft();
            yield return null;
        }
    }

    IEnumerator MoveRight() {
        while (movesRight) {
            playerController.MoveRight();
            yield return null;
        }
    }

    public void AttackButton() {
        swordAttack.AttackByButton();
    }

    public void JumpButton() {
        playerController.JumpByButton();
    }

    
}
