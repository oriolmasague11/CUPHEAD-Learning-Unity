using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FrogScript : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Vector2 _dir;
    [SerializeField]
    private float _rayDistance;
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        runSpeed = 2; 
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_dir.x * runSpeed, _rb.velocity.y);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, -_rayDistance, 0f), Color.red, 1f);
        if (Physics2D.Raycast(gameObject.transform.position, Vector2.down, _rayDistance, layer))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _dir.y * jumpSpeed);
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
}
