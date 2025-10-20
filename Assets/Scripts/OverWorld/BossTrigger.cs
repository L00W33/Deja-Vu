using UnityEngine;
using UnityEngine.WSA;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;
using UnityEngine.SceneManagement;

public class BossTrigger : MonoBehaviour
{

    Dialogue dialogueBox;
    bool activated;
    bool startFight;

    public string sceneName;
    public string[] lines;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueBox = Resources.FindObjectsOfTypeAll<Dialogue>()[0];
        activated = false;
        startFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && !(dialogueBox.gameObject.activeSelf) && !startFight)
        {
            dialogueBox.gameObject.SetActive(true);
            dialogueBox.NewDialogue(lines);
            startFight = true;
        }

        if (startFight && !(dialogueBox.gameObject.activeSelf))
            SceneManager.LoadScene(sceneName);
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            activated = true;
    }
}
