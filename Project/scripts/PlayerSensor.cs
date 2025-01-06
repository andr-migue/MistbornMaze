using Godot;
using System.Collections.Generic;
public partial class PlayerSensor : Area2D {
    public List<Node2D> collisions = new List<Node2D>();
    public override void _Ready() {
        BodyEntered += StoreBody;
        BodyExited += RemoveBody;
    }
    private void StoreBody(Node2D body) {
        collisions.Add(body);
    }
    private void RemoveBody(Node2D body) {
        collisions.Remove(body);
    }
}