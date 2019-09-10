class Book:
    '''This is a class to define Book with some common attributes.'''
    def __init__(self):
        '''Initializes the attributes.'''
        self.title=""
        self.author=""
        self.isbn=""
        self.pages=0
        self.publisher=""
        self.edition=0


if __name__ == "__main__":
    print(" Class Book: ",Book.__doc__)
    print("__init__ function of the class ",Book.__init__.__doc__)    

    ooinpy = Book()  # here we create the object
    # let's assign values for our first object
    ooinpy.title  = "Object Oriented Programming in Python."
    ooinpy.author = "Michael H. Goldwasser"
    ooinpy.isbn = "0136150314"
    ooinpy.pages = 666
    ooinpy.publisher = "Pearson Prentice Hall"
    ooinpy.edition = 3

    # let's read some values from the book
    print("The title of the book is: "+ooinpy.title+" Authored by: "+ooinpy.author)