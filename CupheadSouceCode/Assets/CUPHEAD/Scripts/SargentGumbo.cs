using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SargentGumbo : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    public float dir;
    public float rot;
    private Animator _animator;
    private float speedcopy;
    public int lives;
    private float timer;

    public Material per_defecte;
    public Material fire; 


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        dir = -1f;
        rot = 0f;
        _animator = GetComponent<Animator>();
        timer = 0f;
        lives = 3; 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().material = per_defecte;
            }
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(dir * speed, 0f);
        transform.eulerAngles = new Vector3(0, rot, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "box")
        {
            _animator.SetTrigger("collision");
        }*/

        if (collision.gameObject.tag == "cuphead")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 50f);
            collision.gameObject.GetComponent<Animator>().SetTrigger("touched");
        }
        else
        {
            _animator.SetTrigger("collision");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("finish", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //Destroy(collision.gameObject);
            --lives;
            if (lives == 0) _animator.SetTrigger("death");
            timer = 0.2f; 
            gameObject.GetComponentInChildren<SpriteRenderer>().material = fire; 
        }
    }

    public void stop()
    {
        speedcopy = speed;
        speed = 0f; 
    }

    public void continuar()
    {
        speed = speedcopy;
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

    public void final()
    {
        Destroy(gameObject);
    }
}
