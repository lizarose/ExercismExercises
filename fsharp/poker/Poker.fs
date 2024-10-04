module Poker
// TODO: implement this module
(*
    1. Define the types
        -hands
    2. Find the best hands 
        -function bestHands
*)
type Suit =
    | Hearts
    | Spades
    | Clubs
    | Diamonds

type Value = 
    | Two | Three | Four | Five | Six | Seven | Eight | Nine  | Ten 
    | Jack | Queen | King | Ace 

type HandType = 
    | RoyalFlush
    | StraightFlush
    | FourOfKind
    | FullHouse
    | Flush
    | Straight
    | ThreeOfKind
    | TwoPair
    | OnePair
    | HighCard

type Card = { Suit: Suit; Value: Value; }

//list of Card objects
type Hand = Card list 

let bestHands string = failwith "Best Hands Error";

let isFlush (hand: Hand) = failwith "Flush Error";

let isStraight (hand: Hand) = failwith "Straight Error";

let isRoyalFlush (hand: Hand) = failwith "Royal Flush Error";

let isGroup (hand: Hand) = failwith "Group Error";






//find card values
//find highest score
//return best hand


//define poker hand rankings

//function to check for each poker type hand

//need to determine the high ranking hand

//ties? --> compare based on highest card then
//sort so can compare highest to lowest
//sort descending --> need it to be Ace, King, 10, 9, etc. 


//compare by high card --> compare hands --> best hands




(*
    Flush --> cards need to be same suit
    Straight --> consecutive sequence   
        -straight flush --> is both 
        -royal flush --> Ace, King, Queen, Jack, Ten + same suit

    Pairs --> group by values to find matches
        -one pair
        -two pair
        -three of a kind
        -four of a kind
        -full house (3, 2)

- high card is default case as if all else fails, it defaults to the highest card --> this is also the weakest hand to have 

*)


(*
    Ranking:
        -match the HandType with functions to rank hand

    Order:
        1. Royal Flush     --> Ace, King, Queen, Jack, Ten + Same Suit 
        2. Straight Flush  --> 10, 9, 8, 7, 6 + Same Suit
        3. Four of a Kind  --> Ace, Ace, Ace, Ace
        4. Full House      --> 7, 7, 7, 3, 3
        5. Flush           --> Jack, 9, 5, 3, 2 + Same Suit
        6. Straight        --> 10, 9, 8, 7, 6
        7. Three of a Kind --> 4, 4, 4
        8. Two Pair        --> 8, 8, 9, 9
        9. One Pair        --> 8, 8
        10. High Card      --> default
*)