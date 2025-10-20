using UnityEngine;

public class OverPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    Dialogue dialogueBox;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = Resources.FindObjectsOfTypeAll<Dialogue>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        //player can move freely, up, down, left and right
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!(dialogueBox.gameObject.activeSelf))
            rb.MovePosition(rb.position + movement * Time.deltaTime * speed);
    }
}
