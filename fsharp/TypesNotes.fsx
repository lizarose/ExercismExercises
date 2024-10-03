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