using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float Velocidad = 10;

    private Player _player;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Destroy(this.gameObject, 5);
    }

    void Update()
    {
        _rb.velocity = new Vector2(Velocidad, _rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        Debug.Log(tag);

        if (tag == "Enemy")
        {
            _player.IncrementarPuntaje(10);
            Destroy(this.gameObject);
            Destroy(other.gameObject);

        }
    }

    public void SetPlayerController(Player player)
    {
        _player = player;
    }
}
