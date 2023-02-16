using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class QueenHornet : MonoBehaviour
{
    // Setup Animation
    public Sprite[] animationSprites;
    public float animationTime = 0.1f;

    private SpriteRenderer _spriteRenderer;
    private int _animationIndex;

    private Vector3 destinition;
    private float moveRate;
    private float moveSpeed;

    LogicScript logic;

    private int HitPoint;
    private float fireRate;
    private Camera cam;

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
        cam = Camera.main;
        this.HitPoint = 10;
        this.fireRate = 2.2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        this.moveSpeed = 0.5f;
        moveRate = 5.0f;
        Grow();
        QueenMove();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = destinition - this.transform.position;
        this.transform.position += moveSpeed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Web"))
        {
            this.HitPoint -= 1;
            CancelInvoke(nameof(PoopDrop));
            QueenMove();
            Grow();
            var die = DieYet();
            if (die)
            {
                Destroy(this.gameObject);
                logic.WinGame();
            }
        }
    }

    #region Poop
    public Projectile poopPrefab;
    private void PoopDrop()
    {
        Instantiate(this.poopPrefab, this.transform.position, Quaternion.identity);
    }
    private void Grow()
    {
        Rage();
        InvokeRepeating(nameof(PoopDrop), 0.0f, this.fireRate);
    }
    #endregion

    #region HP

    private bool DieYet()
    {
        return this.HitPoint <= 0;
    }
    public void Rage()
    {
        fireRate -= 0.19f;
        this.moveSpeed += 0.1f;
        this.moveRate -= 0.45f;
    }
    #endregion

    #region Move
    private Vector3 GetRandomPosition()
    {
        var randomposition = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, 0));
        randomposition.z = 0;
        return randomposition;
    }
    private void Move()
    {
        this.destinition = this.GetRandomPosition();
    }
    private void QueenMove()
    {
        CancelInvoke(nameof(Move));
        InvokeRepeating(nameof(Move),0, this.moveRate);
    }
    #endregion
}
