<h1 align=center> Mistborn Maze 🎮  </h2>

![image_2025-02-03_02-16-22](https://github.com/user-attachments/assets/e43eeae7-cd50-4838-b591-34b6dd9a9212)

Mistborn Maze is an exciting real-time multiplayer game developed with Godot Engine and C#. Set in the Mistborn universe, players venture into a maze filled with traps and enemies, competing to escape.

## Introduction 🌫️

Under a perpetually cloudy sky, the maze stretches out like an endless cemetery. You walk among broken tombstones and moss-covered mausoleums, while the mist dances on the ground, hiding what your eyes don't want to see. The faint light filtering through the clouds casts long, distorted shadows, making the cemetery seem alive. The air is thick with heavy silence, broken only by the occasional crunch of your steps or the distant echo of something crawling in the dark. Here, among the forgotten dead, every path seems to lead deeper into despair.

Hidden among the crypts and graves are five fragments of Lerasium, glowing with an otherworldly light, as if they don't belong in this world. This mineral is not only the key to your escape but also the reward for your survival. But you're not alone. Something is following you, perhaps the maze itself, perhaps the echoes of those who failed before. As the fragments draw near, so does the danger. In this place of eternal rest, only one will leave the realm of the dead. The rest will stay here, forever.

## Game Features 🕹️

### Maze Generation 🔄
- The maze is dynamically generated using the Depth-First Search (DFS) algorithm.

### Traps and Enemies ⚔️
- Throughout the maze, players will encounter traps and enemies that they must evade or die trying.

### Multiplayer 👥
- The game is for 2 players competing in real-time.

<img width="1276" alt="image_2025-01-22_00-20-41" src="https://github.com/user-attachments/assets/cea65d1b-071c-4759-8b3b-ac26e97775a3" />

### Objective 🏆
- The first player to collect 5 fragments of Lerasium wins the game.

### Playable Characters 🎭
- Choose from 6 playable characters, each with unique abilities. DISCOVER THEM FOR YOURSELF!

<img width="1278" alt="image_2025-01-22_00-20-34" src="https://github.com/user-attachments/assets/d8cf9a7a-0662-45b6-8f63-c2c3d40b8d87" />

## Project Structure 📂

- **assets/**: Contains game resource files.
- **font/**: Contains the fonts used in the game.
- **scenes/**: Contains the game scenes.
- **scripts/**: Contains the C# scripts used in the game.
- **soundtrack/**: Contains the game soundtrack.

## Requirements 🛠️

- [Godot Engine](https://godotengine.org/) 4.3.0 or higher.
- [.NET SDK](https://dotnet.microsoft.com/download) 6.0 or higher.

## Project Setup 💻

1. Clone the repository:
    ```sh
    git clone https://github.com/andr-migue/MistbornMaze.git
    ```

2. Open the project in Godot:
    - Open Godot Engine.
    - Select "Import" and navigate to the project directory.
    - Select [project.godot](http://_vscodecontentref_/0) and click "Import & Edit."

## Project Execution 🚀

1. Navigate to the `Executable` folder.
2. Unzip the contents.
3. Run `MistBornMaze.exe`.

## Classes Structure 📘

### Main Classes

- **Board.cs**: Manages the generation of the game map, including creating the maze, placing players, traps, enemies, and items.
- **Game.cs**: Configures the cameras and associates the players' view with the board.
- **GlobalData.cs**: Stores global variables and playlists. Defines static variables to be used throughout the game.

### Game Control
- **Pause.cs**: Manages the game's pause and the detection of winners.
- **Menu.cs**: Controls the main game menu, including methods to navigate between menu options and change settings.
- **Intro.cs**: Manages the game's introduction screen using a timer to gradually show the introduction text.
- **SelectionPlayer.cs**: Manages the character selection screen, allowing navigation between characters and selecting playable characters.

### Movement and Collisions
- **Movement.cs**: Enables the movement of entities.
- **Player1.cs and Player2.cs**: Control the input and behavior of players 1 and 2, initializing characters and handling keyboard inputs.
- **PlayerSensor.cs**: Detects collisions with other objects in the game, using lists to store and remove collisions.
- **Sensor.cs**: Determines the target towards which entities will move, using methods to scan and select the nearest target.

### Health and Scoring System
- **HealthBox.cs**: Manages the players' health system, including methods to update health and handle respawn.
- **HitBox.cs**: Manages the damage to entities in the game, with a simple method to apply damage.
- **ScoreSystem.cs**: Displays the players' scores on the interface, using labels to show scores and animations for the gems.

### Traps, Enemies, and Items
- **Fire.cs**: Controls the fire animation in the game.
- **Gema.cs**: Manages the behavior of the gems in the game, including their collection and respawn.
- **Heart.cs**: Controls the healing of players when they collect hearts, using events to detect collection and handle respawn.
- **Mist.cs**: Controls the visibility of the mist in the game, using a timer to manage mist visibility when a character enters the area.
- **Trap.cs**: Controls the traps in the game, using animations and timers to activate and deactivate traps.
- **Teleport.cs**: Controls the teleportation of players, using methods to detect a player's entry and change their position.
- **Skeleton.cs**: Controls the behavior of the skeleton enemy, using sensors and animations to move the skeleton towards the target.
- **Spectre.cs**: Controls the behavior of the specter enemy, similar to Skeleton.cs.
- **Wolf.cs**: Controls the behavior of the wolf enemy, similar to Skeleton.cs.

### Sound System
- **SoundManager.cs**: Manages the game's soundtrack, including methods to play, randomize, and change songs.

### Characters
All characters share the following common features:
- Extend the `CharacterBody2D` class from Godot, allowing them to have a 2D body and move.
- Use a `Movement` object to handle their movements.
- Have specific animations for different movement directions using `AnimatedSprite2D`.
- Possess unique abilities that can be activated and have a cooldown period.
- Each character has a `Timer` to manage the cooldown of their abilities.
- Use labels (`Label`) to display relevant information on the interface, such as the cooldown of their abilities.
- Detect and respond to player input through `InputVector`.

These common features provide a basic and consistent structure for all characters, allowing each to have unique abilities while sharing a similar base functionality.

#### Specific Abilities
- **Cid**: Teleports to a random position on the map. Cooldown: 5 seconds.
- **Jake**: Temporarily increases movement speed. Cooldown: 20 seconds.
- **Rain**: Increases the size of their light to illuminate more area. Cooldown: 20 seconds.
- **Cleome**: Transforms into a specter, becoming invisible to enemies. Cooldown: 20 seconds.
- **Nichols**: Fades, becoming less visible and increasing speed. Cooldown: 20 seconds.
- **Lasswell**: Recovers health. Cooldown: 30 seconds.

## Maze Generation Algorithm 🔄

### Integer Matrix Generation

1. **Initialization**:
   - A 2D integer matrix is created with all cells initialized as walls (value `1`).

2. **Maze Creation Using DFS**:
   - The algorithm uses Depth-First Search (DFS) starting from a specific cell.
   - It randomly selects directions (North, South, East, West).
   - If the adjacent cell in the chosen direction is a valid wall, it converts the wall and the intermediate cell into paths (value `0`).
   - This process continues recursively until all cells are visited.

3. **Ensuring Accessibility**:
   - The algorithm ensures a specific position in the maze is a path.
   - It connects this position to at least one adjacent cell that is already a path.

4. **Adding Random Paths**:
   - To increase the maze's accessibility, the algorithm randomly converts some additional walls into paths.

### Node Matrix Generation

1. **Node Initialization**:
   - A 2D node matrix is created based on the integer matrix.
   - For each cell in the integer matrix:
     - If the cell is a path (`0`), a path node is instantiated.
     - If the cell is a wall (`1`), a wall node is instantiated randomly from a predefined set of walls.

2. **Node Positioning**:
   - Each node is positioned in the game world according to its coordinates in the matrix.
   - The positioning is scaled based on the cell size of the board.

### Additional Elements Placement

1. **Players**:
   - Player instances are created and placed at predefined starting positions.

2. **Traps, Fires, Teleports, and Mist**:
   - These elements are placed randomly in the maze at positions that are paths (`0`).
   - The algorithm ensures that these elements are not placed on walls.

3. **Gems and Hearts**:
   - Similar to traps and other elements, gems and hearts are placed at random path positions.

4. **Enemies (Wolves, Spectres, Skeletons)**:
   - Enemy instances are created and placed at random path positions, ensuring they are not on walls.

## Contact 📧

For any inquiries, you can contact me at:  
- [My Email.](mailto:miguelzamora210405@gmail.com)
- [My 𝕏 Account.](https://x.com/andr_migue)
