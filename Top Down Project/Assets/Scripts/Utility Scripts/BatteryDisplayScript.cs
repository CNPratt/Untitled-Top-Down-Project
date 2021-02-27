using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class BatteryDisplayScript : MonoBehaviour
{
    public Image display;

    public Sprite fullSprite;
    public Sprite fourthSprite;
    public Sprite thirdSprite;
    public Sprite secondSprite;
    public Sprite lastSprite;
    public Sprite emptySprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInventoryScript.currentEnergy >= 25)
        {
            display.sprite = fullSprite;
        }

        if (PlayerInventoryScript.currentEnergy == 20)
        {
            display.sprite = fourthSprite;
        }

        if (PlayerInventoryScript.currentEnergy == 15)
        {
            display.sprite = thirdSprite;
        }

        if (PlayerInventoryScript.currentEnergy == 10)
        {
            display.sprite = secondSprite;
        }

        if (PlayerInventoryScript.currentEnergy == 5)
        {
            display.sprite = lastSprite;
        }

        if (PlayerInventoryScript.currentEnergy == 0)
        {
            display.sprite = emptySprite;
        }
    }
}
