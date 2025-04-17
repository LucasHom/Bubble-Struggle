using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewageExplosion : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animLength);
    }
}
