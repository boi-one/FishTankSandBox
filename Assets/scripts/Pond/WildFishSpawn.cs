using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildFishSpawn : MonoBehaviour
{
    private bool catched, sethookpos, hooked; 
    public bool menuOpen;
    private float cooldown = 4f;
    private Vector3 originalHookPos;
    [SerializeField]private Transform hook;
    [SerializeField]private GameObject fishGameObject;
    [SerializeField]private GameObject rod;
    public Fish catchedFish;
    public Image fishStat;
    private WildFish hookedFish = null;
    public static List<WildFish> WildFishes = new List<WildFish>();
    public Text fishtype, sex;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        fishStat.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hookedFish);
        //FishBehaviour(wildfishes);
        if (hook.transform.position.y < -10f && hook.transform.position.y > -20)
        {
            bool isMale;
            if (Random.Range(0, 2) == 0)
                isMale = true;
            else
                isMale = false;
            if (WildFishes.Count < 10)
            { 
                WildFishes.Add(new WildFish(new Vector3(Random.Range(-5, 5),Random.Range(-5, -10)), fishGameObject, "", 2f, 2f, 2.5f, Type.red));
                Debug.Log(WildFishes.Count);
            }
        }
        foreach (WildFish wf in WildFishes)
        {
            if (wf.instantiatedObject.GetComponent<SpriteRenderer>().color.a < 1f)
                wf.instantiatedObject.GetComponent<SpriteRenderer>().color += new Color(1f, 1f, 1f, 0.1f) * 8 * Time.deltaTime;
        }
        if (catched)
        {
            menuOpen = true;
            fishStat.gameObject.SetActive(true);
            fishStat.transform.GetChild(2).GetComponent<Text>().text += " " + hookedFish.fishType.ToString(); //fishtype doesnt exist anymore
            fishStat.transform.GetChild(3).GetComponent<Text>().text += " " + hookedFish.IsMale(hookedFish.isMale);
            fishStat.transform.GetChild(6).GetComponent<Image>().sprite = hookedFish.instantiatedObject.GetComponent<SpriteRenderer>().sprite;
            if(hookedFish != null)
            {
                Debug.Log(hookedFish.fishType); //fish type doesnt exist anymore
                Debug.Log(hookedFish.IsMale(hookedFish.isMale));
            }
            catched = false;
        }
        if (canvas.GetComponent<Confirm>().toTank || canvas.GetComponent<Confirm>().sell)
        {
            menuOpen = false;
            hookedFish = null;
            catched = false;
        }
        if (!menuOpen)
            fishStat.gameObject.SetActive(false);
    }
    public void FishBehaviour(List<WildFish> fishlist)
    {
        WildFish closestFish = null;
        float DistanceClosestFish = 9999;
        foreach (WildFish fish in fishlist)
        {
            //closest fish
            if ((hook.transform.position - fish.instantiatedObject.transform.position).magnitude < DistanceClosestFish)
            {
                DistanceClosestFish = (hook.transform.position - fish.instantiatedObject.transform.position).magnitude;
                closestFish = fish;
            }
            if (fish.instantiatedObject.transform.position.y > -1f)
            {
                catched = true;
            }
            if (Time.time > cooldown)
            {
                cooldown = Time.time + 0.25f;
                originalHookPos = hook.transform.position;
                fish.gone = false;
            }
            if (!fish.bite)
            {
                if((fish.instantiatedObject.transform.position-hook.transform.position).magnitude < 1)
                    Debug.Log(fish.isInterested);
                //makes the fish look at where its going
                if (fish.pos.x > fish.instantiatedObject.transform.position.x)
                    fish.instantiatedObject.GetComponent<SpriteRenderer>().flipX = false;
                else if (fish.pos.x < fish.instantiatedObject.transform.position.x)
                    fish.instantiatedObject.GetComponent<SpriteRenderer>().flipX = true;
                //fish swimming around
                fish.instantiatedObject.transform.position += ((fish.pos - fish.instantiatedObject.transform.position).normalized * fish.speed * Time.deltaTime);
                if ((fish.pos - fish.instantiatedObject.transform.position).magnitude < 1f)
                {
                    fish.speed = fish.originalSpeed;
                    fish.pos = new Vector3(Random.Range(-12, 12), Random.Range(fish.originalPos.y - 1, fish.originalPos.y + 1));
                }
                //swim away if the line goes to fast
                if ((fish.instantiatedObject.transform.position - hook.transform.position).magnitude < 4)
                {
                    if ((originalHookPos - hook.transform.position).magnitude > 1.33f && !fish.gone)
                    {
                        fish.gone = true;
                        fish.pos = new Vector3(-fish.pos.x, fish.pos.y);
                        fish.speed = fish.originalSpeed * 4.5f;
                    }
                }
                //look at the bait if close
                SpriteRenderer fishspriterenderer = fish.instantiatedObject.GetComponent<SpriteRenderer>();
                fish.instantiatedObject.transform.eulerAngles = new Vector3(0, 0, 0);
                if (fishspriterenderer.flipY == true)
                    fishspriterenderer.flipY = false;
                //move to hook
                if ((closestFish.instantiatedObject.transform.position - hook.transform.position).magnitude > 1.5f && (originalHookPos - hook.transform.position).magnitude < 0.66f) //LAAT DE viS SNELLER GEVIST WORDEN ANDERS IS HET HEEL SAAI,
                    closestFish.isInterested = true;                                                                                                                         //ALS DE VIS BOVEN WATER KOMT KRIJG EEN SCHERM MET DE STATS VAN           
            }
            FishEscape(hookedFish);
        }
        Debug.Log(closestFish);
        if (closestFish != null && (closestFish.instantiatedObject.transform.position - hook.transform.position).magnitude < 1.5f && !closestFish.gone && !hooked)
        {
            SpriteRenderer spriterenderer = closestFish.instantiatedObject.GetComponent<SpriteRenderer>();
            if (closestFish.isInterested)
            {
                closestFish.interest += Random.Range(1, 4);
                closestFish.isInterested = false;
            }
            Vector2 direction = hook.transform.position - closestFish.instantiatedObject.transform.position;
            if (closestFish.instantiatedObject.transform.position.x > hook.transform.position.x)
            {
                closestFish.instantiatedObject.transform.right = direction;
                spriterenderer.flipX = false;
                spriterenderer.flipY = true;
            }
            else if (closestFish.instantiatedObject.transform.position.x < hook.transform.position.x)
            {
                closestFish.instantiatedObject.transform.right = -direction;
                spriterenderer.flipX = true;
                spriterenderer.flipY = false;
            }
        }
        //fish the fish                                                                                                                                              //DE VIS EN JE KAN HEM EEN NAAM GEVEN EN KOMT IN DE TANK
        if (closestFish != null && closestFish.interest > 3 && !hooked)
        {
            closestFish.pos = hook.transform.position;
            //pos is hook
            if ((closestFish.instantiatedObject.transform.position - closestFish.pos).magnitude < 0.5f)
            {
                hookedFish = closestFish;
                SpriteRenderer spriterenderer = hookedFish.instantiatedObject.GetComponent<SpriteRenderer>();
                spriterenderer.flipY = false;
                spriterenderer.flipX = false;
                hooked = true;
                hookedFish.instantiatedObject.transform.Rotate(new Vector3(0, 0, 0));
                hook.transform.position = closestFish.instantiatedObject.transform.position;
                hookedFish.instantiatedObject.transform.SetParent(hook.transform, true);
                hookedFish.bite = true;
            }
        }
    }
    void FishEscape(WildFish onHook)
    {
        if (onHook != null)
            Debug.Log(onHook.bite);
        if (hookedFish != null && (originalHookPos - hook.transform.position).magnitude > 1.33f)
        {
            hookedFish.instantiatedObject.transform.SetParent(null, true);
            hookedFish.bite = false;
            hooked = false;
            hookedFish.interest = -5;
            hookedFish = null;
        }
    }
    void SetName(string name)
    {
        catchedFish.name = name;
    }
}
public class OldWildFishClass
{
    public Vector3 pos, hookedpos;
    public Vector3 originalpos;
    public GameObject instantiatedObject;
    public Type fishtype;
    public bool isMale;
    public float speed, originalspeed;
    public bool alive, isinterested = true;
    public bool gone, bite;
    public float size;
    public int interest;
    public string IsMale(bool isMale)
    {
        if (isMale)
            return "Male";
        else
            return "Female";
    }
    public OldWildFishClass(Vector3 _pos, GameObject prefab, Type _fishtype, bool _isMale, float _speed, float _size)
    {
        switch (fishtype)
        {
            case Type.red:
                speed = 1;
                pos = new Vector3(Random.Range(-12, 12), Random.Range(-15f, -30f));
                break;
            case Type.yellow:
                speed = 2;
                break;
        }
    }
}

