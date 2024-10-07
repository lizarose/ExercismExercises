module Poker
(*
    TODO: implement this module:
    - pick the best hand(s) from a list of poker hands

        1. Define the types
        2. Find the best hands 
*)
type Suit =
    | Hearts = 'H'
    | Spades = 'S'
    | Clubs = 'C'
    | Diamonds = 'D'

type Value = 
    | Two = 2
    | Three = 3
    | Four = 4
    | Five = 5
    | Six = 6
    | Seven = 7
    | Eight = 8
    | Nine = 9
    | Ten = 10
    | Jack = 11
    | Queen = 12
    | King = 13
    | Ace = 14

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

type HandOutcome = 
    | Win of Hand
    | Lose of Hand
    | Tie 


let isFlush (hand: Hand) : bool = 
    hand 
    |> List.map (fun card -> card.Suit)
    |> List.distinct
    |> List.length = 1
(*
    - need to check if all the cards have the same suit
    - List.map          --> finds the suits in hand, ignore value, and puts the suits into a list
    - List.distinct     --> takes that list of suits that are in hand and removes extras
    - List.length = 1   --> says that for it to be a flush, the list of suits must be one
*)


let isStraight (hand: Hand) : bool = 
    let values = 
        hand 
        |> List.map (fun card -> int card.Value)
        |> List.sort
    values 
    |> List.pairwise
    |> List.forall(fun (x, y) -> y = x + 1)
    || values = [2; 3; 4; 5; 14] //low straight --> Ace is value at 1 
(*
    - need to check if all the cards are consecutive
    - ignore suit --> focus on value
    
    - List.map              --> finds the value for each card in the hand and puts into a list
    - List.sort             --> sorts the list by values 
    - List.pairwise         --> takes the list of values and pairs them together (x, y), 
                                with (y) being the next value that goes into (x)
                                [2, 3, 4, 5, 6]
                                (2, 3) (3, 4) (4, 5) (5, 6)
    -List.forall            --> applies the function to all the elements in the list
                                -this function takes the pairs and checks them by making 
                                sure (y) is equal to the next value if (x + 1)

    - The OR for values is a special case that checks if it is a low straight, having
        Ace be valued at 1 instead of 14
*)


let isStraightFlush (hand: Hand) : bool = isFlush hand && isStraight hand 


let isRoyalFlush (hand: Hand) : bool = 
    isStraightFlush hand &&
    let values = hand |> List.map (fun card -> int card.Value)
    values |> List.sort = [10; 11; 12; 13; 14]
(*
    - needs to be Flush and Straight
    - highest value needs to be Ace

    - call isFlush and isStraight bc Royal Flush must be both of those

    - List.map          --> getting value of card and puts into list
    - List.distinct     --> removing extras from list
    
    - call values and sort it, having the nums equal certain nums bc a Royal Flush is 
        10, Jack, Queen, King, Ace of same suit
*)

let countValuePairs hand =
    hand
    |> List.map (fun card -> int card.Value)
    |> List.countBy id
    |> List.map snd
(*
    -use this function for isPair and isFullHouse 
    -just call it on both since the code is the same for both
*)


// let isPair (hand: Hand) : bool = 
//     let count = countValuePairs hand 
//     count |> List.exists (fun count -> count = 2)
(*
    - group by value
    - count how many 
    - One Pair
    - Two Pair
    - Three of a Kind
    - Four of a Kind
    - Full House (3x, 2y)

    - List.map          --> get value from hand
    - List.exists       --> count the pairs
    - List.countBy id   --> counts the times a distinct element is found
*)



let countPairs (hand: Hand) = 
    let cvp = countValuePairs hand
    cvp |> List.filter (fun count -> count = 2) |> List.length 

let isOnePair (hand: Hand) : bool = countPairs hand = 1

let isTwoPair (hand: Hand) : bool = countPairs hand = 2


let isThreeOfKind (hand: Hand) : bool = 
    let cvp = countValuePairs hand
    cvp |> List.exists (fun count -> int count = 3)


let isFourOfKind (hand: Hand) : bool = 
    let cvp = countValuePairs hand
    cvp |> List.exists (fun count -> int count = 4)


let isFullHouse (hand: Hand) : bool = 
    let cvp = countValuePairs hand 
    let hasThreeOfKind = isThreeOfKind hand
    let hasPair = cvp |> List.exists (fun count -> count = 2)
    hasThreeOfKind && hasPair 
(*
    -needs to have count of 3
    -needs to have count of 2
*)


let rankHand (hand: Hand) : HandType = 
    if isRoyalFlush hand then RoyalFlush
    elif isStraightFlush hand then StraightFlush
    elif isFourOfKind hand then FourOfKind
    elif isFullHouse hand then FullHouse
    elif isFlush hand then Flush
    elif isStraight hand then Straight
    elif isThreeOfKind hand then ThreeOfKind
    elif isTwoPair hand then TwoPair
    elif isOnePair hand then OnePair
    else HighCard


let handRankings = 
    Map.ofList [
        (RoyalFlush, 10);
        (StraightFlush, 9);
        (FourOfKind, 8);
        (FullHouse, 7);
        (Flush, 6);
        (Straight, 5);
        (ThreeOfKind, 4);
        (TwoPair, 3);
        (OnePair, 2);
        (HighCard, 1);
    ]

// let compareHands (hand1: Hand) (hand2: Hand) : HandOutcome = 
//     let rankHand1 = rankHand hand1
//     let rankHand2 = rankHand hand2

//     match rankHand1, rankHand2 with
//     | r1, r2 when r1 > r2 -> Win hand1
//     | r1, r2 when r1 < r2 -> Lose hand1
//     | _ -> 
//         let findHighCard hand = 
//             hand
//             |> List.map (fun card -> int card.Value)
//             |> List.max

//         let highCardHand1 = findHighCard hand1
//         let highCardHand2 = findHighCard hand2

//         if highCardHand1 > highCardHand2 then Win hand1
//         elif highCardHand1 < highCardHand2 then Lose hand1
//         else Tie
(*
    - compare two hands
    - they are the same rank so looking to find highest card
    - this is for ties to help break them
*)

let parseCard (cardStr: string) : Card = 
    let value = 
        match cardStr.[0] with
        | '2' -> Value.Two
        | '3' -> Value.Three
        | '4' -> Value.Four
        | '5' -> Value.Five
        | '6' -> Value.Six
        | '7' -> Value.Seven
        | '8' -> Value.Eight
        | '9' -> Value.Nine
        | 'T' -> Value.Ten
        | 'J' -> Value.Jack
        | 'Q' -> Value.Queen
        | 'K' -> Value.King
        | 'A' -> Value.Ace
        | _ -> failwith "Invalid Value"
    let suit = 
        match cardStr.[1] with
        | 'H' -> Suit.Hearts
        | 'S' -> Suit.Spades
        | 'C' -> Suit.Clubs
        | 'D' -> Suit.Diamonds
        | _ -> failwith "Invalid Suit"  
    { Suit = suit; Value = value }

let parseHand (handStr: string) : Hand = 
    handStr.Split(' ') |> Array.toList |> List.map parseCard


let bestHands (hands: string list) =
    let parsedHands = hands |> List.map parseHand

    let bestRank = 
        parsedHands
        |> List.map rankHand
        |> List.maxBy (fun handType -> Map.find handType handRankings)

    hands |> List.filter (fun hand -> parseHand hand |> rankHand = bestRank)

(*
    - find best hand from list of hands
    - need to rank them
    - find highest rank
    - determine best hand 
    -
*)


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
        -full house (3x, 2y)

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