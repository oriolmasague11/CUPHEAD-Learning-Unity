using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Animator>().SetBool("finish", true);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
    public void destruir()
    {
        Destroy(gameObject, 0.1f);
    }
}
