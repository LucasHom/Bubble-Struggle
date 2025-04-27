using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingNotification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Notify());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Notify()
    {
        StartCoroutine(EmphasizeObject.Emphasize(gameObject, Vector3.one * 0.7f, 1.1f));
        yield return new WaitForSeconds(2f);
        if (gameObject.transform.parent != null)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
            
    }
}
