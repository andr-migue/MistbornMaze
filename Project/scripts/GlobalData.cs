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
    public override void _Ready() {
        Filas = 20;
        Columnas = 20;
        Traps = 5;
        Player1Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Rain.tscn");
        Player2Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Lasswell.tscn");
    }
}