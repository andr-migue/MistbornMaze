using Godot;
using System;
public partial class Gema : Area2D {
    [Export] AnimatedSprite2D animatedGema;
    public override void _Ready(){
        animatedGema.Play("default");
        BodyEntered += CatchGema;
    }
    void CatchGema(Node2D body) {
        Node2D parent = (Node2D)body.GetParent();
        if (parent is Player1) GlobalData.Score1 += 1;
        if (parent is Player2) GlobalData.Score2 += 1;
        Respawn();
    }
    void Respawn() {
        Random r = new Random();
        int x = 0;
        int y = 0;
        while (GlobalData.IntBoard[x, y] != 0) {
            x = r.Next(1, GlobalData.Filas - 1);
            y = r.Next(1, GlobalData.Columnas - 1);
        }
        Position = new Vector2(y * 64, x * 64);
    }
}