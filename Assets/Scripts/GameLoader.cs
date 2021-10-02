using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameObject Game;

    void Awake()
    {
        if (!GameObject.Find("Game")) 
        {
            var game = Instantiate(Game);
            game.name = Game.name;
        }
    }
}
