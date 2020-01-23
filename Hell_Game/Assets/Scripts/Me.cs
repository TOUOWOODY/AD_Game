using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Me : MonoBehaviour
{
    private bool move = false;

    private bool up = true;
    private bool down = true;
    private bool right = true;
    private bool left = true;

    private bool up_wall = false;
    private bool down_wall = false;
    private bool right_wall = false;
    private bool left_wall = false;

    [SerializeField]
    private List<GameObject> wall = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bomb" || collision.name == "AD" || collision.name == "AD_SKIP")
        {
            //Ingame.Instance.Start_Panel.SetActive(true);
        }
        for(int i = 0; i < 4; i++)
        {
            if (collision.gameObject == wall[i].gameObject)
            {
                switch(i)
                {
                    case 0:
                        up_wall = true;
                        break;
                    case 1:
                        down_wall = true;
                        break;
                    case 2:
                        right_wall = true;
                        break;
                    case 3:
                        left_wall = true;
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < 4; i++)
        {
            if (collision.gameObject == wall[i].gameObject)
            {
                switch (i)
                {
                    case 0:
                        up_wall = false;
                        break;
                    case 1:
                        down_wall = false;
                        break;
                    case 2:
                        right_wall = false;
                        break;
                    case 3:
                        left_wall = false;
                        break;
                }
            }
        }
    }

    IEnumerator UP()
    {
        if (!up || up_wall)
        {
            yield break;
        }
        transform.Translate(0, 0.05f, 0);
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(UP());
    }

    IEnumerator DOWN()
    {
        if (!down || down_wall)
        {
            yield break;
        }
        transform.Translate(0, -0.05f, 0);
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(DOWN());
    }

    IEnumerator RIGHT()
    {
        if (!right || right_wall)
        {
            yield break;
        }
        transform.Translate(0.05f, 0, 0);

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(RIGHT());
    }

    IEnumerator LEFT()
    {
        if (!left || left_wall)
        {
            yield break;
        }

        transform.Translate(-0.05f, 0, 0);

        yield return new WaitForSeconds(0.01f);
        StartCoroutine(LEFT());
    }




    public void Click_UP_Btn()
    {
        up = true;
        StartCoroutine(UP());
    }
    public void Stop_UP()
    {
        up = false;
    }


    public void Click_DOWN_Btn()
    {
        down = true;
        StartCoroutine(DOWN());
    }
    public void Stop_DOWN()
    {
        down = false;
    }


    public void Click_RIGHT_Btn()
    {
        right = true;
        StartCoroutine(RIGHT());
    }
    public void Stop_RIGHT()
    {
        right = false;
    }


    public void Click_LEFT_Btn()
    {
        left = true;
        StartCoroutine(LEFT());
    }
    public void Stop_LEFT()
    {
        left = false;
    }
}
