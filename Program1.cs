using System.Text;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO;
using Microsoft.VisualBasic;

Student student1 = new Student("Ivanov", "Ivan", "Ivanovich", "M18O-213Б-21", "C#");
Console.WriteLine(student1.GetName);

Console.WriteLine(student1.ToString());

Console.WriteLine(student1.GetNumberOfCourse);

Dictionary<Student, string> hashTable = new Dictionary<Student, string>();
var key = new Student("Ivanov", "Ivan", "Ivanovich", "M18O-210C-20", "GO");
hashTable.Add(key, "1234");

Student student2 = new Student("Ivanov", "Andrey", "Ivanovich", "M12O-213Б-21", "GO");
Console.WriteLine(student1.Equals((object)student2));

Student std1 = new Student("Petrov", "Dmitry", "Alekseevich", "M2O-110C-20", "Yandex");
Student std2 = new Student("Petrov", "Dmitry", "Alekseevich", "M2O-110C-20", "GO");
Console.WriteLine(std1.Equals((object)std2));
Console.WriteLine(((IEquatable<object>)std1).Equals(std2));

Student student3 = new Student("1", null, "Ivanovich", null, "C#"); // исключение
public class Student :
    IEquatable<string>,
    IEquatable<Student>,
    IEquatable<object>
{
    private string surname;
    private string name;
    private string patronymic;
    private string group;
    private string course;

    public Student(string surName, string firstName, string middleName, string studyGroup, string studyCourse) // конструктор объекта класса
    {

        if (surName == null)
        {
            throw new ArgumentOutOfRangeException(nameof(surName), "Invalid argument.");
        }

        else if (firstName == null)
        {
            throw new ArgumentOutOfRangeException(nameof(firstName), "Invalid argument.");
        }

        else if (middleName == null) 
        {
            throw new ArgumentOutOfRangeException(nameof(middleName), "Invalid argument.");
        }

        else if (studyGroup == null) 
        {
            throw new ArgumentOutOfRangeException(nameof(studyGroup), "Invalid argument.");
        }

        else if (studyCourse == null)
        {
            throw new ArgumentOutOfRangeException(nameof(studyCourse), "Invalid argument.");
        }

        // Другой вариант:  surname = surName ?? throw new ArgumentNullException(surName);

        else
        {
            surname = surName;
            name = firstName;
            patronymic = middleName;
            group = studyGroup;
            course = studyCourse;
        }
    }
    public string GetSurname // свойство для запроса фамилии
    {
        get
        {
            return surname;
        }

        //set
        //{
        //    surname = value;
        //}
    }

    public string GetName
    {
        get
        {
            return name;
        }

        //set
        //{
        //    name = value;
        //}
    }

    public string GetPatronymic
    {
        get
        {
            return patronymic;
        }

        //set
        //{
        //    patronymic = value;
        //}
    }

    public string GetGroup
    {
        get
        {
            return group;
        }

        //set
        //{
        //    group = value;
        //}
    }
    public string GetCourse
    {
        get
        {
            return course;
        }
        //privateset
        //{
        //    course = value;
        //}
    }
    public int GetNumberOfCourse //свойство для получения номера курса по учебной группе
    {
        get
        {
            if (group.Length == 11) // могут быть M8О-213-21 или M10O-213-21
            {
                return group[4] - 48;
            }
            else
            {
                return group[5] - 48;
            }
        }
    }

    // переопределение методов

    public override string ToString()
    {
        return $"Student: {GetSurname} {GetName} {GetPatronymic}\nGroup: {GetGroup}\nCourse: {GetCourse}";
    }
    public override int GetHashCode() // для hash-кода не можем менять поля, поэтоу отсутствует set
    {
        return GetSurname.GetHashCode() * 23 + GetName.GetHashCode() * 11 + GetPatronymic.GetHashCode() * 7 + GetGroup.GetHashCode() * 17 + GetCourse.GetHashCode();
    }

    public bool Equals(string str) // у нас все поля строковые
    {
        return GetSurname.Equals(str);
    }

    public bool Equals(
        Student? str)
    {
        if (str == null)
        {
            return false;
        }
        // Можно проверить по всем параметрам
        return GetName == str.GetName
               && GetSurname.Equals(str.GetSurname, StringComparison.Ordinal) 
               && GetPatronymic.Equals(str.GetPatronymic, StringComparison.Ordinal)
               && GetCourse.Equals(str.GetCourse, StringComparison.Ordinal)
               && GetGroup.Equals(str.GetGroup, StringComparison.Ordinal);

    }

    public override bool Equals(object? std) // вызван метод, который живет на уровне базового типа 
    {
        Console.WriteLine("Method object.Equals called.");
    
        if (std == null)
        {
            return false;
        }

        if (std is Student mgl)
        {
           return Equals(mgl);
        }

        if (std is string str)
        {
            return Equals(str);
        }
        return false;

    }

    bool IEquatable<object>.Equals(

        object? std)
    {
        Console.WriteLine("IEquatable<object>.Equals called."); // вызван метод, который живет на уровне интерфеса 

        if (std == null)
        {
            return false;
        }

        if (std is Student mgl)
        {
            return Equals(mgl);
        }

        if (std is string str)
        {
            return Equals(str);
        }
        return false;
    } 
} 