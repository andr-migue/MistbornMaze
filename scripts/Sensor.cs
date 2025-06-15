using Godot;
using System.Collections.Generic;
public partial class Sensor : Area2D {
    // Clase para determinar hacia que objetivo se moverán las entidades que tengan este nodo asociado.
    public CharacterBody2D target;
    public float targetDistance => GetDistance(); //lambda
    public Vector2 targetDirection => GetDirection();
    private List<Node2D> collisions = new List<Node2D>(); 
    public override void _Ready() {
        BodyEntered += StoreBody;
        BodyExited += RemoveBody;
    }
    public override void _PhysicsProcess(double delta) {
        Scan();
    }
    private float GetDistance() {
        return target != null ? GlobalPosition.DistanceTo(target.GlobalPosition) : float.MaxValue;
    }
    private Vector2 GetDirection() {
        return target != null ? target.GlobalPosition - GlobalPosition : Vector2.Zero;
    }
    private void StoreBody(Node2D body) {
        collisions.Add(body);
        if (body is CharacterBody2D character) {
            target = character;
        }
    }
    private void RemoveBody(Node2D body) {
        collisions.Remove(body);
        if (collisions.Count == 0) {
            target = null;
        }
    }
    private void Scan() {
        if (collisions.Count == 0) {
            target = null;
            return;
        }
        Node2D closestBody = ClosestBody(collisions);
        target = closestBody as CharacterBody2D;
    }
    private Node2D ClosestBody(List<Node2D> collisions) {
        // Obtener el personaje más cercano
        float closestDistance = float.MaxValue;
        Node2D closestBody = null;
        foreach (Node2D body in collisions) {
            if (body is CharacterBody2D character) {
                float distance = GlobalPosition.DistanceTo(character.GlobalPosition);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestBody = character;
                }
            }
        }
        return closestBody;
    }
}