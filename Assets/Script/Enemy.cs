
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Awake
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //Invoke Animation
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Setup Animation
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private int _animationIndex;

    private void AnimateSprite()
    {
        _animationIndex++;
        if(_animationIndex >= this.animationSprites.Length )
        {
            _animationIndex= 0;
        }
        
        _spriteRenderer.sprite= this.animationSprites[ _animationIndex ];
    }

    public Action killed;
    public Projectile Coin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Web"))
        {
            this.killed.Invoke();
            Instantiate(this.Coin, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
