/* 
    Expressions, Statements, and Patterns:

    There are 3 main constructs that can be composed together:
        1. Expressions: single chunk of code that resolves to a value
        2. Statements: executable code that results in no value
        3. Patterns: description used to match values and extract data

        -can be used in placese that expect statements --> final value ignored
        -statements can NOT be used where expressions are expected, and 
        patterns can only be used in very specific places


        expressions --> statements
        statements !--> expressions
                *patterns*



        Expressions:
            -can be a variable name, a literal value, a property/constractor/method/function/lambda/index invocation, an operator evaluation, a lambda, a method name, terinary experssion, a switch expression, is/as keyword.
           
*/

 foo //Variable name
   "Hello" //string literal
   7 //int literal
   a < b // operator evaluation
   foo[2] // index invocation
   foo.Bar() // method invocation
   () => foo.Bar()  //lambda
   foo.Bar //method name (when Bar is a method with arguments)
   a == 3 ? "Hello" : "World" //terinary expression
   a switch { 3 => "Hello", _ => "world" }  // switch expression C# 8.0
   a is String 
 
 
 //Expression can be chained and nested as much as you want
(a is String ? "Hello" : "World").Substring(foo.Bar()).ToUpper();

/* 
    Statements:
        -are return/break/goto statements, variable assignment, any block statements (if/else, try/catch/finally,switch, for/foreach/while/do while, just plain{}, a local function, disposal using block).

        -Statements end with ; if they don't use {}
*/

return 7; //return statement
   break; //break statement
   
   var foo = new Foo(); //variable assignment
   
   //if/else statement
   if (a  ==3 ){ 
   } else{
   }
   
   //try/catch/finally statement
   try { 
   } catch {
   } finally {
   }
   
   //loop
   for(int i=0; i < 10; i++){
   }
   
   bool Foo() => false;  //local function

/* 
    Patterns:
        -are new and less common. they are used in switch cases and with the is Keyword. Patterns are Types and Literals, in C#8 and later there are more complex patterns with their own syntax.
*/




    What usages expects expressions, statements and pattern
//variable assignment
var varName = /*expresion*/;

//Method arguments
/*expession*/.MethodName(/*expression*/, /*expression*/);
TypeName.StaticMethodName(/*expression*/, /*expression*/);

//return value
return /*expression*/;

//Terinary expression
/*expression*/ ? /*expression*/ : /*expresion*/

//if/else
if (/*expression*/) {
 /*statements*/
} else {
 /*statements*/
}

//operators
/*expression*/ + /*expression*/
/*expression*/ >= /*expression*/

//is keyword
/*expression*/ is /*Pattern*/

//switch expression - C# 8.0
/*expression*/ switch {/*pattern*/ => /*expression*/, /*pattern*/ =>/*expression*/}

//swtich statement
switch (/*expression*/) {
   case /*pattern*/ :
      /*statements*/  
   case /*pattern*/ when /*expression*/ :
      /*statements*/
   default:
     /*statements*/
}

//try catch finally
try {
  /*statements*/
} catch (/*pattern*/) {
 /*statements*/
} catch (/*pattern*/) when (/*expression*/) {
 /*statements*/
} finally {
 /*statements*/
}

//for loop
for(/*statement*/; /*expression*/; /*statement*/) }
  /*statements*/
}

//foreach loop
foreach(var itemName in /*expression*/) {
 /*statements*/
}

//while loop
while (/*expression*/){
 /*statements*/
}

// do while
do {
/*statements*/
} while (/*expression*/)

//using disposal after enclosing scope - c# 8.0
using var itemName = /*expression*/;

//using disposal after block statements
using(var itemName = /*expression*/){
 /*statements*/
}

//plain code block (uncommon, can add additional nested scope to variables)
{
  /*statements*/
}

//lambda - expression bodied
(argName1, argName1) => /*expression*/

//lambda - statement bodied
(argName1, argName2) => {
 /*statements*/
}

//functions/method - expression bodied
TypeName FuncName (TypeName argName1, TypeName argname2) => /*expression*/;

//functions/method - statement bodied
TypeName FuncName (TypeName argName1, TypeName argname2) {
    /*statements*/
}
Eg. Method/Function can be Expression Bodied or Statement Bodied
   //Method - Expression Body Style
   public class Foo {
      public string ConvertToInt(int value) => value.ToString(); 
   }
   //Method - Statement Body Style
   public class Foo {
      public string ConvertToInt(int value){ 
         return value.ToString();
      }
   }

   //Local Function - Expression Body Style
   string ConvertToInt(int value) => value.ToString();
   //Local Function - Statement Body Style
   string ConvertToInt(int value) {
        return value.ToString();
   }
   
   //Lambda assigned to variable - Expression Body Style
   Func<int, string> ConvertToInt = (value) => value.ToString();
   //Lambda assigned to variable - Statement Body Style
   Func<int, string> ConvertToInt = (value) => {
        return value.ToString();
   }
