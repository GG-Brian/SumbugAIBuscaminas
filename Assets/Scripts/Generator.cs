using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Generator : MonoBehaviour {

    public static Generator matrixGenerator;
    public GameObject Piece;
    public int width, height, bombNumber;
    public GameObject[][] matrix;

    void Start() {
        matrixGenerator = this; //Piece (Place.cs) needs to access it to GetBombsAround()
        matrix = new GameObject[width][];
        for (int currentHeight = 0; currentHeight < matrix.Length; currentHeight++) {
            matrix[currentHeight] = new GameObject[height];
        }
        for (int axisX = 0; axisX < width; axisX++) {
            for (int axisY = 0; axisY < height; axisY++) {
                matrix[axisX][axisY] = Instantiate(Piece, new Vector2(axisX, axisY), Quaternion.identity);
                matrix[axisX][axisY].GetComponent<Place>().x = axisX;
                matrix[axisX][axisY].GetComponent<Place>().y = axisY;
            }
        }
        Vector3 matrixCentered = new(width/2, height/2, -10);
        Camera.main.transform.position = matrixCentered;

        SetBombs();
    }

    public void SetBombs() {
        for (int bomb = 0; bomb < bombNumber; bomb++) {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            bool isBomb = matrix[x][y].GetComponent<Place>().bomb;
            if (!isBomb)  matrix[x][y].GetComponent<Place>().bomb = true;
            else bomb--;
        }
    }

    public int GetBombsAround(int x, int y) { // coordenate axis of Clicked-On Piece
        int bombsFound = 0;
        // if spot is far from matrix end -1 (so last piece of that side)  // Check if side piece has bomb
        // Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight
        if (x < width - 1                   && matrix[x + 1][y]    .GetComponent<Place>().bomb) bombsFound++;
        if (x > 0                           && matrix[x - 1][y]    .GetComponent<Place>().bomb) bombsFound++;
        if (                 y < height - 1 && matrix[x]    [y + 1].GetComponent<Place>().bomb) bombsFound++;
        if (y > 0                           && matrix[x]    [y - 1].GetComponent<Place>().bomb) bombsFound++;
        if (x > 0 && y < height - 1         && matrix[x - 1][y + 1].GetComponent<Place>().bomb) bombsFound++;
        if (x < width - 1 && y < height - 1 && matrix[x + 1][y + 1].GetComponent<Place>().bomb) bombsFound++;
        if (x > 0 && y > 0                  && matrix[x - 1][y - 1].GetComponent<Place>().bomb) bombsFound++;
        if (x < width - 1 && y > 0          && matrix[x + 1][y - 1].GetComponent<Place>().bomb) bombsFound++;
        return bombsFound;
    }
}
