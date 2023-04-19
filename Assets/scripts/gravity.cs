using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    public float secondstowait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= -4)
        {
            transform.position += (new Vector3(transform.position.x, -4) - transform.position).normalized * Time.deltaTime * 1.5f;
        }
        StartCoroutine(Despawn());
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(secondstowait);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        GameObject.Find("fish spawn")?.GetComponent<food>().foodlist.Remove(gameObject);
    }
}
