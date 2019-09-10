class student:

    Graduated =200

    def __init__(self, id, name, major):
    
        self.id = id

        self.name = name

        self.major=major

        self.status = 'Active'

    def ChangeStatus(self, NewStatus):
        self.status = NewStatus
        if NewStatus == 'Graduated':
            student.Graduated +=1 
        else:
            print('FUCK ASSAD')
        
        

                                        

    def GetStatus(self):

        return self.status, self.__class__.Graduated
st1 = student(123,'name1', 'computer')
st2 = student(234,'name2', 'computer')
st1.ChangeStatus('Graduated')
st2.ChangeStatus('Graduated')
print(st1.GetStatus())

    
    