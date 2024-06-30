using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Maze
{
  public bool[] cells;
  public int width;
  public int height;

  public Maze(int width, int height)
  {
    this.width = width;
    this.height = height;
    cells = new bool[width * height];
    for (int i = 0; i < width * height; i++)
    {
      cells[i] = true;
    }
  }

  public bool Get(int x, int y)
  {
    return cells[y * width + x];
  }

  public void Set(int x, int y, bool value)
  {
    cells[y * width + x] = value;
  }

  public void GenerateRecursive(int x, int y)
  {
    Set(x, y, false);
    Vector2[] directions = new Vector2[] { Vector2.right, Vector2.up, Vector2.left, Vector2.down };
    directions = directions.OrderBy(x => Random.value).ToArray();
    foreach (Vector2 direction in directions)
    {
      int nx = x + (int)direction.x * 2;
      int ny = y + (int)direction.y * 2;
      if (nx >= 0 && nx < width && ny >= 0 && ny < height && Get(nx, ny))
      {
        Set(x + (int)direction.x, y + (int)direction.y, false);
        GenerateRecursive(nx, ny);
      }
    }
  }

  public IEnumerable<Vector2> GetWalls()
  {
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        if (Get(x, y))
        {
          yield return new Vector2(x, y);
        }
      }
    }
  }
}
