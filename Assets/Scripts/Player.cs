using UnityEngine;

public class Player : MonoBehaviour{
    
    [ReadOnly]public float Health;
    public float MaxHealth = 100;
    public float HealthPercent {
        get{
            return MaxHealth / Health;
        }
    }


}