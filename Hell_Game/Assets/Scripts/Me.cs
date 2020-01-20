using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Me : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bomb" || collision.name == "AD" || collision.name == "AD_SKIP")
        {
            Ingame.Instance.Start_Panel.SetActive(true);
        }
    }
}
