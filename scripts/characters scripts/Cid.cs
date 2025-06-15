using System;
using Godot;
public partial class Cid : CharacterBody2D {
    [Export] Movement movement;
    [Export] Label Cooldown;
    [Export] AnimatedSprite2D animatedSprite;
    private Vector2 inputVector;
    private Timer Timer;
    private bool IsHability = true;
    public override void _Ready() {
        movement.setup(this);
        InitTimer();
    }
    public override void _Process(double delta) {
        Animation();
        if (!Timer.IsStopped()) Cooldown.Text = $"Cooldown: {Timer.TimeLeft:F1}";
    }
    public override void _PhysicsProcess(double delta) {
        CheckInputVector();
        // Ejecutar movimiento de acuerdo al Vector.
        movement.move(inputVector.Normalized());
    }
    private void Animation() {
        CheckInputVector();
        // Ejecutar animación de acuerdo al Vector.
        if (inputVector.Length() > 0) {
            if (inputVector.Y != 0) {
                if (inputVector.Y < 0) animatedSprite.Play("move_up");
                else if (inputVector.Y > 0) animatedSprite.Play("move_down");
            } 
            else {
                if (inputVector.X < 0) animatedSprite.Play("move_left");
                else if (inputVector.X > 0) animatedSprite.Play("move_right");
            }
        } 
        else animatedSprite.Play("stop");
    }
    void CheckInputVector() {
        // Obtener Vector de movimiento correspondiente.
        Node parent = GetParent();
        if (parent is Player1 player1) inputVector = player1.InputVector;
        else if (parent is Player2 player2) inputVector = player2.InputVector;
    }
    public void Hability() {
        if (IsHability) {
            Teleport();
            IsHability = false;
            Cooldown.Visible = true;
            Timer.Start();
        }
    }
    void Teleport() {
        Random r = new Random();
        int x = 0;
        int y = 0;
        while (GlobalData.IntBoard[x, y] != 0) {
            x = r.Next(1, GlobalData.Filas - 1);
            y = r.Next(1, GlobalData.Columnas - 1);
        }
        GlobalPosition = new Vector2(y * 64, x * 64);
    }
    private void InitTimer() {
        Timer = new Timer();
        Timer.WaitTime = 5.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(Timeout)));
        AddChild(Timer);
    }
    private void Timeout() {
        IsHability = true;
        Cooldown.Visible = false;
    }
}