# loop-energy

Loop Energy style game, but with drag/drop instead of rotating pieces.

## Levels

Levels are saved in Scriptable Objects containing:

* Board - list of slots that make the board (the spaces where pieces can be placed)
* Pieces - list of pieces with their type, inicial and final positions

Levels are built folowing a layout.
Square Layout layouts slots and pieces in a grid format.
Another type of layout could be hexagonal (not implemented).
