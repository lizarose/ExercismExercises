module LuciansLusciousLasagna

// TODO: define the 'expectedMinutesInOven' binding
let expectedMinutesInOven = 40

// TODO: define the 'remainingMinutesInOven' function
let remainingMinutesInOven currentOvenTimeIn = expectedMinutesInOven - currentOvenTimeIn

// TODO: define the 'preparationTimeInMinutes' function
let preparationTimeInMinutes layers = layers * 2

// TODO: define the 'elapsedTimeInMinutes' function
let elapsedTimeInMinutes layers currentOvenTimeIn = preparationTimeInMinutes layers + currentOvenTimeIn


(*
    - You have layers and currentOvenTimeIn as parameters going in to elapsedTimeInMinutes.
    - To use preparationTimeInMinutes you need to have layers with it bc it takes layers in as an argument.

    - example in C#:


public int preparationTimeInMinutes(int layers) {
    return layers * 2;
}
public int elapsedTimeInMinutes(int layers, int currentOvenTimeIn) {
    return preparationTimeInMinutes(layers) + currentOvenTimeIn;
}


*)




