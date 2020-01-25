using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_Skip : MonoBehaviour
{
    private Ingame Manager;
    private Vector3 target;


    public GameObject Bomb;
    public void Initialize()
    {
        Manager = Ingame.Instance;
        target = Ingame.Instance.Me.transform.localPosition;
        StartCoroutine(Attact0(35, 0.01f));
        StartCoroutine(bomb_Move0());
    }


    public IEnumerator bomb_Move0()
    {
        transform.localScale += new Vector3(10, 5,0);

        if (transform.localScale.x > 1100)
        {
            StartCoroutine(bomb_Move1());
            yield break;
        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move0());
    }

    public IEnumerator bomb_Move1()
    {
        transform.Translate(0,-0.1f, 0);

        if (transform.localPosition.y < -500)
        {
            transform.Translate(0, -1f, 0);
        }

        if(transform.localPosition.y < -900)
        {
            yield return new WaitForSeconds(2f);
            target = Manager.Spot[3].transform.localPosition;

            StartCoroutine(bomb_Move2());
            yield break;
        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move1());
    }

    public IEnumerator bomb_Move2()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target , 50f);

        if (transform.localPosition == target)
        {
            yield return new WaitForSeconds(1f);

            target = new Vector3(0, 0, 0);
            StartCoroutine(bomb_Move3());
            yield break;
        }

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move2());
    }

    public IEnumerator bomb_Move3()
    {
        transform.localScale -= new Vector3(20, 10, 0);

        if (transform.localScale.x < 500)
        {
            Debug.LogError("ATTACK");
            StartCoroutine(Attact0(60, 0.1f));
            StartCoroutine(Bomb.GetComponent<Bomb>().Follow_Move(70));

            yield return new WaitForSeconds(5f);
            StartCoroutine(Bomb.GetComponent<Bomb>().Random_Attack(70, 0.1f));
            yield return new WaitForSeconds(26f);
            StartCoroutine(bomb_Move4());
            yield break;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 20f);

        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move3());
    }


    public IEnumerator bomb_Move4()
    {
        transform.localScale -= new Vector3(10, 5, 0);

        if (this.transform.localScale.x < 10)
        {
            this.gameObject.SetActive(false);
            yield break;
        }
        yield return new WaitForSeconds(0.005f);
        StartCoroutine(bomb_Move4());
    }


    IEnumerator Attact0(int finsh_Time, float speed)
    {

        GameObject ad = Manager.object_Pooling.AD_OP.Dequeue();

        Object_Dequeue(ad, Manager.AD_Parent, "AD", transform.localPosition);

        ad.GetComponent<AD>().Enemy = new Vector3(UnityEngine.Random.Range(-1000, 1000), UnityEngine.Random.Range(-2000, 2000), 0);

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        if ((int)Ingame.Instance.time == finsh_Time)
        {
            yield break;
        }
        yield return new WaitForSeconds(speed);

        StartCoroutine(Attact0(finsh_Time, speed));
    }

    

    private void Object_Dequeue(GameObject prefab, GameObject Parents, string Name, Vector3 position)
    {
        prefab.SetActive(true);
        prefab.name = Name;
        prefab.transform.SetParent(Parents.transform, false);
        prefab.transform.localPosition = position;
    }
}
