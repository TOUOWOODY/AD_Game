using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD2 : MonoBehaviour
{
    private float rotation = 0;
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
    private void Start()
    {
    }
    void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, enemy, 7f);
        if (transform.localPosition == enemy)
        {
            Delete_Shot();
        }
        if (Ingame.Instance.Start_Panel.activeSelf || Ingame.Instance.Success_Panel.activeSelf)
        {
            Delete_Shot();
        }

        rotation += (Time.deltaTime * 500f);
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
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
        Ingame.Instance.object_Pooling.AD2_OP.Enqueue(this.gameObject);
        this.transform.SetParent(Ingame.Instance.object_Pooling.OP_Parents.transform, false);
        this.gameObject.SetActive(false);
    }

    public IEnumerator scale_up()
    {
        transform.localScale += new Vector3(10f, 10f, 0);

        if (transform.localScale.x > 300)
        {
            StartCoroutine(scale_down());
            yield break;
        }

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(scale_up());
    }




    IEnumerator scale_down()
    {
        transform.localScale -= new Vector3(10f, 10f, 0);

        if (transform.localScale.x < 150)
        {
            StartCoroutine(scale_up());
            yield break;
        }

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(scale_down());
    }
}
