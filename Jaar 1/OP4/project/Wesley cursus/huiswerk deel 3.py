# Ik heb de random library nodig om dice te kunnen maken:
import random
#Ik heb de time library nodig om de programe te herhalen:
import time
# roll_again betekent dat de gebruiker een keuze heeft 
# om de dice nog een keer te gooien:
roll_again = 'Y'
while roll_again == 'Y':
    print('\nRolling the dice (;')
    time.sleep(3)
    
    #nu gaat de computer de kansen berekenen tussen 1 en 6:
    dice1 = random.randint(1, 6)
    dice2 = random.randint(1, 6)
    
    #nu zien we de resultaat:
    print('The values are: ')
    print('Dice 1 = ', dice1, '\nDice 2 = ', dice2)
    
    #nu geven we aan wat we op scherm willen krijgen:
    if dice1 == 1 and dice2 ==1:
        print('Limits')
    elif dice1 == 6 and dice2 ==6:
        print('Limits')
    elif dice1 == dice2:
       print('even')
    else:
        print('odd')
    roll_again =   input('would you like to try agin? Y/N ---- ')
    
    

   
