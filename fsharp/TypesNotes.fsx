(*
    Designing with Types: Introduction

    -the thoughtful use of types can make a design more transparent and improve correctness

*)

type Contact =
    {
        FirstName: string;
        MiddleInitial: string;
        LastName: string;

        EmailAddress: string;
        IsEmailVerified: bool; //true if ownership of email address is confirmerd

        Address1: string;
        Address2: string;
        City: string;
        State: string;
        Zip: string;

        IsAddressValid: bool; //true if validated against address service
    }


(*
    Creating "Atomic" Types:
    -the first thing to do is look at the usage pattern of data access and updates
        -would it be likely that Zip is updated without also updating Address1 at the same time?
        -it might be common that a transaction updates EmailAddress but not FirstName

    -Guideline: 
        --> use records or tuples to group together data that are required
            to be consisten (that is "atomic") but don't needlessly group
            together data that is not related.

                [FirstName, MiddleInitial, LastName]
                [Address1, Address2, City, State, Zip, IsAddressVerified]
                [EmailAddress, IsEmailVerified]

                -if email changes then IsEmailVerified needs to be reset
*)

type PostalAddress =
    {
        Address1: string;
        Address2: string;
        City: string;
        State: string;
        Zip: string;
    }

type PostalContactInfo =
    {
        Address: PostalAddress;
        IsAddressValid: bool;
    }

type PersonalName =
    {
        FirstName: string;
        MiddleInitial: string option; // allows this to be optional
        LastName: string;
    }

type EmailContactInfo =
    {
        EmailAddress: string;
        IsEmailVerified: bool;
    }

type ContactInfo = 
    {
        Name: PersonalName;
        EmailContactInfo: EmailContactInfo;
        PostalContactInfo: PostalContactInfo;
    }






(*
    Designing with Types: Single Case Union Types:

    Wrapping Primitive Types:
        -the simplest way to create a separate type is to wrap the underlying string type inside another type
*)

//Single Case Union Type:
type EmailAddress = EmailAddress of string
type ZipCode = ZipCode of string
type StateCode = StateCode of string

//Record Types with One Field:
type EmailAddress = { EmailAddress: string }
type ZipCode = { ZipCode: string }
type StateCode = { StateCode: string }

(*
    Which is the best way?
        --> Single Case Discriminated Union
            - it's much easier to "wrap" and "unwrap" as the "union case"
                is a proper constructor function in its own right
            -unwrapping can be done using inline pattern matching
*)

//EmailAddress Constructed and Deconstructed:
type EmailAddress = EmailAddress of string

//using the constructor as a function:
"a" |> EmailAddress
["a"; "b"; "c"] |> List.map EmailAddress

//inline deconstruction:
let a' = "a" |> EmailAddress
let (EmailAddress a'') = a' 

let addresses = 
    ["a"; "b"; "c"] 
    |> List.map EmailAddress

let addresses' = 
    addresses
    |> List.map (fun (EmailAddress e) -> e)




//Refactor Example:
type PersonalName =
    {
    FirstName: string;
    MiddleInitial: string option;
    LastName: string;
    }

type EmailAddress = EmailAddress of string

type EmailContactInfo =
    {
    EmailAddress: EmailAddress;
    IsEmailVerified: bool;
    }

type ZipCode = ZipCode of string
type StateCode = StateCode of string

type PostalAddress =
    {
    Address1: string;
    Address2: string;
    City: string;
    State: StateCode;
    Zip: ZipCode;
    }

type PostalContactInfo =
    {
    Address: PostalAddress;
    IsAddressValid: bool;
    }

type Contact =
    {
    Name: PersonalName;
    EmailContactInfo: EmailContactInfo;
    PostalContactInfo: PostalContactInfo;
    }


//another nice thing about the union type is that the implementation can be encapsulated with module signatures


(*
    Naming the "Case" of a Single Case Union:
*)

type EmailAddress = EmailAddress of string
type ZipCode = ZipCode of string
type StateCode = StateCode of string


//this refers to the type EmailAddress --> types
val f: string -> EmailAddress

//this refers to the constructor function EmailAddress --> values
let x = EmailAddress y



//add validation to constructor 
... types as above ...

let CreateEmailAddress (s:string) =
    if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
        then Some (EmailAddress s)
        else None

let CreateStateCode (s:string) =
    let s' = s.ToUpper()
    let stateCodes = ["AZ";"CA";"NY"] //etc
    if stateCodes |> List.exists ((=) s')
        then Some (StateCode s')
        else None

public class EmailAddress {
    string EmailAddress {
        get =>
        set =>
    }
    public EmailAddress (string s) {
        // if s is an email pattern (<letters>@<letters>.<letters>)
        // - create EmailAddress = s;
        // else
        // - EmailAddress = null;
    }
}
type ContactMethod =
   | Email of 
   | Post
   | Phone

type Contact = {
    Name: PeronalName;
    PrimaryContact: ContactMethod;
    OtherContact: ContactMethod list;
}
