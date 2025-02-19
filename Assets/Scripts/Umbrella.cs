using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    private static Stack<Umbrella> activeUmbrellas = new Stack<Umbrella>();

    [SerializeField] private CitizenManager citizen;


    // Start is called before the first frame update
    void Start()
    {
        citizen = FindObjectOfType<CitizenManager>();

        if (activeUmbrellas.Count == 0)
        {
            transform.position = citizen.transform.position + new Vector3(0, 0.5f);
            FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();
            fixedJoint.connectedBody = citizen.GetComponent<Rigidbody2D>();
        }
        else
        {
            transform.position = activeUmbrellas.Peek().transform.position + new Vector3(0, 0.95f);
            HingeJoint2D hinge = gameObject.AddComponent<HingeJoint2D>();
            hinge.connectedBody = activeUmbrellas.Peek().GetComponent<Rigidbody2D>();

            JointAngleLimits2D limits = hinge.limits;
            limits.min = -5f;
            limits.max = 5f;
            hinge.limits = limits;
            hinge.useLimits = true;

        }

        activeUmbrellas.Push(this);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "BallGuard")
        {
            //Transform guardedBall = col.gameObject.transform.parent;
            //guardedBall.GetComponent<HitGuard>().ActivateHitGuardPS();
            //Destroy(transform.parent.gameObject);
        }
        if (col.tag == "Ball")
        {
            col.GetComponent<Ball>().Split();
            //Destroy(transform.parent.gameObject);
        }
        if (col.tag == "SupportBall")
        {
            //col.GetComponent<SupportBall>().Grow();
            //Destroy(transform.parent.gameObject);
        }
        if (col.gameObject.tag == "Wall")
        {
            //Special wall particlees?
            //Destroy(transform.parent.gameObject);
        }
        if (col.gameObject.tag == "Ground")
        {
            //Destroy(transform.parent.gameObject);
        }

    }



}
