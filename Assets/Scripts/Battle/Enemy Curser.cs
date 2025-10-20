using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyCurser : MonoBehaviour
{
    float scrollSpeed;
    Rigidbody2D rb;
    float t;
    float timer;
    

    BattleSystem System1;
    Vector2 startPos;
    Vector2 endPos;

    public int numCursers;
    public float TimeToReachTarget;
    public GameObject Target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        rb = GetComponent<Rigidbody2D>();
        System1 = FindAnyObjectByType<BattleSystem> ();
        startPos = transform.position;
        endPos = Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // curser slowly lerps towards target
        timer += Time.deltaTime;
        t += Time.deltaTime / TimeToReachTarget;
        transform.position = Vector3.Lerp (startPos, endPos, t);

        // if the player presses space or fails to in timethe curser resets
        if (Input.GetKeyDown(KeyCode.Space) || timer >= TimeToReachTarget)
        {
            curserReset();
        }

        // destroys object of player looses
        if (System1.State == BattleState.Loss)
        {
            Destroy(transform.parent.gameObject);
        }

        // when the number of counters is exausted, destroy the entire scroll bar
        if (numCursers < 1)
        {
            System1.State = BattleState.APphase;
            Destroy(transform.parent.gameObject);
        }
    }

    // resets curser position and decrements it
    void curserReset()
    {
        // damages player when they press space
        float dmgMult = Mathf.Abs((TimeToReachTarget / 2) - timer);

        // allows the user to avoid damage alltogether if close enough
        if (dmgMult <= 0.1)
            dmgMult = 0;
        else
            dmgMult *= 2;

        System1.PlayerTakesDamage(dmgMult);

        // resets attributes so it can loop
        timer = 0;
        t = 0;
        transform.position = startPos;

        numCursers--;
    }
}
