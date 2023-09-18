using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

public class FishLine : MonoBehaviour
{
    public Transform hook;
    public Transform mouse;
    private LineRenderer lr;
    public List<Transform> points;
    public float Distance, Distancenow;
    public bool sinking; //open bail if true
    public bool hookcam, over, up;
    public Vector3 center;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        points.Add(gameObject.transform);
        points.Add(hook.transform);
        Distance = transform.position.y - hook.transform.position.y;
        hook.GetChild(0).GetComponent<Light2D>().intensity = 0.2f;

        for (int i = 0; i < 10; i++)
        {
            Debug.DrawLine(new Vector3(-10, i * 10), new Vector3(10, i * 10), Color.red);
        }
    }
    // Update is called once per frame
    void Update()
    {
        lr.material.SetColor("_Color", new Color(1f, 1f, 1f, hook.GetChild(0).GetComponent<Light2D>().intensity));

        if (transform.position.x <= 0)
            mouse.transform.rotation = Quaternion.Euler(0, 0, 45);
        else
            mouse.transform.rotation = Quaternion.Euler(0, 0, -45);
        Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.transform.position = new Vector3(worldpos.x, worldpos.y, mouse.transform.position.z);
        sinking = GameObject.Find("Canvas").GetComponent<OpenCloseBail>().click;
        hookcam = GameObject.Find("Canvas").GetComponent<OpenCloseBail>().click2;
        Distancenow = transform.position.y - hook.transform.position.y;
        lr.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
        if (hook.transform.position.x > transform.position.x || hook.transform.position.x < transform.position.x) //moved under end of rod
        {
            hook.transform.position += new Vector3((transform.position.x - hook.transform.position.x), 0) * 10 * Time.deltaTime;
        }
        if (Distancenow != Distance)
        {
            hook.transform.position = new Vector3(hook.transform.position.x, transform.position.y - Distance);
        }
        Scrolling();
        Border();
        
        Light();
    }
    public void Scrolling()
    {
        if (!menu.GetComponent<WildFishSpawn>().menuOpen)
        {
            List<WildFish> wildfishes = WildFishSpawn.WildFishes;
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && Distance < 100 && !sinking) //hook going down
            {
                hook.transform.position += new Vector3(0, -80) * Time.deltaTime;
                Distance = transform.position.y - hook.transform.position.y;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && Distance > 4)//hook going up
            {
                hook.transform.position += new Vector3(0, 80) * Time.deltaTime;
                Distance = transform.position.y - hook.transform.position.y;
            }
            if (wildfishes.Count > 3)
            {
                foreach (WildFish f in wildfishes)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse2) && !up && !f.bite)// fast up
                        up = true;
                    else if ((Input.GetKeyDown(KeyCode.Mouse2) && up) || Distance < 4)
                        up = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse2) && !up)// fast up
                    up = true;
                else if ((Input.GetKeyDown(KeyCode.Mouse2) && up) || Distance < 4)
                    up = false;
            }
        }
        if (up && Distance > 4)
        {
            hook.transform.position += new Vector3(0, 40) * Time.deltaTime;
            Distance = transform.position.y - hook.transform.position.y;

        }
        if (sinking && Distance < 99)
        {
            hook.transform.position += new Vector3(0, -40) * Time.deltaTime;
            Distance = transform.position.y - hook.transform.position.y;
        }
    }
    void Border()
    {
        if (transform.position.x < -10)
        {
            transform.SetParent(null, true);
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
        }
        if (transform.position.x > 10)
        {
            transform.SetParent(null, true);
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
        }
        if (transform.position.y > 6)
        {
            transform.SetParent(null, true);
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
        }
        if (transform.position.y < 1)
        {
            transform.SetParent(null, true);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
        }
    }
    public void Light()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && hook.transform.position.y < -30)
        {
            Debug.Log("light-");
            hook.GetChild(0).GetComponent<Light2D>().intensity -= 0.008f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && hook.transform.position.y < 30 && hook.GetChild(0).GetComponent<Light2D>().intensity < 1f)
            hook.GetChild(0).GetComponent<Light2D>().intensity += 0.008f;
    }
    private void OnMouseDown()
    {
        if(!hookcam)
            transform.SetParent(mouse.transform, true);
        
    }
    private void OnMouseUp()
    {
        if(!hookcam)
            transform.SetParent(null, true);
    }
}
