using Godot;
using System;
public partial class Wolf : CharacterBody2D {
    [Export] Movement movement;
    [Export] AnimatedSprite2D animatedWolf;
    [Export] Sensor sensor;
    public override void _Ready() {
        movement.setup(this);
    }
    public override void _PhysicsProcess(double delta) {
        if (sensor.target != null) {
            if (sensor.target_distance < 2000) {
                movement.move(sensor.target_direction);
                Animation(sensor.target_direction);
            }
        }
    }
    private void Animation(Vector2 direction) {
        if (direction.Length() > 0) {
            if (direction.X != 0) {
                if (direction.X < 0) animatedWolf.Play("move_left");
                else if (direction.X > 0) animatedWolf.Play("move_right");
            }
            else {
                if (direction.Y < 0) animatedWolf.Play("move_up");
                else if (direction.Y > 0) animatedWolf.Play("move_down");
            } 
        } 
        else animatedWolf.Stop();
    }
}