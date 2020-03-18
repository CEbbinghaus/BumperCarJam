using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Undefined,
    Menu,
    Instructions,
    Game,
    End
}

public class GameManager : MonoBehaviour
{
    public Player[] players;

    public GameObject MainMenu;
    public GameObject Instrctions;
    public GameObject Overlay;
    public EndScreen EndMenu;

    GameState lastState = GameState.Undefined;
    GameState currentState = GameState.Undefined;
 
    Player winner;

    void Start()
    {
        currentState = GameState.Menu;
    }

    void UpdateScreens()
    {
        switch (currentState)
        {
            case GameState.Menu:
                MainMenu.SetActive(true);
                Overlay.SetActive(false);
                Instrctions.SetActive(false);
                EndMenu.gameObject.SetActive(false);
                break;
            case GameState.Instructions:
                MainMenu.SetActive(false);
                Instrctions.SetActive(true);
                break;
            case GameState.Game:
                    MainMenu.SetActive(false);
                    Overlay.SetActive(true);
                    EndMenu.gameObject.SetActive(false);
                    break;
            case GameState.End:
                    MainMenu.SetActive(false);
                    Overlay.SetActive(false);
                    EndMenu.gameObject.SetActive(true);
                    break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(currentState != lastState)
        {
            switch (currentState)
            {
                case GameState.Menu:
                    break;
                case GameState.Game:
                    foreach(Player player in players)
                    {
                        player.Reset();
                    }
                    Time.timeScale = 1.0f;
                    break;
                case GameState.End:
                    Time.timeScale = 0.01f;
                    if (winner == null)
                        EndMenu.winText.text = "Game Ended in a Draw";
                    else
                        EndMenu.winText.text = winner.gameObject.name + " Won!";
                    break;
            }
            UpdateScreens();
            lastState = currentState;
        }

        if (currentState == GameState.Game)
        {
            foreach (Player player in players)
            {
                if (player.Health <= 0)
                {
                    currentState = GameState.End;
                    winner = (new List<Player>(players)).Find((Player p) => p.Health > 0);
                }

            }
        }
    }

    public void NavigateMenu()
    {
        currentState = GameState.Menu;
    }
    public void NavigateInstructions()
    {
        currentState = GameState.Instructions;
    }
    public void NavigateGame()
    {
        currentState = GameState.Game;
    }
    public void NavigateEnd()
    {
        currentState = GameState.End;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
