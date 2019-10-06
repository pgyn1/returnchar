﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ReturnChar
{
    class Player
    {

        //We want to have the option of pass_
        private static string playerinput;
        private static string computerinput;
        private static string ExceededLimit;
        private static string CompAnsValid;
        private static readonly string alpha;
        private static int computercounter;
        private static int playercounter;
        private static int limitzero;

        private static int playerlevelscore;

        static readonly Random RandPlayer = new Random();
        static readonly UserBox UserBoxPlayer = new UserBox();
        static readonly Valid ValidPlayer = new Valid();

        private static Stopwatch stopwatchplayer = new Stopwatch();

        //remove static references? call an object on player methods.
        public Player()
        {
            
        }

        static Player()
        {
            playerinput = "";
            computerinput = "";
            ExceededLimit = "{Exceeded time limit}";
            CompAnsValid = "\n{ Answer valid BookID added, Computer+1}";
            computercounter = 0;
            playercounter = 0;
            limitzero = 45000;
            alpha = "abcdefghijklmnopqrstuvwxyz";

            playerlevelscore = 0;
     

    }

        public static void SetPlayerLevelScore(int x)
        {
            playerlevelscore += x;
        }

        public static int GetPlayerLevelScore()
        {
            return playerlevelscore;
        }

        public static void Restoreplayerlevelscore(int x)
        {
            playerlevelscore = x;
        }

        public static int GetTimerLimit()
        {
            return limitzero;
        }

        public static void SetGameRequest(string computerinput)
        {
            stopwatchplayer.Restart();
            SetContinueWithInput();
            stopwatchplayer.Stop();

            if (stopwatchplayer.ElapsedMilliseconds > limitzero)
            {
                IfPlayerNotValidLimit();
            }
            else
            {
                if (ValidPlayer.ToCase(computerinput, playerinput))
                {
                    if (ValidPlayer.ValidName(playerinput))
                    {
                        SetPlayerScore(1);
                       // SetPlayerLevelScore(1);
                    }
                    else
                    {
                        IfPlayerNotValid();
                    }
                }
                else
                {
                    IfPlayerNotValid();
                }
            }
        }

        public static void SetGameRequestEasy(string computerinput)
        {
            stopwatchplayer.Restart();
            SetContinueWithInput();
            stopwatchplayer.Stop();

            if(stopwatchplayer.ElapsedMilliseconds > limitzero)
            {
                IfPlayerNotValidLimit();
            }
            else
            {
                if(ValidPlayer.ToCase(computerinput, playerinput))
                {
                    if (ValidPlayer.ValidName(playerinput))
                    {
                        SetPlayerScore(1);
                        //SetPlayerLevelScore(1);
                    }
                    else
                    {
                        Console.WriteLine("{Player fails to respond}");
                        playerinput = GetAlphabet();
                    }
                }
                else
                {
                    Console.WriteLine("{Player fails to respond}");
                    playerinput = GetAlphabet();
                }
            }
        }

        public static void SetGameRequestSuper(string computerinput)
        {
            List<string> playerinputlist = new List<string>();

            //Start the time limit stopwatch;
            stopwatchplayer.Restart();
            do
            {
                playerinputlist.Clear(); //clear the list

                //name, type, where, extra // 0 - 3
                //Request playerinput 4 times and append to input to the list
                for (int i = 0; i < 4; i++)
                {
                    Console.Write($"{UserBoxPlayer.GamePromptPlayerInput} "); // Request Prompt for input
                    var playinput = Console.ReadLine(); //Read player input
                    playerinputlist.Add(playinput); //append to the inputlist

                }
                Console.Write("Continue (c)?"); // Prompt for verification

            } while (!Console.ReadLine().ToLower().Equals("c")); //if equals c then accept the inputlist

            stopwatchplayer.Stop(); // Stop the stopwatch timer

            //If the elapsedmilliseconds > the limit then call the random character input, assign to the 
            // playerinput var and decrement the player's score by 1
            if (stopwatchplayer.ElapsedMilliseconds > limitzero)
            {
                IfPlayerNotValidLimit();
            }
            else
            {
              
                //ValidNameTypeWhereExtra{ }
                //Verify that the prev == the current (char)
                if (ValidPlayer.ToCase(computerinput, playerinputlist[0]))
                {                       
                    //Verify the inputlist with a call from the valid class
                    if (ValidPlayer.ValidNameTypeWhereExtra(playerinputlist))
                    {
                        playerinput = playerinputlist[0]; //Assign the name input to the playerinput
                        SetPlayerScore(1); //Increment the player's score
                        //SetPlayerLevelScore(1);
                    }
                    else
                    {
                        IfPlayerNotValid(); //Call the ifplayernotvalid method and assign a random char to the player's input
                    }
                }
                else
                {
                    IfPlayerNotValid();
                }
            }
        }

        // SetPlayerInput depend. difficulty level
        // responding to computer input
        public static void SetPlayerInput(char difflvl, string computer)
        {
            try
            {     
                if (difflvl.ToString().ToLower().Equals("s"))
                {
                    SetGameRequestSuper(computer);
                }
                else if (difflvl.ToString().ToLower().Equals("e"))
                {
                    SetGameRequestEasy(computer);
                }
                else  // Medium || Hard
                {
                    SetGameRequest(computer);
                }
            }
            catch (InvalidOperationException invexc)
            {
                Console.WriteLine($"{invexc.Message}\n{ invexc.InnerException }");
               
                IfPlayerNotValid();

            }catch(Exception e)
            {
                Console.WriteLine($"Player -> SetPlayerInput(char, string) + {e.Message}");
                Console.WriteLine($"{e.InnerException}");

                IfPlayerNotValid();
            }

        }

        public static void SetPlayerInput()
        {
            try
            {
                stopwatchplayer.Restart();
                SetContinueWithInput();
                stopwatchplayer.Stop();

                if(stopwatchplayer.ElapsedMilliseconds > limitzero)
                {
                    IfPlayerNotValidLimit();
                }
                else
                {
                    if (!ValidPlayer.ValidName(playerinput)) { IfPlayerNotValid(); } else { SetPlayerScore(1);  }
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine($"Player -> SetPlayerInput() + {e.Message} + {e.InnerException}");
                Console.WriteLine($"Random character assigned to playerinput");
                IfPlayerNotValid();
            }
           
        }

        //Used in the case of setting a random char to the playerinput
        public static void SetPlayerInput(string x)
        {
            playerinput = x;
        }

        public static void SetComputerInput(List<Book> gamearray)
        {

            //var c = UserBoxPlayer.ConvertBookToListGetX(gamearray, "N"); // returning a list<string> of names
            //computerinput = c[RandPlayer.Next(c.Count())]; // Select a random answer from the list<strings> in the current gamearray
            try
            {
                if (gamearray.Any())
                {
                    int randindx = RandPlayer.Next(gamearray.Count());
                    computerinput = gamearray[randindx].Name;

                    if (ValidPlayer.ValidComputerName(computerinput))
                    {
                        Console.WriteLine($"{UserBoxPlayer.GamePromptCompInput} {computerinput} ]"); //print output
                        Console.WriteLine($"{CompAnsValid}");
                        SetComputerScore(1);
                    }
                    else
                    {
                        Console.WriteLine("Player --> SetComputerInput(List<Book>) + Returning GetAlphabet, not displaying prompt just letter!");
                        computerinput = GetAlphabet();
                        Console.WriteLine($"{UserBoxPlayer.GamePromptCompInput} {computerinput} ]"); //print output
                    }
                    
                }
                else
                {
                    // We may need to revise this as we don't want to return a random character if the list is empty
                    Console.WriteLine("Player --> SetComputerInput(List<Book>)");
                    Console.WriteLine("Gamearray = none. Returning a random char.");
                    computerinput = GetAlphabet();
                    Console.WriteLine($"{UserBoxPlayer.GamePromptCompInput} {computerinput} ]");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Player -> SetComputerInput(List<Book>) + {e.Message}\n{e.InnerException}");
                computerinput = GetAlphabet();
                Console.WriteLine($"{UserBoxPlayer.GamePromptCompInput} {computerinput} ]");
            }
           

        }

        public static void SetComputerInput(List<Book> gamearrayX, string player, char difflvl)
        {

            //Handling argument null exceptions //var name = .Split(','); // playerinput as a string array name[0]

            try
            {
                var lastletter = player.Last().ToString().ToLower();
                var SelectedListNew = gamearrayX.FindAll(q => q.Name.ToLower().StartsWith(lastletter)); //Return a list of books // get a sublist (letters beg. with)

                if (SelectedListNew.Any())
                {
                    var randindx = RandPlayer.Next(SelectedListNew.Count());
                    computerinput = SelectedListNew[randindx].Name;

                    if (ValidPlayer.ValidComputerName(computerinput))
                    {
                        Console.WriteLine($"{UserBoxPlayer.GamePromptCompInput} {computerinput} ]");
                        Console.WriteLine($"{CompAnsValid}");
                        SetComputerScore(1);
                    }
                    else
                    {  
                        GetAlphabet();
                        Console.WriteLine($"{UserBoxPlayer.GamePromptCompInput} {computerinput} ]");
                    }
                   
                }
                else
                {
                    if (difflvl.ToString().ToLower().Equals("e"))
                    {
                        computerinput = GetAlphabet();
                        Console.WriteLine("Computer failed to respond.");
                    }
                    else
                    {
                        computerinput = GetAlphabet();
                        Console.WriteLine($"{UserBoxPlayer.ComputerNoResponse} {computerinput} ] {"}"}");
                    }
                    
                    //SetPlayerScore(1); // increment the player's score
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Player -> SetComputerInput(List<Book>, string) + {e.Message}");
                Console.WriteLine($"{e.InnerException}");
            }

        }

        public static void SetComputerInput(string x)
        {
            //SetPlayerScore(1); // increment the player's score
            computerinput = x;
        }

        public static string GetComputerInput()
        {
            return computerinput;
        }

        public static string GetPlayerInput()
        {
            return playerinput;
        }

        //The computer score should only be set in this class via a public method or set in another class with the restore method
        private static void SetComputerScore(int x)
        {
            computercounter += x;
        }

        public static int GetComputerScore()
        {
            return computercounter;
        }

        //The player score should only be set in this class or via a public setter
        private static void SetPlayerScore(int x)
        {
            playercounter += x;
            if (x > 0) playerlevelscore += 1;
        }

        public static int GetPlayerScore()
        {
            return playercounter;
        }

        public static void RestoreComputerScore(int x)
        {
            computercounter = x;
        }

        public static void RestorePlayerScore(int x)
        {
            playercounter = x;
        }

        public static void SetContinueWithInput()
        {
            // to the left of prompt - Console.Write($"");
            do
            {
                //Console.WriteLine($"\t\t\t\tTime stamp = {stopwatchplayer.Elapsed}");
                Console.Write($"{UserBoxPlayer.GamePromptPlayerInput} "); //Prompt for input
                playerinput = Console.ReadLine();
                Console.Write("Continue(c) ?");
                
            } while (!Console.ReadLine().ToLower().Equals("c"));
        }

        private static void IfPlayerNotValid()
        {
            playerinput = GetAlphabet(); //Assign a random char to the playerinput var
            Console.WriteLine($"{UserBoxPlayer.PlayerNoResponse} {playerinput} ] {"}"}");
            //SetComputerScore(1); //Increment the computer score by 1
        }

        private static void IfPlayerNotValidLimit()
        {
            Console.WriteLine($"{ExceededLimit}");
            playerinput = GetAlphabet(); //Assign a random char to the playerinput var
            Console.WriteLine($"Player returns invalid, playerscore - 1, Return char[ {playerinput} ]");
            SetPlayerScore(-1); //Decrement the player's score by 1
        }


        public static string GetAlphabet()
        {
            try { return alpha[RandPlayer.Next(alpha.Length)].ToString(); } catch (Exception e) { Console.WriteLine($"player -> GetAlphabet() + {e.Message}\n{e.InnerException}"); return 'a'.ToString(); }
        }

    }
}

