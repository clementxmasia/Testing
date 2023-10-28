using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int mazeWidth = 10;
    public int mazeHeight = 10;
    public GameObject wallPrefab; // Cube prefab for walls
    public GameObject floorPrefab; // Cube prefab for floor
    public Transform player; // Player object (optional)
    public Transform endGoal; // End goal object (optional)

    private int[,] mazeGrid;
    private Stack<Vector2> stack = new Stack<Vector2>();
    private List<Vector2> directions = new List<Vector2> { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        mazeGrid = new int[mazeWidth, mazeHeight];
        Vector2 currentPosition = new Vector2(1, 1);
        stack.Push(currentPosition);

        while (stack.Count > 0)
        {
            mazeGrid[(int)currentPosition.x, (int)currentPosition.y] = 1;

            List<Vector2> unvisitedNeighbors = new List<Vector2>();

            foreach (Vector2 direction in directions)
            {
                Vector2 neighbor = currentPosition + direction * 2;

                if (IsInBounds(neighbor) && mazeGrid[(int)neighbor.x, (int)neighbor.y] == 0)
                {
                    unvisitedNeighbors.Add(neighbor);
                }
            }

            if (unvisitedNeighbors.Count > 0)
            {
                stack.Push(currentPosition);
                int randomIndex = Random.Range(0, unvisitedNeighbors.Count);
                Vector2 randomNeighbor = unvisitedNeighbors[randomIndex];
                mazeGrid[(int)randomNeighbor.x, (int)randomNeighbor.y] = 1;
                mazeGrid[(int)((currentPosition.x + randomNeighbor.x) / 2), (int)((currentPosition.y + randomNeighbor.y) / 2)] = 1;
                currentPosition = randomNeighbor;
            }
            else
            {
                currentPosition = stack.Pop();
            }
        }

        InstantiateMazeObjects();
    }

    void InstantiateMazeObjects()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                if (mazeGrid[x, y] == 0)
                {
                    Instantiate(wallPrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                }
                else
                {
                    Instantiate(floorPrefab, new Vector3(x, 0.0f, y), Quaternion.identity);
                }
            }
        }

        if (player != null)
        {
            player.position = new Vector3(0.5f, 0.5f, 0.5f); // Place player at start
        }

        if (endGoal != null)
        {
            endGoal.position = new Vector3(mazeWidth - 0.5f, 0.5f, mazeHeight - 0.5f); // Place end goal at the end
        }
    }

    bool IsInBounds(Vector2 position)
    {
        return position.x >= 0 && position.x < mazeWidth && position.y >= 0 && position.y < mazeHeight;
    }
}
