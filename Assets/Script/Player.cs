using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public Projectile web;
    private bool _webActive;
    public LogicScript logic;
    private bool playerIsAlive = true;
    


    // Awake
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // Invoke Animation
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }


    // Setup Animation
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private int _animationIndex;

    private void AnimateSprite()
    {
        _animationIndex++;
        if (_animationIndex >= this.animationSprites.Length)
        {
            _animationIndex = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationIndex];
    }

    

    // Update is called once per frame
    void Update()
    {
        if(playerIsAlive)
        {
            Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
            if (this.transform.position.x >= (leftEdge.x + 1.0f))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    this.transform.position += Vector3.left * this.speed * Time.deltaTime;
                }
            }
            if (this.transform.position.x <= (rightEdge.x - 1.0f))
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    this.transform.position += Vector3.right * this.speed * Time.deltaTime;
                }
            }



            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (!_webActive)
        {
            Projectile projectile = Instantiate(this.web, this.transform.position, Quaternion.identity);
            projectile.destroyed += WebDestroyed;
            _webActive = true;
        }
    }

    private void WebDestroyed()
    {
        _webActive = false;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Poop"))
        {
            logic.LoseLife(1);
            if (logic.playerLife<=0)
            {
                logic.GameOver();
                
                playerIsAlive = false;
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Reward"))
        {
            logic.Score(500);
        }
    }
    
}
