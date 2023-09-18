using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : WildFish
{
    public GameObject bubble;
    public bool passed, changespeed, fooddead, troughegg;
    public string fishname, growstring;
    public int hunger, growstate;
    public float breathe, rate, range;
    public List<Fish> fishMemory = new List<Fish>();
    public List<int> amountFishPassed = new List<int>();

    public string IsMale(bool isMale)
    {
        return isMale ? "Male" : "Female";
    }
    
    public Fish(Vector3 _pos, GameObject prefab, string _name, float _speed, float _originalSpeed, float _range, int _hunger, Type type) : base(_pos, prefab, _name, _speed, _originalSpeed, _range, type)
    {
        pos = _pos;
        name = _name;
        speed = _speed;
        originalSpeed = _originalSpeed;
        range = _range;
        hunger = _hunger;
        fishType = type;
    }
}
