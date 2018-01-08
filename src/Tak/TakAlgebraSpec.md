
https://www.reddit.com/r/Tak/wiki/portable_tak_notation

#Portable Tak Notation

Portable Tak Notation (PTN) is a simple text notation system for recording the moves of a game of Tak. The standard is loosely based on the [PGN](https://en.wikipedia.org/wiki/Portable_Game_Notation) standard of chess notation.

When sharing a game recorded in PTN format, additional metadata must be included as specified in the [PTN File Format](https://www.reddit.com/r/Tak/wiki/PTN%20File%20Format).

### Basic Format

The basic arrangement of PTN moves is similar to that of the PGN of chess. Each move is first numbered. The number must be followed by a period. Then a space and the move notation for player 1, followed by another space and the move notation for player 2. 

There is another space, or a newline, before the next move number.

### Board

The squares on the Tak board are identified with standard algebraic notation. The lower (or nearest) left square for player 1 is identified as `a1`, this will be called a *square identifier*. The files are lettered across the bottom of the board, and the ranks are numbered up the side. As the board size in Tak is able to vary, the upper right square can range from `d4` to `h8`. The file letters are always lowercase.

In general, the `a1` square is a dark square purely by force of habit, but as there is no difference due to orientation of squares in Tak this is not an official standard and makes no difference to play. Also, though the `1` rank is presumed to be on the player 1 side of the board, it is not strictly necessary for players to sit opposite one another since the pieces are placed arbitrarily on the board during play. Only ensure that the personal space of a player is not being encroached upon.

### Move Notation

PTN is essentially condensed english in the format of a set of instructions.  
For example:

Take `5` stones from square `b4` and move them `right` dropping `2` stones, `1` stone, then `2` stones as you come to each square.

OR

`5b4>212`

#### Stone Identifiers

At the beginning of a game of Tak, you start by placing pieces on the board. There are 3 piece types: Flat Stones, Standing Stones, and Capstones. These are called the *stone identifiers* and are represented by an `F`, `S`, and `C` throughout the notation. These are always uppercase letters.

#### Direction Identifiers

When a stone or stack of stones is being moved on the Tak board, the direction of the move is indicated by the *direction identifiers*. The four possible move directions are represented by the symbols `<` `>` `+` `-`.

The `<` and `>` identifiers represent a movement across files. `<` moves toward the `a` file, and `>` moves the opposite direction, away from the `a` file. It is easiest to think of these simply as arrows which point in the direction of movement from player 1's perspective, `<` moving to the *left*, and `>` moving to the *right*.

The `+` and `-` identifiers represent movement up and down ranks. `-` moves toward the `1` rank, and `+` moves the opposite direction, away from the `1` rank. It helps to think of these as simple mathematical operators, so that `+` indicates "adding" or moving *up* in rank, and `-` indicates "subtraction" or moving *down* in rank.

##### **Alternate Identifiers**

To simplify some displays and the writing of notation by hand, use of regular arrows are also acceptable to show direction of movement. When used, the arrows `↑` `↓` `←` `→` denote the direction of a move from the perspective of player 1. 

In other words, arrows `↑` `↓` replace `+` `-`, and `←` `→` replace `<` `>`.

#### Placing Stones

The notation format for placing stones is: `(stone)(square)`.  

It is recognized that flat stones will be by far the most common stone placed in a game of Tak, and so the stone identifier may be omitted in that case and a flat stone will be assumed. 

##### Examples

Move | Notation
---: | :---
Place flat stone at a1 | `a1`
Place capstone at b4 | `Cb4`  
Place standing stone at d3 | `Sd3`

#### Moving Stones

The notation format for moving one or more stones is: `(count)(square)(direction)(drop counts)(stone)`

1. The *count* of stones to be lifted from a square is given. This may be omitted only if the count is 1.  
2. The *square* wich stones are being moved from is given. This is always required.  
3. The direction to move the stones is given. This is always required.  
4. The number of stones to drop on each square in the given direction are listed, without spaces. This may be omitted if all of the stones given in the count are dropped on a square immediately adjacent to the source square. If the stack is moving more than one square, all drop counts must be listed and must add up to equal the lift count from parameter 1 above.
5. The stone type of the top stone of the moved stack is given. If the top stone is a flat stone the `F` identifier is never needed, flat stones are always assumed. If the top stone is a standing stone or capstone, the `S` or `C` can be used, though it is not required and infrequently used.

##### Examples

Move | Notation
---: | :---
Move a single stone from a1 to b1 | `a1>`
Move 4 stones from c3 to d3 | `4c3>`
Move 3 stones from b2, dropping one each on b3, b4, b5 | `3b2+111`
Move 2 stones from d4 to d3. There is a capstone on top | `2d4-2C`
Move 5 stones from e4 toward c4, dropping 2 and then 3 as you move | `5e4<23`


### Game End

The result of the game is recorded in the notation after the final move, separated by a space.

A road win for player 1 and player 2 are notated respectively `R-0` and `0-R`.  
A flat win for player 1 and player 2 are notated respectively `F-0` and `0-F`.  
A win by resignation or time for player 1 and player 2 are notated respectively `1-0` and `0-1`.  
A draw is notated `1/2-1/2`.

### Informational Marks

Outside of the essential move notation there are 3 additional characters which can be added to a move to give additional information or commentary regarding that move. These are always placed *at the end* of the move notation.

The most common mark is a single quote `'`. This denotes a "tak": the existence of a road opportunity on your next turn. Whether or not you verbally call the "tak", it is recommended practice to included the mark in your notation to assist with later evaluation. Along with it two single quotes, or a double quote, `''` is used to denote a "Tinuë": the existence of an unblockable road threat, and hence a guaranteed win.

The other two characters are the `!` and `?`, which are purely representative of a subjective evaluation of the move. The `!` denotes a surprising or especially good move, while the `?` represents a questionable move or blunder. They are often used in pairs to emphasize a move as (for example) an outright blunder `??` or questionable but surprising `?!`. 

From worst to best the evaluations would roughly be: `??` `?` `?!` `!?` `!` `!!`

### Comments

Comments can be added anywhere between moves in the notation to add whatever information you like to the game.

Comments are enclosed in curly brackets `{ }` and separated from any other element by a space or newline. Within the brackets you can include any information you wish and use any character except for the closing bracket `}`, which would end the comment.