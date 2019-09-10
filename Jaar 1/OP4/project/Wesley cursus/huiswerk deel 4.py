n = int(input('Enter the number of rows: '))
for i in range(0,n):
    for s in range(0,n-i-1):
        print(end=' ')
    for s in range(0,1*i+1):
        print('*',end=' ')
    print()
# Tweede oplossing: 
#n = int(input('Please enter the number of rows: '))
#for i in range(n):
    #print(" "*(n-1-i)+"* "*(i+1))

    