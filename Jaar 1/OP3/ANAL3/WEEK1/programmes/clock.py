class clock:
    def __init__(self, hour, min, sec ):
      self.hour = hour
      self.min = min
      self.sec = sec

    def GetTime(self):
        return self.hour, self.min, self.sec 


    def SetTime(self, hour, min, sec):
        self.hour = hour
        self.min = min
        self.sec = sec


C1 = clock(3, 20, 0)
C2 = clock(2, 20, 0)
C3 = clock(4, 20, 0)
C4 = clock(5, 20, 0)
C5 = clock(6, 20, 0)
C6 = clock(7, 20, 0)


print(C3.GetTime())


