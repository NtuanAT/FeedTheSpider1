using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public Action destroyed;

    

    // Setup Animation
    public Sprite[] animationSprites;
    public float animationTime = 0.3f;

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
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyed != null)
        {
            this.destroyed.Invoke();
        }        
        Destroy(this.gameObject);
    }
}
