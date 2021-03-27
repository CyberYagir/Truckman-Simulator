using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TreeRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.localEulerAngles = new Vector3(0, Random.Range(0,360), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
