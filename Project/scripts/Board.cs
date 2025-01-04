using Godot;
using System;
using System.Collections.Generic;
public partial class Board : Node2D{
    public int Filas;
    public int Columnas;
    [Export] public PackedScene path;
    [Export] public PackedScene wall;
    [Export] public PackedScene player1;
    [Export] public PackedScene player2;
    public Player1 Player1Instance;
    public Player2 Player2Instance;
    public override void _Ready(){
        Filas = GlobalData.Filas;
        Columnas = GlobalData.Columnas;
        int[,] IntBoard = GenerateIntBoard(Filas, Columnas);
        Node2D[,] NodeBoard = GenerateNodeBoard(IntBoard);
        PrintBoard(NodeBoard);
        PlacePlayers();
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
        CreateMaze(IntBoard, r, 1, 1);
        // Asegurar accesibilidad de esta posicion
        BeAccesible(IntBoard, filas - 2, columnas - 2);
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
        return x >= 1 && x < IntBoard.GetLength(0) - 1 && y >= 1 && y < IntBoard.GetLength(1) - 1; 
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
        
        Player1Instance.Position = new Vector2(1 * 64, 1 * 64);
        Player2Instance.Position = new Vector2((Columnas - 2) * 64, (Filas - 2) * 64);
    }
}