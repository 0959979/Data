from random import *
class Die:
    def __init__(self,s=6):
        self.sides=s
        self.state=randint(1,self.sides)
    def throw(self):
        self.state=randint(1,self.sides)
        return self.sides

class player:
        def __init__(self,n=''):
            self.name = n 
            self.d1 = Die()
            self.d2 = Die(10)
        def play(self):
            num1 = d1.throw()
            num2 = d2.throw()
            self.move = (num2)
            self.move = (num2)
            nums=[d.throw() for d in dice]
            for x in nums:
                self.move(x)
            #rest of code
        def move(self,n):
        # move your pawns here
        
            print(self.name,'moved pawns',n)
        
if __name__=='__main__':
    d1 = Die() # a normal die
    d2 = Die(10)# a die with 10 sides
    players = [] # list of players
    players.append(player('Alex'))
    players.append(player('Bob'))
    players.append(player('Dianna'))
    players.append(player('John'))
        
    for p in players:
        p.play(d1)
