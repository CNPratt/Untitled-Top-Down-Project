using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBaseScript : MonoBehaviour
{
    public Image dBox;
    public Text dText;
    public List<string> toPrint;

    public Coroutine thisDialogue;

    IEnumerator StartDialogue()
    {
        Debug.Log("called");

        dBox.gameObject.SetActive(true);
        dText.gameObject.SetActive(true);

        foreach (string sent in toPrint)
        {
            dText.text = sent;

            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.O));

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.O));
        }

        dBox.gameObject.SetActive(false);
        dText.gameObject.SetActive(false);

        thisDialogue = null;
        PlayerController.isTalking = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
