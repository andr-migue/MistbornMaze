using System;
using Godot;
public partial class Chaos : CharacterBody2D {
    [Export] Movement movement;
    Vector2 inputVector;
    [Export] AnimatedSprite2D animatedSprite;
    private bool IsHability = true;
    private Timer Timer;
    [Export] Label Cooldown;
    public override void _Ready() {
        movement.setup(this);
        InicializarTimer();
    }
    public override void _Process(double delta) {
        Animation();
        if (!Timer.IsStopped()) {
            Cooldown.Text = $"Cooldown: {Timer.TimeLeft:F1}";
        }
    }
    public override void _PhysicsProcess(double delta) {
        CheckParent();
        movement.move(inputVector.Normalized());
    }
    private void Animation() {
        CheckParent();
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
    void CheckParent(){        
        Node parent = GetParent();
        if (parent is Player1 player1) inputVector = player1.InputVector;
        else if (parent is Player2 player2) inputVector = player2.InputVector;
    }
    public void Habilidad() {
        if (IsHability) {
            Teleport();
            IsHability = false;
            Timer.Start();
        }
    }
    void Teleport(){
        Random r = new Random();
        int x = 0;
        int y = 0;
        while (GlobalData.IntBoard[x, y] != 0){
            x = r.Next(1, GlobalData.Filas - 1);
            y = r.Next(1, GlobalData.Columnas - 1);
        }
        Position = new Vector2(y * 64, x * 64);
    }
    private void InicializarTimer() {
        Timer = new Timer();
        Timer.WaitTime = 5.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout2)));
        AddChild(Timer);
    }
    private void OnTimerTimeout2() {
        IsHability = true;
        Cooldown.Visible = false;
    }
}