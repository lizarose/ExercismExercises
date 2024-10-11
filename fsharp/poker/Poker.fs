module Poker

type Suit = Hearts = 'H' | Spades = 'S' | Clubs = 'C' | Diamonds = 'D'
type Value = Val of int
type HandType = HighCard | OnePair | TwoPair  | ThreeOfKind  | Straight | LowStraight | Flush  | FullHouse  | FourOfKind  | StraightFlush | RoyalFlush
type Card = { Suit: Suit; Value: Value; }
type Hand = Card list 

let (|IntVal|_|) (s: string) = 
    match System.Int32.TryParse s with
    | true, i -> Some i
    | _ -> None

let parseCard (cardStr: string) : Card =    
    let parseSuit = function 'H' -> Suit.Hearts | 'S' -> Suit.Spades | 'C' -> Suit.Clubs | 'D' -> Suit.Diamonds | _ -> failwith "Invalid Suit"
    let parseValue = function 
        | "J" -> Val 11
        | "Q" -> Val 12
        | "K" -> Val 13
        | "A" -> Val 14
        | IntVal x-> Val x
        | _ -> failwith "Invalid Value"
    let valueOfCard, suitOfCard = 
        if cardStr.Length = 3 then cardStr.[0..1], cardStr.[2] else cardStr.[0].ToString(), cardStr.[1]
    { Suit = parseSuit suitOfCard; Value = parseValue valueOfCard }

let isFlush hand =  hand |> List.map (fun card -> card.Suit) |> List.distinct |> List.length = 1
let isLowStraight values = values = [Val 2; Val 3; Val 4; Val 5; Val 14]
let isStraight hand = 
    let values = hand |> List.map (fun card -> card.Value) |> List.sort
    values |> List.pairwise |> List.forall (fun (Val x, Val y) -> y = x + 1) || isLowStraight values
let isStraightFlush hand = isFlush hand && isStraight hand 
let isRoyalFlush hand = 
    isStraightFlush hand && hand |> List.map (fun card -> card.Value) |> List.sort = [Val 10; Val 11; Val 12; Val 13; Val 14]
let countValuePairs hand = 
    hand |> List.map (fun card -> card.Value) |> List.countBy id |> List.map snd
let countPairs hand = countValuePairs hand |> List.filter ((=) 2) |> List.length 
let isOnePair hand = countPairs hand = 1
let isTwoPair hand = countPairs hand = 2
let isThreeOfKind hand = countValuePairs hand |> List.exists ((=) 3)
let isFourOfKind hand = countValuePairs hand |> List.exists ((=) 4)
let isFullHouse hand = isThreeOfKind hand && isOnePair hand

let rankHand hand = 
    match hand with
    | _ when isRoyalFlush hand -> RoyalFlush
    | _ when isStraightFlush hand -> StraightFlush
    | _ when isFourOfKind hand -> FourOfKind
    | _ when isFullHouse hand -> FullHouse
    | _ when isFlush hand -> Flush
    | _ when isStraight hand -> 
        let values = hand |> List.map (fun card -> card.Value) |> List.sort
        if isLowStraight values then LowStraight else Straight
    | _ when isThreeOfKind hand -> ThreeOfKind
    | _ when isTwoPair hand -> TwoPair
    | _ when isOnePair hand -> OnePair
    | _ -> HighCard
let handRankings = 
    Map.ofList [RoyalFlush, 10; StraightFlush, 9; FourOfKind, 8; FullHouse, 7; Flush, 6; Straight, 5; LowStraight, 4; ThreeOfKind, 3; TwoPair, 2; OnePair, 1; HighCard, 0]

let compareHands (hand1: Hand) (hand2: Hand) : Hand list =
    if rankHand hand1 > rankHand hand2 then [hand1]
    elif rankHand hand1 < rankHand hand2 then [hand2]
    else 
        let compareHandsByValues (hand1: Hand) (hand2: Hand) (descending: bool) : Hand list = 
            let sortFunc = if descending then List.sortDescending else List.sort
            let sortedValues hand = hand |> List.map (fun card -> card.Value) |> sortFunc
            if sortedValues hand1 > sortedValues hand2 then [hand1]
            elif sortedValues hand1 < sortedValues hand2 then [hand2]
            else [hand1; hand2]
        match rankHand hand1 with 
        | RoyalFlush | LowStraight -> [hand1; hand2]
        | StraightFlush | Flush | Straight | ThreeOfKind | TwoPair | OnePair | HighCard -> compareHandsByValues hand1 hand2 true
        | FourOfKind | FullHouse -> compareHandsByValues hand1 hand2 false

let bestHands hands = 
    let parseHand (handStr: string) =  handStr.Split(' ') |> Array.map parseCard |> Array.toList

    let parsedHands = hands |> List.map (fun handStr -> handStr, parseHand handStr)

    let getBestRank parsedHands = 
        parsedHands |> List.map (snd >> rankHand) |> List.maxBy (fun handType -> handRankings.[handType])

    let getWinningHands parsedHands bestRank = 
        parsedHands |> List.filter (fun (_, hand) -> rankHand hand = bestRank) |> List.map snd

    let findBestHand winningHands = 
        List.fold (fun bestHands hand ->
            match compareHands hand (List.head bestHands) with
            | [h] when h = hand -> [hand]
            | [h] when h = List.head bestHands -> bestHands 
            | _ -> hand :: bestHands) [List.head winningHands] winningHands

    let bestHands = 
        parsedHands 
        |> getBestRank 
        |> getWinningHands parsedHands 
        |> findBestHand
    parsedHands |> List.choose (fun (og, hand) -> if bestHands |> List.contains hand  then Some og else None)