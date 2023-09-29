using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebugAssert : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int count = 2;
        Debug.Assert(count < 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
