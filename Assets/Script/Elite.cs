using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite : MonoBehaviour
{
    public float speed;
    public float moveRate;
    private float stoppingDistance = 0;

    private Vector3 targetPosition;

    void Start()
    {
        InvokeRepeating(nameof(GetRandomDestination), this.moveRate, this.moveRate);
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget > stoppingDistance)
        {
            float distanceToMove = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, distanceToMove);
        }

    }

    public void GetRandomDestination()
    {
        targetPosition = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.value, UnityEngine.Random.Range(0.25f,1f), 0));
        targetPosition.z = 0;
    }
    public Action killed;
    public Projectile Coin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Web"))
        {
            this.killed.Invoke();
            Instantiate(this.Coin, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
