using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Me : MonoBehaviour
{
    private Vector3 click_pos;

    private Vector3 target;

    private bool move = false;

    [SerializeField]
    private GameObject Direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bomb" || collision.name == "AD" || collision.name == "AD_SKIP")
        {
            Ingame.Instance.Start_Panel.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            move = true;
            //click_pos = Direction.transform.localPosition;
            click_pos = Input.mousePosition;
            StartCoroutine(Moving());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            move = false;
        }
    }

    IEnumerator Moving()
    {
        //target = click_pos + Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = Input.mousePosition - click_pos;

        Debug.Log(target);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target * 100, 10f);

        if (!move)
        {
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Moving());
    }
}
