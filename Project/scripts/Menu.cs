using Godot;
public partial class Menu : Control {
    [Export] LineEdit lineEdit1;
    [Export] LineEdit lineEdit2;
    public override void _Ready() {
        lineEdit1.Text = GlobalData.Filas + "";
        lineEdit2.Text = GlobalData.Columnas + "";
    }
    public void PressPlay() {
        GetTree().ChangeSceneToFile("res://scenes/intro.tscn");
    }
    void ChangeRows(string text) {
        if (IsNumber(text)) GlobalData.Filas = int.Parse(text);
    }
    void ChangeCollumns(string text) {
        if (IsNumber(text)) GlobalData.Columnas = int.Parse(text);
    }
    public void PressSelectPlayer() {
        GetTree().ChangeSceneToFile("res://scenes/selection_player.tscn");
    }
    public void PressExit() {
        GetTree().Quit();
    }
    private bool IsNumber(string text) {
        // Verificar si el texto ingresado es un número.
        if (string.IsNullOrEmpty(text)) return false;
        for (int i = 0; i < text.Length; i++) {
            if (char.IsDigit(text[i])) return true;
        }
        return false;
    }
}