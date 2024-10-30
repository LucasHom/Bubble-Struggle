using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed = 2f;

    public static bool IsFired = false;
    // Start is called before the first frame update
    void Start()
    {
        IsFired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsFired = true;
        }
        if (IsFired)
        {
            transform.localScale = transform.localScale + Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            transform.position = player.position;
            //localscale transforms elements parents
            transform.localScale = new Vector3(1f, 0f, 1f);
        }
    }
}
