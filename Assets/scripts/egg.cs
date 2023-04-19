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
        if (GameObject.Find("fish spawn").GetComponent<spawnfish>().fishes.Count < 10)
        {
            GameObject spawnedfish = Instantiate(fish);
            List<OldFishClass> randomfish = new List<OldFishClass>() {new OldFishClass(new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0), spawnedfish, GameObject.Find("fish spawn").GetComponent<spawnfish>().randomname[UnityEngine.Random.Range(0, 5)], type.red, 2f, 4f, 20, 2f, airbubble),
            new OldFishClass(new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0), spawnedfish, GameObject.Find("fish spawn").GetComponent<spawnfish>().randomname[UnityEngine.Random.Range(0, 5)], type.yellow, 3.5f, 3.5f, 20, 3.5f, airbubble)};
            OldFishClass fc = randomfish[UnityEngine.Random.Range(0, 2)];
            if (fc.type == type.red)
                fc.gameobject.GetComponent<SpriteRenderer>().sprite = redFish;
            if (fc.type == type.yellow)
                fc.gameobject.GetComponent<SpriteRenderer>().sprite = yellowFish;
            GameObject.Find("fish spawn").GetComponent<spawnfish>().fishes.Add(fc);
            fc.gameobject.transform.position = transform.position;
            //fc.gameobject.transform.localScale = 0.06f;
            fc.cameviaegg = true;
            Destroy(gameObject);
        }
    }
}
