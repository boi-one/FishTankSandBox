using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class clickbubbles : MonoBehaviour
{
    Transform mouse;
    // Start is called before the first frame update
    void Start()
    {
        mouse = GetComponent<food>().mouse;
    }

    // Update is called once per frame
    void Update()
    {
        float closestbubbledist = 999;
        GameObject closestbubble = null;
        foreach (GameObject b in GetComponent<spawnfish>().allairbubbles.ToList())
        {
            if((b.transform.position - mouse.transform.position).magnitude < closestbubbledist)
            {
                closestbubbledist = (b.transform.position - mouse.transform.position).magnitude;
                closestbubble = b;
            }
            if (closestbubbledist < 0.5f && Input.GetMouseButtonDown(0))
            {
                GetComponent<spawnfish>().allairbubbles.Remove(closestbubble);
                Destroy(closestbubble);
            }
            if (b.transform.position.y > 6)
            {
                GetComponent<spawnfish>().allairbubbles.Remove(closestbubble);
                Destroy(closestbubble);
            }
        }
    }
}
