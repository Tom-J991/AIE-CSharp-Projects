using AdventureGame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

namespace AdventureGame
{
    public struct IVec2
    {
        public int x;
        public int y;
    }

    public class Room
    {
        protected List<GameObject> objects = new List<GameObject> { null, null, null };

        public void AddGameObject(GameObject go)
        {
            // the input argument is inserted into the first index in the array containing a null value.
            objects.Insert(0, go);
        }
        public void RemoveGameObject(GameObject go)
        {
            // the array must be reordered such that any null values are at the end of the array.
            // For example, if the value at the first index in the array is removed, all following values must be shifted so that the null value occurs at index 2.
            objects.Remove(go);
            objects = objects.OrderBy(x => x == null).ThenBy(x => x != null).ToList();
        }
        
        public bool CheckGameObject(GameObject go)
        {
            return objects.First() == go;
        }

        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            if (objects.First() != null)
            {
                objects.First().Draw();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("~");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    public abstract class GameObject
    {
        protected IVec2 position;

        public virtual IVec2 GetPosition() { return position; }
        public virtual void SetPosition(int x, int y) { this.position.x = x; this.position.y = y; }

        public abstract void Draw();
    }

    public abstract class Character : GameObject
    {
        protected int HP = 100; // Health
        protected int AT = 0; // Attack
        protected int DF = 0; // Defense

        public virtual int GetHealth() { return HP; }
        public virtual int GetAttack() { return AT; }
        public virtual int GetDefense() { return DF; }
        public virtual void SetHealth(int hp) { this.HP = hp; }
        public virtual void SetAttack(int at) { this.AT = at; }
        public virtual void SetDefense(int df) { this.DF = df; }

        public virtual void MoveRight(Room[,] map, int mapWidth, int mapHeight, int distance)
        {
            map[this.GetPosition().x, this.GetPosition().y].RemoveGameObject(this);
            this.SetPosition(Math.Clamp(this.GetPosition().x + distance, 0, mapWidth - 1), this.GetPosition().y); // Clamped so it doesn't leave array bounds.
            map[this.GetPosition().x, this.GetPosition().y].AddGameObject(this);
        }
        public virtual void MoveLeft(Room[,] map, int mapWidth, int mapHeight, int distance)
        {
            map[this.GetPosition().x, this.GetPosition().y].RemoveGameObject(this);
            this.SetPosition(Math.Clamp(this.GetPosition().x - distance, 0, mapWidth - 1), this.GetPosition().y); // Clamped so it doesn't leave array bounds.
            map[this.GetPosition().x, this.GetPosition().y].AddGameObject(this);
        }
        public virtual void MoveDown(Room[,] map, int mapWidth, int mapHeight, int distance)
        {
            map[this.GetPosition().x, this.GetPosition().y].RemoveGameObject(this);
            this.SetPosition(this.GetPosition().x, Math.Clamp(this.GetPosition().y + distance, 0, mapHeight - 1)); // Clamped so it doesn't leave array bounds.
            map[this.GetPosition().x, this.GetPosition().y].AddGameObject(this);
        }
        public virtual void MoveUp(Room[,] map, int mapWidth, int mapHeight, int distance)
        {
            map[this.GetPosition().x, this.GetPosition().y].RemoveGameObject(this);
            this.SetPosition(this.GetPosition().x, Math.Clamp(this.GetPosition().y - distance, 0, mapHeight - 1)); // Clamped so it doesn't leave array bounds.
            map[this.GetPosition().x, this.GetPosition().y].AddGameObject(this);
        }

        public virtual bool IsAlive()
        {
            return true;
        }
    }

    public class Player : Character
    {
        public Player()
        { }
        public Player(int hp, int at, int df)
        {
            this.HP = hp;
            this.AT = at;
            this.DF = df;
        }
        public Player(int x, int y)
        {
            this.position.x = x;
            this.position.y = y;
        }
        public Player(int hp, int at, int df, int x, int y)
        {
            this.HP = hp;
            this.AT = at;
            this.DF = df;
            this.position.x = x;
            this.position.y = y;
        }

        public override void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    public class Enemy : Character
    {
        public Enemy()
        { }
        public Enemy(int hp, int at, int df)
        {
            this.HP = hp;
            this.AT = at;
            this.DF = df;
        }
        public Enemy(int x, int y)
        {
            this.position.x = x;
            this.position.y = y;
        }
        public Enemy(int hp, int at, int df, int x, int y)
        {
            this.HP = hp;
            this.AT = at;
            this.DF = df;
            this.position.x = x;
            this.position.y = y;
        }

        public override void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            if (HP > 10)
                Console.Write("O");
            else
                Console.Write("o");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    public class PowerUp : GameObject
    {
        protected ConsoleColor bgColor = ConsoleColor.DarkBlue;
        protected ConsoleColor fgColor = ConsoleColor.Blue;

        public PowerUp()
        { }
        public PowerUp(int x, int y)
        {
            this.position.x = x;
            this.position.y = y;
        }

        public void SetBackgroundColor(ConsoleColor col) { bgColor = col; }
        public void SetForegroundColor(ConsoleColor col) { fgColor = col; }

        public override void Draw()
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.Write("?");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    public class Renderer
    {
        protected int mapWidth = 3, mapHeight = 3;
        protected Room[,] map;

        public Renderer(int mapwidth, int mapheight)
        {
            this.mapWidth = mapwidth;
            this.mapHeight = mapheight;

            Console.WindowWidth = mapWidth * 3;
            Console.WindowHeight = mapheight * 3;

            map = new Room[mapWidth, mapHeight];

            for (int y = 0; y < mapHeight; y++)
                for (int x = 0; x < mapWidth; x++)
                    map[x, y] = new Room();
        }

        public void DrawMap()
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    map[x, y].Draw();
                }
                Console.WriteLine();
            }
        }

        public Room[,] GetMap()
        {
            return map;
        }
    }

    public enum ShopOptions
    {
        HP = 0,
        AT = 1,
        DF = 2,
        MAX = 3
    }

    public class Game
    {
        protected int updateRate = 60;

        protected int mapWidth = 64;
        protected int mapHeight = 16;

        protected Renderer renderer;
        protected Random rng;

        protected Player player;
        protected Enemy enemy;
        protected PowerUp power;

        // Score
        protected int enemiesDefeated = 0;
        protected int score = 0;
        protected int money = 0;

        // Game Variables
        protected bool gameOver = false;
        protected bool canMove = true;

        // Battle Variables
        protected bool battle = false;
        protected string battleText = "";
        protected int battleLine = 4;
        protected int battleSpeed = 2;
        protected bool battleChosen = false;
        protected int battleChoice = 0;

        // Shop Variables
        protected bool shop = false;
        protected bool inShopRange = false;
        protected ShopOptions shopPos = ShopOptions.HP;
        protected string shopFailString = "";

        protected bool running;

        public Game()
        {
            rng = new Random();
            renderer = new Renderer(mapWidth, mapHeight);

            Setup();
            running = true;
            GameLoop();
        }

        public void GameLoop()
        {
            while (running)
            {
                Update();
                Console.WriteLine("- For this activity you will create part of a text-based adventure game - Press Escape to Exit.");
                Draw();
                Thread.Sleep(1000 / updateRate); // Update Rate
            }
        }

        public void Setup()
        {
            // Setup Game
            player = new Player(100, 20, 6, mapWidth / 2, mapHeight / 2);
            enemy = new Enemy(100, 4, 1, rng.Next(0, mapWidth), rng.Next(0, mapHeight));
            power = new PowerUp(rng.Next(0, mapWidth), rng.Next(0, mapHeight));

            renderer.GetMap()[player.GetPosition().x, player.GetPosition().y].AddGameObject(player);
            renderer.GetMap()[enemy.GetPosition().x, enemy.GetPosition().y].AddGameObject(enemy);
            renderer.GetMap()[power.GetPosition().x, power.GetPosition().y].AddGameObject(power);
        }

        public void Update()
        {
            // Get Keyboard Input
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                while (Console.KeyAvailable) Console.ReadKey(true);
                // Quit
                if (pressedKey.Key == ConsoleKey.Escape)
                    running = false; // Exit

                if (!gameOver)
                {
                    // Player Movement
                    if (pressedKey.Key == ConsoleKey.RightArrow && canMove)
                        player.MoveRight(renderer.GetMap(), mapWidth, mapHeight, 1);
                    else if (pressedKey.Key == ConsoleKey.LeftArrow && canMove)
                        player.MoveLeft(renderer.GetMap(), mapWidth, mapHeight, 1);
                    else if (pressedKey.Key == ConsoleKey.UpArrow && canMove)
                        player.MoveUp(renderer.GetMap(), mapWidth, mapHeight, 1);
                    else if (pressedKey.Key == ConsoleKey.DownArrow && canMove)
                        player.MoveDown(renderer.GetMap(), mapWidth, mapHeight, 1);
                    // Shop
                    if (pressedKey.Key == ConsoleKey.Enter && inShopRange && !shop)
                    {
                        shop = true;
                        canMove = false;
                        shopFailString = "                  ";
                        Console.Clear();
                    }
                    else if (pressedKey.Key == ConsoleKey.Q && shop)
                    {
                        shop = false;
                        canMove = true;
                        shopFailString = "                  ";
                        Console.Clear();
                    }
                    else if (pressedKey.Key == ConsoleKey.Enter && shop)
                    {
                        switch (shopPos)
                        {
                            case ShopOptions.HP:
                                if (money >= 2)
                                {
                                    money -= 2;
                                    shopFailString = "                  ";
                                    player.SetHealth(100);
                                }
                                else
                                    shopFailString = "Not Enough Coins!";
                                break;
                            case ShopOptions.AT:
                                if (money >= 1)
                                {
                                    money -= 1;
                                    shopFailString = "                  ";
                                    player.SetAttack(player.GetAttack() + 1);
                                }
                                else
                                    shopFailString = "Not Enough Coins!";
                                break;
                            case ShopOptions.DF:
                                if (money >= 1)
                                {
                                    money -= 1;
                                    shopFailString = "                  ";
                                    player.SetDefense(player.GetDefense() + 1);
                                }
                                else
                                    shopFailString = "Not Enough Coins!";
                                break;
                            default:
                                break;
                        };
                    }
                    else if (pressedKey.Key == ConsoleKey.UpArrow && shop)
                    {
                        shopPos--;
                        if (shopPos < ShopOptions.HP)
                            shopPos = ShopOptions.DF;
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow && shop)
                    {
                        shopPos++;
                        if (shopPos >= ShopOptions.MAX)
                            shopPos = ShopOptions.HP;
                    }
                }
            }

            if (gameOver)
                return;

            // Check Shop Collision
            if ((renderer.GetMap()[Math.Clamp(player.GetPosition().x + 1, 0, mapWidth - 1), player.GetPosition().y].CheckGameObject(power) ||
                renderer.GetMap()[Math.Clamp(player.GetPosition().x - 1, 0, mapWidth - 1), player.GetPosition().y].CheckGameObject(power) ||
                renderer.GetMap()[player.GetPosition().x, Math.Clamp(player.GetPosition().y + 1, 0, mapHeight - 1)].CheckGameObject(power) ||
                renderer.GetMap()[player.GetPosition().x, Math.Clamp(player.GetPosition().y - 1, 0, mapHeight - 1)].CheckGameObject(power)))
            {
                inShopRange = true;
                power.SetBackgroundColor(ConsoleColor.Blue);
                power.SetForegroundColor(ConsoleColor.DarkBlue);
            }
            else
            {
                inShopRange = false;
                power.SetBackgroundColor(ConsoleColor.DarkBlue);
                power.SetForegroundColor(ConsoleColor.Blue);
            }

            // Check Enemy Collision
            if ((renderer.GetMap()[Math.Clamp(player.GetPosition().x + 1, 0, mapWidth - 1), player.GetPosition().y].CheckGameObject(enemy) ||
                renderer.GetMap()[Math.Clamp(player.GetPosition().x - 1, 0, mapWidth - 1), player.GetPosition().y].CheckGameObject(enemy) ||
                renderer.GetMap()[player.GetPosition().x, Math.Clamp(player.GetPosition().y + 1, 0, mapHeight - 1)].CheckGameObject(enemy) ||
                renderer.GetMap()[player.GetPosition().x, Math.Clamp(player.GetPosition().y - 1, 0, mapHeight - 1)].CheckGameObject(enemy)))
            {
                battle = true;
                canMove = false;
            }

            // Battle Mode
            if (battle)
            {
                if (battleChosen == false)
                {
                    Console.Clear(); // Reset
                    // Choose who goes first.
                    int rollDice = rng.Next(0, 1);
                    if (rollDice == 0)
                    {
                        battleChosen = true;
                        battleChoice = rollDice;
                        battleText = "Enemy goes first.";
                        battleLine++;
                    }
                    else
                    {
                        battleChosen = true;
                        battleChoice = rollDice;
                        battleText = "Player goes first.";
                        battleLine++;
                    }
                }
                else
                {
                    // Battle
                    if (battleChoice == 0)
                    {
                        // Enemy
                        int rollDef = rng.Next(0, 4);
                        if (rollDef == 2)
                        {
                            // Attack Defense
                            player.SetHealth(player.GetHealth() - (enemy.GetAttack() / player.GetDefense()));
                            battleText = "Enemy Attacks But Player Defended! Player -" + (enemy.GetAttack() / player.GetDefense()) + " Health!";
                            battleLine++;
                        }
                        else
                        {
                            // Normal Attack
                            player.SetHealth(player.GetHealth() - enemy.GetAttack());
                            battleText = "Enemy Attack! Player -" + (enemy.GetAttack()) + " Health!";
                            battleLine++;
                        }
                        battleChoice = 1; // Change Turn
                    }
                    else
                    {
                        // Player
                        int rollDef = rng.Next(0, 8);
                        if (rollDef == 2)
                        {
                            // Attack Defense
                            enemy.SetHealth(enemy.GetHealth() - (player.GetAttack() / enemy.GetDefense()));
                            battleText = "Player Attacks But Enemy Defends! Enemy -" + (player.GetAttack() / enemy.GetDefense()) + " Health!";
                            battleLine++;
                        }
                        else
                        {
                            // Normal Attack
                            enemy.SetHealth(enemy.GetHealth() - player.GetAttack());
                            battleText = "Player Attacks! Enemy -" + (player.GetAttack()) + " Health!";
                            battleLine++;
                        }
                        battleChoice = 0; // Change Turn
                    }

                    if (player.GetHealth() <= 0)
                    {
                        // Game Over
                        Console.Clear();
                        gameOver = true;
                        battleLine = 4;
                        battleText = "Game Over! Enemy is Victorious!";
                        battle = false;
                        battleChosen = false;
                        battleChoice = 0;

                        // Exit
                        renderer.GetMap()[player.GetPosition().x, player.GetPosition().y].RemoveGameObject(player);
                    }
                    if (enemy.GetHealth() <= 0)
                    {
                        int moneyDrop = rng.Next(2, 4);

                        // Win
                        Console.Clear();
                        battleLine = 4;
                        battleText = "Enemy Loses! Player is Victorious! Coins + " + moneyDrop;
                        battle = false;
                        battleChosen = false;
                        battleChoice = 0;

                        // Score
                        score++;
                        enemiesDefeated++;
                        money += moneyDrop;

                        // Exit and generate new Enemy
                        renderer.GetMap()[enemy.GetPosition().x, enemy.GetPosition().y].RemoveGameObject(enemy);
                        int newX = rng.Next(0, mapWidth-1);
                        int newY = rng.Next(0, mapHeight-1);
                        int newAT = (int)(enemy.GetAttack() + (enemiesDefeated * 1.5f));
                        int newDF = enemy.GetDefense() + (enemiesDefeated);
                        enemy = new Enemy(50, newAT, newDF, newX, newY);
                        renderer.GetMap()[enemy.GetPosition().x, enemy.GetPosition().y].AddGameObject(enemy);

                        canMove = true;
                    }
                }

                Thread.Sleep(1000 / battleSpeed);
            }
        }

        public void Draw()
        {
            Console.CursorVisible = false;

            { // Map
                renderer.DrawMap();
                string UIText = " - Player Health: " + player.GetHealth() + ", Player Attack: " + player.GetAttack() + ", Player Defense: " + player.GetDefense() + " -";
                Console.WriteLine(UIText);
                UIText = " - Score: " + score + ", Coins: " + money + " -";
                Console.WriteLine(UIText);
            }
            { // Battle
                Console.SetCursorPosition(mapWidth + 4, 2);
                Console.WriteLine("- Battle");
                Console.SetCursorPosition(mapWidth + 6, 3);
                string battleUIText = "- Enemy Health: " + enemy.GetHealth() + ", Enemy Attack: " + enemy.GetAttack() + ", Enemy Defense: " + enemy.GetDefense() + " -";
                Console.WriteLine(battleUIText);
                Console.SetCursorPosition(mapWidth + 8, battleLine);
                Console.WriteLine(battleText);
            }
            { // Shop
                Console.SetCursorPosition(2, mapHeight + 4);
                Console.WriteLine("- Shop (Press Enter when in range)");
                if (shop)
                {
                    Console.SetCursorPosition(4, mapHeight + 5);
                    Console.WriteLine("(Exit Shop by pressing Q)");
                    Console.SetCursorPosition(6, mapHeight + 6);
                    Console.WriteLine((shopPos == ShopOptions.HP ? "> " : "  ") + "+Full HP ($2)");
                    Console.SetCursorPosition(6, mapHeight + 7);
                    Console.WriteLine((shopPos == ShopOptions.AT ? "> " : "  ") + "+AT +1 ($1)");
                    Console.SetCursorPosition(6, mapHeight + 8);
                    Console.WriteLine((shopPos == ShopOptions.DF ? "> " : "  ") + "+DF +1 ($1)");
                    Console.SetCursorPosition(8, mapHeight + 9);
                    Console.WriteLine(shopFailString);
                }
            }

            Console.SetCursorPosition(0, 0); // Set cursor back to top left so that it redraws the frame.
        }
    }
}

namespace TextBasedAdventure
{
    internal class TextBasedAdventure
    {
        // For this activity you will create part of a text-based adventure game.
        // This game will contain a 2D array of rooms(the map), and each room will either be empty, contain the Player, an Enemy, a Powerup, or any combination of these three.
        public static void Main()
        {
            Game g = new Game();
            //Console.ReadKey();
        }
    }
}
