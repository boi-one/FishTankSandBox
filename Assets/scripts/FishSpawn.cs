using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using Random = System.Random;

public class FishSpawn : MonoBehaviour
{
    public GameObject fish;
    public List<Fish> fishes = new List<Fish>();
    public List<Fish> malefish = new List<Fish>();
    public List<Fish> femalefish = new List<Fish>();
    public List<GameObject> allAirBubbles;
    public List<string> randomname = new List<string>() { "bruh", "henk", "fish386", "lole", "fish" };
    public Canvas canvas;
    public Sprite redFish, yellowFish;
    public string growstat;
    public GameObject airBubble, FishEgg;
    public int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        Fish fc = new Fish(new Vector3(0, 0), Instantiate(fish), "a", 2f, 2f, 2.5f,20, Type.red);
        fishes.Add(fc);
        fc.hunger = 20;
        Debug.Log(fc.hunger);
        int numbermf = UnityEngine.Random.Range(0,2);
        if (numbermf == 0)
        {
            fc.isMale = true;
            malefish.Add(fc);
        }
        else
        {
            fc.isMale = false;
            fishes.Add(fc);
        }
    }
    // Update is called once per frame
    void Update()
    {
        FishBehaviour(fishes);
        Egging();
        /*foreach(Fish f in fishes.ToList())//de;ete
        {
            if (f.fishGameObject.transform.localScale.y >= 0.22f) //turns fish into big fish when its big enough
            {
                f.growstring = "Max";
                Fish bigfish = f;
                bigfish.range = 2.2f;
                fishes.Remove(bigfish);
                bigFishes.Add(bigfish);
            }
            if (f.fishGameObject.transform.localScale.y < 0.22f)
                f.growstring = Convert.ToString(f.growstate);
            //if (fishes[fishes.Count].troughegg = true)
                //f.fishGameObject.transform.localScale.y > 0.06f;
        }*/

        foreach (Fish f in fishes)
        {
            if (f.fishGameObject.transform.localScale.x >= 0.22f) //lets the big fish eat smaller fish
            {
                Fish bf = f;
                float closestvictimpos = 9999;
                Fish closestvictim = null;
                
                if (fishes.Count > 0 && f.alive)
                {
                    if ((f.fishGameObject.transform.position - bf.fishGameObject.transform.position).magnitude < closestvictimpos)    
                    {
                        closestvictimpos = (f.fishGameObject.transform.position - bf.fishGameObject.transform.position).magnitude;
                        closestvictim = f;
                    }
                    if (closestvictimpos < f.range && f.hunger < 5)
                    {
                        bf.fooddead = false;
                        bf.pos = closestvictim.fishGameObject.transform.position;
                    }
                    else
                        bf.fooddead = true;
                    if (closestvictimpos < 1f && bf.hunger < 5)
                    {
                        bf.hunger += 8;
                        fishes.Remove(closestvictim);
                        if (closestvictim.isMale)
                            malefish.Remove(closestvictim);
                        else
                            femalefish.Remove(closestvictim);
                        Destroy(closestvictim.fishGameObject);
                        bf.previousPos = bf.pos;
                        bf.pos = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0);
                        if(bf.pos == bf.previousPos)
                            bf.pos = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0);
                    }
                }
                
            }
        }
    }
    public void FishBehaviour(List<Fish> fishlist)
    {
        foreach (Fish fish in fishlist.ToList())
        {
            if (fish.alive)
            {
                fish.rate = UnityEngine.Random.Range(1, 10);  //spawns the airbubbles
                if (Time.time > fish.breathe)
                {
                    fish.breathe = Time.time + fish.rate;
                    GameObject Iairbubble = Instantiate(airBubble);
                    if (fish.pos.x > fish.fishGameObject.transform.position.x)
                        Iairbubble.transform.position = new Vector3(fish.fishGameObject.transform.position.x+fish.fishGameObject.transform.localScale.x*0.8f, fish.fishGameObject.transform.position.y);
                    else
                        Iairbubble.transform.position = new Vector3(fish.fishGameObject.transform.position.x-fish.fishGameObject.transform.localScale.x * 0.8f, fish.fishGameObject.transform.position.y);
                    allAirBubbles.Add(Iairbubble);
                }
            }
            /*fish.fishGameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TMPro.TMP_Text>().text = "Name: " + 
                fish.name + ", " + fish.IsMale(fish.isMale) + "\nHunger: " + fish.hunger + "\nSpeed: " + fish.speed + "\nGrow " + 
                fish.growstring; //null reference
            */
            if (fish.pos.x < fish.fishGameObject.transform.position.x)
                fish.fishGameObject.GetComponent<SpriteRenderer>().flipX = true;     //makes the fish turn into the right direction
            if (fish.pos.x > fish.fishGameObject.transform.position.x)
                fish.fishGameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (fish.alive)
            {
                fish.fishGameObject.transform.position += (fish.pos - fish.fishGameObject.transform.position).normalized * (Time.deltaTime * fish.speed); //moves the fish to the chosen position
                if(fish.pos == fish.previousPos)
                    fish.pos = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-4, 5), 0);
                if (Vector3.Distance(fish.fishGameObject.transform.position, fish.pos) < 1f && fish.fooddead)
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
                if ((fo.transform.position - fish.fishGameObject.transform.position).magnitude < closestFoodDistance && fish.hunger < 19)
                {
                    closestFoodDistance = (fo.transform.position - fish.fishGameObject.transform.position).magnitude;  //finds the closest food
                    closestFood = fo;
                }
                if (closestFoodDistance < fish.range) //goes to the closest food
                    fish.pos = fo.transform.position;
                if (closestFoodDistance < 1f && fish.alive && fish.hunger < 19) //eats the food
                {
                    fish.hunger += 3;
                    if (fish.fishGameObject.transform.localScale.y < 0.22f)
                    {
                        fish.growstate++;
                        if (fish.growstate == 5) //the fish grows
                        {
                            fish.fishGameObject.transform.localScale *= 1.1f;
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
                    fish.speed = fish.originalSpeed;
                    break;
                case 15:
                    if (fish.changespeed)
                        fish.speed = fish.originalSpeed * 0.8f;
                    fish.changespeed = false;
                    break;
                case 10:
                    if (fish.changespeed)
                        fish.speed = fish.originalSpeed * 0.6f;
                    fish.changespeed = false;
                    break;
                case 5:
                    if (fish.changespeed)
                        fish.speed = fish.originalSpeed * 0.4f;
                    fish.changespeed = false;
                    break;
                case 2:
                    if (fish.changespeed)
                        fish.speed = fish.originalSpeed * 0.2f;
                    fish.changespeed = false;
                    break;
                case 0:
                    if (fish.alive)
                    {
                        fish.alive = false;
                    }
                    fish.fishGameObject.GetComponent<SpriteRenderer>().flipY = true;
                    if (fish.fishGameObject.transform.position.y > -4f)
                        fish.fishGameObject.transform.position += (new Vector3(fish.fishGameObject.transform.position.x, -4) - fish.fishGameObject.transform.position).normalized * Time.deltaTime * 1.5f;
                    break;
            }
        }
    }
    public void ToggleStats(List<Fish> fishlist)
    {
        foreach(Fish fish in fishlist)
        {
            bool stats = true;
            if (Input.GetKeyDown(KeyCode.S) && stats == true)
            {
                stats = false;
                fish.fishGameObject.GetComponent<TextMeshPro>().gameObject.SetActive(false);
            }
            else
            {
                stats = true;
                fish.fishGameObject.GetComponent<TextMeshPro>().gameObject.SetActive(true);
            }
        }
    }
    public void Egging() //if fish meet multiple times the female fish lays an egg
    {
        List<Fish> fishMemoryDictionary =  GetComponent<Fish>().fishMemory;
        float fishDistance = 999;
        Fish closestFish = null;
        foreach (Fish m in malefish)
        {
            foreach (Fish f in femalefish)
            {
                //find closest fish
                if((f.fishGameObject.transform.position - m.fishGameObject.transform.position).magnitude < fishDistance)
                {
                    fishDistance = (f.fishGameObject.transform.position - m.fishGameObject.transform.position).magnitude;
                    closestFish = f;
                }
                if (fishDistance < 0.6f && !m.passed)
                {
                    if (!m.fishMemory.Contains(closestFish))
                    {
                        m.fishMemory.Add(closestFish);
                        m.amountFishPassed.Add(1);
                        m.passed = true;
                    }
                    else
                    {
                        for (int i = 0; i < m.fishMemory.Count; i++)
                        {
                            if (m.amountFishPassed[i] == 8)
                            {
                                GameObject Iegg = Instantiate(FishEgg);
                                Iegg.transform.position = m.fishMemory[i].fishGameObject.transform.position;
                                m.amountFishPassed[i] = 0;
                            }
                            else
                            {
                                m.amountFishPassed[i]++;
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
//delete
public class OldFish
{
    public Vector3 pos;
    public Vector3 previouspos;
    public GameObject gameobject;
    public bool passed = false;
    public string name;
    public int id;
    public Type type;
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
    public List<Fish> fishmemory;
    public List<int> amountfishpassed;
    public bool cameviaegg;
    public string IsMale(bool isMale)
    {
        if (isMale)
            return "Male";
        else
            return "Female";
    }
    public OldFish(Vector3 _pos, GameObject _gameobject, string _name, Type _type, float _speed, float _range, int _hunger, float _originalspeed, GameObject _airbubble)
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