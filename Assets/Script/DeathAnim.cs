using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
		{
			Destroy(this.gameObject);
		}
	}
}
