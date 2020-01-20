using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD_Skip : MonoBehaviour
{
    private Ingame Manager;
    private Vector3 target;

    private float attack_speed;
    public void Initialize()
    {
        Manager = Ingame.Instance;
        target = Ingame.Instance.Me.transform.localPosition;
        attack_speed = 0.01f;
        StartCoroutine(Attact0());
    }


    public IEnumerator bomb_Move0()
    {
        transform.localScale += new Vector3(10, 5,0);

        if (transform.localScale.x > 1100)
        {
            StartCoroutine(bomb_Move1());
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move0());
    }

    public IEnumerator bomb_Move1()
    {
        transform.Translate(0,-0.05f, 0);

        if (transform.localPosition.y < -500)
        {
            transform.Translate(0, -0.5f, 0);
        }

        if(transform.localPosition.y < -900)
        {
            yield return new WaitForSeconds(2f);
            target = Manager.Spot[1].transform.localPosition;

            StartCoroutine(bomb_Move2());
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move1());
    }

    public IEnumerator bomb_Move2()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target , 30f);

        if (transform.localPosition == target)
        {
            yield break;
        }

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(bomb_Move2());
    }



    IEnumerator Attact0()
    {
        GameObject ad = Manager.object_Pooling.AD_OP.Dequeue();

        Object_Dequeue(ad, Manager.AD_Parent, "AD", transform.localPosition);

        ad.GetComponent<AD>().Enemy = new Vector3(UnityEngine.Random.Range(-1000, 1000), UnityEngine.Random.Range(-2000, 2000), 0);

        if (!this.gameObject.activeSelf)
        {
            yield break;
        }

        if ((int)Ingame.Instance.time == 35)
        {
            StartCoroutine(bomb_Move0());
            yield break;
        }
        yield return new WaitForSeconds(attack_speed);

        StartCoroutine(Attact0());
    }




    private void Object_Dequeue(GameObject prefab, GameObject Parents, string Name, Vector3 position)
    {
        prefab.SetActive(true);
        prefab.name = Name;
        prefab.transform.SetParent(Parents.transform, false);
        prefab.transform.localPosition = position;
    }
}
