using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class DragonScript : MonoBehaviour
{
    public GameObject meteor;
    public GameObject fp;
    public GameObject ch; 
    public float meteor_speed;

    public Material per_defecte;
    public Material fire;
    private float timer;

    public float lives;
    private Animator _animator;
    public GameObject reff; 

    private void Start()
    {
        timer = 0f;
        _animator = GetComponent<Animator>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("finish", true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            //Destroy(collision.gameObject);
            --lives;
            if (lives == 0) { gameObject.GetComponentInChildren<SpriteRenderer>().material = per_defecte;
                _animator.SetTrigger("death"); }
            timer = 0.2f;
            gameObject.GetComponentInChildren<SpriteRenderer>().material = fire;
        }
    }

    private void Shoot()
    {
        GameObject b = Instantiate(meteor, fp.transform.position, Quaternion.identity);  
        Vector2 aux = new Vector2((ch.transform.position.x - fp.transform.position.x), 
                             (ch.transform.position.y - fp.transform.position.y)).normalized;
        b.GetComponent<Rigidbody2D>().velocity = aux * meteor_speed;  
        if ((ch.transform.position.x - fp.transform.position.x) > 0f)
        {
            b.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if ((ch.transform.position.x - fp.transform.position.x) < 0f)
        {
            b.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        Destroy(b, 10f);
    }

    public void destruir()
    {
        Destroy(gameObject);
        reff.gameObject.GetComponent<Animator>().SetTrigger("senyal");
    }
}
