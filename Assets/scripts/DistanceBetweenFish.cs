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
    public void ForEachLoop(List<OldFishClass> fishlist)
    {
        if ((fishspawn.GetComponent<spawnfish>().fishes.Count + fishspawn.GetComponent<spawnfish>().bigfishes.Count) > 1)
        {
            foreach (OldFishClass f in fishlist.ToList())
            {
                fishlistgameobjects.Add(f.gameobject);
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
        }
    }
}
