using Godot;
using System;
public partial class GlobalData : Node{
    public static int Filas;
    public static int Columnas;
    public static PackedScene Player1Scene;
    public static PackedScene Player2Scene;
    public static int Score1;
    public static int Score2;
    public override void _Ready() {
        Filas = 30;
        Columnas = 30;
        Player1Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Cleome.tscn");
        Player2Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Lasswell.tscn");
    }
}