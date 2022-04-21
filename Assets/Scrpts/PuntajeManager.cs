using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntajeManager : MonoBehaviour
{
    public Text _PuntajeTexto;
    public Text _ContTexto;
    public Player _Player;

    // Update is called once per frame
    void Update()
    {
       _PuntajeTexto.text = "Puntaje: " + _Player.puntaje;
       _ContTexto.text = "Oro: " + _Player.GoldCoin + " Plata: " + _Player.SilverCoin + " Bronce: " + _Player.BronceCoin;
    }
}
