using Godot;
public partial class SelectionPlayer : Control { 
    // Clase que gestiona la pantalla de seleccionar personajes.
    [Export] Node2D position1;
    [Export] Node2D position2;
    private PackedScene[] showCharacters = new PackedScene[6];
    private PackedScene[] playableCharacters = new PackedScene[6];
    private Node2D current1;
    private Node2D current2;
    private int index1 = 4;
    private int index2 = 2;
    public override void _Ready() {
        // Cargar escenas para visualizar en el selector
        showCharacters[0] = GD.Load<PackedScene>("res://scenes/show_characters/chaos.tscn");
        showCharacters[1] = GD.Load<PackedScene>("res://scenes/show_characters/cleome.tscn");
        showCharacters[2] = GD.Load<PackedScene>("res://scenes/show_characters/lasswell.tscn");
        showCharacters[3] = GD.Load<PackedScene>("res://scenes/show_characters/lid.tscn");
        showCharacters[4] = GD.Load<PackedScene>("res://scenes/show_characters/rain.tscn");
        showCharacters[5] = GD.Load<PackedScene>("res://scenes/show_characters/sophie.tscn");
        // Cargar personajes jugables
        playableCharacters[0] = GD.Load<PackedScene>("res://scenes/playable_characters/Chaos.tscn");
        playableCharacters[1] = GD.Load<PackedScene>("res://scenes/playable_characters/Cleome.tscn");
        playableCharacters[2] = GD.Load<PackedScene>("res://scenes/playable_characters/Lasswell.tscn");
        playableCharacters[3] = GD.Load<PackedScene>("res://scenes/playable_characters/Lid.tscn");
        playableCharacters[4] = GD.Load<PackedScene>("res://scenes/playable_characters/Rain.tscn");
        playableCharacters[5] = GD.Load<PackedScene>("res://scenes/playable_characters/Sophie.tscn");
        current1 = showCharacters[index1].Instantiate<Node2D>();
        current2 = showCharacters[index2].Instantiate<Node2D>();
        AddChild(current1);
        AddChild(current2);
        UpdateCharacterPositions();
    }
    private void UpdateCharacterPositions() {
        current1.Position = position1.Position;
        current2.Position = position2.Position;
    }
    public void PressLeft1() {
        index1 = (index1 - 1 + 6) % 6;
        UpdateCurrentCharacter(ref current1, index1);
    }
    public void PressRight1() {
        index1 = (index1 + 1) % 6;
        UpdateCurrentCharacter(ref current1, index1);
    }
    public void PressAccept1() {
        GlobalData.Player1Scene = playableCharacters[index1];
}
    public void PressLeft2() {
        index2 = (index2 - 1 + 6) % 6;
        UpdateCurrentCharacter(ref current2, index2);
    }
    public void PressRight2() {
        index2 = (index2 + 1) % 6;
        UpdateCurrentCharacter(ref current2, index2);
    }
    public void PressAccept2() {
        GlobalData.Player2Scene = playableCharacters[index2];
}
    private void UpdateCurrentCharacter(ref Node2D current, int index) {
        // Cambiar personaje actual accediendo a la variable current por referencia.
        if (current != null) current.QueueFree();
        current = showCharacters[index].Instantiate<Node2D>();
        AddChild(current);
        UpdateCharacterPositions();
    }
    public void PressBack() {
        GetTree().ChangeSceneToFile("res://scenes/start_menu.tscn");
    }
}