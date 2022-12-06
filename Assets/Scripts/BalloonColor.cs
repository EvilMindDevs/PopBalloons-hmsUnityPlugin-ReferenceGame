using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonColor : MonoBehaviour
{
    [SerializeField]
    private GameObject thisBaloon;

    private int randomInt;

    public List<Color> colorList;

    void Start()
    {
        randomInt = UnityEngine.Random.Range(0, colorList.Count);
        thisBaloon.GetComponent<MeshRenderer>().material.color = colorList[randomInt];

    }

}
