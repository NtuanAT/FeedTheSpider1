using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScreen : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Action destroyed;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.x <= 12)
        {
            this.transform.localScale += this.direction * this.speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.destroyed.Invoke();
            Destroy(this.gameObject);
        }
    }
}
