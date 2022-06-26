using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using System.Numerics;

namespace lab7
{
    class Program
    {
        public struct Key
        {
            public string name { get; set; }

            public string surname { get; set; }

            public Key(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
            }

        }
        public class Value
        {
            public string password { get; set; }

            public string email { get; set; }

            public LinkedList<Key> friends { get; set; }

            public Value(string password, string email, LinkedList<Key> friends)
            {
                this.password = password;
                this.email = email;
                this.friends = friends;
            }

            public void addFriend(Key key)
            {
                foreach (Key friend in friends)
                    if (friend.name == key.name && friend.surname == key.surname)
                        return;

                friends.AddLast(key);
            }

            public void removeFriend(Key key)
            {
                foreach (Key friend in friends)
                {
                    if (friend.name == key.name && friend.surname == key.surname)
                    {
                        friends.Remove(friend);
                        return;
                    }
                }
            }

        }
        public class User
        {
            public Key key { get; set; }

            public Value value { get; set; }

            public User(Key key, Value value)
            {
                this.key = key;
                this.value = value;
            }

        }
        class HashTable
        {
            User[] table;
            private int userCount = 0;
            private double loadness;

            public HashTable(int Size)
            {
                table = new User[Size];
            }

            public bool addUser(Key key, Value value)
            {
                userCount++;
                loadness = userCount * 1.0 / table.Length;

                if (loadness > 0.5)
                    rehashing();

                int hash = hashCode(key);
                while (table[hash] != null)
                {
                    if (table[hash].key.name == key.name && table[hash].key.surname == key.surname)
                    {
                        WriteLine("This user is already registered");
                        return false;
                    }
                    hash = (hash + 1) % table.Length;
                }

                value.password = hashPassword(value.password) + "";
                table[hash] = new User(key, value);
                return true;
            }

            public void removeUser(Key key)
            {
                int hash = hashCode(key);

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[hash] != null && (table[hash].key.name == key.name && table[hash].key.surname == key.surname))
                    {
                    o:
                        foreach (Key friend in table[hash].value.friends)
                        {
                            removeFriend(friend, table[hash].key);
                            goto o;
                        }
                        table[hash] = null;
                        userCount--;
                        return;
                    }
                    hash = (hash + 1) % table.Length;
                }

            }

            public Value findUser(Key key)
            {
                int hash = hashCode(key);

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[hash] != null)
                    {
                        if (table[hash].key.name == key.name && table[hash].key.surname == key.surname)
                        {
                            return table[hash].value;
                        }
                    }
                    else { WriteLine("Wrong User"); return null; }
                    hash = (hash + 1) % table.Length;
                }
                WriteLine("Wrong User");

                return null;

            }

            private bool IsPrimaryNum(int num)
            {
                int m = num / 2;

                for (int i = 2; i <= m; i++)
                    if (num % i == 0)
                        return false;

                return true;
            }

            private int newHashSize()
            {
                Random r = new Random();
                int randNum = r.Next(3, 10);

                while (!IsPrimaryNum(randNum))
                    randNum = r.Next(3, 10);

                return table.Length * randNum;
            }

            private void rehashing()
            {
                int hash, n;
                int newSize = newHashSize();
                User[] newTable = new User[newSize];

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                    {
                        BigInteger sum = 0;
                        n = table[i].key.name.Length + table[i].key.surname.Length;
                        for (int j = 0; j < table[i].key.name.Length; j++)
                        {
                            n--;
                            sum = (sum + (BigInteger)(((int)table[i].key.name.ToLower()[j] * Pow(31, n)) % newTable.Length)) % newTable.Length;
                            if (sum < 0)
                            {
                                sum += newTable.Length;
                            }
                        }
                        for (int j = 0; j < table[i].key.surname.Length; j++)
                        {
                            n--;
                            sum = (sum + (BigInteger)(((int)table[i].key.surname.ToLower()[j] * Pow(31, n)) % newTable.Length)) % newTable.Length;
                            if (sum < 0)
                            {
                                sum += newTable.Length;
                            }
                        }
                        hash = (int)(sum % newTable.Length);
                        if (hash < 0)
                        {
                            hash += newTable.Length;
                        }
                        while (newTable[hash] != null)
                            hash = (hash + 1) % newTable.Length;

                        newTable[hash] = table[i];
                    }
                }

                table = newTable;
            }

            private int hashCode(Key key)
            {
                BigInteger summ = 0;
                int n = key.name.Length + key.surname.Length;

                for (int i = 0; i < key.name.Length; i++)
                {
                    n--;
                    summ = (summ + (BigInteger)(((int)key.name.ToLower()[i] * Pow(31, n)) % table.Length)) % table.Length;
                    if (summ < 0)
                    {
                        summ += table.Length;
                    }
                }
                for (int i = 0; i < key.surname.Length; i++)
                {
                    n--;
                    summ = (summ + (BigInteger)(((int)key.surname.ToLower()[i] * Pow(31, n)) % table.Length)) % table.Length;
                    if (summ < 0)
                    {
                        summ += table.Length;
                    }
                }
                int hash = (int)(summ % table.Length);
                if (hash < 0)
                {
                    hash += table.Length;
                }
                return hash;
            }

            public BigInteger hashPassword(string password)
            {
                BigInteger summ = 0;
                int n = password.Length;

                for (int i = 0; i < password.Length; i++)
                {
                    n--;
                    summ += (BigInteger)((int)password.ToLower()[i] * Pow(31, n));
                }
                return summ;
            }

            public void addFriend(Key userKey, Key friendKey)
            {
                if (userKey.name == friendKey.name && userKey.surname == friendKey.surname)
                {
                    WriteLine("You can't be friend with yourself");
                    return;
                }
                Value friend = findUser(friendKey);

                if (friend == null)
                    return;

                int hash = hashCode(userKey);

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[hash].key.name == userKey.name && table[hash].key.surname == userKey.surname)
                    {
                        foreach (Key userFriend in table[hash].value.friends)
                        {
                            if (userFriend.surname == friendKey.surname && userFriend.name == friendKey.name)
                            {
                                WriteLine(friendKey.name + " " + friendKey.surname + " is already friend with you");
                                return;
                            }
                        }
                        table[hash].value.addFriend(friendKey);
                        hash = hashCode(friendKey);
                        for (int j = 0; j < table.Length; j++)
                        {
                            if (table[hash].key.name == friendKey.name && table[hash].key.surname == friendKey.surname)
                            {
                                table[hash].value.addFriend(userKey);
                                break;
                            }
                            hash = (hash + 1) % table.Length;
                        }
                        WriteLine(friendKey.name + " " + friendKey.surname + " is friend with you now");
                        return;
                    }
                    hash = (hash + 1) % table.Length;
                }
                WriteLine("Wrong User");
            }

            public void removeFriend(Key userKey, Key friendKey)
            {
                Value friend = findUser(friendKey);

                if (friend == null)
                    return;

                int hash = hashCode(userKey);

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[hash].key.name == userKey.name && table[hash].key.surname == userKey.surname)
                    {
                        table[hash].value.removeFriend(friendKey);
                        hash = hashCode(friendKey);
                        for (int j = 0; j < table.Length; j++)
                        {
                            if (table[hash].key.name == friendKey.name && table[hash].key.surname == friendKey.surname)
                            {
                                table[hash].value.removeFriend(userKey);
                                break;
                            }
                            hash = (hash + 1) % table.Length;
                        }
                        WriteLine(friendKey.name + " " + friendKey.surname + " is't friend with you now");
                        return;
                    }
                    hash = (hash + 1) % table.Length;
                }
                WriteLine("Wrong user");
            }

            public void showFriends(Key userKey)
            {
                int hash = hashCode(userKey);

                for (int i = 0; i < table.Length; i++)
                {
                    if (table[hash].key.name == userKey.name && table[hash].key.surname == userKey.surname)
                    {
                        if (table[hash].value.friends.Count == 0)
                        {
                            WriteLine("This user has no friends");
                            return;
                        }
                        foreach (Key friend in table[hash].value.friends)
                            WriteLine(String.Join(" ", friend.name, friend.surname));
                        return;
                    }
                    hash = (hash + 1) % table.Length;
                }

                WriteLine("Wrong user");
            }

            public void showUsers()
            {
                foreach (User user in table)
                {
                    if (user != null)
                        WriteLine(String.Join("\n", "Name: " + user.key.name, "Surname: " + user.key.surname, "Email: " + user.value.email + "\n\n\n"));
                }
            }

        }
        static void Main(string[] args)
        {
            HashTable hashTable = new HashTable(5);
            int choice, index = 0;
            bool state = false, userState = true;
            while (true)
            {
                index = 0;
                userState = true;
                Clear();
                WriteLine("Menu");
                if (!state)
                    WriteLine((index + 1) + ") Test values");
                else
                    index--;
                WriteLine((index + 2) + ") To login");
                WriteLine((index + 3) + ") To register");
                WriteLine((index + 4) + ") To see all users");
                try
                {
                    choice = Convert.ToInt32(ReadLine());
                    if (state)
                    {
                        choice++;
                    }
                    switch (choice)
                    {
                        case 1:
                            if (!state)
                            {
                                Clear();
                                //hashTable.addUser(new Key("Alex", "Green"), new Value("qwerty", "voxel@gmail.com", new LinkedList<Key>()));
                                //hashTable.addUser(new Key("Vasily", "Zybenko"), new Value("12345", "Odin@gmail.com", new LinkedList<Key>()));
                                //hashTable.addUser(new Key("Dimil", "Kotliar"), new Value("pass", "dimilKot@gmail.com", new LinkedList<Key>()));
                                //hashTable.addUser(new Key("Krong", "Trud"), new Value("abcde", "kroooong@ukr.net", new LinkedList<Key>()));
                                //hashTable.addUser(new Key("Alisa", "Hotkevich"), new Value("qwerty12", "Aliiise@gmail.com", new LinkedList<Key>()));
                                //hashTable.addUser(new Key("Mouse", "Grobovski"), new Value("qwerty", "vitgrab@gmail.com", new LinkedList<Key>()));

                                //hashTable.addFriend(new Key("Alex", "Green"), new Key("Vasily", "Zybenko"));
                                //hashTable.addFriend(new Key("Alex", "Green"), new Key("Dimil", "Kotliar"));
                                //hashTable.addFriend(new Key("Alex", "Green"), new Key("Alisa", "Hotkevich"));

                                //hashTable.addFriend(new Key("Vasily", "Zybenko"), new Key("Alisa", "Hotkevich"));
                                //hashTable.addFriend(new Key("Vasily", "Zybenko"), new Key("Mouse", "Grobovski"));
                                //hashTable.addFriend(new Key("Vasily", "Zybenko"), new Key("Krong", "Trud"));

                                //hashTable.addFriend(new Key("Alisa", "Hotkevich"), new Key("Alex", "Green"));
                                //hashTable.addFriend(new Key("Alisa", "Hotkevich"), new Key("Mouse", "Grobovski"));

                                //hashTable.addFriend(new Key("Mouse", "Grobovski"), new Key("Alisa", "Hotkevich"));

                                //hashTable.addFriend(new Key("Krong", "Trud"), new Key("Alex", "Green"));
                                //hashTable.addFriend(new Key("Krong", "Trud"), new Key("Vasily", "Zybenko"));

                                //hashTable.addFriend(new Key("Dimil", "Kotliar"), new Key("Alex", "Green"));
                                //hashTable.addFriend(new Key("Dimil", "Kotliar"), new Key("Alisa", "Hotkevich"));
                                Clear();
                                hashTable.showUsers();
                                state = true;
                            }
                            break;

                        case 2:

                        a: Clear();
                            Write("Login\n" +
                                          " Name: ");
                            string nameUser = ReadLine();
                            if (nameUser == "")
                            {
                                goto a;
                            }
                        b: Write(" Surname: ");
                            string surnameUser = ReadLine();
                            if (surnameUser == "")
                            {
                                Clear();
                                WriteLine("Login\n" +
                                          " Name: " + nameUser);
                                goto b;
                            }
                        c: Write(" Password: ");
                            string passwordUser = ReadLine();
                            if (passwordUser == "")
                            {
                                Clear();
                                WriteLine("Login\n" +
                                          " Name: " + nameUser + "\n" +
                                          " Surname: " + surnameUser);
                                goto c;
                            }
                            Value user = hashTable.findUser(new Key(nameUser, surnameUser));
                            if (user == null)
                            {
                                WriteLine("This user is not registered");
                                break;
                            }

                            if (hashTable.hashPassword(passwordUser) + "" == user.password)
                            {
                                while (userState)
                                {
                                    Clear();
                                    WriteLine($"{nameUser} {surnameUser}'s menu\n" +
                                              "1) To add friends\n" +
                                              "2) To delete friends\n" +
                                              "3) Your friends\n" +
                                              "4) Delete this user\n" +
                                              "5) Exit to the main menu");
                                    try
                                    {
                                        switch (Convert.ToInt32(ReadLine()))
                                        {
                                            case 1:
                                            d: Clear();
                                                Write("Adding a new friend\n" +
                                                      "Friend's name: ");
                                                string friendNameAdd = ReadLine();
                                                if (friendNameAdd == "")
                                                {
                                                    goto d;
                                                }
                                            e: Write("Surname: ");
                                                string friendSurAdd = ReadLine();
                                                if (friendSurAdd == "")
                                                {
                                                    Clear();
                                                    WriteLine("Adding a new friend\n" +
                                                              "Friend's name: " + friendNameAdd);
                                                    goto e;
                                                }

                                                hashTable.addFriend(new Key(nameUser, surnameUser), new Key(friendNameAdd, friendSurAdd));
                                                break;
                                            case 2:
                                            f: Clear();
                                                Write("Deleting a friend\n" +
                                                      "Friend's name: ");
                                                string friendNameDel = ReadLine();
                                                if (friendNameDel == "")
                                                {
                                                    goto f;
                                                }
                                            g: Write("Surname: ");
                                                string friendSurnameDel = ReadLine();
                                                if (friendSurnameDel == "")
                                                {
                                                    Clear();
                                                    WriteLine("Deleting a friend\n" +
                                                              "Friend's name: " + friendNameDel);
                                                    goto g;
                                                }
                                                hashTable.removeFriend(new Key(nameUser, surnameUser), new Key(friendNameDel, friendSurnameDel));
                                                break;
                                            case 3:
                                                Clear();
                                                WriteLine("Your friends:");
                                                hashTable.showFriends(new Key(nameUser, surnameUser));
                                                break;
                                            case 4:
                                                hashTable.removeUser(new Key(nameUser, surnameUser));
                                                Clear();
                                                WriteLine($"{nameUser} {surnameUser} was successfully deleted");
                                                userState = false;
                                                break;
                                            case 5:
                                                Clear();
                                                userState = false;
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        WriteLine("Wrong button");
                                    }
                                    if (userState)
                                    {
                                        WriteLine("\nPress any button to go back....");
                                        ReadKey();
                                    }
                                }
                            }
                            else
                                WriteLine(" Wrong password");
                            break;
                        case 3:
                        q: Clear();
                            Write("Registration\n" +
                                          " Name: ");
                            string newNameUser = ReadLine();
                            if (newNameUser == "")
                            {
                                goto q;
                            }
                        r: Write(" Surname: ");
                            string newSurnameUser = ReadLine();
                            if (newSurnameUser == "")
                            {
                                Clear();
                                WriteLine("Registration\n" +
                                          " Name: " + newNameUser);
                                goto r;
                            }
                        n: Write(" Password: ");
                            string newPasswordUser = ReadLine();
                            if (newPasswordUser == "")
                            {
                                Clear();
                                WriteLine("Registration\n" +
                                          " Name: " + newNameUser + "\n" +
                                          " Surname: " + newSurnameUser);
                                goto n;
                            }
                        l: Write(" E-mail: ");
                            string newemailUser = ReadLine();
                            if (newemailUser == "")
                            {
                                Clear();
                                WriteLine("Registration\n" +
                                          " Name: " + newNameUser + "\n" +
                                          " Surname: " + newSurnameUser + "\n" +
                                          " Password: " + newPasswordUser);
                                goto l;
                            }
                            if (hashTable.addUser(new Key(newNameUser, newSurnameUser), new Value(newPasswordUser, newemailUser, new LinkedList<Key>())))
                            {
                                while (userState)
                                {
                                    Clear();
                                    WriteLine($"{newNameUser} {newSurnameUser}'s menu\n" +
                                              "1) To add friends\n" +
                                              "2) To delete friends\n" +
                                              "3) Your friends\n" +
                                              "4) Delete this user\n" +
                                              "5) Exit to the main menu");
                                    try
                                    {
                                        switch (Convert.ToInt32(ReadLine()))
                                        {
                                            case 1:
                                            d: Clear();
                                                Write("Adding a new friend\n" +
                                                      "Friend's name: ");
                                                string friendNameAdd = ReadLine();
                                                if (friendNameAdd == "")
                                                {
                                                    goto d;
                                                }
                                            e: Write("Surname: ");
                                                string friendSurAdd = ReadLine();
                                                if (friendSurAdd == "")
                                                {
                                                    Clear();
                                                    WriteLine("Adding a new friend\n" +
                                                              "Friend's name: " + friendNameAdd);
                                                    goto e;
                                                }

                                                hashTable.addFriend(new Key(newNameUser, newSurnameUser), new Key(friendNameAdd, friendSurAdd));
                                                break;
                                            case 2:
                                            f: Clear();
                                                Write("Deleting a friend\n" +
                                                      "Friend's name: ");
                                                string friendNameDel = ReadLine();
                                                if (friendNameDel == "")
                                                {
                                                    goto f;
                                                }
                                            g: Write("Surname: ");
                                                string friendSurnameDel = ReadLine();
                                                if (friendSurnameDel == "")
                                                {
                                                    Clear();
                                                    WriteLine("Deleting a friend\n" +
                                                              "Friend's name: " + friendNameDel);
                                                    goto g;
                                                }
                                                hashTable.removeFriend(new Key(newNameUser, newSurnameUser), new Key(friendNameDel, friendSurnameDel));
                                                break;
                                            case 3:
                                                Clear();
                                                WriteLine("Your friends:");
                                                hashTable.showFriends(new Key(newNameUser, newSurnameUser));
                                                break;
                                            case 4:
                                                hashTable.removeUser(new Key(newNameUser, newSurnameUser));
                                                Clear();
                                                WriteLine($"{newNameUser} {newSurnameUser} was successfully deleted");
                                                userState = false;
                                                break;
                                            case 5:
                                                Clear();
                                                userState = false;
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        WriteLine("Wrong button");
                                    }
                                    if (userState)
                                    {
                                        WriteLine("\nPress any button to go back....");
                                        ReadKey();
                                    }
                                }
                            }
                            break;

                        case 4:
                            Clear();
                            hashTable.showUsers();
                            break;

                    }
                }
                catch
                {
                    WriteLine("Wrong button");
                }
                WriteLine("\nPress any button to go back....");
                ReadKey();
            }
        }

    }

}