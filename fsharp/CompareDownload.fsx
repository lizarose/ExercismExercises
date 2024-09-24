(*Comparing F# with C#: Downloading a Web Page*)

(*Compare downloading a web page with a callback to process the text stream:
*)

open System.Net
open System
open System.IO

(* "open" brings a .NET namespace into visibility*)

//Fetch the contents of a web page
let fetchURL callback url = 
    let req = WebRequest.Create(Uri(url))
    use resp = req.GetResponse()
    use stream = resp.GetResponseStream()
    use reader = new IO.StreamReader(stream)
    callback reader url

(*
    This defines a function(FetchURL) that fetches the contents of a web page, processes it using a callback function, and then returns the result.
    It opens several .NET namespaces required for handling web requests, 
    input/output, and other system operations.

    Key Components:
    1. open System.Net, open System, open System.IO
        -this brings the necessary .NET namespace into scope

        -System.NET --> provides classes for making web requests
        -System --> basic system utilities
        -System.IO --> input/output operations, such as reading and writing
            files and streams
        
        *similar to using "using System.Net" in C#
    
    2. let FetchURL callback url
        -this defines a function(FetchURL) that takes 2 parameters:
            1. callback --> a function that will be applied to the 
                    response of the web request
            2. url --> the URL of the web page to fetch
    
    3. let req = WebRequest.Create(Uri(url))
        - this creates a web request(req) by calling WebRequest.Create with 
            the Uri of the given URL
            -sets up a request to the web page but does not execute it yet
        
        -wrap the url string in a Uri so the compiler knows what version
            of WebRequest.Create to use

    4. use resp = req.GetResponse()
        -this send the request to the server using req.GetResponse() and 
            retrieves the response(resp)
                -the 'use' keyword ensures that the response is automatically disposed of after it is used

    5. use stream = resp.GetResponseStream()
        -this gets the stream(stream) containing the raw data(usually HTML)
            from the server's response.
                -'use' ensures the stream is properly closed when no longer
                needed

    6. use reader = new IO.StreamReader(stream)
        -a StreamReader (reader) is created to read the stream's contents
            (the web page's data) as a string
    
    7. callback reader url
        -the callback function is applied to the reader and url
        -the reader contains the web page content, and the url is passed in
        case it's needed by the callabck function for further processing



    *When declaring the response, stream, and reader values, the "use" 
    keyword is used instead of "let". This can only be used in conjunction
    with classes that implement IDisposable. It tells the compiler to 
    automatically dispose of the resource when it goes out of scope. This is equivalent to the C# "using" keyword.
*)




let myCallback (reader:IO.StreamReader) url =
    let html = reader.ReadToEnd()
    let html1000 = html.Substring(0,1000)
    printfn "Downloaded %s. First 1000 is %s" url html1000
    html      // return all the html

//test
let google = fetchUrl myCallback "http://google.com"

(*Finally, we have to resort to a type declaration for the reader parameter
 (reader:IO.StreamReader). This is required because the F# compiler cannot
  determine the type of the “reader” parameter automatically.
  
A very useful feature of F# is that you can “bake in” parameters in a function so that they don’t have to be passed in every time. This is why the url parameter was placed last rather than first, as in the C# version. The callback can be setup once, while the url varies from call to call.*)


// build a function with the callback "baked in"
let fetchUrl2 = fetchUrl myCallback

// test
let google = fetchUrl2 "http://www.google.com"
let bbc    = fetchUrl2 "http://news.bbc.co.uk"

// test with a list of sites
let sites = ["http://www.bing.com";
             "http://www.google.com";
             "http://www.yahoo.com"]

// process each site in the list
sites |> List.map fetchUrl2
(*The last line (using List.map) shows how the new function can be easily
 used in conjunction with list processing functions to download a whole 
 list at once.*)