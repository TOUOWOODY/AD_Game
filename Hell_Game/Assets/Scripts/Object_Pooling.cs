using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooling : MonoBehaviour
{
    [SerializeField]
    private GameObject AD;
    [SerializeField]
    private GameObject AD2;

    public Queue<GameObject> AD_OP = null;
    public Queue<GameObject> AD2_OP = null;

    public GameObject OP_Parents;

    public void Initialized()
    {
        AD_OP = new Queue<GameObject>();
        AD2_OP = new Queue<GameObject>();

        for (int i = 0; i < 100; i++)
        {
            OP(AD, AD_OP);
        }

        for (int i = 0; i < 100; i++)
        {
            OP(AD2, AD2_OP);
        }


    }
    private void OP(GameObject Object, Queue<GameObject> queue)
    {
        GameObject op = Instantiate(Object, new Vector3(0, 0, 0), Quaternion.identity);
        queue.Enqueue(op);
        op.transform.SetParent(OP_Parents.transform, false);
        op.SetActive(false);
    }

}
