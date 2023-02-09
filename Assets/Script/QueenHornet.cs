using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenHornet : MonoBehaviour
{
    // Setup Animation
    public Sprite[] animationSprites;
    public float animationTime = 0.1f;

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
        
    }
}
