using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public float warningTime = 5f;
    private float _timeElapse = 0f;

    // Update is called once per frame
    void Update()
    {
        _timeElapse += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (_timeElapse > warningTime)
        {
            this.gameObject.SetActive(false);
        }
    }
}
