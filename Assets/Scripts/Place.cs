using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place : MonoBehaviour {

    public int x, y;
    public bool bomb;

    private void OnMouseDown() {
        if (bomb) GetComponent<SpriteRenderer>().material.color = Color.red;
        //(abajo)Esto > su Canvas > hijo Text >  Componente Text   > texto interno
        else transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
                Generator.matrixGenerator.GetBombsAround(x, y).ToString();
             
    }
}
