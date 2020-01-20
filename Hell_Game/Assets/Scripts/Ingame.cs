using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ingame : MonoBehaviour
{
    private static Ingame instance = null;    // 싱글톤


    public GameObject Me;

    public float time = 0;

    [SerializeField]
    private Text time_Text;

    public GameObject Start_Panel;

    public GameObject AD_Parent;
    public Object_Pooling object_Pooling;

    public List<GameObject> Spot = new List<GameObject>();

    public static Ingame Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            Initialize();
        }
    }


    private void Initialize()
    {
        object_Pooling.Initialized();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        time_Text.text = (int)time + "s";
    }
}
