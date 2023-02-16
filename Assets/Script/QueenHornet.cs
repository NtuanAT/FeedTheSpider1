using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class QueenHornet : MonoBehaviour
{
    // Setup Animation
    public Sprite[] animationSprites;
    public float animationTime = 0.1f;

    private SpriteRenderer _spriteRenderer;
    private int _animationIndex;
    
    LogicScript logic;

    public int HitPoint { get; set; }
    public float fireRate;
    private float speed;
    private float moveRate;
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
        this.fireRate = 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        this.speed = 80.0f;
        moveRate = 5.0f;
        Grow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Web"))
        {
            this.HitPoint -= 1;
            CancelInvoke(nameof(PoopDrop));
            CancelInvoke(nameof(Regularmove));
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
        InvokeRepeating(nameof(Regularmove), 0.0f, this.moveRate);
    }
    #endregion

    #region HP

    private bool DieYet()
    {
        return this.HitPoint <= 0;
    }
    public void Rage()
    {
        fireRate = (float)this.HitPoint / (float)10.0f;
        this.speed += 10;
        this.moveRate -= 0.4f;
    }
    #endregion

    #region Move
    private void Move(Vector3 direction, float speed)
    {
        this.transform.position += speed * Time.deltaTime * direction;
    }
    private void Regularmove()
    {
        var position = GetRandomPosition();
        Vector3 direction = position - this.transform.position;
        Move(direction, this.speed);
    }
    private Vector3 GetRandomPosition()
    {
        var randomposition = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, 0));
        randomposition.z = 0;
        return randomposition;
    }
    #endregion
}
