using Godot;
using System;
public partial class Mist : Area2D {
    [Export] AnimatedSprite2D animatedMist;
    Timer Timer;
    public override void _Ready(){
        animatedMist.Visible = false;
        animatedMist.Play("default");
        BodyEntered += ShowMist;
        InicializarTimer();
    }
    void ShowMist(Node2D body) {
        if (body is CharacterBody2D) {
            animatedMist.Visible = true;
            Timer.Start();
        }
    }
    private void InicializarTimer() {
        Timer = new Timer();
        Timer.WaitTime = 10.0f;
        Timer.OneShot = true;
        Timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
        AddChild(Timer);
    }
    private void OnTimerTimeout() {
        animatedMist.Visible = false;
    }
}