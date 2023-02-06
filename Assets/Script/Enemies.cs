using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemies : MonoBehaviour
{
    // Enemies grid
    public int rows = 5;
    public int columns = 11;
    public float space = 2.0f;
    public AnimationCurve speed;
    // Enemy Type Array
    public Enemy[] enemies;
    
    // Poop Drop Attribute
    public float dropRate = 1.0f;
    public Projectile poopPrefab;

    // Calculate Enemies
    public int total => this.rows * this.columns;
    public int amountKilled { get; private set; }
    public int amountRemaining => this.total - amountKilled;    
    public float percentKilled => (float) this.amountKilled / (float)this.total;
    
    // Awake
    private void Awake()
    {
        // Spawn Enemies into the Grid
        for (int row = 0; row < this.rows; row++)
        {
            float width = space * (this.columns - 1);
            float height = space * (this.rows - 1);
            Vector2 centering = new(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + row * space, 0.0f);
            for (int column = 0; column < this.columns; column++)
            {
                Enemy enemy = Instantiate(this.enemies[row], this.transform);
                enemy.killed += EnemyKilled;
                Vector3 position = rowPosition;
                position.x += column * space;
                enemy.transform.localPosition = position;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Drop Poop Invoked
        InvokeRepeating(nameof(PoopDrop), this.dropRate, this.dropRate);
    }

    private Vector3 _direction = Vector3.right;
    // Update is called once per frame
    void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in this.transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (_direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }
            else if (_direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }
    // Enemies movement
    void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }
    // Drop Poop 
    private void PoopDrop()
    {
        foreach(Transform enemy in this.transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }

            if(Random.value < (1.0f/(float)this.amountRemaining))
            {
                Instantiate(this.poopPrefab, enemy.position, Quaternion.identity);
                break;

            }
        }
    }
    // Enemy is killed
    private void EnemyKilled()
    {
        this.amountKilled++;
        if(this.amountKilled >= this.total)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
