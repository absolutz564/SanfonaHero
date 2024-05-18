using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    public bool isPlayAgain;
    // Start is called before the first frame update
    public static DontDestroy Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //DontDestroy Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
