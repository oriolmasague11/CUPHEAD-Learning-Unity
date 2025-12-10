using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public bool canMove;
    public float speed;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        target = GameManager.Instance.player.transform;
    }

    private void FixedUpdate()
    {
        if (canMove) transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }
}
