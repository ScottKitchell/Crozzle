# Crozzle
Crozzle is a type of word puzzle played on a grid of various sizes depending on difficulty. To score, the player places words from a supplied list onto the grid, one letter in each grid square. The score is calculated as the sum of the points given to each non-intersecting letter, each intersecting letter and each word.  
The rules regarding word placement can be seen in [Crozzle_Project_ScottKitchell.pdf](https://github.com/ScottKitchell/Crozzle/raw/master/Crozzle_Project_ScottKitchell.pdf).

![Screenshot](https://github.com/ScottKitchell/Crozzle/raw/master/Screenshot.jpg)

Determining the optimal Crozzle given a set of available words is an almost impossible task if done manually. Computer generation of an optimal Crozzle is an obvious solution, however it too has problems given the huge number of possible word combinations. As a brute force approach to generating the optimal Crozzle may take years to process, a Greedy algorithm was used. This type of algorithm works in a loop, determining the next best word and adding it, until no more words can be added. To ensure accuracy, the algorithms required to generate an optimal Crozzle must be tested with a range of Crozzle puzzles with known results.  

## Development
Crozzle was developed in C# and completed in late 2016 as part of a university project. The algorithm chosen to determine the optimal scoring Crozzle board was a greedy algorithm as it provides the greatest balance between the runtime and quality of results.

## Testing 
Tests and results can be seen in [Crozzle_Project_ScottKitchell.pdf](https://github.com/ScottKitchell/Crozzle/raw/master/Crozzle_Project_ScottKitchell.pdf).
