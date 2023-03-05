using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elites : MonoBehaviour
{
    // Enemies grid
    public int rows = 5;
    public int columns = 11;
    public float space = 2.0f;
    public AnimationCurve speed;
    // Enemy Type Array
    public Elite[] enemies;

    // Calculate Enemies
    public int total => this.rows * this.columns;
    public int amountKilled { get; private set; }
    public int amountRemaining => this.total - amountKilled;
    public float percentKilled => (float)this.amountKilled / (float)this.total;

    // Awake
    private void Awake()
    {
        float width = space * (this.columns - 1);
        float height = space * (this.rows - 1);
        Vector2 centering = new(-width / 2, -height / 2);
        // Spawn Enemies into the Grid
        for (int row = 0; row < this.rows; row++)
        {
            Vector3 rowPosition = new Vector3(centering.x, centering.y + row * space, 0.0f);
            for (int column = 0; column < this.columns; column++)
            {
                Elite enemy = Instantiate(this.enemies[row], this.transform);
                enemy.killed += EnemyKilled;
                Vector3 position = rowPosition;
                position.x += column * space;
                enemy.transform.localPosition = position;
            }
        }
    }

    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // Drop Poop Invoked
        InvokeRepeating(nameof(PoopDrop), this.dropRate, this.dropRate);
    }

    // Poop Drop Attribute
    public float dropRate = 1.0f;
    public Projectile poopPrefab;
    // Drop Poop 
    private void PoopDrop()
    {
        foreach (Transform enemy in this.transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1.0f / (float)this.amountRemaining))
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
        if (this.amountKilled >= this.total)
        {
            logic.WinGame();
        }
    }
}
