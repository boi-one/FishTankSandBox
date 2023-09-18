using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    public GameObject fish, airbubble;
    public Sprite redFish, yellowFish;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FishEggHatching());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator FishEggHatching()
    {
        yield return new WaitForSeconds(119);
        SpawnFishie();
    }
    public void SpawnFishie()
    {
        //change gameobject.find
        if (GameObject.Find("fish spawn").GetComponent<FishSpawn>().fishes.Count < 10)
        {
            GameObject spawnedfish = Instantiate(fish);
            List<Fish> randomfish = new List<Fish>();
            Fish fc = randomfish[UnityEngine.Random.Range(0, 2)];
            if (fc.fishType == Type.red)
                fc.fishGameObject.GetComponent<SpriteRenderer>().sprite = redFish;
            if (fc.fishType == Type.yellow)
                fc.fishGameObject.GetComponent<SpriteRenderer>().sprite = yellowFish;
            GameObject.Find("fish spawn").GetComponent<FishSpawn>().fishes.Add(fc);
            fc.fishGameObject.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
