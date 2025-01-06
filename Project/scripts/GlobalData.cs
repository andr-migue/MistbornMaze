using Godot;
using System.Collections.Generic;
public partial class GlobalData : Node {
    // Autoload GlobalData para almacenar variables globales.
    public static int Filas;
    public static int Columnas;
    public static int[,] IntBoard;
    public static PackedScene Player1Scene;
    public static PackedScene Player2Scene;
    public static int Score1;
    public static int Score2;
    public static int Traps;
    public static int Fire;
    public static int Teleport;
    public static int Mist;
    public static int Gema;
    public static int Heart;
    public static int Wolf;
    public static int Spectre;
    public static int skeleton;
    public static List<string> SongsMenu = new List<string>();
    public static List<string> SongsGame = new List<string>();
    public override void _Ready() {
        // Cargar Lista de Reproduccion del menu
        SongsMenu.Add("res://soundtrack/startmenu/01 Baldur's Gate 3 OST - Main Theme Part I.mp3");
        SongsMenu.Add("res://soundtrack/startmenu/02 Baldur's Gate 3 OST - Main Theme Part II.mp3");
        SongsMenu.Add("res://soundtrack/startmenu/04  Baldur's Gate 3 OST - Who Are You.mp3");
        SongsMenu.Add("res://soundtrack/startmenu/06 Baldur's Gate 3 OST - Quest For A Cure.mp3");
        SongsMenu.Add("res://soundtrack/startmenu/08  Baldur's Gate 3 OST - Harpy Song.mp3");
        SongsMenu.Add("res://soundtrack/startmenu/41 Baldur's Gate 3 OST - Main Theme Part III.mp3");
        SongsMenu.Add("res://soundtrack/startmenu/42 Baldur's Gate 3 OST - I Want To Live.mp3");
        SongsMenu.Add("res://soundtrack/startmenu/43 Baldur's Gate 3 OST  - The Power (Credits Song).mp3");
        // Cargar Lista de Reproduccion del Juego
        SongsGame.Add("res://soundtrack/ingame/03  Baldur's Gate 3 OST - Mind Flayer Theme.mp3");
        SongsGame.Add("res://soundtrack/ingame/05 Baldur's Gate 3 OST - Nine Blades.mp3");
        SongsGame.Add("res://soundtrack/ingame/07 Baldur's Gate 3 OST - Lead Your Fights.mp3");
        SongsGame.Add("res://soundtrack/ingame/26 Baldur's Gate 3 OST - The Odds Are Cast Anew.mp3");
        SongsGame.Add("res://soundtrack/ingame/31 Baldur's Gate 3 OST - Song Of Balduran.mp3");
        SongsGame.Add("res://soundtrack/ingame/32 Baldur's Gate 3 OST - The Legacy Of Bhaal.mp3");
        SongsGame.Add("res://soundtrack/ingame/36 Baldur's Gate 3 OST - Raphael's Final Act.mp3");
        SongsGame.Add("res://soundtrack/ingame/37 Baldur's Gate 3 OST - The Grand Design (Requiem).mp3");
        Filas = 33;
        Columnas = 33;
        Traps = 10;
        Fire =3;
        Teleport = 1;
        Mist = 5;
        Gema = 3;
        Heart = 1;
        Wolf = 2;
        skeleton = 5;
        Spectre = 1;
        Score1 = 0;
        Score2 = 0;
        Player1Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Rain.tscn");
        Player2Scene = GD.Load<PackedScene>("res://scenes/playable_characters/Lasswell.tscn");
        SoundManager.Play(SongsMenu);
    }
}