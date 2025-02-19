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
            transform.position = citizen.transform.position + new Vector3(0, 1f);
            FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();
            fixedJoint.connectedBody = citizen.GetComponent<Rigidbody2D>();
        }
        else
        {
            transform.position = activeUmbrellas.Peek().transform.position + new Vector3(0, 0.6f);
            HingeJoint2D hinge = gameObject.AddComponent<HingeJoint2D>();
            hinge.connectedBody = citizen.GetComponent<Rigidbody2D>();

            JointAngleLimits2D limits = hinge.limits;
            limits.min = -5f;
            limits.max = 5f;
            hinge.limits = limits;
            hinge.useLimits = true;

        }

        activeUmbrellas.Push(this);
        foreach (var item in activeUmbrellas)
        {
            Debug.Log(item);
        }
    }

   
        

}
