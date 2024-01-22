using System.Net.NetworkInformation;

Console.Title = "Boss Battle: Tick-Tack-Toe.";



BoardGame game = new BoardGame();
game.Run();

/// /// 

public class BoardGame
{
    private readonly Square[] _squares;
    private StatusSystem _status;
    private PlayersSystem _playerOne;
    private PlayersSystem _playerTwo;

    public BoardGame()
    {
         _squares = new Square[9];
        for (int i = 0; i < _squares.Length; i++)
            _squares[i] = new Square(SquareState.Empty);
        
        _playerOne = new PlayersSystem(PlayerCharacter.X);
        _playerTwo = new PlayersSystem(PlayerCharacter.O);
        _status = new StatusSystem(_playerOne, _playerTwo);

    }
   public void Run()
    {
        bool isRuning = true;
        while (isRuning)
        {
            _status.CurrentStatus(_squares);
            _status._currentTurn.PickSquare(_squares);
            _status.CharacterTurnChanger(_status._currentTurn);
            _status.winnerChecker(_squares);//
            if (_status.EndGameChecker(_squares)) isRuning = false;

        }
    }
   

}


public class StatusSystem
{
    private PlayersSystem _playerOne { get; set; }
    private PlayersSystem _playerTwo { get; set; }

    public PlayersSystem _currentTurn { get; set; }

    public StatusSystem(PlayersSystem playerOne, PlayersSystem playerTwo)
    {
        _playerOne = playerOne;
        _playerTwo = playerTwo;
        _currentTurn = playerOne;
    }
   
    public void CurrentStatus(Square[] squares)
    {
  
        Console.WriteLine("\n     |     |      ");
        Console.WriteLine($"  {GetValue(squares[6].Status)}  |  {GetValue(squares[7].Status)}  | {GetValue(squares[8].Status)}");
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine($"  {GetValue(squares[3].Status)}  |  {GetValue(squares[4].Status)}  |  {GetValue(squares[5].Status)}");
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine($"  {GetValue(squares[0].Status)}  |  {GetValue(squares[1].Status)}  |  {GetValue(squares[2].Status)}");
        Console.WriteLine("     |     |      \n");
        Console.WriteLine($"It is {_currentTurn.Character} turn.");
        ///
     

    }
    public void CharacterTurnChanger(PlayersSystem currentPlayerTurn)
    {
        if (currentPlayerTurn.Character == _playerOne.Character) _currentTurn = _playerTwo;
        else _currentTurn = _playerOne;


    }
    public bool EndGameChecker(Square[] squares)
    {
        int validator = 0;
        foreach (Square square in squares)
        {

            if (square.Status == SquareState.Empty) validator += 1;
        }
        if (validator == 0) return true;
        else return false;
    }
    public void winnerChecker(Square[] squares)
    {
        if(squares[0].Status == SquareState.O && squares[1].Status == SquareState.O & squares[2].Status == SquareState.O)
        {
            Console.WriteLine("Player O has won the match!");
        }
        if (squares[3].Status == SquareState.O && squares[4].Status == SquareState.O & squares[5].Status == SquareState.O)
        {
            Console.WriteLine("Player O has won the match!");
        }
        if (squares[6].Status == SquareState.O && squares[7].Status == SquareState.O & squares[8].Status == SquareState.O)
        {
            Console.WriteLine("Player O has won the match!");
        }



    }
    private string GetValue (SquareState state)
    {
        switch (state)
        {
            case SquareState.Empty:
                return " ";
            case SquareState.O:
                    return "O";
            case SquareState.X:
                return "X";
            default:
                return " ";

        }
    }


}
public class PlayersSystem
{
    public PlayerCharacter Character { get; }

    public PlayersSystem(PlayerCharacter choice)
    {
        Character = choice;
    }
    
    public void PickSquare(Square[] squares)
    {
        Console.WriteLine("What square do you want to play in?");
      
        bool isNotValid = true;
        while (isNotValid)
        {
            Console.WriteLine("Enter a number between 1 and 9");
            string? input = Console.ReadLine();
            SquarePosition squareSelected = (SquarePosition)Enum.Parse(typeof(SquarePosition), input) - 1;

            if (squares[(int)squareSelected].Status == SquareState.Empty)
            {
                squares[(int)squareSelected].Status = (SquareState)Character; //end of turn
                Console.WriteLine($"squareSelected");
                isNotValid = false;
            }

            else Console.WriteLine("that square is not emply, please select another one");
        }
       

    }
}
public class Square
{
    public SquareState Status { get; set; } = SquareState.Empty;

    public Square(SquareState playerChoice)
    {
        Status = playerChoice;
    }
}

public enum SquareState { X, O, Empty};
public enum PlayerCharacter { X, O};
public enum SquarePosition { DownLeft = 1, Down, DownRight, Right, Center, Left, UpLeft, Up, UpRight}


//Console.Title = "Boss Battle: The Password Validator.";
//bool isInfiniteLoop = true;
//while (isInfiniteLoop)
//{
//    Console.WriteLine("Enter a password to chech it is valid: ");
//    Console.WriteLine("( or press 0 to exit the validation process). ");
//    string? passwordToCheck = Console.ReadLine() ;

//    PasswordValidator myValidator = new PasswordValidator(passwordToCheck);
//    bool validationResult = myValidator.isValid();

//    if (validationResult) Console.WriteLine($"{passwordToCheck} is a valid password! ");
//    else Console.WriteLine($" I'm sorry but {passwordToCheck} is not a valid password...");


//}


//public class PasswordValidator
//{
//    private string? Password { get; set; }

//    public PasswordValidator(string password)
//    {
//        Password = password ?? "";
//    }
//    public bool isValid()
//    {
//        if (Password == null) return false;
//        else if (Password.Length < 6) return false;
//        else if (TandAmpersandChecker() && UpperLowerNumberChecker(Validate.UpperCase) && UpperLowerNumberChecker(Validate.LowerCase) && UpperLowerNumberChecker(Validate.Number)) return true;
//        //else if (TandAmpersandChecker() == true) return false;
//        else return false;

//    }
//    private bool UpperLowerNumberChecker(Validate myValidation)
//    {
//        if (Password == null) return false;
//        if(myValidation == Validate.UpperCase)
//        {
//            foreach (char letter in Password)
//            {
//                bool lettterCheck = char.IsUpper(letter);
//                if (lettterCheck == true) return true;

//            }
//            return false;

//        }else if (myValidation == Validate.LowerCase)
//        {
//            foreach (char letter in Password)
//            {
//                bool lettterCheck = char.IsLower(letter);
//                if (lettterCheck == true) return true;

//            }
//            return false;
//        } else
//        {
//            foreach (char letter in Password)
//            {
//                bool lettterCheck = char.IsDigit(letter);
//                if (lettterCheck == true) return true;

//            }
//            return false;

//        }

//    }
//    private bool TandAmpersandChecker()
//    {
//        if (Password == null) return false;
//        foreach (char character in Password)
//        {
//            if(character == 'T'|| character == '&')
//            {
//                return false;
//            }

//        }
//        return true;

//    }
//}
//enum Validate { UpperCase, LowerCase, Number}
//Console.Title = "Boss Battle:The Locked Door.";

//Console.WriteLine("please create a passcode for your new door: ");
//int anwser = int.Parse(Console.ReadLine());

//Door newDoor = new Door(anwser);

//Console.WriteLine($"What do you want to do with the door? currently the door is {newDoor.DoorStatus}: ");
//Console.WriteLine($"1. Open the door.");
//Console.WriteLine($"2. Close the door.");
//Console.WriteLine($"3. Lock the door.");
//Console.WriteLine($"4. Unlock the door.");
//Console.WriteLine($"5. Change the passcode.");
//Console.WriteLine($"6. Exit.");


//int doorAction = int.Parse(Console.ReadLine());

//while (doorAction < 1 || doorAction > 6)
//{
//    Console.WriteLine("select a valid option, please");
//    Console.WriteLine($"1. Open the door.");
//    Console.WriteLine($"2. Close the door.");
//    Console.WriteLine($"3. Lock the door.");
//    Console.WriteLine($"4. Unlock the door.");
//    Console.WriteLine($"5. Change the passcode.");
//    Console.WriteLine($"6. Exit.");

//    doorAction = int.Parse(Console.ReadLine());
//}
//if(doorAction == 6)
//{
//    return;
//} else if (doorAction == 5)
//{
//    Console.WriteLine("introduce current passcode to change to new one: ");
//    int currentPasscode = int.Parse(Console.ReadLine());
//    Console.WriteLine("introduce new passcode : ");
//    int newPasscode = int.Parse(Console.ReadLine());
//    newDoor.ChangePasscode(currentPasscode, newPasscode);
//} else
//{
//    newDoor.ChangeStatus((Transitions)doorAction);
//}

//while(doorAction != 6)
//{
//    Console.WriteLine($"What do you want to do with the door? currently the door is {newDoor.DoorStatus}: ");
//    Console.WriteLine($"1. Open the door.");
//    Console.WriteLine($"2. Close the door.");
//    Console.WriteLine($"3. Lock the door.");
//    Console.WriteLine($"4. Unlock the door.");
//    Console.WriteLine($"5. Change the passcode.");
//    Console.WriteLine($"6. Exit.");


//     doorAction = int.Parse(Console.ReadLine());

//    while (doorAction < 1 || doorAction > 6)
//    {
//        Console.WriteLine("select a valid option, please");
//        Console.WriteLine($"1. Open the door.");
//        Console.WriteLine($"2. Close the door.");
//        Console.WriteLine($"3. Lock the door.");
//        Console.WriteLine($"4. Unlock the door.");
//        Console.WriteLine($"5. Change the passcode.");
//        Console.WriteLine($"6. Exit.");

//        doorAction = int.Parse(Console.ReadLine());
//    }
//    if (doorAction == 6)
//    {
//        return;
//    }
//    else if (doorAction == 5)
//    {
//        Console.WriteLine("introduce current passcode to change to new one: ");
//        int currentPasscode = int.Parse(Console.ReadLine());
//        Console.WriteLine("introduce new passcode : ");
//        int newPasscode = int.Parse(Console.ReadLine());
//        newDoor.ChangePasscode(currentPasscode, newPasscode);
//    }
//    else
//    {
//        newDoor.ChangeStatus((Transitions)doorAction);
//    }

//}




//public class Door
//{
//    private int _passcode;
//    public Status DoorStatus { get; private set; }

//    public Door(int passcode)
//    {
//        _passcode = passcode;
//        DoorStatus = Status.Open;
//    }
//    public void ChangePasscode(int currentPasscode, int newPasscode)
//    {
//        if (currentPasscode == _passcode)
//        {
//            _passcode = newPasscode;
//            Console.WriteLine("You successfully change the passcode!");
//        }else
//        {
//            Console.WriteLine("You introduce an incorrect passcode. Changing passcode is not allowed.");
//        }
//    }

//    public void ChangeStatus(Transitions newTransition)
//    {
//        if(DoorStatus == Status.Open)
//        {
//            if(newTransition == Transitions.Close)
//            {
//                DoorStatus = Status.Closed;
//            } else
//            {
//                Console.WriteLine("you cannot do that, the door is open!");
//            }

//        } else if (DoorStatus == Status.Closed)
//        {
//            if(newTransition == Transitions.Open)
//            {
//                DoorStatus = Status.Open;
//            }else if (newTransition == Transitions.Lock)
//            {
//                DoorStatus = Status.Locked;
//            } else
//            {
//                Console.WriteLine("you cannot do that, the door is closed!");
//            }
//        } else
//        {
//            if(newTransition == Transitions.Unlock)
//            {
//                Console.WriteLine("Please, enter the passcode to unlock the door:");
//                int? answer = int.Parse(Console.ReadLine());
//                if(answer != null)
//                {
//                    if (answer == _passcode)
//                    {
//                        DoorStatus = Status.Closed;
//                    }
//                    else
//                    {
//                        Console.WriteLine("Wrong password!");
//                    }
//                } else
//                {
//                    Console.WriteLine("insert an int type!");
//                }



//            } else
//            {
//                Console.WriteLine("you cannot do that, the door is locked!");
//            }
//        }
//    }

//};
//public enum Status { Open = 1, Closed, Locked};
//public enum Transitions { Open = 1, Close, Lock, Unlock };

///////////////

//Console.Title = "Boss Battle: The Card.";

//Card.DisplayDeck();

//public class Card
//{
//    public Colours Colour { get; }
//    public Ranks Rank { get; }

//    public Card (Colours colourCard,Ranks rankCard)
//    {
//        Colour = colourCard;
//        Rank = rankCard;
//    }

//    public static void CardType ( Card card)
//    {
//        if((int)card.Rank > 9)
//        {
//            Console.WriteLine("This card is a symbol card");
//        } else
//        {
//            Console.WriteLine("This card is a number card");
//        }
//    }

//    public static void DisplayDeck()
//    {
//       for(int i = 0; i < 4; i++)
//        {
//            for( int j = 0; j < 14; j++)
//            {
//                Card currentCard = new Card( ( Colours )i , ( Ranks )j );
//                Console.WriteLine($"The {currentCard.Colour} {currentCard.Rank}");
//            }
//        }
//    }
//}

//public enum Ranks { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Dollar, Percentage, Caret, Ampersand }
//public enum Colours { Red, Green, Blue, Yellow}

/////////////
/////Console.Title = "Boss Battle: The Colour.";
//Colour myColour = new Colour(100, 10, 31);
//Colour green = Colour.Green;
//Console.WriteLine($" my color has this values: ({myColour.RedChannel}, {myColour.GreenChannel}, {myColour.BlueChannel})");
//Console.WriteLine($" green color has this values: ({green.RedChannel}, {green.GreenChannel}, {green.BlueChannel})");



//public class Colour
//{
//    public byte RedChannel { get; }
//    public byte GreenChannel { get; }
//    public byte BlueChannel { get; }

//    public static Colour White { get; } = new Colour( 255, 255, 255 );
//    public static Colour Red { get; } = new Colour( 255, 0, 0 );
//    public static Colour Orange { get; } = new Colour( 255, 165, 0 );
//    public static Colour Yellow { get; } = new Colour( 255, 255, 0 );
//    public static Colour Blue { get; } = new Colour( 0, 0, 255 );
//    public static Colour Purple { get; } = new Colour( 128, 0, 128 );
//    public static Colour Green { get; }
//    public static  Colour Black { get; }

//    public Colour(byte red, byte green, byte blue)
//    {

//        RedChannel = red;
//        GreenChannel = green;
//        BlueChannel = blue;
//    }
//    static Colour()
//    {
//        Black = new Colour( 0, 0, 0 );
//        Green = new Colour(0, 128, 0);
//    }

//}
/////////////////////

//private void Validator( byte redValue, byte greenValue, byte blueValue)
//{
//    if (redValue < 0 || greenValue < 0 || blueValue < 0 || redValue >= 255 || greenValue >= 255 || blueValue >= 255)
//    {
//        Console.WriteLine("value must be between 0 and 255!");
//        return;
//    } else
//    {

//    }

//}

//enum ColourType { Red, Green, Blue};
///////////////////////////

//Point myFirstPoint = new Point(2,3);
//Point mySecondPoint = new Point(-4, 0);
//Console.WriteLine($"My first point cordenates is ({myFirstPoint.X}, {myFirstPoint.Y})");
//Console.WriteLine($"My second point cordenates is ({mySecondPoint.X}, {mySecondPoint.Y})");

//public class Point
//{
//    public int X { get; }
//    public int Y { get; }

//    public Point (int x, int y)
//    {
//        X = x;
//        Y = y;
//    }
//    public Point() : this(0, 0) { }

//}


///////////////////////////////////////////
//Console.WriteLine("do you want a custom arrow or one predifine? ");
//Console.WriteLine("1. Custom arrow.");
//Console.WriteLine("2. Predefine one is ok.");
//int answer = int.Parse(Console.ReadLine());
//while (answer < 1 || answer > 2)
//{
//    Console.WriteLine("please, select a valid option: ");
//    answer = int.Parse(Console.ReadLine());
//}
//if( answer == 1)
//{
//    Arrow myCustomArrow = arrowClass();
//    float myArrowCost = myCustomArrow.GetCost();
//    Console.WriteLine($"All right, sir! My work is done. Here you have your arrow with a {myCustomArrow.Arrowhead} arrowhead, {myCustomArrow.Fletching} fletching with {myCustomArrow.Lenght} cm long.");
//    Console.WriteLine($"It all cost {myArrowCost} Gold, please sir.");
//} else
//{
//    Console.WriteLine("Ok, which arrow do you want?: ");
//    Console.WriteLine("1. Elite arrow");
//    Console.WriteLine("2. Beginners arrow");
//    Console.WriteLine("3. Marksman arrow");

//    int predefinedChoice = int.Parse(Console.ReadLine());
//    while (answer < 1 || answer > 3)
//    {
//        Console.WriteLine("please, select a valid option: ");
//        predefinedChoice = int.Parse(Console.ReadLine());
//    }
//    Arrow arrowType;

//    if(predefinedChoice == 1)
//    {
//        arrowType = Arrow.CreateEliteArrow();
//    }else if (predefinedChoice == 2)
//    {
//        arrowType = Arrow.CreateBeginnerArrow();
//    }
//    else
//    {
//        arrowType = Arrow.CreateMarksmanArrow();
//    }
//    Console.WriteLine($"here you have your arrow, it will cost {arrowType.GetCost()}, sir");
//}




// Arrowhead GetTypeArrowhead()
//{
//    Console.WriteLine("What type of arrowhead do you want, noble sir?, select an option: ");
//    Console.WriteLine("1. Steel : 10 Gold");
//    Console.WriteLine("2. Wood: 3 Gold");
//    Console.WriteLine("3. Obsidian: 5 Gold");
//    int answer = int.Parse(Console.ReadLine());
//   while(answer < 1 || answer > 3)
//    {
//        Console.WriteLine( "sorry, we don't have that chose one of the following options: " ) ;
//        Console.WriteLine( "1. Steel : 10 Gold" );
//        Console.WriteLine( "2. Wood: 3 Gold" );
//        Console.WriteLine( "3. Obsidian: 5 Gold" );
//        answer = int.Parse( Console.ReadLine() );
//    };
//    if (answer == 1) {
//        return Arrowhead.Steal;
//    }else if (answer == 2) {
//        return Arrowhead.Wood;
//    }
//    else
//    {
//        return Arrowhead.Obsidian;
//    };
//};

//Fletching GetTypeFletching()
//{
//    Console.WriteLine("What type of fletching do you want, noble sir?, select an option: ");
//    Console.WriteLine("1. Plastic : 10 Gold");
//    Console.WriteLine("2. Turkey Feathers: 5 Gold");
//    Console.WriteLine("3. Goose Feathers: 3 Gold");
//    int answer = int.Parse(Console.ReadLine());
//    while (answer < 1 || answer > 3)
//    {
//        Console.WriteLine("sorry, we don't have that chose one of the following options: ");
//        Console.WriteLine("1. Plastic : 10 Gold");
//        Console.WriteLine("2. Turkey Feathers: 5 Gold");
//        Console.WriteLine("3. Goose Feathers: 3 Gold");
//        answer = int.Parse(Console.ReadLine());
//    };
//    if (answer == 1)
//    {
//        return Fletching.Plastic;
//    }
//    else if (answer == 2)
//    {
//        return Fletching.TurkeyFeathers;
//    }
//    else
//    {
//        return Fletching.GooseFeathers;
//    };
//}
//int GetLengthShaft()
//{
//    Console.WriteLine("How long do you want your arrow?, I can make it between O to 60 cm, sir");
//    int answer = int.Parse(Console.ReadLine());
//    while(answer<= 0 || answer > 60) {
//        Console.WriteLine("sorry noble sir, but that measure is not possible. Please introduce a int between 0 and 60. ");
//        answer = int.Parse(Console.ReadLine());
//    }
//    return answer;
//}

//Arrow arrowClass()
//{
//    Arrowhead arrowhead = GetTypeArrowhead();
//    Fletching fletching = GetTypeFletching();
//    int arrowLength = GetLengthShaft();
//    Arrow newArrow = new Arrow(arrowhead, fletching, arrowLength);
//    return newArrow;
//};


//public enum Arrowhead { Steal, Wood, Obsidian };
//public enum Fletching { Plastic, TurkeyFeathers, GooseFeathers};

//public class Arrow
//{
//  public Arrowhead Arrowhead { get; }
//  public Fletching Fletching { get;  }
//  public int Lenght { get; }

//  public Arrow(Arrowhead arrowhead, Fletching fletching, int lenght)
//   {
//        Arrowhead = arrowhead;
//        Fletching= fletching;
//        Lenght= lenght;

//   }

//  public float GetCost()
//    {
//        float totalCost = 0;
//        switch (Arrowhead)
//        {
//            case Arrowhead.Steal:
//                totalCost += 10;
//                break;
//            case Arrowhead.Wood:
//                totalCost += 3;
//                break;
//            case Arrowhead.Obsidian:
//                totalCost += 5;
//                break;
//            default:
//                break;
//        };
//        if (Fletching == Fletching.Plastic)
//        {
//            totalCost += 10;
//        } else if (Fletching == Fletching.TurkeyFeathers)
//        {
//            totalCost += 5;
//        } else
//        {
//            totalCost += 3;
//        };

//        float costLenght = Lenght * 0.05f;
//        totalCost += costLenght;

//        return totalCost;
//    }

//  public static Arrow CreateEliteArrow() => new Arrow(Arrowhead.Steal, Fletching.Plastic, 95);
//  public static Arrow CreateBeginnerArrow() =>new Arrow(Arrowhead.Wood, Fletching.GooseFeathers, 75);  
//  public static Arrow CreateMarksmanArrow() => new Arrow(Arrowhead.Steal, Fletching.GooseFeathers, 65);

//}

////////////////////////////////

//(Food food, Ingridient ingridient, Seasoning seasoning) soup;
//Console.WriteLine("Hi there sweety, What would you like to eat today?");
//Console.WriteLine("Choose one of the options: ");
//Console.WriteLine("1.- Soup");
//Console.WriteLine("2.- Stew");
//Console.WriteLine("3.- Gumbo");

//int foodChoice = int.Parse(Console.ReadLine());

//while(foodChoice > 3 || foodChoice < 1)
//{
//    Console.WriteLine("sorry love, we don't have that. Choose one of the following:");
//    Console.WriteLine("1.- Soup");
//    Console.WriteLine("2.- Stew");
//    Console.WriteLine("3.- Gumbo");
//    foodChoice = int.Parse(Console.ReadLine());
//}
//Console.WriteLine(" What would you like as a main ingredient?");
//Console.WriteLine("Choose one of the options: ");
//Console.WriteLine("1.- Mushrooms");
//Console.WriteLine("2.- Chicken");
//Console.WriteLine("3.- Potato");
//Console.WriteLine("4.- Carrot");

//int ingridientChoice = int.Parse(Console.ReadLine());

//while (ingridientChoice > 4 || ingridientChoice < 1)
//{
//    Console.WriteLine("sorry love, we don't have that. Choose one of the following:");
//    Console.WriteLine("1.- Mushrooms");
//    Console.WriteLine("2.- Chicken");
//    Console.WriteLine("3.- Potato");
//    Console.WriteLine("4.- Carrot");
//    ingridientChoice = int.Parse(Console.ReadLine());
//}
//Console.WriteLine(" What would you like for seasoning?");
//Console.WriteLine("Choose one of the options: ");
//Console.WriteLine("1.- Salty");
//Console.WriteLine("2.- Spicy");
//Console.WriteLine("3.- Sweet");

//int seasoningChoice = int.Parse(Console.ReadLine());

//while (seasoningChoice > 3 || seasoningChoice < 1)
//{
//    Console.WriteLine("sorry love, we don't have that. Choose one of the following:");
//    Console.WriteLine("1.- Salty");
//    Console.WriteLine("2.- Spicy");
//    Console.WriteLine("3.- Sweet");

//    seasoningChoice = int.Parse(Console.ReadLine());
//}
//Console.Clear();
//soup = ((Food)foodChoice, (Ingridient)ingridientChoice, (Seasoning)seasoningChoice);
//Console.WriteLine($"Ok, let's cook a nice meal with {soup}!!");

//enum Food  { Soup = 1, Stew, Gumbo};
//enum Ingridient {Mushrooms = 1, Chicken, Potato, Carrot };
//enum Seasoning { Salty = 1, Spicy, Sweet };

//////////////////////////////////////////////
//ChestState currentState = ChestState.Open;
//while(true)
//{
//    string chestState = Actions(currentState);
//    Console.Write($"The chest is {chestState}. What do you want to do? ");
//    string answer = Console.ReadLine();
//    ChangeState(answer);
//}
//void ChangeState (string command)
//{
//    switch (command)
//    {
//        case "open":
//            if(currentState == ChestState.Closed)
//            {
//                currentState = ChestState.Open;
//            }
//            break;
//        case "close":
//            if(currentState == ChestState.Open)
//            {
//                currentState = ChestState.Closed;

//            }
//            break;
//        case "unlock":
//            if(currentState == ChestState.Locked)
//            {
//                currentState = ChestState.Closed;
//            }
//            break;
//        case "lock":
//            if (currentState == ChestState.Closed)
//            {
//                currentState = ChestState.Locked;
//            }
//            break;
//        default:
//            {
//                break;
//            }
//    }
//}

//string Actions (ChestState state)
//{
//    switch (state)
//    {
//        case ChestState.Open:
//            return "open";
//        case ChestState.Closed:
//            return "unlocked";
//        case ChestState.Locked:
//            return "locked";
//        default:
//            return "open";
//    }
//}
//enum ChestState { Open, Closed, Locked};

