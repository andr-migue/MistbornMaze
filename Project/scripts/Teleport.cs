using Godot;
using System;
public partial class Teleport : Area2D {
    [Export] AnimatedSprite2D animatedTeleport;
    public override void _Ready(){
        animatedTeleport.Play("default");
        BodyEntered += Respawn;
    }
    void Respawn(Node2D body) {
        if (body is CharacterBody2D character) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            character.GlobalPosition = new Vector2(y * 64, x * 64);
        }
    }
}