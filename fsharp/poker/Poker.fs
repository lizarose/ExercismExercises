module Poker

type Suit =
    | Hearts = 'H' | Spades = 'S' | Clubs = 'C' | Diamonds = 'D'
    
type Value = 
    | Two = 2 | Three = 3 | Four = 4 | Five = 5 | Six = 6 | Seven = 7 | Eight = 8 | Nine = 9 | Ten = 10 | Jack = 11 | Queen = 12 | King = 13 | Ace = 14
    
type HandType = 
    | HighCard | OnePair | TwoPair  | ThreeOfKind  | Straight | LowStraight | Flush  | FullHouse  | FourOfKind  | StraightFlush | RoyalFlush 

type Card = { Suit: Suit; Value: Value; }
type Hand = Card list 

let isFlush (hand: Hand) = 
    hand
    |> List.map (fun card -> card.Suit)
    |> List.distinct
    |> List.length = 1

let isLowStraight (values: int list) = 
    values = [2; 3; 4; 5; 14]
    
let isStraight (hand: Hand) = 
    let values = 
        hand 
        |> List.map (fun card -> int card.Value) 
        |> List.sort
    values 
    |> List.pairwise 
    |> List.forall(fun (x, y) -> y = x + 1) 
    || isLowStraight values
    
let isStraightFlush (hand: Hand) = isFlush hand && isStraight hand 

let isRoyalFlush (hand: Hand) = 
    isStraightFlush hand &&
    let values =  hand |> List.map (fun card -> int card.Value)
    values |> List.sort = [10; 11; 12; 13; 14]

let countValuePairs hand = 
    hand 
    |> List.map (fun card -> int card.Value) 
    |> List.countBy id 
    |> List.map snd

let countPairs (hand: Hand) = 
    let cvp = countValuePairs hand
    cvp |> List.filter (fun count -> count = 2) |> List.length 

let isOnePair (hand: Hand) = countPairs hand = 1

let isTwoPair (hand: Hand) = countPairs hand = 2

let isThreeOfKind (hand: Hand) = 
    let cvp = countValuePairs hand
    cvp |> List.exists (fun count -> int count = 3)

let isFourOfKind (hand: Hand) = 
    let cvp = countValuePairs hand
    cvp |> List.exists (fun count -> int count = 4)

let isFullHouse (hand: Hand) = 
    let cvp = countValuePairs hand 
    let hasThreeOfKind = isThreeOfKind hand
    let hasPair = cvp |> List.exists (fun count -> count = 2)
    hasThreeOfKind && hasPair 

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
    
let compareByCardValues (hand1: Hand) (hand2: Hand) : Hand list = 
    let hand1Values = hand1 |> List.map (fun card -> int card.Value) |> List.sortDescending
    let hand2Values = hand2 |> List.map (fun card -> int card.Value) |> List.sortDescending
    let compare (a: int) (b: int) = a.CompareTo(b)
    match (hand1Values, hand2Values) ||> List.compareWith compare with     
    | 1 -> [hand1]
    | -1 -> [hand2]
    | _ -> [hand1; hand2]

let compareStraightFlush (hand1: Hand) (hand2: Hand) : Hand list = 
    compareByCardValues hand1 hand2

let compareFourOfKind (hand1: Hand) (hand2: Hand) : Hand list = 
    let fourKindValue1 = hand1 |> List.sortBy (fun card -> int card.Value)
    let fourKindValue2 = hand2 |> List.sortBy (fun card -> int card.Value)
    if fourKindValue1 > fourKindValue2 then [hand1] 
    elif fourKindValue1 < fourKindValue2 then [hand2]
    else [hand1; hand2]

let compareFullHouse (hand1: Hand) (hand2: Hand) : Hand list = 
    let threeValue1 = hand1 |> List.sortBy (fun card -> int card.Value)
    let threeValue2 = hand2 |> List.sortBy (fun card -> int card.Value)

    if threeValue1 > threeValue2 then [hand1]
    elif threeValue1 < threeValue2 then [hand2]
    else [hand1; hand2]

let compareHands (hand1: Hand) (hand2: Hand) : Hand list =
    let rank1 = rankHand hand1
    let rank2 = rankHand hand2

    if rank1 > rank2 then [hand1]
    elif rank1 < rank2 then [hand2]
    else 
        match (rank1, rank2) with 
        | (RoyalFlush, RoyalFlush) -> [hand1; hand2]
        | (StraightFlush, StraightFlush) -> compareStraightFlush hand1 hand2
        | (Flush, Flush) -> compareByCardValues hand1 hand2
        | (Straight, Straight) -> compareStraightFlush hand1 hand2
        | (FourOfKind, FourOfKind) -> compareFourOfKind hand1 hand2
        | (FullHouse, FullHouse) -> compareFullHouse hand1 hand2
        | (ThreeOfKind, ThreeOfKind) -> compareByCardValues hand1 hand2
        | (TwoPair, TwoPair) -> compareByCardValues hand1 hand2
        | (OnePair, OnePair) -> compareByCardValues hand1 hand2
        | (HighCard, HighCard) -> compareByCardValues hand1 hand2
        | _ -> failwith "Unknown Hand Type"

let parseSuit (s: char) : Suit = 
    match s with
    | 'H' -> Suit.Hearts
    | 'S' -> Suit.Spades
    | 'C' -> Suit.Clubs
    | 'D' -> Suit.Diamonds
    | _ -> failwith "Invalid Suit"

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

let parseCard (cardStr: string) : Card =    
    let valueOfCard, suitOfCard = 
        if cardStr.Length = 3 then cardStr.[0..1], cardStr.[2] 
        else cardStr.[0].ToString(), cardStr.[1]
    
    let value = if valueOfCard = "10" then parseValue 'T' else parseValue valueOfCard.[0]
    let suit = parseSuit suitOfCard
    
    { Suit = suit; Value = value }

let parseHand (handStr: string) : Hand = 
    handStr.Split(' ') 
    |> Array.toList 
    |> List.map parseCard


//parse hands into tuple list
let parsedHands (hands: string list) = 
    hands |> List.map (fun handStr -> handStr, parseHand handStr)

//find rank of hands from parsedHands then find highest ranking
let getBestRank (parsedHands: (string * Hand) list) : HandType = 
    parsedHands 
    |> List.map (snd >> rankHand)
    |> List.maxBy (fun handType -> handRankings.[handType])

//filter parsedHands to only show rank that matches bestRank --> list of winningHands
let getWinningHands (parsedHands: (string * Hand) list) (bestRank: HandType): Hand list = 
    parsedHands
    |> List.filter (fun (_, hand) -> rankHand hand = bestRank)
    |> List.map snd

//find best hand with multiple winning hands
let findBestHand (winningHands: Hand list) : Hand list = 
    match winningHands with
    | firstHand :: remainHand ->
        List.fold (fun (bestHands: Hand list) hand ->
            let compareResults = compareHands hand (List.head bestHands)
            if compareResults = [List.head bestHands] then bestHands
            elif compareResults = [hand] then [hand]
            else 
                if List.contains hand bestHands then bestHands
                else hand :: bestHands
        ) [firstHand] remainHand
    | [] -> []

let bestHands (hands: string list) : string list = 
    let parsedHands = parsedHands hands 
    let bestRank = getBestRank parsedHands
    let winningHands = getWinningHands parsedHands bestRank
    let bestHand = findBestHand winningHands

    parsedHands |> List.choose (fun (og, hand) -> if List.contains hand bestHand then Some og else None) 
