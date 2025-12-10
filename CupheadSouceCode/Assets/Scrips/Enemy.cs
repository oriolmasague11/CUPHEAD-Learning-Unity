using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    public float dir;
    public float rot;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        dir = 1f;
        rot = 180f; 
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(dir * speed, 0f);
        transform.eulerAngles = new Vector3(0, rot, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "box")
        {
            if (rot == 180f)
            {
                rot = 0f;
                dir = -1f;
            }
            else
            {
                rot = 180f;
                dir = 1f;
            }
        }
    }
}
