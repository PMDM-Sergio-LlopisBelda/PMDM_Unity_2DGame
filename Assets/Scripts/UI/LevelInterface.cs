using UnityEngine;
using UnityEngine.UI;

public class LevelInterface : MonoBehaviour
{
    public Text coinText;
    public Text bonusDamage;
    public Text bonusHealth;
    public GameObject left;
    public GameObject right;
    public GameObject jump;
    public GameObject attack;

    void Start()
    {
        #if UNITY_EDITOR 
        {
            EnableButtons(false);
        }
        #else
        {
            EnableButtons(true);
        }
        #endif
    }
    
    void Update()
    {
        coinText.text = GameManager.coindsCollected.ToString();
        bonusDamage.text = "Damage: " + GameManager.playerDmg + " points";
        bonusHealth.text = "Max. HP: " +  GameManager.playerMaxHp + " points";
    }

    private void EnableButtons(bool enable) {
        left.GetComponent<Button>().enabled = false;
        left.SetActive(enable);

        right.GetComponent<Button>().enabled = false;
        right.SetActive(enable);

        jump.GetComponent<Button>().enabled = false;
        jump.SetActive(enable);

        attack.GetComponent<Button>().enabled = false;
        attack.SetActive(enable);
    }   

}
