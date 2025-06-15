using Godot;
using System;
public partial class Heart : Area2D{
    [Export] AnimatedSprite2D animatedHeart;
    public override void _Ready() {
        animatedHeart.Play("default");
        AreaEntered += Heal;
    }
    void Heal(Area2D body) {
        if (body is HealthBox healthBox) {
            healthBox.TakeHealth(100);
            Respawn();
        }
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