using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistanceBetweenFish : MonoBehaviour
{
    GameObject fishspawn;
    List<GameObject> fishlistgameobjects;
    // Start is called before the first frame update
    void Start()
    {
        fishspawn = GameObject.Find("fish spawn");
    }

    // Update is called once per frame
    void Update()
    {
        //ForEachLoop(fishspawn.GetComponent<spawnfish>().allfish);
    }
    public void ForEachLoop(List<Fish> fishlist)
    {
        /*if ((fishspawn.GetComponent<SpawnFish>().fishes.Count + fishspawn.GetComponent<SpawnFish>().bigFishes.Count) > 1)
        {
            foreach (Fish f in fishlist.ToList())
            {
                fishlistgameobjects.Add(f.instantiatedObject);
            }
            foreach(GameObject fishGameObject in fishlistgameobjects.ToList())
            {
                float closestFishDistance = 999;
                GameObject closestFish = null;
                if ((fishGameObject.transform.position - gameObject.transform.position).magnitude < closestFishDistance)
                {
                    closestFishDistance = (fishGameObject.transform.position - gameObject.transform.position).magnitude;
                    closestFish = fishGameObject;
                }
            }
        }*/
    }
}
