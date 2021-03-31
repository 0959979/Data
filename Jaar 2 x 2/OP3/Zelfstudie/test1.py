howManyInBed = 10

song = ''

while howManyInBed > 1  :
  song += str(howManyInBed) + ' in a bed and the little one said,\n'
  song += '``Roll over, roll over``\n'
  song += 'They all rolled over and one fell out,\n'
  howManyInBed = howManyInBed - 1

song += str(howManyInBed) + ' in a bed and the little one said,\n'
song += '``Alone at last``'

print()
