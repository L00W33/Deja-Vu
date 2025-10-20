using System;
using UnityEngine;

public class InteractableDialogue : MonoBehaviour
{
    public Collider2D radius;
    public GameObject spaceGraphic;
    public string[] lines;

    Dialogue dialogueBox;
    bool canInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueBox = Resources.FindObjectsOfTypeAll<Dialogue>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.Space) && !(dialogueBox.gameObject.activeSelf))
        {
            dialogueBox.gameObject.SetActive(true);
            dialogueBox.NewDialogue(lines);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spaceGraphic.SetActive(true);

            if (!(dialogueBox.gameObject.activeSelf))
                canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spaceGraphic.SetActive(false);
            canInteract = false;
        }   
    }
}
