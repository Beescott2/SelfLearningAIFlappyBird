using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    List<int> list;
	// Use this for initialization
	void Start () {
        list = new List<int>();
        list.Add(1);
        list.Add(2);
    }
	
	// Update is called once per frame
	void Update () {
        int a = list[1];
        int b = list[1];
        a++;

        print("a " + a);
        print("b " + b);
        print("list " + list[1]);
    }
}
