using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public static int[] sizes = new int[] { 5, 7, 9, 13, 17, 21, 25, 27, 31, 45 };

    public int level
    {
        get
        {
            return PlayerPrefs.GetInt("level", 0) % MazeGenerator.sizes.Length;
        }
    }

    public int width
    {
        get { return MazeGenerator.sizes[this.level]; }
    }
    public int height
    {
        get { return MazeGenerator.sizes[this.level]; }
    }
    public GameObject wallPrefab;  // Prefab for the wall

    int endY = 0; // End position of the maze at left edge
    int startY // Start position of the maze at right edge
    {
        get { return this.height - 1; }
    }

    public Camera mainCamera; // Main camera

    public GameObject playerPrefab; // Prefab for the player

    public GameObject enderPrefab; // Prefab for the ender

    void Start()
    {
        Debug.Log(this.level);
        Maze maze = new Maze(width, height);
        maze.GenerateRecursive(0, startY);
        CreateMazeWalls(maze);
        PositionCamera(width, height);
        CreatePlayer();
        CreateEnder();
    }

    void CreateMazeWalls(Maze maze)
    {
        foreach (Vector2 wallPosition in maze.GetWalls())
        {
            Vector3 wallPosition3D = new Vector3(wallPosition.x, 0, wallPosition.y);
            Instantiate(wallPrefab, wallPosition3D, Quaternion.identity);
        }

        // Create walls around the maze
        for (int x = -1; x <= width; x++)
        {
            Instantiate(wallPrefab, new Vector3(x, 0, -1), Quaternion.identity);
            Instantiate(wallPrefab, new Vector3(x, 0, height), Quaternion.identity);
        }
        for (int y = 0; y < height; y++)
        {
            Instantiate(wallPrefab, new Vector3(-1, 0, y), Quaternion.identity);

            if (y != endY)
            {
                Instantiate(wallPrefab, new Vector3(width, 0, y), Quaternion.identity);
            }
        }
    }

    void PositionCamera(int width, int height)
    {
        mainCamera.transform.position = new Vector3(width / 2, 10, height / 2);
        mainCamera.orthographicSize = Mathf.Max(width, height) / 2 + 3;
    }

    void CreatePlayer()
    {
        Instantiate(playerPrefab, new Vector3(0, 0, startY), Quaternion.identity);
    }

    void CreateEnder()
    {
        Instantiate(enderPrefab, new Vector3(width, 0, endY), Quaternion.identity);
    }
}

