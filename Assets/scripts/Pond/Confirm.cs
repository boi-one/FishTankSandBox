using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Confirm : MonoBehaviour
{
    // Start is called before the first frame update
    public string fishname;
    public GameObject input;
    public GameObject fishspawn;
    public bool toTank, sell = false;

    public void SaveInput()
    {
        fishname = input.GetComponent<TMP_InputField>().text;
        fishspawn.GetComponent<WildFishSpawn>().catchedFish.name = fishname;
        Debug.Log(GetComponent<WildFishSpawn>().catchedFish.name);
    }
    public void ToTank()
    {
        toTank = true;
    }
    public void Sell()
    {
        sell = true;
    }
}
