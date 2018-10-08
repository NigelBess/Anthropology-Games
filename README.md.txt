Here are the formats for all the data files:

****************************************************************************************



Mental Rotation:
outcome, choice, time, question,

outcome : 0 (incorrect) or 1 (correct)
choice : which option did they choose? (0, 1, 2, or 3) 
time : time in seconds since the game started
question : which question are they answering (0 through 9)

****************************************************************************************


Trajectory Prediction (First and Third Person):
distance, shadow, projectile,

distance : distance between the subject's guess and the projectile's landing point in virtual meters
shadow : 0 (no shadow) or 1 (with shadow)
projectile : 0 (arrow) or 1 (tennis ball)

****************************************************************************************


Animal Tracking:
distance,

distance : average distance between the mouse and the center of the animal during the trial in pixels

****************************************************************************************


Animal Finding:
time, animal,

time : time to find the animal in seconds
animal : 0 (pidgeon), 1 (Giraffe), 2 (Parrot), 3 (Wolf), 4 (Grasshopper), 5 (Brown Bird), 6 (Deer)

****************************************************************************************