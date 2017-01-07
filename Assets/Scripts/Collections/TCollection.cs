﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


internal class TCollection<T> : ITCollection<T> where T : IValue
{
    private T[] items;
    private int size;
    private int itemCount;

    public TCollection()
        : this(16)
    {

    }

    public TCollection(int size)
    {
        this.size = size;
        items = new T[size];
    }

    /// <summary>
    /// The size of the collection.
    /// </summary>
    public int Count
    {
        get
        {
            return itemCount;
        }
    }

    /// <summary>
    /// Returns and removes the object with the highest value from the collection.
    /// </summary>
    /// <returns></returns>
    public T DequeueMax()
    {
        int maxIndex = MaxIndex();
        if (maxIndex < 0) return default(T);

        T max = items[maxIndex];
        items = ExtensionMethods.RemoveAt(items, maxIndex);
        itemCount--;
        return max;
    }

    /// <summary>
    /// Returns and removes the object with the highest value from the collection if the value is higher or the same as the given value.
    /// </summary>
    /// <param name="value">The minimum value of the object you want</param>
    /// <returns>The object with the highest value if it's higher or the same as the given value</returns>
    public T DequeueMax(float value)
    {
        int maxIndex = MaxIndex();
        if (maxIndex < 0) return default(T);

        T max = items[maxIndex];
        if (max.Value >= value)
        {
            items.RemoveAt(maxIndex);
            itemCount--;
            return max;
        }
        else
        {
            return default(T);
        }
    }

    /// <summary>
    /// Returns and removes the object with the lowest value from the collection.
    /// </summary>
    /// <returns>The object with the lowest value</returns>
    public T DequeueMin()
    {
        int minIndex = MinIndex();
        if (minIndex < 0) return default(T);

        T min = items[minIndex];
        items.RemoveAt(minIndex);
        itemCount--;
        return min;
    }

    /// <summary>
    /// Returns and removes the object with the lowest value from the collection if the value is lower or the same as the given value.
    /// </summary>
    /// <param name="value">The maximum value of the object you want</param>
    /// <returns>The object with the lowest value if it's below or the same as the given value</returns>
    public T DequeueMin(float value)
    {
        int minIndex = MinIndex();
        if (minIndex < 0) return default(T);

        T min = items[minIndex];
        if (min.Value <= value)
        {
            items.RemoveAt(minIndex);
            itemCount--;
            return min;
        }
        else
        {
            return default(T);
        }
    }

    /// <summary>
    /// Adds the given object to the collection.
    /// </summary>
    /// <param name="item">The item you want to add to the collection</param>
    public void Enqueue(T item)
    {
        if (itemCount >= size)
        {
            var temp = items;
            size *= 2;
            items = new T[size];
            Array.Copy(temp, items, temp.Length);
        }
        items[itemCount] = item;
        itemCount++;
    }

    /// <summary>
    /// Returns the object with the highest value in the collection.
    /// </summary>
    /// <returns>The object with the highest value</returns>
    public T Max()
    {
        return items[MaxIndex()];
    }

    /// <summary>
    /// Returns the index of the item with the highest value.
    /// </summary>
    /// <returns>The index of the item with the highest value</returns>
    private int MaxIndex()
    {
        int highestIndex = -1;
        for (int i = 0; i < itemCount; i++)
        {
            if(!items[i].IsObjectUsed)
            {
                if (highestIndex < 0)
                {
                    highestIndex = i;
                }
                else if (items[highestIndex].Value < items[i].Value) highestIndex = i;
            }

            //if (items[highestIndex].GetValue() < items[i].GetValue()) highestIndex = i;
        }
        return highestIndex;
    }

    /// <summary>
    /// Retrieves the object with the lowest value in the collection.
    /// </summary>
    /// <returns>The object with the lowest value</returns>
    public T Min()
    {
        return items[MinIndex()];
    }

    /// <summary>
    /// Returns the index of the item with the lowest value
    /// </summary>
    /// <returns>The index of the item with the lowest value</returns>
    private int MinIndex()
    {
        int lowestIndex = -1;
        for (int i = 0; i < itemCount; i++)
        {
            if (!items[i].IsObjectUsed)
            {
                if(lowestIndex < 0)
                {
                    lowestIndex = i;
                }
                else if (items[lowestIndex].Value > items[i].Value) lowestIndex = i;
            }
        }
        return lowestIndex;
    }

    /// <summary>
    /// Removes the given object from the collection.
    /// </summary>
    /// <param name="item">The item that needs to be removed from the collection</param>
    /// <returns>Wether the object has been deleted or not</returns>
    public bool Remove(T item)
    {
        for (int i = 0; i < itemCount; i++)
        {
            if (EqualityComparer<T>.Default.Equals(item))
            {
                items.RemoveAt(i);
                return true;
                //items = items.Where((source, index) => index != i).ToArray();
            }
        }
        return false;
    }
}

public static class ExtensionMethods
{
    /// <summary>
    /// First i wanted to use linq to get a new array but that is way to slow, this is the fastest solution.
    /// http://stackoverflow.com/questions/457453/remove-element-of-a-regular-array
    /// </summary>
    /// <typeparam name="T">The type of object</typeparam>
    /// <param name="source">The array</param>
    /// <param name="index">Index of the item.</param>
    /// <returns>The renewed array</returns>
    internal static T[] RemoveAt<T>(this T[] source, int index)
    {
        T[] dest = new T[source.Length];
        if (index > 0)
            Array.Copy(source, 0, dest, 0, index); //Kopieert het gedeelte van de items array tot het item waarvan je de index gaf in dest.

        if (index < source.Length - 1)
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1); //Kopieert het gedeelte van de items array in dest na het item waarvan je de index gaf.

        return dest;
    }
}
