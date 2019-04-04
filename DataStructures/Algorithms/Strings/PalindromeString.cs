﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Algorithms.Strings
{
    public static class PalindromeString
    {
        public static bool IsPalindrome (string source)
        {
            int i = 0;
            int j = source.Length - 1;

            while (i < j && source[i] == source[j])
            {
                ++i;
                --j;
            }

            if (i < j)
                return false;
            return true;
        }
    }
}