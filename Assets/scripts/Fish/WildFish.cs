using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFish
{
    public Vector3 originalPos, pos, hookedPos, previousPos;
    public GameObject instantiatedObject;
    public bool isMale, alive, isInterested, gone, bite;
    public int interest;
    public float speed, originalSpeed, size, range;
    public Type fishType;
    public string name;

    public GameObject fishGameObject => instantiatedObject;

    public WildFish(Vector3 _pos, GameObject prefab, string _name, float _speed, float _originalSpeed, float _range, Type type)
    {
        pos = _pos;
        instantiatedObject = prefab;
        name = _name;
        speed = _speed;
        originalSpeed = _originalSpeed;
        range = _range;
        fishType = type;
        
        MonoBehaviour.Instantiate(prefab);
    }
    public string IsMale(bool isMale) => isMale ? "Male" : "Female";
}
public enum Type
{
    red,
    yellow
}
