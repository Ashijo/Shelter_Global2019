
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

    /// <summary>
    /// return true if the difference between a & b < epsilon
    /// </summary>
    /// <param name="a">first number</param>
    /// <param name="b">number to check</param>
    /// <param name="epsilon">the accept difference between a & b</param>
    /// <returns></returns>
    public static bool Near(this float a, float b, float epsilon = .1f) {
        bool backer = false;

        if (a + epsilon > b && a - epsilon < b) {
            backer = true;
        }

        return backer;
    }

    public static bool Near(this Vector3 a, Vector3 b, float epsilon = .1f) {
        bool backer = true;

        if (a.x + epsilon < b.x || a.x - epsilon > b.x) {
            backer = false;
        }
        else if (a.y + epsilon < b.y || a.y - epsilon > b.y) {
            backer = false;
        }
        else if (a.z + epsilon < b.z || a.z - epsilon > b.z) {
            backer = false;
        }

        return backer;
    }

    /// <summary>
    /// Sadly i cannot keep this one in a library, or, 
    /// i must link this class with my utils one :/
    /// Soon change to fit with a tupple system
    /// </summary>
    /// <typeparam name="K">Finally they would not be use</typeparam>
    /// <typeparam name="V">but i need this info here anyways</typeparam>
    /// <param name="list">the list to check</param>
    /// <returns>The number of KVPaire without any null elements</returns>
    public static int GetCountNotNull<K, V>(this List<Utils.KVPaires<K, V>> list) {
        int count = 0;
        for (int i = 0; i < list.Count; i++) {
            if (list[i].Key != null && list[i].Value != null) count++;
        }

        return count;
    }

    /// <summary>
    /// Simple cycle Clamp 
    /// </summary>
    /// <param name="max">well, the max i guess</param>
    /// <returns>if the value is > max ret 0</returns>
    public static int Clamp(this int a, int max) {
        if (a < 0) a = max;
        else if (a > max) a = 0;
        return a;
    }

    public static int Clamp(this int a, int min ,int max) {
        if (a < min) a = max;
        else if (a > max) a = min;
        return a;
    }


    public static Stack<T> ToStack<T>(this List<T> ToStack) {
        Stack<T> stack = new Stack<T>();

        foreach (T t in ToStack) {
            stack.Push(t);
        }
        return stack;
    }

}
