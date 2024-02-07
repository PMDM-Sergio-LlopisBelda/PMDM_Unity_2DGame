using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    private Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
        hpSlider.maxValue = GameManager.playerMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = GameManager.currentHP;
    }
}
