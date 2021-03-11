using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class NPCTalk : DialogueBaseScript
{
    public GameObject player;

//    public Image dBox;
//    public Text dText;
//    public List<string> toPrint;
//    public Coroutine thisDialogue;

    public string sent1;
    public string sent2;
    public string sent3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        if (sent1 != null)
        {
            toPrint.Add(sent1);
        }

        if (sent2 != null)
        {
            toPrint.Add(sent2);
        }

        if (sent3 != null)
        {
            toPrint.Add(sent3);
        }
    }

    // Update is called once per frame
    void Update()
    {
 //       if (Vector2.Distance(transform.position, player.transform.position) < .5f && Input.GetKeyDown(KeyCode.I) && thisDialogue == null)
 //       {
 //           //          Debug.Log("called");
 //
 //           thisDialogue = StartCoroutine("StartDialogue");
 //       }

        if(thisDialogue != null && Vector2.Distance(transform.position, player.transform.position) > .6f)
        {
            thisDialogue = null;
            StopAllCoroutines();
            PlayerController.isTalking = false;

            dBox.gameObject.SetActive(false);
            dText.gameObject.SetActive(false);
        }

    }
}
