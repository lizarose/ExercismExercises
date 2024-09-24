(*Out-of-the-Box Behavior for Types*)

(*
    -immutability
    -built-in equality
    -comparisons
    -pretty printing when debugging 
*)


//this is an immutable type
type PersonalName = {FirstName:string; LastName:string}



(*
    You don't have to override ToString() so you get pretty printing
*)

type USAddress =
   {Street:string; City:string; State:string; Zip:string}
type UKAddress =
   {Street:string; Town:string; PostCode:string}
type Address =
   | US of USAddress
   | UK of UKAddress
type Person =
   {Name:string; Address:Address}

let alice = {
   Name="Alice";
   Address=US {Street="123 Main";City="LA";State="CA";Zip="91201"}}
let bob = {
   Name="Bob";
   Address=UK {Street="221b Baker St";Town="London";PostCode="NW1 6XE"}}

printfn "Alice is %A" alice
printfn "Bob is %A" bob

//This is the output
Alice is {Name = "Alice";
 Address = US {Street = "123 Main";
               City = "LA";
               State = "CA";
               Zip = "91201" };}

Bob is {Name = "Bob";
 Address = UK {Street = "221b Baker St";
               Town = "London";
               PostCode = "NW1 6XE";};}



//Comparing two names using built-in structural equality
type PersonalName = {FirstName:string; LastName:string}
let alice1 = {FirstName="Alice"; LastName="Adams"}
let alice2 = {FirstName="Alice"; LastName="Adams"}
let bob1 = {FirstName="Bob"; LastName="Bishop"}

//test
printfn "alice1=alice2 is %A" (alice1=alice2)
printfn "alice1=bob1 is %A" (alice1=bob1)




//Sort Objects 
type Suit = Club | Diamond | Spade | Heart
type Rank = Two | Three | Four | Five | Six | Seven | Eight
            | Nine | Ten | Jack | Queen | King | Ace

//Write function to test comparison logic
let compareCard card1 card2 =
    if card1 < card2
    then printfn "%A is greater than %A" card2 card1
    else printfn "%A is greater than %A" card1 card2

//Test
let aceHearts = Heart, Ace
let twoHearts = Heart, Two
let aceSpades = Spade, Ace

compareCard aceHearts twoHearts
compareCard twoHearts aceSpades


(*
    Ace of Hearts is automatically greater than the 2 of Hearts, because
    the "Ace" rank value comes after the "2" rank value.

    The 2 of Hearts is automatically greater than the Ace of Spades because
    the Suit part is compared first, and the "Heart" suit value comes 
    after the "Spade" value.
*)

let hand = [ Club,Ace; Heart,Three; Heart,Ace;
             Spade,Jack; Diamond,Two; Diamond,Ace ]

//instant sorting!
List.sort hand |> printfn "sorted hand is (low to high) %A"

List.max hand |> printfn "high card is %A"
List.min hand |> printfn "low card is %A"
