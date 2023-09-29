using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCode : MonoBehaviour
{

    public int toAdd;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.singleton.SetAdd(toAdd);
    }
}
