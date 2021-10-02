using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder;

public class TicTacToe : MonoBehaviour
{
    public bool PlayerVictory;
    public Material Win;

    List<TicTacToeTile> tiles;

    void Start()
    {
        tiles = FindObjectsOfType<TicTacToeTile>().ToList();
        tiles.ForEach(t => t.Triggered = () =>
        {
            bool playerWon =
                tiles.Where(t => t.X == 0).All(t => t.Circle.activeSelf)
                || tiles.Where(t => t.X == 1).All(t => t.Circle.activeSelf)
                || tiles.Where(t => t.X == 2).All(t => t.Circle.activeSelf)
                || tiles.Where(t => t.Y == 0).All(t => t.Circle.activeSelf)
                || tiles.Where(t => t.Y == 1).All(t => t.Circle.activeSelf)
                || tiles.Where(t => t.Y == 2).All(t => t.Circle.activeSelf)
                || tiles.Where(t => (t.Y == 0 && t.X == 0) || (t.Y == 1 && t.X == 1) || (t.Y == 2 && t.X == 2)).All(t => t.Circle.activeSelf)
                || tiles.Where(t => (t.Y == 0 && t.X == 2) || (t.Y == 1 && t.X == 1) || (t.Y == 2 && t.X == 0)).All(t => t.Circle.activeSelf);

            if (playerWon)
            {
                PlayerVictory = true;
                tiles.ForEach(t => t.Ready = false);
                tiles.ForEach(t => t.Circle.GetComponent<MeshRenderer>().material = Win);
                return;
            }

            tiles.Where(t => !t.Cross.activeSelf && !t.Circle.activeSelf).ToList().Random().SetCross();
        });
    }

    void Update()
    {
        bool enemyWon =
            tiles.All(t => t.Circle.activeSelf || t.Cross.activeSelf)
            || tiles.Where(t => t.X == 0).All(t => t.Cross.activeSelf)
            || tiles.Where(t => t.X == 1).All(t => t.Cross.activeSelf)
            || tiles.Where(t => t.X == 2).All(t => t.Cross.activeSelf)
            || tiles.Where(t => t.Y == 0).All(t => t.Cross.activeSelf)
            || tiles.Where(t => t.Y == 1).All(t => t.Cross.activeSelf)
            || tiles.Where(t => t.Y == 2).All(t => t.Cross.activeSelf)
            || tiles.Where(t => (t.Y == 0 && t.X == 0) || (t.Y == 1 && t.X == 1) || (t.Y == 2 && t.X == 2)).All(t => t.Cross.activeSelf)
            || tiles.Where(t => (t.Y == 0 && t.X == 2) || (t.Y == 1 && t.X == 1) || (t.Y == 2 && t.X == 0)).All(t => t.Cross.activeSelf);

        if (enemyWon)
        {
            tiles.ForEach(t => t.Ready = false);
            tiles.ForEach(t => t.Cross.GetComponent<MeshRenderer>().material = Win);

            Timer.Create(1f, () =>
            {
                tiles.ForEach(t => t.Reset());
                tiles.ForEach(t => t.Ready = true);
            });
        }
    }
}
