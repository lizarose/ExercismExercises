module Poker
//Types
type Suit =
    | Hearts = 'H' | Spades = 'S' | Clubs = 'C' | Diamonds = 'D'
    
type Value = 
    | Two = 2 | Three = 3 | Four = 4 | Five = 5 | Six = 6 | Seven = 7 | Eight = 8 | Nine = 9 | Ten = 10 | Jack = 11 | Queen = 12 | King = 13 | Ace = 14
    
type HandType = 
    | HighCard | OnePair | TwoPair  | ThreeOfKind  | Straight | LowStraight | Flush  | FullHouse  | FourOfKind  | StraightFlush | RoyalFlush 

type Card = { Suit: Suit; Value: Value; }
type Hand = Card list 





//HandType Functions



//Flush --> checks for cards to all be the same suit, ignoring values
let isFlush (hand: Hand) = 
    hand
    |> List.map (fun card -> card.Suit)
    |> List.distinct
    |> List.length = 1


//Low Straight --> checks that values are Ace, 2, 3, 4, 5
let isLowStraight (values: int list) = values = [2; 3; 4; 5; 14]


//Straight --> checks that values go in consecutive order, ignoring suits
let isStraight (hand: Hand) = 
    let values = 
        hand 
        |> List.map (fun card -> int card.Value) 
        |> List.sort
    values 
    |> List.pairwise 
    |> List.forall(fun (x, y) -> y = x + 1) 
    || isLowStraight values


//Straight Flush --> checks that cards are all the same suit and go in consecutive order
let isStraightFlush (hand: Hand) = isFlush hand && isStraight hand 


//Royal Flush --> checks that cards are same suit and that values go in consecutive order, but only highest values: 10, Jack, Queen, King, Ace
let isRoyalFlush (hand: Hand) = 
    isStraightFlush hand && 
    hand |> List.map (fun card -> int card.Value) |> List.sort = [10; 11; 12; 13; 14]


//Count Value Pairs --> list of tuples that returns list of ints (x, y) with (x) = Card Value and (y) = occurances of (x)
let countValuePairs hand = 
    hand 
    |> List.map (fun card -> int card.Value) |> List.countBy id |> List.map snd


//Count Pairs --> calls countValuePairs function, filters to keep only those equal to 2 (looking for pairs)
let countPairs (hand: Hand) = 
    countValuePairs hand |> List.filter (fun count -> count = 2) |> List.length 


//One Pair --> calls countPairs, checks if there is one pair 
let isOnePair (hand: Hand) = countPairs hand = 1


//Two Pair --> calls countPairs, checks if there are two pairs
let isTwoPair (hand: Hand) = countPairs hand = 2


//Three of a Kind --> calls countValuePairs, checks if there are any in the list that equal 3 
let isThreeOfKind (hand: Hand) = countValuePairs hand |> List.exists (fun count -> int count = 3)


//Four of a Kind --> calls countValuePairs, checks if there are any in the list that equal 4
let isFourOfKind (hand: Hand) = countValuePairs hand |> List.exists (fun count -> int count = 4)


//Full House --> calls isThreeOfKind, calls isOnePair (Full House = (3 cards, 2 card) that match) - both must be true
let isFullHouse (hand: Hand) = isThreeOfKind hand && isOnePair hand




//Rank the Hands



//Rank Hand --> determines what a HandType is based on if it returns true on the HandType Functions
let rankHand (hand: Hand) : HandType = 
    if isRoyalFlush hand then RoyalFlush
    elif isStraightFlush hand then StraightFlush
    elif isFourOfKind hand then FourOfKind
    elif isFullHouse hand then FullHouse
    elif isFlush hand then Flush
    elif isStraight hand then
        let values = hand |> List.map (fun card -> int card.Value) |> List.sort
        if isLowStraight values then LowStraight else Straight
    elif isThreeOfKind hand then ThreeOfKind
    elif isTwoPair hand then TwoPair
    elif isOnePair hand then OnePair
    else HighCard

//Hand Rankings --> mapping each HandType to an int to show the value of what beats what
let handRankings = 
    Map.ofList [
        (RoyalFlush, 10);
        (StraightFlush, 9);
        (FourOfKind, 8);
        (FullHouse, 7);
        (Flush, 6);
        (Straight, 5);
        (LowStraight, 4)
        (ThreeOfKind, 3);
        (TwoPair, 2);
        (OnePair, 1);
        (HighCard, 0);
    ]




//Compare the HandTypes



//Compare By Highest Card --> used for finding the highest card value, compares cards by their highest card
let compareByHighestCard (hand1: Hand) (hand2: Hand) : Hand list = 
    let hand1Values = hand1 |> List.map (fun card -> int card.Value) |> List.sortDescending
    let hand2Values = hand2 |> List.map (fun card -> int card.Value) |> List.sortDescending
    let compare (a: int) (b: int) = a.CompareTo(b)
    match (hand1Values, hand2Values) ||> List.compareWith compare with     
    | 1 -> [hand1]
    | -1 -> [hand2]
    | _ -> [hand1; hand2]


//Compare By Card Values --> used for comparing hands that have multiples of the same value
let compareByCardValues (hand1: Hand) (hand2: Hand) : Hand list = 
    let values1 = hand1 |> List.sortBy (fun card -> int card.Value)
    let values2 = hand2 |> List.sortBy (fun card -> int card.Value)

    if values1 > values2 then [hand1]
    elif values1 < values2 then [hand2]
    else [hand1; hand2]


//Compare Hands --> this compares the HandTypes to determine which hand wins
let compareHands (hand1: Hand) (hand2: Hand) : Hand list =
    let rank1 = rankHand hand1
    let rank2 = rankHand hand2

    if rank1 > rank2 then [hand1]
    elif rank1 < rank2 then [hand2]
    else 
        match (rank1, rank2) with 
        | (RoyalFlush, RoyalFlush) -> [hand1; hand2]
        | (StraightFlush, StraightFlush) -> compareByHighestCard hand1 hand2
        | (Flush, Flush) -> compareByHighestCard hand1 hand2
        | (Straight, Straight) -> compareByHighestCard hand1 hand2
        | (LowStraight, LowStraight) -> [hand1; hand2]
        | (FourOfKind, FourOfKind) -> compareByCardValues hand1 hand2
        | (FullHouse, FullHouse) -> compareByCardValues hand1 hand2
        | (ThreeOfKind, ThreeOfKind) -> compareByHighestCard hand1 hand2
        | (TwoPair, TwoPair) -> compareByHighestCard hand1 hand2
        | (OnePair, OnePair) -> compareByHighestCard hand1 hand2
        | (HighCard, HighCard) -> compareByHighestCard hand1 hand2
        | _ -> failwith "Unknown Hand Type"




//Parsing Hands



//Parse Suit --> parses the suit so input can be matched with the correct suit
let parseSuit (s: char) : Suit = 
    match s with
    | 'H' -> Suit.Hearts
    | 'S' -> Suit.Spades
    | 'C' -> Suit.Clubs
    | 'D' -> Suit.Diamonds
    | _ -> failwith "Invalid Suit"

//Parse Value --> parses the value so input can be matched with correct value *Ten is a special case as 'char' can only go 0-9 so 10 is handled in parseCard*
let parseValue (v: char) : Value  = 
    match v with
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
    | _ -> failwith "Invalid Valuessss"

//Parse Card --> extracts the value and suit, returns a Card  *10 is handled here*
let parseCard (cardStr: string) : Card =    
    let valueOfCard, suitOfCard = 
        if cardStr.Length = 3 then cardStr.[0..1], cardStr.[2] 
        else cardStr.[0].ToString(), cardStr.[1]
    
    let value = if valueOfCard = "10" then parseValue 'T' else parseValue valueOfCard.[0]
    let suit = parseSuit suitOfCard
    
    { Suit = suit; Value = value }

//Parse Hand --> takes a string of cards and splits the cards to their own string, returns a Hand (list of Card objects)
let parseHand (handStr: string) : Hand = 
    handStr.Split(' ') 
    |> Array.toList 
    |> List.map parseCard





//Best Hands Functions



//Parsed Hands --> returns list of tuples where each has string and Hand
let parsedHands (hands: string list) = 
    hands |> List.map (fun handStr -> handStr, parseHand handStr)


//Get Best Rank --> returns HandType from list of parsed hands, uses handRanking mapping to get value of HandType
let getBestRank (parsedHands: (string * Hand) list) : HandType = 
    parsedHands 
    |> List.map (snd >> rankHand)
    |> List.maxBy (fun handType -> handRankings.[handType])


//Get Winning Hands --> filter parsedHands to only show rank that matches bestRank --> list of winningHands
let getWinningHands (parsedHands: (string * Hand) list) (bestRank: HandType): Hand list = 
    parsedHands
    |> List.filter (fun (_, hand) -> rankHand hand = bestRank)
    |> List.map snd


//Find Best Hand --> find best hand among list of winningHands
let findBestHand (winningHands: Hand list) : Hand list = 
    List.fold (fun bestHands hand ->
        let results = compareHands hand (List.head bestHands)
        if results = [hand] then [hand]
        elif results = [List.head bestHands] then bestHands
        else 
            hand :: bestHands
        ) [List.head winningHands] winningHands


//Best Hands --> calls parsedHands, calls getBestRank, calls getWinningHands, calls findBestHand - returns the best hand(s)
let bestHands (hands: string list) : string list = 
    let parsedHands = parsedHands hands                                                             //parses input string into list of hands
    let bestRank = getBestRank parsedHands                                                                      //finds highest rank in hands
    let winningHands = getWinningHands parsedHands bestRank                                                    //matches hands to best rank
    let bestHand = findBestHand winningHands                                                                   //tie breaker

    parsedHands |> List.choose (fun (og, hand) -> if List.contains hand bestHand then Some og else None)   //return original string for winning hand(s)
