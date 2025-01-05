using Godot;
public partial class SelectionPlayer : Control { 
    [Export] Node2D position1;
    [Export] Node2D position2;
    private PackedScene[] characterScenes = new PackedScene[6];
    private PackedScene[] playablecharacters = new PackedScene[6];
    private Node2D currentPlayer1Character;
    private Node2D currentPlayer2Character;
    private int currentPlayer1Index = 4;
    private int currentPlayer2Index = 2;
    public override void _Ready() {
        // Cargar escenas para la imagen
        characterScenes[0] = GD.Load<PackedScene>("res://scenes/show_characters/chaos.tscn");
        characterScenes[1] = GD.Load<PackedScene>("res://scenes/show_characters/cleome.tscn");
        characterScenes[2] = GD.Load<PackedScene>("res://scenes/show_characters/lasswell.tscn");
        characterScenes[3] = GD.Load<PackedScene>("res://scenes/show_characters/lid.tscn");
        characterScenes[4] = GD.Load<PackedScene>("res://scenes/show_characters/rain.tscn");
        characterScenes[5] = GD.Load<PackedScene>("res://scenes/show_characters/sophie.tscn");
        // Cargar personaje jugable
        playablecharacters[0] = GD.Load<PackedScene>("res://scenes/playable_characters/Chaos.tscn");
        playablecharacters[1] = GD.Load<PackedScene>("res://scenes/playable_characters/Cleome.tscn");
        playablecharacters[2] = GD.Load<PackedScene>("res://scenes/playable_characters/Lasswell.tscn");
        playablecharacters[3] = GD.Load<PackedScene>("res://scenes/playable_characters/Lid.tscn");
        playablecharacters[4] = GD.Load<PackedScene>("res://scenes/playable_characters/Rain.tscn");
        playablecharacters[5] = GD.Load<PackedScene>("res://scenes/playable_characters/Sophie.tscn");
        currentPlayer1Character = characterScenes[currentPlayer1Index].Instantiate<Node2D>();
        currentPlayer2Character = characterScenes[currentPlayer2Index].Instantiate<Node2D>();
        AddChild(currentPlayer1Character);
        AddChild(currentPlayer2Character);
        UpdateCharacterPositions();
    }
    private void UpdateCharacterPositions(){
        currentPlayer1Character.Position = position1.Position;
        currentPlayer2Character.Position = position2.Position;
    }
    public void PressLeft1(){
        currentPlayer1Index = (currentPlayer1Index - 1 + characterScenes.Length) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer1Character, currentPlayer1Index);
    }
    public void PressRight1(){
        currentPlayer1Index = (currentPlayer1Index + 1) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer1Character, currentPlayer1Index);
    }
    public void PressAccept1(){
        GlobalData.Player1Scene = playablecharacters[currentPlayer1Index];
}
    public void PressLeft2(){
        currentPlayer2Index = (currentPlayer2Index - 1 + characterScenes.Length) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer2Character, currentPlayer2Index);
    }
    public void PressRight2(){
        currentPlayer2Index = (currentPlayer2Index + 1) % characterScenes.Length;
        UpdateCurrentCharacter(ref currentPlayer2Character, currentPlayer2Index);
    }
    public void PressAccept2(){
        GlobalData.Player2Scene = playablecharacters[currentPlayer2Index];
}
    private void UpdateCurrentCharacter(ref Node2D currentCharacter, int index){
        if (currentCharacter != null) currentCharacter.QueueFree();
        currentCharacter = characterScenes[index].Instantiate<Node2D>();
        AddChild(currentCharacter);
        UpdateCharacterPositions();
    }
    public void PressBack(){
        GetTree().ChangeSceneToFile("res://scenes/start_menu.tscn");
    }
}