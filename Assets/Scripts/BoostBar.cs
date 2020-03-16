using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    // 1.0f Means full charge.
    public float BoostAmount = 0.25f;
    public Slider[] boostBar = null;
    public Player[] Player = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            boostBar[i].value = Player[i].BoostAmount;
        }
    }
}
