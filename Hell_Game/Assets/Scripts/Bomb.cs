using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool stop = false;

    private Vector3 target;

    private float count = 0;

    private Ingame Manager;

    private float attack_speed;

    [SerializeField]
    private GameObject AD_Skip;
    void Start()
    {
        Manager = Ingame.Instance;

        target = Ingame.Instance.Me.transform.localPosition;
        count = 5;
        StartCoroutine(bomb_Move0());
    }


    public IEnumerator bomb_Move0()
    {
        count -= Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 15f);

        if(transform.localPosition == target)
        {
            target = Ingame.Instance.Me.transform.localPosition;
        }

        if(count <= 0)
        {
            stop = false;
            StartCoroutine(bomb_Move1());
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move0());
    }



    public IEnumerator bomb_Move1()
    {
        if(transform.localPosition == new Vector3(0,0,0))
        {
            if(!stop)
            {
                transform.localScale += new Vector3(5f, 5f, 0);

                if (transform.localScale.x > 300)
                {
                    transform.localScale += new Vector3(20f, 20f, 0);
                }
            }
            else
            {
                stop = false;
                count = 0;
                StartCoroutine(bomb_Move3());
                yield break;
            }
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,0,0), 10f);
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move1());
    }

    public IEnumerator bomb_Move3()
    {
        count += Time.deltaTime;

        if (count > 2f && count <= 2.5f)
        {
            transform.Translate(0, 0.1f, 0);
        }
        else if (count > 2.5f && count <= 3.5f)
        {
            transform.Translate(0, -0.1f, 0);
        }
        else if (count >= 4)
        {
            StartCoroutine(bomb_Move4());
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move3());
    }

    public IEnumerator bomb_Move4()
    {
        count += Time.deltaTime;
        if(transform.localScale.x > 200)
        {
            transform.localScale -= new Vector3(10, 10, 0);
        }

        if (transform.localPosition == Ingame.Instance.Spot[0].transform.localPosition)
        {
            count = 0;
            attack_speed = 0.2f;
            StartCoroutine(Attact0());
            StartCoroutine(bomb_Move5());
            yield break;
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Ingame.Instance.Spot[0].transform.localPosition, 10f);
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move4());
    }


    public IEnumerator bomb_Move5()
    {
        if (transform.localScale.x < 400)
        {
            transform.localScale += new Vector3(10, 10, 0);
        }

        if ((int)Ingame.Instance.time == 20)
        {
            transform.localPosition = new Vector3(-300, -500, 0);
        }
        if ((int)Ingame.Instance.time == 21)
        {
            transform.localPosition = new Vector3(250, 0, 0);
        }
        if ((int)Ingame.Instance.time == 22)
        {
            transform.localPosition = new Vector3(100, 500, 0);
        }
        if ((int)Ingame.Instance.time == 23)
        {
            transform.localPosition = new Vector3(0, -300, 0);
        }
        if ((int)Ingame.Instance.time == 24)
        {
            transform.localScale = new Vector3(700, 700, 0);
            transform.localPosition = new Vector3(-100, 400, 0);
        }
        if ((int)Ingame.Instance.time == 25)
        {
            transform.localScale = new Vector3(100, 100, 0);
            transform.localPosition = new Vector3(500, -500, 0);
        }
        if ((int)Ingame.Instance.time == 26)
        {
            transform.localScale = new Vector3(1000, 1000, 0);
            transform.localPosition = new Vector3(0, 0, 0);
        }
        if ((int)Ingame.Instance.time == 29)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            attack_speed = 0.1f;
            StartCoroutine(Attact1());
            StartCoroutine(bomb_Move6());
            AD_Skip.SetActive(true);
            yield break;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Ingame.Instance.Me.transform.localPosition, 5f);


        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move5());
    }



    public IEnumerator bomb_Move6()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 900, 0), 5f);

        if(transform.localScale.x > 100)
        {
            transform.localScale -= new Vector3(10, 10, 0);
        }

        if (transform.localPosition == Vector3.MoveTowards(transform.localPosition, new Vector3(0, 900, 0), 5f))
        {
            AD_Skip.GetComponent<AD_Skip>().Initialize();
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move6());
    }





    IEnumerator Attact0()
    {
        GameObject ad = Manager.object_Pooling.AD_OP.Dequeue();

        Object_Dequeue(ad, Manager.AD_Parent, "AD", transform.localPosition);

        ad.GetComponent<AD>().Enemy = Manager.Me.transform.localPosition;

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        if ((int)Ingame.Instance.time == 30)
        {
            yield break;
        }
        yield return new WaitForSeconds(attack_speed);

        StartCoroutine(Attact0());
    }

    IEnumerator Attact1()
    {
        GameObject ad = Manager.object_Pooling.AD_OP.Dequeue();

        Object_Dequeue(ad, Manager.AD_Parent, "AD", transform.localPosition);

        ad.GetComponent<AD>().Enemy = new Vector3(UnityEngine.Random.Range(-1000, 1000), UnityEngine.Random.Range(-2000, 2000), 0);

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        if((int)Ingame.Instance.time == 30)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.05f);

        StartCoroutine(Attact1());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Wall")
        {
            stop = true;
        }
    }





    private void Object_Dequeue(GameObject prefab, GameObject Parents, string Name, Vector3 position)
    {
        prefab.SetActive(true);
        prefab.name = Name;
        prefab.transform.SetParent(Parents.transform, false);
        prefab.transform.localPosition = position;
    }

}
