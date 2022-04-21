using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpForce = 10;
    public float Velocity = 10;
    public GameObject BulletPrefab;
    public Object PuntajeManager;

    public int BronceCoin = 0;
    public int SilverCoin = 0;
    public int GoldCoin = 0;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Animator _animator;

    private static readonly string ANIMATOR_STATE = "State";
    private static readonly int ANIMATION_IDLE = 0;
    private static readonly int ANIMATION_RUN = 1;
    private static readonly int ANIMATION_JUMP = 2;
    private static readonly int ANIMATION_SLIDE = 3;

    private static readonly int RIGHT = 1;
    private static readonly int LEFT = -1;
    public int puntaje = 0;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocidadActualY = _rb.velocity.y;

        _rb.velocity = new Vector2(0, velocidadActualY);
        ChangeAnimation(ANIMATION_IDLE);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Desplazarse(RIGHT);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Desplazarse(LEFT);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Deslizarse();

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            ChangeAnimation(ANIMATION_JUMP);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Disparar();
        }
    }

    private void Deslizarse()
    {
        ChangeAnimation(ANIMATION_SLIDE);

    }

    private void Desplazarse(int position)
    {
        _rb.velocity = new Vector2(Velocity * position, _rb.velocity.y);
        _spriteRenderer.flipX = position == LEFT;
        ChangeAnimation(ANIMATION_RUN);
    }

    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE, animation);
    }

    private void Disparar()
    {
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var bulletGO = Instantiate(BulletPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
        var controller = bulletGO.GetComponent<BulletController>();
        controller.SetPlayerController(this);
        if (_spriteRenderer.flipX)
        {
            controller.Velocidad = controller.Velocidad * -1;
        }
    }

    public void IncrementarPuntaje(int puntajeAux)
    {
        puntaje += puntajeAux;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Coin10")
        {
            BronceCoin += 1;
            IncrementarPuntaje(10);
            Destroy(other.gameObject);
        }
        if (tag == "Coin20")
        {
            SilverCoin += 1;
            IncrementarPuntaje(20);
            Destroy(other.gameObject);
        }
        if (tag == "Coin30")
        {
            GoldCoin += 1;
            IncrementarPuntaje(30);
            Destroy(other.gameObject);
        }
    }
}
