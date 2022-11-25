using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    private const int WALL_W = 27;
    private const int WALL_H = 27;
    private const float INIT_X = 0.0f;
    private const float INIT_Y = -2.7f; 
    [SerializeField] private GameObject brick;
    [SerializeField] private Material[] colors;

    private int[,] wall = new int[WALL_W, WALL_H];

    void Start()
    {
        GeneratingWall();
    }

    private void GeneratingWall() 
    {
        float width = brick.transform.localScale.x;
        float height = brick.transform.localScale.y;

        for(int x = 0; x < WALL_W; x++) {
            for(int y = 0; y < WALL_H; y++) {
                GameObject curObject = Instantiate(brick, new Vector3(INIT_X + width * x, INIT_Y + height * y, 0), Quaternion.identity);
                
                int color = Random.Range(0, colors.Length);
                curObject.GetComponent<MeshRenderer>().material = colors[color];
            }
        }
    }
}
