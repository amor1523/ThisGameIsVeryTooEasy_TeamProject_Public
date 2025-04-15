using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator instance; // 싱글톤 인스턴스

    public int width = 10; // 미로의 너비
    public int height = 10; // 미로의 높이
    public float wallHeight = 1.0f; // 벽의 높이
    public Vector3 mazeStartPosition = Vector3.zero; // 미로 생성 시작 위치
    public GameObject exitTriggerPrefab; // 출구 트리거 오브젝트
    public float mazeScale = 1.0f; // 미로의 스케일 조절 변수
    private int[,] maze; // 미로 배열
    private Vector2Int entrance; // 입구 위치
    private Vector2Int exit; // 출구 위치

    void Awake()
    {
        // 싱글톤 인스턴스 초기화
        instance = this;
    }

    void Start()
    {
        // 미로 생성, 입구 및 출구 설정, 미로 그리기
        GenerateMaze();
        SetEntrance();
        SetExit();
        DrawMaze();
    }

    // 미로 생성
    void GenerateMaze()
    {
        maze = new int[width, height];
        System.Random rand = new System.Random();
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int startPos = new Vector2Int(1, 1); // 내부에서 시작
        maze[startPos.x, startPos.y] = 1;

        stack.Push(startPos);

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();
            List<Vector2Int> neighbors = new List<Vector2Int>();

            // 각 방향 (왼쪽, 오른쪽, 위, 아래) 체크
            if (current.x - 2 > 0 && maze[current.x - 2, current.y] == 0)
                neighbors.Add(new Vector2Int(current.x - 2, current.y));
            if (current.x + 2 < width - 1 && maze[current.x + 2, current.y] == 0)
                neighbors.Add(new Vector2Int(current.x + 2, current.y));
            if (current.y - 2 > 0 && maze[current.x, current.y - 2] == 0)
                neighbors.Add(new Vector2Int(current.x, current.y - 2));
            if (current.y + 2 < height - 1 && maze[current.x, current.y + 2] == 0)
                neighbors.Add(new Vector2Int(current.x, current.y + 2));

            if (neighbors.Count > 0)
            {
                stack.Push(current);
                Vector2Int chosen = neighbors[rand.Next(neighbors.Count)];
                maze[chosen.x, chosen.y] = 1;
                maze[(current.x + chosen.x) / 2, (current.y + chosen.y) / 2] = 1;
                stack.Push(chosen);
            }
        }
    }

    // 입구 설정
    void SetEntrance()
    {
        entrance = new Vector2Int(0, height / 2);
        maze[entrance.x, entrance.y] = 1;
        maze[entrance.x + 1, entrance.y] = 1; // 입구 옆도 길로 만들어서 플레이어가 통과할 수 있게 함
    }

    // 출구 설정
    void SetExit()
    {
        System.Random rand = new System.Random();
        bool exitFound = false;
        while (!exitFound)
        {
            exit = new Vector2Int(rand.Next(1, width - 1), rand.Next(1, height - 1));

            if (exit.x != 0 && exit.x != width - 1 && exit.y != 0 && exit.y != height - 1)
            {
                if (maze[exit.x, exit.y] == 0)
                {
                    exitFound = true;
                    maze[exit.x, exit.y] = 1;
                }
            }
        }

        // 출구 트리거 오브젝트 생성 및 설정
        GameObject exitTrigger = Instantiate(exitTriggerPrefab, new Vector3(exit.x, 0.0f, exit.y) * mazeScale + mazeStartPosition, Quaternion.identity);
        exitTrigger.transform.SetParent(transform);

        // 출구 트리거의 크기를 원래 크기로 유지
        exitTrigger.transform.localScale = Vector3.one;
    }

    // 미로 그리기
    void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0)
                {
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = new Vector3(x, wallHeight / 2, y) * mazeScale + mazeStartPosition;
                    wall.transform.localScale = new Vector3(1.0f, wallHeight, 1.0f) * mazeScale; // 벽의 크기 조절
                    wall.transform.SetParent(transform);
                }
            }
        }
    }

    // 입구 위치 반환
    public Vector3 GetEntrancePosition()
    {
        return new Vector3(entrance.x, 0, entrance.y) * mazeScale + mazeStartPosition;
    }
}