using Godot;
using System.Collections.Generic;
public partial class PlayerSensor : Area2D {
    public List<Node2D> colisiones = new List<Node2D>();
    public override void _Ready() {
        this.BodyEntered += store_body;
        this.BodyExited += remove_body;
    }
    private void store_body(Node2D body) {
        colisiones.Add(body);
    }
    private void remove_body(Node2D body) {
        colisiones.Remove(body);
    }
}