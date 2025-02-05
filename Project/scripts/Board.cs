using Godot;
using System;
using System.Collections.Generic;
public partial class Board : Node2D {
    // Clase Board para la generacion del mapa del juego.
    [Export] PackedScene path;
    [Export] PackedScene wall1;
    [Export] PackedScene wall2;
    [Export] PackedScene wall3;
    [Export] PackedScene wall4;
    [Export] PackedScene wall5;
    [Export] PackedScene player1;
    [Export] PackedScene player2;
    [Export] CanvasModulate ilumination;
    public Player1 Player1Instance;
    public Player2 Player2Instance;
    private List<PackedScene> walls = new List<PackedScene>();
    private Color modulateColor;
    private int iluminationCount = 0;
    private int Filas;
    private int Columnas;
    private int TrapCount;
    private int FireCount;
    private int TeleportCount;
    private int MistCount;
    private int GemaCount;
    private int HeartCount;
    private int WolfCount;
    private int SpectreCount;
    private int SkeletonCount;
    public override void _Ready() {
        walls.Add(wall1);
        walls.Add(wall2);
        walls.Add(wall3);
        walls.Add(wall4);
        walls.Add(wall5);
        Filas = GlobalData.Filas;
        Columnas = GlobalData.Columnas;
        int[,] IntBoard = GenerateIntBoard(Filas, Columnas);
        GlobalData.IntBoard = IntBoard;
        Node2D[,] NodeBoard = GenerateNodeBoard(IntBoard);
        PrintBoard(NodeBoard);
        PlacePlayers();
        Place(TrapCount, GlobalData.Traps, "res://scenes/trap.tscn");
        Place(FireCount, GlobalData.Fire, "res://scenes/fire.tscn");
        Place(TeleportCount, GlobalData.Teleport, "res://scenes/teleport.tscn");
        Place(MistCount, GlobalData.Mist, "res://scenes/mist.tscn");
        Place(GemaCount, GlobalData.Gema, "res://scenes/gema.tscn");
        Place(HeartCount, GlobalData.Heart, "res://scenes/heart.tscn");
        Place(WolfCount, GlobalData.Wolf, "res://scenes/wolf.tscn");
        Place(SpectreCount, GlobalData.Spectre, "res://scenes/spectre.tscn");
        Place(SkeletonCount, GlobalData.skeleton, "res://scenes/skeleton.tscn");
    }
    public override void _Process(double delta) {
        if (Input.IsActionJustPressed("DecreaseIlumination")) DecreaseIlumination();
        if (Input.IsActionJustPressed("IncreaseIlumination")) IncreaseIlumination();
    }
    public void DecreaseIlumination() {
        if (iluminationCount > -3) {
            modulateColor = ilumination.Color;
            ilumination.Color = new Color(modulateColor.R - 0.05f, modulateColor.G - 0.05f, modulateColor.B - 0.05f, modulateColor.A);
            iluminationCount--;
        }
    }
    public void IncreaseIlumination() {
        if (iluminationCount < 3) {
            modulateColor = ilumination.Color;
            ilumination.Color = new Color(modulateColor.R + 0.05f, modulateColor.G + 0.05f, modulateColor.B + 0.05f, modulateColor.A);
            iluminationCount++;
        }
    }
    public static int[,] GenerateIntBoard(int filas, int columnas) {
        // Crear matriz de enteros
        int[,] IntBoard = new int[filas, columnas];
        // Inicializar todas las celdas como paredes
        for (int i = 0; i < filas; i++) {
            for (int j = 0; j < columnas; j++) {
                IntBoard[i, j] = 1;
            }
        }
        // Crear un laberinto utilizando DFS
        Random r = new Random();
        CreateMaze(IntBoard, r, 4, 4);
        // Asegurar accesibilidad de esta posicion
        BeAccesible(IntBoard, filas - 5, columnas - 5);
        // Agregar caminos adicionales aleatorios
        AddRandomPaths(IntBoard, r);
        return IntBoard;
    }
    private static void CreateMaze(int[,] IntBoard, Random r, int x, int y) {
        // Arrays de direcciones
        int[] DirX = { 2, -2, 0,  0 };
        int[] DirY = { 0,  0, 2, -2 };
        // Crear una lista de direcciones
        List<int> directions = new List<int> { 0, 1, 2, 3 };
        // Aleatorizar lista de direcciones
        for (int i = directions.Count - 1; i > 0; i--) {
            int j = r.Next(i + 1);
            int temp = directions[i];
            directions[i] = directions[j];
            directions[j] = temp;
        }
        // Recorrer las direcciones mezcladas
        for (int d = 0; d < directions.Count; d++) {
            int direction = directions[d];
            int NewX = x + DirX[direction];
            int NewY = y + DirY[direction];
            if (IsValid(IntBoard, NewX, NewY) && IntBoard[NewX, NewY] == 1) {
                // Eliminar la pared entre la celda actual y la nueva celda
                IntBoard[x + DirX[direction] / 2, y + DirY[direction] / 2] = 0;
                // Hacer la nueva celda un camino
                IntBoard[NewX, NewY] = 0;
                // Recursión en la nueva celda
                CreateMaze(IntBoard, r, NewX, NewY);
            }
        }
    }
    private static void BeAccesible(int[,] IntBoard, int x, int y) {
        // Asegurarse de que la posición (x, y) sea un camino
        if (IntBoard[x, y] != 0) IntBoard[x, y] = 0;
        // Conectar el camino al menos a una celda adyacente que sea un camino
        int[] DirX = { -1, 1,  0, 0 };
        int[] Diry = {  0, 0, -1, 1 };
        for (int d = 0; d < DirX.Length; d++) {
            int NewX = x + DirX[d];
            int NewY = y + Diry[d];
            if (IsValid(IntBoard, NewX, NewY) && IntBoard[NewX, NewY] == 0) return;// Ya está conectado a un camino existente
            else if (IsValid(IntBoard, NewX, NewY) && IntBoard[NewX, NewY] == 1) {
                IntBoard[NewX, NewY] = 0; // Convertir a camino
                return;
            }
        }
    }
    private static void AddRandomPaths(int[,] IntBoard, Random r) {
        for (int i = 0; i < IntBoard.GetLength(0); i++) {
            for (int j = 0; j < IntBoard.GetLength(1); j++) {
                if (IntBoard[i, j] == 1 && IsValid(IntBoard, i, j) && r.NextDouble() < 0.2) {
                    IntBoard[i, j] = 0; // Convertir algunas paredes a caminos aleatoriamente (20%)
                }
            }
        }
    }
    public static bool IsValid(int[,] IntBoard,int x,int y) {
        // Verificar si una celda se encuentra en los limites de la matriz
        return x >= 4 && x < IntBoard.GetLength(0) - 4 && y >= 4 && y < IntBoard.GetLength(1) - 4; 
    }
    public Node2D[,] GenerateNodeBoard(int[,] IntBoard) {
        // Generar matriz de nodos.
        Node2D[,] NodeBoard = new Node2D[IntBoard.GetLength(0), IntBoard.GetLength(1)];
        for (int i = 0; i < Filas; i++) {
            for (int j = 0; j < Columnas; j++) {
                if (IntBoard[i, j] == 0) {
                    Node2D PathInstance = path.Instantiate<Node2D>();
                    AddChild(PathInstance);
                    NodeBoard[i, j] = PathInstance;
                }
                if (IntBoard[i, j] == 1) {
                    // Aleatorizar visualización de las paredes
                    Random r = new Random();
                    int index = r.Next(4);
                    PackedScene wall = walls[index];
                    Node2D WallInstance = wall.Instantiate<Node2D>();
                    AddChild(WallInstance);
                    NodeBoard[i, j] = WallInstance;
                }
            }
        }
        return NodeBoard;
    }
    public void PrintBoard(Node2D[,] NodeBoard) {
        // Mostrar cada nodo en su posicion correspondiente.
        for (int i = 0; i < NodeBoard.GetLength(0); i++) {
            for (int j = 0; j < NodeBoard.GetLength(1); j++) {
                NodeBoard[i, j].Position = new Vector2(j * 64, i * 64);
            }
        }
    }
    // Métodos para la generacion de los jugadores, trampas, enemigos e items.
    public void PlacePlayers() {
        Player1Instance = (Player1)player1.Instantiate<Node2D>();
        AddChild(Player1Instance);
        Player2Instance = (Player2)player2.Instantiate<Node2D>();
        AddChild(Player2Instance);
        Player1Instance.Position = new Vector2(4 * 64, 4 * 64);
        Player2Instance.Position = new Vector2((Columnas - 5) * 64, (Filas - 5) * 64);
    }
    public void Place(int count, int globalCount, string path) {
        count = globalCount;
        while (count > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] == 1) {
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            GlobalData.IntBoard[x, y] = 1;
            Node2D Instance = (Node2D)GD.Load<PackedScene>(path).Instantiate();
            Instance.Position = new Vector2(y * 64, x * 64);
            AddChild(Instance);
            count--;
        }
    }
}