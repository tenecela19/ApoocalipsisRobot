using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D Effector2D;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        Effector2D = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S)){
            waitTime = 0.5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                Effector2D.rotationalOffset = 180;
                waitTime = 0.5f;
            }
            else waitTime -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.Space))
        {
            Effector2D.rotationalOffset = 0;
        }
    }
}
