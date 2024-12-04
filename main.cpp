#include <iostream>

using namespace std;

class Person {
private:
    string address;
public:
    string name;
    int age;

    Person(string address, string name, int age) {
        this->age = age;
        this->name = name;
        this->address = address;
    }

    string to_string() {
        return "%s %d %s";
    }

    ~Person() {
        cout << "\nDestructor executed";
    }

    friend void friend_function(Person);
};


class Employee : public Person {
private:
    int income;
public :
    string position;

    Employee() : Person("", "", 0) {
        income = 0;
        position = "";
    }


    friend void friend_function(Person);

};

void friend_function(Person) {
    Employee employee1;
    employee1.name = "lefv,fev,";
    employee1.age = 30;
    employee1.address = "123 Main St";
    employee1.position = "Software Engineer";
    employee1.income = 50000;

    cout << "\nEmployee Details:" << endl;
    cout << "Name: " << employee1.name << endl;
    cout << "Age: " << employee1.age << endl;
    cout << "Address: " << employee1.address << endl;
    cout << "Position: " << employee1.position << endl;
    cout << "Income: $" << employee1.income << endl;
}


class Dealership;

class Manufacturer {
private:
    int production_cost;
public:
    void get_available_spaces(Dealership space);

    Manufacturer() {
        production_cost = 123433;
    }
    friend class Dealership;
};

class Dealership {
private:
    int available_spaces;

public:
    void get_production_cost(Manufacturer cost) {
        cout << "Production cost:" << cost.production_cost << "$" << endl;
    }

    Dealership() {
        available_spaces = 10;
    }

    friend class Manufacturer;
};

void Manufacturer::get_available_spaces(Dealership space) {
    cout << "Available spaces: " << space.available_spaces;
}

int main() {

    Manufacturer car1;
    Dealership dealership;
    dealership.get_production_cost(car1);
    car1.get_available_spaces(dealership);

    return 0;
}
