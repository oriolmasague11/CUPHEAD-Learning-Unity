using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
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
    private float timer;
    public CMShaking cm;
    private Animator _animator;

    public GameObject bullet;
    public GameObject fp;
    public float bullet_speed;

    private bool _isPaused;
    public GameObject menu;

    public AudioClip clip;
    private AudioSource _audioSource; 

    private void Awake()
    {
        GameManager.Instance.player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timer = 0f;
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_dir.x * speed, _rb.velocity.y);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, -_rayDistance, 0f), Color.red, 1f);
        _animator.SetFloat("speed", _rb.velocity.x);
        if (Physics2D.Raycast(gameObject.transform.position, Vector2.down, _rayDistance, layer))
        {
            _rb.velocity += Vector2.up * _dir.y * jump_speed;
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
    }
    public void OnMovement(InputAction.CallbackContext ctx)
    {
        _dir.x = ctx.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        _dir.y = ctx.ReadValue<float>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "star")
        {
            Destroy(collision.gameObject);  //lultim parametre q podem afegir es el temps q trigui fins a destruirse //me cargo el objeto con el que me he xocado     // o .Equals("star")
            //timer = 0.5f;
            cm.ScreenShake(5f, 2f, 1f);
        }
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) Shoot();   

    }

    private void Shoot()
    {
        if (!_isPaused)
        {
            GameObject b = Instantiate(bullet, fp.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2((fp.transform.position.x - transform.position.x) * bullet_speed, 0f);
            Destroy(b, 2f);
            //_audioSource.pitch = Random.Range(0.8f, 1.2f);
            //_audioSource.PlayOneShot(clip, 1f);
        }

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
}
