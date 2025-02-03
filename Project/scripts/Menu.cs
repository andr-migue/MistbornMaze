using Godot;
public partial class Menu : Control {
    [Export] LineEdit lineEdit1;
    [Export] LineEdit lineEdit2;
    [Export] Control controls;
    [Export] Control Info;
    public override void _Ready() {
        lineEdit1.Text = GlobalData.Filas + "";
        lineEdit2.Text = GlobalData.Columnas + "";
    }
    void PressPlay() {
        GetTree().ChangeSceneToFile("res://scenes/intro.tscn");
    }
    void ChangeRows(string text) {
        if (IsNumber(text) && int.Parse(text) >= 20 && int.Parse(text) <= 80) GlobalData.Filas = int.Parse(text);
        else lineEdit1.Text = GlobalData.Filas + "";
    }
    void ChangeCollumns(string text) {
        if (IsNumber(text) && int.Parse(text) >= 20 && int.Parse(text) <= 80) GlobalData.Columnas = int.Parse(text);
        else lineEdit2.Text = GlobalData.Columnas + "";
    }
    void PressSelectPlayer() {
        GetTree().ChangeSceneToFile("res://scenes/selection_player.tscn");
    }
    void PressControls() {
        controls.Visible = !controls.Visible;
    }
    void PressInfo() {
        Info.Visible = !Info.Visible;
    }
    void PressExit() {
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