using Godot;
using System;
public partial class GlobalData : Node{
    public static int Filas;
    public static int Columnas;
    public static int[,] IntBoard;
    public static PackedScene Player1Scene;
    public static PackedScene Player2Scene;
    public static int Score1;
    public static int Score2;
    public static int Traps;
    public static int Fire;
    public static int Teleport;
    public static int Mist;
    public static int Gema;
    public static int Heart;
    public static int Wolf;
    public static int Spectre;
    public override void _Ready() {
        Filas = 30;
        Columnas = 30;
        Traps = 10;
        Fire = 5;
        Teleport = 1;
        Mist = 5;
        Gema = 3;
        Heart = 1;
        Wolf = 5;
        Spectre = 1;
        Score1 = 0;
        Score2 = 0;
        Player1Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Rain.tscn");
        Player2Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Lasswell.tscn");
    }
}