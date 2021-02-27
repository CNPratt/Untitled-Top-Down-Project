using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class HGlobScript : MonoBehaviour
{
    public int thisFull;
    public int thisThreeQ;
    public int thisHalf;
    public int thisFourth;

    public Image display;

    public Sprite fullSprite;
    public Sprite threefourthSprite;
    public Sprite halfSprite;
    public Sprite fourthSprite;
    public Sprite emptySprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInventoryScript.currentHealth >= thisFull)
        {
            display.sprite = fullSprite;
        }

        if (PlayerInventoryScript.currentHealth == thisThreeQ)
        {
            display.sprite = threefourthSprite;
        }

        if (PlayerInventoryScript.currentHealth == thisHalf)
        {
            display.sprite = halfSprite;
        }

        if (PlayerInventoryScript.currentHealth == thisFourth)
        {
            display.sprite = fourthSprite;
        }

        if (PlayerInventoryScript.currentHealth < thisFourth)
        {
            display.sprite = emptySprite;
        }
    }
}
