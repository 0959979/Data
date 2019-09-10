class BankAccount:
    def __init__(self, A_No, balance=0):
            self.name = A_No
            self.balance = balance

    def Deposite(self, amount):
            self.balance += amount
    def Withdraw(self, amount):
            if self.balance >= amount: 
        
                self.balance -= amount 
                return True
            else:
                return False

    def TransfareMoney(self, amount, To):
            if self.Withdraw(amount) :
                To.Deposite(amount)
            
BA1 = BankAccount(123, 100)
BA2 = BankAccount(234, 50)
BA1.TransfareMoney(50, BA2)
print(BA1.balance)
         
