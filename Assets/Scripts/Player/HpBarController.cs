using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    private Slider hpSlider;
    public HpManager hpManager;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
        hpSlider.maxValue = hpManager.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = hpManager.actualHp;
    }
}
