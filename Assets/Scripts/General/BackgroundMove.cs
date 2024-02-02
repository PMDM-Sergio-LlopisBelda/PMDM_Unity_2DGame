using UnityEngine;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{

    public RawImage backgroundImage1;
    public float backgroundX1;
    public float backgroundY1;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        backgroundImage1.uvRect = new Rect(backgroundImage1.uvRect.position + new Vector2(backgroundX1,backgroundY1) * Time.deltaTime, backgroundImage1.uvRect.size);
    }
}