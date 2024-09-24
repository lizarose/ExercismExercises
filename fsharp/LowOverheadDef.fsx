(*Low Overhead Type Definitions*)

(*
    In F#, there is no penalty for making new types, so it is common
    to have a lot of them. Every time you need to define a structure,
    you can create a special type, rather than reusing and overloading 
    existing types such as strings and lists.

    This means your programs are more type-safe, more self documenting,
    and more maintainable.
*)
open System

// some "record" types
type Person = {FirstName:string; LastName:string; Dob:DateTime}
type Coord = {Lat:float; Long:float}

// some "union" (choice) types
type TimePeriod = Hour | Day | Week | Year
type Temperature = C of int | F of int
type Appointment =
  OneTime of DateTime | Recurring of DateTime list

(*
    This defines several record types and union types.

    Record Types: 
        -used to group related values together, similar to structs or 
        objects
*)
type Person = {FirstName:string; LastName:string; Dob:DateTime}
(*
    This defines a Person record with 3 fields:
        1. FirstName --> a string representing the first name of the person
        2. LastName --> a string representing the last name of the person
        3. Dob --> a DateTime representing the date of birth of the person
*)

type Coord = {Lat:float; Long:float}
(*
    Coord Record (Coordinates):
        This defines a Coord record with 2 fields:
            1. Lat --> a float representing the latitude
            2. Long --> a float represeting the longitude
*)

(*
    Union Types (discriminated unions):
        This represents a value that can be one of several different types,
        allowing for flexible data structures

*)

type TimePeriod = Hour | Day | Week | Year
(*
    TimePeriod Union:
        This defines a TimePeriod union type that can be 1 of 4 choices:
            1. Hour --> represents time period in hours
            2. Day --> represents time period in days
            3. Week --> represents time period in weeks
            4. Year --> represents time period in years
*)

type Temperature = C of int | F of int
(*
    Temperature Union:
        This defines a Temperature union type, which can be either:
            1. C of int --> represents a temp in Celsius, where the value
                int is the temp in degrees
            2. F of int --> represents a temp in Fahrenheit, where the 
                value int is the temp in degrees
*)

type Appointment =
  OneTime of DateTime | Recurring of DateTime list
(*
    Appointment Union:
        This defines an Appointment union type that can represent two kinds of appointments:
            1. OneTime of DateTime --> represents a one-time appointment
                    that happens on a specific date and time
            2. Recurring of DateTime list --> represents a recurring 
                    appointment with a list of dates when the appointment
                    occur
*)

(*
    *"of" keyword is used in union types to indicate that a specific union
    case carries some associated data or a value of a particular type.
    It connects the case name with the data it holds.

    So the example above:
        C of int --> means that the union case C, carries an int value with it
*)




(*F# Types and Domain Driven Design*)

(*
    The conciseness of the type system is useful when doing domain driven 
    design(DDD) as for each real world entity and value object, you 
    ideally want to have a corresponding type. This means creating a lot
    of little types.

    "Value" objects in DDD should have structural equality, meaning that 
    two objects containing the same data should always be equal. 
*)
type PersonalName =
  {FirstName:string; LastName:string}

// Addresses
type StreetAddress = {
  Line1:string;
  Line2:string;
  Line3:string
  }

type ZipCode = ZipCode of string
type StateAbbrev = StateAbbrev of string
type ZipAndState =
  {State:StateAbbrev; Zip:ZipCode }
type USAddress =
  {Street:StreetAddress; Region:ZipAndState}

type UKPostCode = PostCode of string
type UKAddress =
  {Street:StreetAddress; Region:UKPostCode}

type InternationalAddress = {
  Street:StreetAddress;
  Region:string;
  CountryName:string
  }

// choice type -- must be one of these three specific types
type Address =
  | USAddress of USAddress
  | UKAddress of UKAddress
  | InternationalAddress of InternationalAddress

// Email
type Email = Email of string

// Phone
type CountryPrefix = Prefix of int
type Phone =
  {CountryPrefix:CountryPrefix; LocalNumber:string}

type Contact =
  {
  PersonalName: PersonalName;
  // "option" means it might be missing
  Address: Address option;
  Email: Email option;
  Phone: Phone option;
  }

// Put it all together into a CustomerAccount type
type CustomerAccountId = AccountId of string
type CustomerType  = Prospect | Active | Inactive

// override equality and deny comparison
[<CustomEquality; NoComparison>]
type CustomerAccount =
  {
  CustomerAccountId: CustomerAccountId;
  CustomerType: CustomerType;
  ContactInfo: Contact;
  }

  override this.Equals(other) =
    match other with
    | :? CustomerAccount as otherCust ->
      (this.CustomerAccountId = otherCust.CustomerAccountId)
    | _ -> false

  override this.GetHashCode() = hash this.CustomerAccountId

  (*
        This is for the Override Equality and Deny Comparison Section:

        Attributes:
            - CustomeEquality --> indicates that the type will provide a custom implementation for the equality checks (the Equals method)
            - NoComparison --> prevents automatic generation of comparison
            operators (like <, >, etc.) for this type
        
        Record Type:
            CustomerAccount is defined as record type with 3 fields:
                1. CustomerAccountId --> represents a unique identifier 
                        for the customer account
                2. CustomerType --> specifies the type of customer
                        (business or individual)
                3. ContactInfo --> holds contact details for the customer
                        (email, phone number)

        
        Custom Equality Implementation:
            This is for the override section

            Equals Method:
                - checks whether the other object is of type CustomerAcocunt
                    -if it is, then compares the CustomerAccountId of both 
                        instances for equality
                    -if not, returns false

            
        Hash Code Implementation:
            -GetHashCode Method --> this method provides a hash code for the CustomerAccount instance
                -it generates the hash code based on the CustomerAccountId
                -this is important for using instances of CustomerAccount
                    in hash-based collections
                        (hashsets or keys in dictionaries)
  *)