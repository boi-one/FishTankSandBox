using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.Assertions.Must;
using System.Threading;

public class spawnfish : MonoBehaviour
{
    public GameObject fish;
    public List<OldFishClass> fishes = new List<OldFishClass>();
    public List<OldFishClass> bigfishes = new List<OldFishClass>();
    public List<OldFishClass> malefish = new List<OldFishClass>();
    public List<OldFishClass> femalefish = new List<OldFishClass>();
    public List<GameObject> allairbubbles = new List<GameObject>();
    public List<string> randomname = new List<string>() { "bruh", "henk", "fish386", "lole", "fish" };
    public Canvas canvas;
    public Sprite redFish, yellowFish;
    public string growstat;
    public GameObject airbubble, FishEgg;
    public int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        int numbermf = UnityEngine.Random.RandomRange(0,2);
        GameObject spawnedfish = Instantiate(fish);
        OldFishClass fc = new OldFishClass(new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0), spawnedfish, randomname[UnityEngine.Random.Range(0, 5)], type.red, 1f, 4f, 20, 2f, airbubble);
        fishes.Add(fc);
        fc.id = id;
        id++;
        if (numbermf == 0)
        {                                                           //STARTER VIS
            fc.isMale = true;
            malefish.Add(fc);
        }
        else
        {
            fc.isMale = false;
            fishes.Add(fc);
        }
        fc.previouspos = fc.gameobject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        FishBehaviour(fishes);
        FishBehaviour(bigfishes);
        Egging();
        foreach(OldFishClass f in fishes.ToList())
        {
            if (f.gameobject.transform.localScale.y >= 0.22f) //turns fish into big fish when its big enough
            {
                f.growstring = "Max";
                OldFishClass bigfish = f;
                bigfish.range = 2.2f;
                fishes.Remove(bigfish);
                bigfishes.Add(bigfish);
            }
            if (f.gameobject.transform.localScale.y < 0.22f)
                f.growstring = Convert.ToString(f.growstate);
            //if (fishes[fishes.Count].cameviaegg = true)
                //f.gameobject.transform.localScale.y > 0.06f;
        }
        foreach (OldFishClass bf in bigfishes) //lets the big fish eat smaller fish
        {
            float closestvictimpos = 9999;
            OldFishClass closestvictim = null;
            foreach (OldFishClass f in fishes.ToList())
            {
                if (fishes.Count > 0 && bf.alive)
                {
                    if ((f.gameobject.transform.position - bf.gameobject.transform.position).magnitude < closestvictimpos)    
                    {
                        closestvictimpos = (f.gameobject.transform.position - bf.gameobject.transform.position).magnitude;
                        closestvictim = f;
                    }
                    if (closestvictimpos < bf.range && bf.hunger < 5)
                    {
                        bf.foodisntalive = false;
                        bf.pos = closestvictim.gameobject.transform.position;
                    }
                    else
                        bf.foodisntalive = true;
                    if (closestvictimpos < 1f && bf.hunger < 5)
                    {
                        bf.hunger += 8;
                        fishes.Remove(closestvictim);
                        if (closestvictim.isMale)
                            malefish.Remove(closestvictim);
                        else
                            femalefish.Remove(closestvictim);
                        Destroy(closestvictim.gameobject);
                        bf.previouspos = bf.pos;
                        bf.pos = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0);
                        if(bf.pos == bf.previouspos)
                            bf.pos = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0);
                    }
                }
            }
        }
    }
    public void FishBehaviour(List<OldFishClass> fishlist)
    {
        foreach (OldFishClass fish in fishlist.ToList())
        {
            if (fish.alive)
            {
                fish.rate = UnityEngine.Random.Range(1, 10);  //spawns the airbubbles
                if (Time.time > fish.breathe)
                {
                    fish.breathe = Time.time + fish.rate;
                    GameObject Iairbubble = Instantiate(airbubble);
                    if (fish.pos.x > fish.gameobject.transform.position.x)
                        Iairbubble.transform.position = new Vector3(fish.gameobject.transform.position.x+fish.gameobject.transform.localScale.x*0.8f, fish.gameobject.transform.position.y);
                    else
                        Iairbubble.transform.position = new Vector3(fish.gameobject.transform.position.x-fish.gameobject.transform.localScale.x * 0.8f, fish.gameobject.transform.position.y);
                    allairbubbles.Add(Iairbubble);
                }
            }
            fish.gameobject.transform.Find("Canvas").Find("Text (TMP)").GetComponent<TMPro.TMP_Text>().text = "Name: " + fish.name + ", " + fish.IsMale(fish.isMale) + "\nHunger: " + fish.hunger + "\nSpeed: " + fish.speed + "\nGrow " + fish.growstring;
            if (fish.pos.x < fish.gameobject.transform.position.x)
                fish.gameobject.GetComponent<SpriteRenderer>().flipX = true;     //makes the fish turn into the right direction
            if (fish.pos.x > fish.gameobject.transform.position.x)
                fish.gameobject.GetComponent<SpriteRenderer>().flipX = false;
            if (fish.alive)
            {
                fish.gameobject.transform.position += (fish.pos - fish.gameobject.transform.position).normalized * Time.deltaTime * fish.speed; //moves the fish to the chosen position
                if(fish.pos == fish.previouspos)
                    fish.pos = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0);
                if (Vector3.Distance(fish.gameobject.transform.position, fish.pos) < 1f && fish.foodisntalive)
                {
                    fish.pos = new Vector3(UnityEngine.Random.Range(-9, 9), UnityEngine.Random.Range(-4, 5), 0);        
                    fish.hunger--;
                    fish.changespeed = true;
                }
            }
            float closestFoodDistance = 999;
            GameObject closestFood = null;
            foreach (GameObject fo in GetComponent<food>().foodlist)
            {
                if ((fo.transform.position - fish.gameobject.transform.position).magnitude < closestFoodDistance && fish.hunger < 19)
                {
                    closestFoodDistance = (fo.transform.position - fish.gameobject.transform.position).magnitude;  //finds the closest food
                    closestFood = fo;
                }
                if (closestFoodDistance < fish.range) //goes to the closest food
                    fish.pos = fo.transform.position;
                if (closestFoodDistance < 1f && fish.alive && fish.hunger < 19) //eats the food
                {
                    fish.hunger += 3;
                    if (fish.gameobject.transform.localScale.y < 0.22f)
                    {
                        fish.growstate++;
                        if (fish.growstate == 5) //the fish grows
                        {
                            fish.gameobject.transform.localScale *= 1.1f;
                            fish.range += 0.1f;
                            fish.growstate = 0;
                        }
                    }
                    GameObject todelete = fo;
                    Destroy(todelete);
                }
            }
            switch (fish.hunger)            //what will happen when food value is low enough
            {
                case 20:
                    fish.speed = fish.originalspeed;
                    break;
                case 15:
                    if (fish.changespeed)
                        fish.speed = fish.originalspeed * 0.8f;
                    fish.changespeed = false;
                    break;
                case 10:
                    if (fish.changespeed)
                        fish.speed = fish.originalspeed * 0.6f;
                    fish.changespeed = false;
                    break;
                case 5:
                    if (fish.changespeed)
                        fish.speed = fish.originalspeed * 0.4f;
                    fish.changespeed = false;
                    break;
                case 2:
                    if (fish.changespeed)
                        fish.speed = fish.originalspeed * 0.2f;
                    fish.changespeed = false;
                    break;
                case 0:
                    if (fish.alive)
                    {
                        fish.alive = false;
                    }
                    fish.gameobject.GetComponent<SpriteRenderer>().flipY = true;
                    if (fish.gameobject.transform.position.y > -4f)
                        fish.gameobject.transform.position += (new Vector3(fish.gameobject.transform.position.x, -4) - fish.gameobject.transform.position).normalized * Time.deltaTime * 1.5f;
                    break;
            }
        }
    }
    public void ToggleStats(List<OldFishClass> fishlist)
    {
        foreach(OldFishClass fish in fishlist)
        {
            bool stats = true;
            if (Input.GetKeyDown(KeyCode.S) && stats == true)
            {
                stats = false;
                fish.gameobject.GetComponent<TextMeshPro>().gameObject.SetActive(false);
            }
            else
            {
                stats = true;
                fish.gameobject.GetComponent<TextMeshPro>().gameObject.SetActive(true);
            }
        }
    }
    public void Egging() //if fish meet multiple times the female fish lays an egg
    {
        foreach (OldFishClass m in malefish)
        {
            foreach (OldFishClass f in femalefish)
            {
                float fishDistance = 999;
                OldFishClass closestFish = null;  //find closest fish
                if((f.gameobject.transform.position - m.gameobject.transform.position).magnitude < fishDistance)
                {
                    fishDistance = (f.gameobject.transform.position - m.gameobject.transform.position).magnitude;
                    closestFish = f;
                }
                if (fishDistance < 0.6f && !m.passed)
                {
                    if (!m.fishmemory.Contains(closestFish))
                    {
                        m.fishmemory.Add(closestFish);
                        m.amountfishpassed.Add(1);
                        m.passed = true;
                    }
                    else
                    {
                        for (int i = 0; i < m.fishmemory.Count; i++)
                        {
                            if (m.amountfishpassed[i] == 8)
                            {
                                GameObject Iegg = Instantiate(FishEgg);
                                Iegg.transform.position = m.fishmemory[i].gameobject.transform.position;
                                m.amountfishpassed[i] = 0;
                            }
                            else
                            {
                                m.amountfishpassed[i]++;
                                m.passed = true;
                            }
                        }
                    }
                }
                if (fishDistance > 2)
                    m.passed = false;
            }
        }
    }
}
public class OldFishClass
{
    public Vector3 pos;
    public Vector3 previouspos;
    public GameObject gameobject;
    public bool passed = false;
    public string name;
    public int id;
    public type type;
    public bool isMale;
    public float speed;
    public float originalspeed;
    public float range;
    public int hunger;
    public bool alive = true;
    public bool changespeed = true;
    public int growstate = 0;
    public string growstring;
    public bool foodisntalive = true;
    public GameObject airbubble;
    public float breathe = 0.0f;
    public float rate;
    public List<OldFishClass> fishmemory = new List<OldFishClass>();
    public List<int> amountfishpassed = new List<int>();
    public bool cameviaegg;
    public string IsMale(bool isMale)
    {
        if (isMale)
            return "Male";
        else
            return "Female";
    }
    public OldFishClass(Vector3 _pos, GameObject _gameobject, string _name, type _type, float _speed, float _range, int _hunger, float _originalspeed, GameObject _airbubble)
    {
        pos = _pos;
        gameobject = _gameobject;
        name = _name;
        type = _type;
        speed = _speed;
        originalspeed = _originalspeed;
        range = _range;
        hunger = _hunger;
        airbubble = _airbubble;
    }
}

public enum type
{
    red,
    yellow
}