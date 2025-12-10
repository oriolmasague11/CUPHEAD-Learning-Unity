using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class CupheadScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Vector2 _dir;
    public float speed;
    public float jump_speed;
    [SerializeField]
    private float _rayDistance;
    public LayerMask layer;

    private Animator _animator;

    public GameObject bullet;
    public GameObject fp;
    public float bullet_speed;

    public float shoot;
    private float timer;
    bool shoot_disp;

    public GameObject explosion;

    private bool _isPaused;
    public GameObject menu;

    public AudioClip clipShoot;
    public AudioClip clipJump;
    public AudioClip clipCoin;
    private AudioSource _audioSource;

    private void Awake()
    {
        GameManagerCH.instance.cuphead = this; 
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timer = 0f;
        shoot_disp = true; 
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                shoot_disp = true; 
            }
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_dir.x * speed, _rb.velocity.y);

        _animator.SetFloat("speed", _rb.velocity.x);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, -_rayDistance, 0f), Color.red, 1f);   //el temps va en segons     // log("") per treure chivatos per pantalla
        if (Physics2D.Raycast(gameObject.transform.position, Vector2.down, _rayDistance, layer)) 
        {
            //_rb.velocity += Vector2.up * _dir.y * jump_speed;
            _rb.velocity = new Vector2(_rb.velocity.x, _dir.y * jump_speed);
            if(_dir.y == 1)
            {
                //audio de jump
                _audioSource.pitch = Random.Range(0.8f, 1.2f);
                _audioSource.PlayOneShot(clipJump);
            }
        }
        _animator.SetFloat("speedY", _rb.velocity.y);
        if (_rb.velocity.x > 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (_rb.velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //shoot
        if (shoot == 1)
        {
            if(shoot_disp) { Shoot(); shoot_disp=false; timer = 0.3f; }
            _animator.SetBool("shooting", true);
            explosion.GetComponent<Animator>().SetBool("shooting", true); 
        }
        if (shoot == 0)
        {
            _animator.SetBool("shooting", false);
            explosion.GetComponent<Animator>().SetBool("shooting", false);
        }
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        _dir.x = ctx.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        _dir.y = ctx.ReadValue<float>();
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        shoot = ctx.ReadValue<float>();
    }

    private void Shoot()  
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(clipShoot); 
        GameObject b = Instantiate(bullet, fp.transform.position, Quaternion.identity);   //coge el objecto original i crea un clon. esto devuelve la referencia del objeto clonado
        b.GetComponent<Rigidbody2D>().velocity = new Vector2((fp.transform.position.x - transform.position.x) * bullet_speed, 0f); //por defecto coge el del player
        if ( (fp.transform.position.x - transform.position.x) > 0f)
        {
            b.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if ( (fp.transform.position.x - transform.position.x) < 0f)
        {
            b.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        Destroy(b, 0.8f);
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) Pause();
    }

    public void Pause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;    //pauso el juego 
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1f;    //pongo el juego en ejecucion   //velocidad de ejecucion. si ho poso a 0.5 anira a camarea lenta
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("captured");
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(clipCoin);
        }
        if(collision.gameObject.tag == "door")
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(2);  //el seguent nivell
        }
        if (collision.gameObject.tag == "meteor")
        {
            _animator.SetTrigger("touched");
            _rb.velocity = new Vector2(0f, 50f);
            //Destroy(collision.gameObject);
        }
    }
}
