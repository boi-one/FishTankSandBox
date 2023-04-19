using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class food : MonoBehaviour
{
    Vector3 worldpos;
    public GameObject fishfood;
    public Transform mouse;
    public GameObject spawnedfood;
    public Button foodbutton;
    public Canvas canvas;
    public List<GameObject> foodlist;
    void Update()          //spawns food
    {
        worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldpos.z = 0;
        mouse.transform.position = worldpos;
        if (Input.GetMouseButtonDown(0) && canvas.GetComponentInChildren<buttons>().click && !EventSystem.current.IsPointerOverGameObject())
        {
            spawnedfood = Instantiate(fishfood);
            foodlist.Add(spawnedfood);
            spawnedfood.transform.position = mouse.transform.position;
        }
    }
}
