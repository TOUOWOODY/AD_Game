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
    public GameObject Success_Panel;

    public GameObject AD_Parent;
    public Object_Pooling object_Pooling;

    public List<GameObject> Spot = new List<GameObject>();

    public Admob admob;


    public GameObject Bomb;
    public GameObject AD_Skip;



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
        if(!Start_Panel.activeSelf && !Success_Panel.activeSelf)
        {
            time += Time.deltaTime;

            time_Text.text = (int)time + "s";
        }
    }

    public void Start_Btn()
    {
        Start_Panel.SetActive(false);
        Success_Panel.SetActive(false);
        AD_Skip.transform.localPosition = new Vector3(0, 0, 0);
        AD_Skip.SetActive(false);

        Me.transform.localPosition = new Vector3(0, -100, 0);
        Bomb.transform.localPosition = new Vector3(0, 800, 0);
        Bomb.transform.localScale = new Vector2(100, 100);

        time = 0;
        time_Text.text = (int)time + "s";

        StartCoroutine(Bomb.GetComponent<Bomb>().bomb_Move0());
    }
}
