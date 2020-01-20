using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD : MonoBehaviour
{
    private Vector3 enemy;
    public Vector3 Enemy
    {
        get
        {
            return enemy;
        }
        set
        {
            enemy = value;
        }
    }
    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, enemy, 20f);
        if (transform.localPosition == enemy)
        {
            Delete_Shot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Me" || collision.name == "Wall")
        {
            Delete_Shot();
        }
    }

    private void Delete_Shot()
    {
        Ingame.Instance.object_Pooling.AD_OP.Enqueue(this.gameObject);
        this.transform.SetParent(Ingame.Instance.object_Pooling.OP_Parents.transform, false);
        this.gameObject.SetActive(false);
    }
}
