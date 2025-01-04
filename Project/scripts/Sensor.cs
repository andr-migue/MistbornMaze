using Godot;
using System.Collections.Generic;
public partial class Sensor : Area2D {
    public CharacterBody2D target;
    private List<Node2D> colisiones = new List<Node2D>(); 
    public float target_distance => get_distance(); //lambda
    public Vector2 target_direction => target != null ? target.GlobalPosition - GlobalPosition : Vector2.Zero;
    public override void _Ready() {
        BodyEntered += store_body;
        BodyExited += remove_body;
    }
    public override void _PhysicsProcess(double delta) {
        scan();
    }
    private float get_distance() {
        return target != null ? GlobalPosition.DistanceTo(target.GlobalPosition) : float.MaxValue;
    }
    private void store_body(Node2D body) {
        colisiones.Add(body);
        if (body is CharacterBody2D character) {
            target = character;
        }
    }
    private void remove_body(Node2D body) {
        colisiones.Remove(body);
        if (colisiones.Count == 0) {
            target = null;
        }
    }
    private void scan() {
        if (colisiones.Count == 0) {
            target = null;
            return;
        }
        Node2D closest_body = find_closest_body(colisiones);
        target = closest_body as CharacterBody2D;
    }
    private Node2D find_closest_body(List<Node2D> bodies) {
        float closest_distance = float.MaxValue;
        Node2D closest_body = null;
        foreach (Node2D body in bodies) {
            if (body is CharacterBody2D character) {
                float distance = GlobalPosition.DistanceTo(character.GlobalPosition);
                if (distance < closest_distance) {
                    closest_distance = distance;
                    closest_body = character;
                }
            }
        }
        return closest_body;
    }
}