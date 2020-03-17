using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    // 1.0f Means full charge.
    public float BoostAmount = 0.25f;
<<<<<<< Updated upstream
    public Slider[] boostBar = null;
    public Slider[] healthBar = null;
    public OldPlayer[] Player = null;
=======
    public Slider[] boostBar ;
    public Slider[] healthBar ;
    public OldPlayer[] Player ;
>>>>>>> Stashed changes
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
            healthBar[i].value = Player[i].healthAmount;
        }
    }
}
