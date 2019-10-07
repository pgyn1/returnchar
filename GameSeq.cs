using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReturnChar
{
    class GameSeq
    {

        //
        private static Stopwatch stopwatchtime = new Stopwatch();
        public static UserBox UserBoxGameSeq = new UserBox();
        public static Book BookGameSeq = new Book();

        //
        public static string GameOver;
        public static string NoElementsInSequence;
        public static string GameModelZero;
        public static string GameModelOne;
        public static string DisplayMenu;

        public StreamClass StartNewRestore = new StreamClass();
        public Valid ValidGameSeq = new Valid();
        public GetStartGameOptions getstart = new GetStartGameOptions();
       
        public Menu UserBoxMenu = new Menu();
        public Random RandGameSeq = new Random();
        public Player game = new Player();

        public static bool IsPlaying { get; set; }
        public static int Timer { get; set; }
        public static int RandNumber { get; set; }
        public static List<Book> GameArray { get; set; }
        public static int GameScore { get; set; }
        public static char DiffLvl { get; set; }
        public static char Mode { get; set; }
        public static char NewRestore { get; set; }

        //public string[] RestoredSavedArray { get; set; }

        public GameSeq(char newrestore, char difflvl, char mode)
        {

            //if (timespaner.Equals(null)){}TimeSpan timespaner
            NewRestore = newrestore;
            DiffLvl = difflvl;
            Mode = mode; //'X' timer option, 'Y' list==0, 'Z' Target Score
            IsPlaying = true;
            GameArray = new List<Book>();
            GameOver = "\nGame Over";
            GameModelZero = "\nGame model zero";
            GameModelOne = "{Game model one}";
            NoElementsInSequence = "!Any() Sequence";
            DisplayMenu = "\n\nDisplay menu(y) or continue(enter) ?\n";
            RandNumber = RandGameSeq.Next(2); // Return a random number < 2 for the gamemodel cycles
            Game(); // GameNew() else GameRestore() 

        }

        static GameSeq()
        {
         
            
        }


        public static bool ReturnPlayerScore(int x)
        {
            if(Player.GetPlayerLevelScore() >= x)
            {
                Console.ReadLine(); return true;
            }
            else
            {
                Console.WriteLine($"Level score = {Player.GetPlayerLevelScore()}. Level score must be >= {x}"); Console.ReadLine(); return false; 
            }
        }

        public static void SetStatics()
        {
            Player.RestoreComputerScore(0);
            Player.RestorePlayerScore(0);

            //Book.SetReadFromStream(GetStartGameOptions.GetGameFileList());
            
            Book.SetDictionary();
            Valid.SetValidDictionary(Book.GetDictionary()); // Case of the dictionary being reduced in the display
            Valid.GetSelectedBooks().Clear();
        }

        public static void SetRestore()
        {
            //display file(S)
            //prompt for file(s)
           
            //  ...
            //  ...
            List<int> gamearrayints = StreamClass.RestoreGameArray("C:/program files/returnchar/savelist.csv"); // restoring savelists

            foreach(int i in gamearrayints)
            {
                 GameArray.Add(Book.GetBookID(i));
            }

                 GetGameModelOne();
                 break;
               
            // ...
            // ...

            }
        }
        
        public static void CheckGameArrayZero()
        {
            if (GameArray.Count() == 0 || GameArray == null) { } else { GetGameModelZero(); }
        }

        public static void CheckGameArrayOne()
        {
            if (GameArray.Count() == 0 || GameArray == null) { } else { GetGameModelOne(); }
        }

        public static void Game()
        {
            SetStatics();

            if (NewRestore.Equals('N') || NewRestore.Equals('R'))
            {
                try
                {
               
                    switch (Mode.ToString().ToLower())
                    {
                        case "x":
                            Timer = GetStartGameOptions.GetTimer();
                            //TimeSpanTimer = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + Timer, DateTime.Now.Second);
                            stopwatchtime.Restart();
                            break;
                        case "y":
                            break;
                        case "z":
                            GameScore = GetStartGameOptions.GetGameScore();
                            break;
                        default:
                            GameScore = GetStartGameOptions.GetGameScore();
                            break;
                    }

                    switch (DiffLvl.ToString().ToLower())
                    {
                        case "e":
                            GameArray = BookGameSeq.GetGameArray('E');
                            CheckGameArrayZero(); //Calls ModelZero
                            break;
                        case "m":
                            if(ReturnPlayerScore(10))
                            {
                                //Display rules
                                GameArray = BookGameSeq.GetGameArray('M');
                                CheckGameArrayOne();
                                break;
                            }
                            else
                            {
                                Menu.DisplayMenuOptions();
                                break;
                            }
                            
                        case "h":
                            if(ReturnPlayerScore(20))
                            {
                                GameArray = BookGameSeq.GetGameArray('H');
                                CheckGameArrayOne();
                                break;
                            }
                            else
                            {
                                Menu.DisplayMenuOptions();
                                break;
                            }
                        case "s":
                            if(ReturnPlayerScore(40))
                            {
                                GameArray = BookGameSeq.GetGameArray('S');
                                CheckGameArrayOne();
                                break;
                            }
                            else
                            {
                                Menu.DisplayMenuOptions();
                                break;
                            }
                        default:
                            //if (BookGameSeq.GetComStateListEasy()) GameArray = Book.ComStateList;
                            GameArray = BookGameSeq.GetGameArray(DiffLvl);
                            CheckGameArrayZero();
                            break;
                    }
                    //calling stopwatch restart here? Saving a few ms

                }
                catch (ArgumentNullException argexc)
                {
                    Console.WriteLine($"GameSeq -> Game() + {argexc.Message}\n{argexc.InnerException}");
                    Console.WriteLine("Game array = null");
                    Console.WriteLine("Return to menu");
                    Console.ReadKey();
                    Menu.DisplayMenuOptions();
                    //Environment.Exit(Environment.ExitCode);

                }

                catch (Exception e)
                {
                    Console.WriteLine("GameSeq -> Game()");
                    Console.WriteLine($"{e.Message}\n{e.InnerException}");
                    Console.WriteLine("Return to menu");
                    Console.ReadKey();
                    Menu.DisplayMenuOptions();
                }
            }
           

        }

        public static void GameModelZeroDisplays()
        {
            // repeats until computer list exh, player quits, time limit or score 
            UserBoxGameSeq.SetOne(DiffLvl, Mode);
            Console.WriteLine($"{GameModelZero}");
            Console.WriteLine($"Level score = {Player.GetPlayerLevelScore()}");
            Console.WriteLine($"Player has {Player.GetTimerLimit()}ms to select an answer");
            UserBoxGameSeq.SetBorderDisplay();
            Console.ReadLine();
        }

        public static void GameModelOneDisplays()
        {
            // repeats until computer list exh, player quits, time limit or score 
            
            UserBoxGameSeq.SetOne(DiffLvl,Mode);
            Console.WriteLine($"{GameModelOne}");
            Console.WriteLine($"Level score = {Player.GetPlayerLevelScore()}");
            Console.Write($"{"{"}Player has {Player.GetTimerLimit()}ms to select an answer{"}"}");
            UserBoxGameSeq.SetBorderDisplay();
            Console.ReadLine();
        }

        public static void SetIsPlayingModelOne(int randnumber)
        {
            if (randnumber == 0)
            {
                //if 0 the comp turns 1st which means the player must have a random input
                Player.SetPlayerInput(Player.GetAlphabet());
                Console.WriteLine($"Return Char [{Player.GetPlayerInput()}]");
                Console.ReadLine();

            }
            else
            {
                //else the player 1st which means the computer must have a random input // A pass_ or a invalid response sets the prev to a random char GetAlphabet()
                Player.SetComputerInput(Player.GetAlphabet());
                Console.WriteLine($"Return Char [{Player.GetComputerInput()}]");
                Console.ReadLine();
            }
        }

        public static void EndGameModel()
        {
            
            StreamClass.WriteScores(Player.GetComputerScore(), Player.GetPlayerScore());
            Console.ReadKey();
            Menu.DisplayMenuOptions();
        }

        //Model1 // Returning a random char in zero mode  
        public static void GetGameModelZero()
        {
            GameModelZeroDisplays();

            if (RandNumber == 0)
            {
                while (IsPlaying)
                {

                    Console.Clear();

                    Player.SetComputerInput(GameArray); // Computer set an answer from finite list
                    IfContains(Player.GetComputerInput()); //If contains reducing the GameArray
                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput()); // player responds 

                    if (Mode.ToString().ToUpper().Equals("Z")) { if (!IfScore()) break; }
                    IfContains(Player.GetPlayerInput());

                    UserBoxGameSeq.NextTurn();

                    Player.SetPlayerInput(); // player sets an answer from 'finite' 
                    IfContains(Player.GetPlayerInput());
                    Player.SetComputerInput(GameArray, Player.GetPlayerInput(),DiffLvl); // computer responds
                    IfContains(Player.GetComputerInput());

                    SwitchMode();
                    
                }

                EndGameModel();
            }
            else
            {
                while (IsPlaying)
                {

                    Console.Clear();

                    Player.SetPlayerInput(); // player sets an answer from 'finite' list
                    IfContains(Player.GetPlayerInput());
                    Player.SetComputerInput(GameArray, Player.GetPlayerInput(),DiffLvl); // computer responds
                    if (Mode.ToString().ToUpper().Equals("Z")) { if (!IfScore()) break; }
                    IfContains(Player.GetComputerInput());

                    UserBoxGameSeq.NextTurn();

                    Player.SetComputerInput(GameArray); // Computer set an answer from finite list
                    IfContains(Player.GetComputerInput());
                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput()); // player responds 
                    IfContains(Player.GetPlayerInput());

                    SwitchMode();
                }

                EndGameModel();

            }

        }

        //Model2
        public static void GetGameModelOne()
        {

            GameModelOneDisplays();
            SetIsPlayingModelOne(RandNumber);

            if (RandNumber == 0)
            {
                while (IsPlaying)
                {
                    Console.Clear();
                    Console.WriteLine($"Char = [{Player.GetPlayerInput().Last()}]");

                    Player.SetComputerInput(GameArray, Player.GetPlayerInput(),DiffLvl); //computerinput is set based upon the options in the gamearray and last letter of the playerinput
                    IfContains(Player.GetComputerInput());
                    if (Mode.Equals('Z')) if (!IfScore()) break; //If mode, ifscore returns false break loop

                    UserBoxGameSeq.NextTurn();

                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput());
                    IfContains(Player.GetPlayerInput());

                    SwitchMode();
                }

                EndGameModel();
            }
            else
            {

                while (IsPlaying)
                {
                    Console.Clear();
                    Console.WriteLine($"Char = [{Player.GetComputerInput().Last()}]"); ;

                    Player.SetPlayerInput(DiffLvl, Player.GetComputerInput()); //player input is based upon the last letter of the computerinput
                    IfContains(Player.GetPlayerInput());
                    if (Mode.Equals('Z')) if (!IfScore()) break;

                    UserBoxGameSeq.NextTurn();

                    Player.SetComputerInput(GameArray, Player.GetPlayerInput(),DiffLvl);
                    IfContains(Player.GetComputerInput());

                    SwitchMode();
                }

                EndGameModel();
            }

        }

        // X Y Z  TIME EMP T
        public static void SwitchMode()
        {
            switch (Mode.ToString().ToLower())
            {
                case "x":
                    GameSeqTimer();
                    //Mode = timer 
                    break;
                case "y":
                    GameSeqEmpty();
                    // Empty List
                    break;
                case "z":
                    GameSeqTarget();
                    // GameScore / TargetScore
                    break;
                default:
                    GameSeqTarget();
                    break;

            }
        }

        //TIME
        public static void GameSeqTimer()
        {
            if (stopwatchtime.ElapsedMilliseconds >= Timer)
            {
                Console.WriteLine($"{GameOver} Final time = {stopwatchtime.Elapsed}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                stopwatchtime.Stop();
                IsPlaying = false;
            }
            else if (!GameArray.Any())
            {
                Console.WriteLine($"{NoElementsInSequence}");
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;
            }
            else
            {
                DisplayerTimer();
                Console.WriteLine($"{DisplayMenu}");

                if (Console.ReadLine().ToLower().Equals("y")) UserBoxGameSeq.IsPlayingGameDisplays(DiffLvl, Mode, GameScore, Timer, Player.GetPlayerScore(), Player.GetComputerScore(), RandNumber, GameArray);
                IsPlaying = UserBoxGameSeq.IsPlayingGameDisplay();
                Console.ReadLine();
            }
        }

        //EMP
        public static void GameSeqEmpty()
        {
            // If list empty
            if (!GameArray.Any())
            {
                Console.WriteLine($"{NoElementsInSequence}");
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;
            }
            else
            {
                Console.WriteLine($"{DisplayMenu}");
                if (Console.ReadLine().ToLower().Equals("y")) UserBoxGameSeq.IsPlayingGameDisplays(DiffLvl, Mode, GameScore, Timer, Player.GetPlayerScore(), Player.GetComputerScore(), RandNumber, GameArray);
                IsPlaying = UserBoxGameSeq.IsPlayingGameDisplay();
                Console.ReadLine();
            }
        }

        //T
        public static void GameSeqTarget()
        {
            
            if (Player.GetComputerScore() == GameScore || Player.GetPlayerScore() == GameScore)
            {
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;

            }
            else if (!GameArray.Any())
            {
                Console.WriteLine($"{NoElementsInSequence}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                IsPlaying = false;

            }
            else
            {
                Console.WriteLine($"{DisplayMenu}");
                if (Console.ReadLine().ToLower().Equals("y")) UserBoxGameSeq.IsPlayingGameDisplays(DiffLvl, Mode, GameScore, Timer, Player.GetPlayerScore(), Player.GetComputerScore(), RandNumber, GameArray); 
                IsPlaying = UserBoxGameSeq.IsPlayingGameDisplay();
                Console.ReadLine();
            }

        }

        public static void DisplayerTimer()
        {

            Console.WriteLine("");

                switch (Timer)
                {
                    case 60000:
                        Console.Write($"Current Time = {stopwatchtime.Elapsed} Time limit = 1 minute");
                        break;
                    case 120000:
                        Console.Write($"Current Time = {stopwatchtime.Elapsed} Time limit = 2 minutes");
                        break;
                    case 180000:
                        Console.Write($"Current Time = {stopwatchtime.Elapsed} Time limit = 3 minutes");
                        break;
                    case 240000:
                        Console.Write($"Current Time = {stopwatchtime.Elapsed} Time limit = 4 minutes");
                        break;
                    case 300000:
                        Console.Write($"Current Time = {stopwatchtime.Elapsed} Time limit = 5 minutes ");
                        break;
                    default:
                        Console.Write($"Current Time = {stopwatchtime.Elapsed} Time limit = {Timer}");
                        break;
                }

        }

        public static bool IfScore()
        {
            if (Player.GetComputerScore() == GameScore || Player.GetPlayerScore() == GameScore)
            {
                Console.WriteLine($"{GameOver}");
                UserBoxGameSeq.DisplayScores(Player.GetComputerScore(), Player.GetPlayerScore());
                return false;
            }
            else
            {
                return true;
            }

        }

        private static void IfContains(string inputx)
        {

            for (int i = 0; i < GameArray.Count(); i++)
            {
                if (GameArray[i].Name.ToLower().Equals(inputx.ToLower())) GameArray.Remove(GameArray[i]);
            }

        }

    }
}
