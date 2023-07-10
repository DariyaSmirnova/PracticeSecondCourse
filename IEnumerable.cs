using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Lab2
{

    static class IEnumerable
    {
        public static void DuplicateAndEmpty(int count, int dist)
        {
            if (count != dist)
            {
                throw new ArgumentException("The same values are present.");
            }

            if (count == 0)
            {
                throw new ArgumentException("There is an empty array.");
            }
        }


        // Сочетания
        public static IEnumerable<IEnumerable<T>> Combinations<T>(
            this IEnumerable<T> list, // this - доступ к текущему экземпляру класса
            int numberElements,
            IEqualityComparer<T> comparer)
        {

            DuplicateAndEmpty(list.Count(), list.Distinct(comparer).Count()); // Distinct - для удаления дублей в наборе 

            if (numberElements == 1)
            {
               return list.Select(T => new T[] {T}); // Select проецирует каждый элемент последовательности в новую форму
            }
    
           return Combinations(list, numberElements - 1, comparer)
                .SelectMany(t => list, (T1, T2) => T1.Concat(new T[] {T2})); // SelectMany - свести набор коллекций в одну коллекцию
        }

        // Подмножества без повторений
        public static IEnumerable<IEnumerable<T>> Subsets<T>(
          this IEnumerable<T> list,
          IEqualityComparer<T> comparer)
        {
            DuplicateAndEmpty(list.Count(), list.Distinct(comparer).Count());

            // as преобразует результат выражения в указанный ссылочный или поддерживающий NULL тип. Если такое преобразование невозможно, возвращает null
            // ?? Возвращает левый операнд, если он не null, иначе - правый операнд. Левый операнд может принимать 
            // ToArray() Копирует элементы списка List<T> в новый массив.

            var listArray = list as T[] ?? list.ToArray();

            for (int i = 0; i < (1 << listArray.Length); i++)
            {
                var res = Array.Empty<T>();
                var mask = i;

                foreach (var item in listArray)
                {
                    if ((mask & 1) == 1)
                    {
                        res = res.Concat(new[] {item}).ToArray();
                    }
                    mask >>= 1; // >> сдвигает левый операнд вправо на количество битов, определенное правым операндом
                }

                yield return res;
            }
        }

        // Перестановки
        public static IEnumerable<IEnumerable<T>> Reshuffles<T>(
            this IEnumerable<T> list,
            IEqualityComparer<T> comparer)
        {

            DuplicateAndEmpty(list.Count(), list.Distinct(comparer).Count());

            var index = 0;

            foreach (var item in list)
            {
                // Take извлекает определенное число элементов
                // Concat - сцепление
                // Skip пропускает определенное число элементов
                var res = list.Take(index).Concat(list.Skip(index + 1));
    
                foreach (var item2 in res.Reshuffles(comparer))
                {
                    yield return new[] { item }.Concat(item2);
                }
                index++;

            }
        }
    }
}
