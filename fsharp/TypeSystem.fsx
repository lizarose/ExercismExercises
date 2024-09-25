(* Using the Type System to Ensure Correct Code*)

(*
    -Type system is your friend, not your enemy
    -can use static type checking almost as an instant unit test
    -types and associated functions provide abstraction to model the 
        problem domain
    -rarely an excuse to avoid creating types since they are easy
    -well defined types aid in maintenance
    -can rename or restructure types easily without using refactoring tool
    -provide instant documentation about roles 

    The F# type checker is not that much stricter than the C# type 
    checker. But because it is so easy to create new types without clutter,
     you can represent the domain better, and, as a useful side-effect, 
     avoid many common errors.
*)

//define a "safe" email address type
type EmailAddress = EmailAddress of string

//define a function that uses it
let sendEmail (EmailAddress email) =
   printfn "sent an email to %s" email

//try to send one
let aliceEmail = EmailAddress "alice@example.com"
sendEmail aliceEmail

//try to send a plain string
sendEmail "bob@example.com"   //error

(*
    wrapping the email in a special type ensures that normal string can't 
    be used as arguments to specific email functions









    Units of Measure:
    -ability to define units of measure and associate them with floats
    -unit is "attached" to the float as type and prevents mixing different 
        types
*)

// define some measures
[<Measure>]
type cm

[<Measure>]
type inches

[<Measure>]
type feet =
   // add a conversion function
   static member toInches(feet : float<feet>) : float<inches> =
      feet * 12.0<inches/feet>

// define some values
let meter = 100.0<cm>
let yard = 3.0<feet>

//convert to different measure
let yardInInches = feet.toInches(yard)

// can't mix and match!
yard + meter

// now define some currencies
[<Measure>]
type GBP

[<Measure>]
type USD

let gbp10 = 10.0<GBP>
let usd10 = 10.0<USD>
gbp10 + gbp10             // allowed: same currency
gbp10 + usd10             // not allowed: different currency
gbp10 + 1.0               // not allowed: didn't specify a currency
gbp10 + 1.0<_>            // allowed using wildcard




(*
    Type-Safe Equality:
    -if testing equality between two different types, you are doing 
    something wrong

    -you can stop a type from being compared at all
    
*)

// deny comparison
[<NoEquality; NoComparison>]
type CustomerAccount = {CustomerAccountId: int}

let x = {CustomerAccountId = 1}

x = x       // error!
x.CustomerAccountId = x.CustomerAccountId // no error