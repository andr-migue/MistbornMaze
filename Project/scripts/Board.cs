using Godot;
using System;
using System.Collections.Generic;
public partial class Board : Node2D{
    public int Filas;
    public int Columnas;
    [Export] public PackedScene path;
    [Export] public PackedScene wall1;
    [Export] public PackedScene wall2;
    [Export] public PackedScene wall3;
    [Export] public PackedScene wall4;
    [Export] public PackedScene wall5;
    List<PackedScene> walls = new List<PackedScene>();
    [Export] public PackedScene player1;
    [Export] public PackedScene player2;
    public Player1 Player1Instance;
    public Player2 Player2Instance;
    public int TrapCount;
    public int FireCount;
    public int TeleportCount;
    public int MistCount;
    public int GemaCount;
    public int HeartCount;
    public int WolfCount;
    public int SpectreCount;
    public int SkeletonCount;
    public override void _Ready(){
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
        PlaceTraps();
        PlaceFires();
        PlaceTeleport();
        PlaceMist();
        PlaceGema();
        PlaceHeart();
        PlaceWolf();
        PlaceSpectre();
        PlaceSkeleton();
    }
    public static int[,] GenerateIntBoard(int filas, int columnas){
        // Crear matriz de enteros
        int[,] IntBoard = new int[filas, columnas];
        // Inicializar todas las celdas como paredes (1)
        for (int i = 0; i < filas; i++){
            for (int j = 0; j < columnas; j++){
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
    private static void CreateMaze(int[,] IntBoard, Random r, int x, int y){
        // Arrays de direcciones
        int[] DirX = { 2, -2, 0,  0 };
        int[] DirY = { 0,  0, 2, -2 };
        // Crear una lista de direcciones
        List<int> directions = new List<int> { 0, 1, 2, 3 };
        // Aleatorizar lista de direcciones
        for (int i = directions.Count - 1; i > 0; i--){
            int j = r.Next(i + 1);
            int temp = directions[i];
            directions[i] = directions[j];
            directions[j] = temp;
        }
        // Recorrer las direcciones mezcladas
        for (int d = 0; d < directions.Count; d++){
            int direction = directions[d];
            int NewX = x + DirX[direction];
            int NewY = y + DirY[direction];
            if (IsValid(IntBoard, NewX, NewY) && IntBoard[NewX, NewY] == 1){
                // Eliminar la pared entre la celda actual y la nueva celda
                IntBoard[x + DirX[direction] / 2, y + DirY[direction] / 2] = 0;
                // Hacer la nueva celda un camino
                IntBoard[NewX, NewY] = 0;
                // Recursión en la nueva celda
                CreateMaze(IntBoard, r, NewX, NewY);
            }
        }
    }
    private static void BeAccesible(int[,] IntBoard, int x, int y){
        // Asegurarse de que la posición (x, y) sea un camino
        if (IntBoard[x, y] != 0) IntBoard[x, y] = 0; // Convertir a camino
        // Conectar el camino al menos a una celda adyacente que sea un camino
        int[] DirX = { -1, 1,  0, 0 };
        int[] Diry = {  0, 0, -1, 1 };
        for (int d = 0; d < DirX.Length; d++){
            int adjacentX = x + DirX[d];
            int adjacentY = y + Diry[d];
            if (IsValid(IntBoard, adjacentX, adjacentY) && IntBoard[adjacentX, adjacentY] == 0) return; // Ya está conectado a un camino existente
        }
        // Si no hay caminos adyacentes disponibles para conectar,
        // simplemente elimina una pared adyacente aleatoria.
        for (int d = 0; d < DirX.Length; d++){
            int adjacentX = x + DirX[d];
            int adjacentY = y + Diry[d];
            if (IsValid(IntBoard, adjacentX, adjacentY) && IntBoard[adjacentX, adjacentY] == 1){
                IntBoard[adjacentX, adjacentY] = 0; // Convertir a camino
                return;
            }
        }
    }
    private static void AddRandomPaths(int[,] IntBoard, Random r){
        for (int i = 0; i < IntBoard.GetLength(0); i++){
            for (int j = 0; j < IntBoard.GetLength(1); j++){
                if (IntBoard[i, j] == 1 && IsAdjacentToPath(IntBoard, i, j)){
                    if (IsValid(IntBoard, i, j) && IntBoard[i, j] == 1){
                        // Convertir algunas paredes a caminos aleatoriamente (30%)
                        if (r.NextDouble() < 0.3) IntBoard[i, j] = 0;
                    }
                }
            }
        }
    }
    private static bool IsAdjacentToPath(int[,] IntBoard, int x,int y){
        // Verificar si una pared es adyacente a un camino
        int[] DirX = { -1, 1,  0, 0 };
        int[] Diry = {  0, 0, -1, 1 };
        for (int d = 0; d < DirX.Length; d++){
            int NewX = x + DirX[d];
            int NewY = y + Diry[d];
            if (IsValid(IntBoard, NewX, NewY) && IntBoard[NewX,NewY] == 0) return true;
        }
        return false;
    }
    public static bool IsValid(int[,] IntBoard,int x,int y){
        // Verificar si una celda se encuentra en los limites de la matriz
        return x >= 4 && x < IntBoard.GetLength(0) - 4 && y >= 4 && y < IntBoard.GetLength(1) - 4; 
    }
    public Node2D[,] GenerateNodeBoard(int[,] IntBoard){
        //Generar matriz de nodos.
        Node2D[,] NodeBoard = new Node2D[IntBoard.GetLength(0), IntBoard.GetLength(1)];
        for (int i = 0; i < Filas; i++){
            for (int j = 0; j < Columnas; j++){
                if (IntBoard[i, j] == 0){
                    Node2D PathInstance = path.Instantiate<Node2D>();
                    AddChild(PathInstance);
                    NodeBoard[i, j] = PathInstance;
                }
                if (IntBoard[i, j] == 1){
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
    public void PrintBoard(Node2D[,] NodeBoard){
        //Mostrar cada nodo en su posicion correspondiente.
        for (int i = 0; i < NodeBoard.GetLength(0); i++){
            for (int j = 0; j < NodeBoard.GetLength(1); j++){
                NodeBoard[i, j].Position = new Vector2(j * 64, i * 64);
            }
        }
    }
    public void PlacePlayers(){
        //Spawnear Players
        Player1Instance = (Player1)player1.Instantiate<Node2D>();
        AddChild(Player1Instance);
        Player2Instance = (Player2)player2.Instantiate<Node2D>();
        AddChild(Player2Instance);
        
        Player1Instance.Position = new Vector2(4 * 64, 4 * 64);
        Player2Instance.Position = new Vector2((Columnas - 5) * 64, (Filas - 5) * 64);
    }
    public void PlaceTraps(){
        TrapCount = GlobalData.Traps;
        while (TrapCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            Node2D trapInstance = (Node2D)GD.Load<PackedScene>("res://scenes/trap.tscn").Instantiate();
            trapInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(trapInstance);
            TrapCount--;
        }
    }
    public void PlaceFires(){
        FireCount = GlobalData.Fire;
        while (FireCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            Area2D fireInstance = (Area2D)GD.Load<PackedScene>("res://scenes/fire.tscn").Instantiate();
            fireInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(fireInstance);
            FireCount--;
        }
    }
    public void PlaceTeleport(){
        TeleportCount = GlobalData.Teleport;
        while (TeleportCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            Area2D teleportInstance = (Area2D)GD.Load<PackedScene>("res://scenes/teleport.tscn").Instantiate();
            teleportInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(teleportInstance);
            TeleportCount--;
        }
    }
    public void PlaceMist(){
        MistCount = GlobalData.Mist;
        while (MistCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            Area2D mistInstance = (Area2D)GD.Load<PackedScene>("res://scenes/mist.tscn").Instantiate();
            mistInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(mistInstance);
            MistCount--;
        }
    }
    public void PlaceGema(){
        GemaCount = GlobalData.Gema;
        while (GemaCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            Area2D gemaInstance = (Area2D)GD.Load<PackedScene>("res://scenes/gema.tscn").Instantiate();
            gemaInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(gemaInstance);
            GemaCount--;
        }
    }
    public void PlaceHeart(){
        HeartCount = GlobalData.Heart;
        while (HeartCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            Area2D heartInstance = (Area2D)GD.Load<PackedScene>("res://scenes/heart.tscn").Instantiate();
            heartInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(heartInstance);
            HeartCount--;
        }
    }
    public void PlaceWolf(){
        WolfCount = GlobalData.Wolf;
        while (WolfCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            CharacterBody2D wolfInstance = (CharacterBody2D)GD.Load<PackedScene>("res://scenes/wolf.tscn").Instantiate();
            wolfInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(wolfInstance);
            WolfCount--;
        }
    }
    public void PlaceSpectre(){
        SpectreCount = GlobalData.Spectre;
        while (SpectreCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            CharacterBody2D spectreInstance = (CharacterBody2D)GD.Load<PackedScene>("res://scenes/spectre.tscn").Instantiate();
            spectreInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(spectreInstance);
            SpectreCount--;
        }
    }
    public void PlaceSkeleton(){
        SkeletonCount = GlobalData.skeleton;
        while (SkeletonCount > 0) {
            Random r = new Random();
            int x = 0;
            int y = 0;
            while (GlobalData.IntBoard[x, y] != 0){
                x = r.Next(1, GlobalData.Filas - 1);
                y = r.Next(1, GlobalData.Columnas - 1);
            }
            CharacterBody2D skeletonInstance = (CharacterBody2D)GD.Load<PackedScene>("res://scenes/skeleton.tscn").Instantiate();
            skeletonInstance.Position = new Vector2(y * 64, x * 64);
            AddChild(skeletonInstance);
            SkeletonCount--;
        }
    }
}