using UnityEngine;
using UnityEngine.UI;

public class HpControllerEnemy : MonoBehaviour
{
    private Slider hpSlider;
    public HpManagerEnemy1 HpManagerEnemy1;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
        hpSlider.maxValue = HpManagerEnemy1.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = HpManagerEnemy1.actualHp;
    }
}
