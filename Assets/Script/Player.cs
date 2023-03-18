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
    //public GameObject addHeart;
    private bool _webActive;
    
    private bool playerIsAlive = true;
    [HideInInspector]
    public AudioManager audioManager;
    [HideInInspector]
    public LogicScript logic;


    // Awake
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
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
            if (Input.GetKeyDown(KeyCode.Space))
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
            audioManager.Play("Hurt");
            if (logic.playerLife<=0)
            {
                logic.GameOver();
                
                playerIsAlive = false;
                this.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Reward"))
        {
            logic.Score(50);
            audioManager.Play("Score");
            if (logic.playerScore % 1000 == 0)
            {
                logic.AddLife(1);
                //Instantiate(this.addHeart, this.transform.position, Quaternion.identity);
            }
        }
    }
    
}
