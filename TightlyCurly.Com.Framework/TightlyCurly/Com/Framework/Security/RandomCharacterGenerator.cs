using System;
using System.Security.Cryptography;

namespace TightlyCurly.Com.Framework.Security;

public class RandomCharacterGenerator
{
    private static int DEFAULT_MIN_PASSWORD_LENGTH = 30;

    private static int DEFAULT_MAX_PASSWORD_LENGTH = 45;

    private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";

    private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";

    private static string PASSWORD_CHARS_NUMERIC = "23456789";

    private static string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

    public static string Generate()
    {
        return Generate(DEFAULT_MIN_PASSWORD_LENGTH, DEFAULT_MAX_PASSWORD_LENGTH);
    }

    public static string Generate(int length)
    {
        return Generate(length, length);
    }

    public static string Generate(int minLength, int maxLength)
    {
        if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
        {
            return null;
        }
        char[][] array = new char[4][]
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()
        };
        int[] array2 = new int[array.Length];
        for (int i = 0; i < array2.Length; i++)
        {
            array2[i] = array[i].Length;
        }
        int[] array3 = new int[array.Length];
        for (int i = 0; i < array3.Length; i++)
        {
            array3[i] = i;
        }
        byte[] array4 = new byte[4];
        using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
        {
            randomNumberGenerator.GetBytes(array4);
        }
        int seed = ((array4[0] & 0x7F) << 24) | (array4[1] << 16) | (array4[2] << 8) | array4[3];
        Random random = new Random(seed);
        char[] array5 = null;
        array5 = ((minLength >= maxLength) ? new char[minLength] : new char[random.Next(minLength, maxLength + 1)]);
        int num = array3.Length - 1;
        for (int i = 0; i < array5.Length; i++)
        {
            int num2 = ((num != 0) ? random.Next(0, num) : 0);
            int num3 = array3[num2];
            int num4 = array2[num3] - 1;
            int num5 = ((num4 != 0) ? random.Next(0, num4 + 1) : 0);
            array5[i] = array[num3][num5];
            if (num4 == 0)
            {
                array2[num3] = array[num3].Length;
            }
            else
            {
                if (num4 != num5)
                {
                    char c = array[num3][num4];
                    array[num3][num4] = array[num3][num5];
                    array[num3][num5] = c;
                }
                array2[num3]--;
            }
            if (num == 0)
            {
                num = array3.Length - 1;
                continue;
            }
            if (num != num2)
            {
                int num6 = array3[num];
                array3[num] = array3[num2];
                array3[num2] = num6;
            }
            num--;
        }
        return new string(array5);
    }
}
