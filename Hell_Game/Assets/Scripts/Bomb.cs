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

    private SpriteRenderer bomb_color;
    void Start()
    {
        Manager = Ingame.Instance;
        bomb_color = this.gameObject.GetComponent<SpriteRenderer>();
        target = Ingame.Instance.Me.transform.localPosition;
        count = 5;
        StartCoroutine(bomb_Move0());
    }


    public IEnumerator bomb_Move0()
    {
        count -= Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 9f);

        if (transform.localPosition == target)
        {
            target = Ingame.Instance.Me.transform.localPosition;
        }

        if(count <= 0)
        {
            stop = false;
            StartCoroutine(bomb_Move1());
            yield break;
        }

        yield return new WaitForSeconds(0.005f);
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

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move1());
    }

    public IEnumerator bomb_Move3()
    {
        count += Time.deltaTime;

        if (count > 2f && count <= 3f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Ingame.Instance.Spot[1].transform.localPosition, 15f);
        }
        else if (count > 3f && count <= 5f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Ingame.Instance.Spot[0].transform.localPosition, 15f);
        }
        else if (count >= 5.2f)
        {
            StartCoroutine(bomb_Move4());
            yield break;
        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move3());
    }

    public IEnumerator bomb_Move4()
    {
        if(transform.localScale.x > 200)
        {
            transform.localScale -= new Vector3(20, 20, 0);
        }

        if (transform.localPosition == Ingame.Instance.Spot[2].transform.localPosition)
        {
            count = 0;
            attack_speed = 0.2f;
            StartCoroutine(Attact0());
            StartCoroutine(bomb_Move5());
            yield break;
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Ingame.Instance.Spot[2].transform.localPosition, 10f);
        }

        yield return new WaitForSeconds(0.005f);
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
            transform.localPosition = new Vector3(450, -500, 0);
        }
        if ((int)Ingame.Instance.time == 26)
        {
            transform.localScale = new Vector3(1000, 1000, 0);
            transform.localPosition = new Vector3(0, 0, 0);
        }
        if ((int)Ingame.Instance.time == 28)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            attack_speed = 0.1f;
            StartCoroutine(Attact1());
            StartCoroutine(bomb_Move6());
            AD_Skip.SetActive(true);
            yield break;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Ingame.Instance.Me.transform.localPosition, 5f);


        yield return new WaitForSeconds(0.005f);
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

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move6());
    }

    public IEnumerator bomb_Move7()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 0), 20f);

        if (transform.localScale.x < 200 )
        {
            transform.localScale += new Vector3(1, 1, 0);
        }

        bomb_color.color += new Color(1 / 255f, 0 / 255f, 0 / 255f);

        if (Manager.time > 75)
        {
            StartCoroutine(Attact_AD2(100, 0.5f));
            yield return new WaitForSeconds(5f);
            StartCoroutine(Random_Attack(100, 0.1f));
            StartCoroutine(Finish_Move(100));
            yield break;
        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move7());
    }

    public IEnumerator Follow_Move(int finish_Time)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 7f);

        if (transform.localPosition == target)
        {
            target = Ingame.Instance.Me.transform.localPosition;
        }

        if (Manager.time > finish_Time)
        {
            StartCoroutine(bomb_Move7());
            yield break;

        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(Follow_Move(finish_Time));
    }

    public IEnumerator Finish_Move(int finish_Time)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 50f);

        if (transform.localPosition == target)
        {
            target = new Vector3(UnityEngine.Random.Range(-500, 500), UnityEngine.Random.Range(-1000, 1000), 0);
        }

        if (Manager.time > finish_Time)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(Finish_Move(finish_Time));
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

    public IEnumerator Random_Attack(int finish_Time, float speed)
    {
        GameObject ad = Manager.object_Pooling.AD_OP.Dequeue();

        Object_Dequeue(ad, Manager.AD_Parent, "AD", transform.localPosition);

        ad.GetComponent<AD>().Enemy = new Vector3(UnityEngine.Random.Range(-1000, 1000), UnityEngine.Random.Range(-2000, 2000), 0);

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        if ((int)Ingame.Instance.time > finish_Time)
        {
            yield break;
        }

        yield return new WaitForSeconds(speed);

        StartCoroutine(Random_Attack(finish_Time, speed));
    }

    IEnumerator Attact_AD2(int finish_Time, float speed)
    {
        GameObject ad2 = Manager.object_Pooling.AD2_OP.Dequeue();

        Object_Dequeue(ad2, Manager.AD_Parent, "AD2", transform.localPosition);

        ad2.GetComponent<AD2>().Enemy = Manager.Me.transform.localPosition;

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        if ((int)Ingame.Instance.time > finish_Time)
        {
            yield break;
        }
        yield return new WaitForSeconds(speed);

        StartCoroutine(Attact_AD2(finish_Time, speed));
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
