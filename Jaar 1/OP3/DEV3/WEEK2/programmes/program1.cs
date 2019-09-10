class Person
{
    public string name;
    public string surname;
    public int age;

    public Person(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
        this.age = 0;
    }

    public string greet()
    {
        return $"{this.name} {this.surname} says hi!";
    }

    public void get_older()
    {
        this.age = this.age + 1;
    }
}