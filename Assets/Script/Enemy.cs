
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action killed;
    public Projectile Coin;
    public GameObject DeathAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Web"))
        {
            this.killed.Invoke();
            Instantiate(this.DeathAnim, this.transform.position,Quaternion.identity);
            Instantiate(this.Coin, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
