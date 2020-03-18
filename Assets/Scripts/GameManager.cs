using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Menu,
    Game,
    End
}

public class GameManager : MonoBehaviour
{
    public Player[] players;

    public GameObject MainMenu;
    public GameObject Overlay;
    public GameObject EndMenu;

    GameState lastState;
    GameState currentState;

    Player winner;

    // Update is called once per frame
    void Update()
    {
        foreach(Player player in players){
            if(player.Health <= 0){
                currentState = GameState.End;
                winner = (new List<Player>(players)).Find((Player p) => p.Health > 0);
            }

        }




        if(currentState != lastState)
        {
            switch (currentState)
            {
                case GameState.Menu:
                    //WHAT WE NEED TO DO TO GET TO MENU
                    break;
                case GameState.Game:
                    foreach(Player player in players)
                    {
                        player.Reset();
                    }
                    Time.timeScale = 1.0f;
                    break;
                case GameState.End:
                    Time.timeScale = 0.1f;

                    break;
            }
            currentState = lastState;
        }
    }
}
