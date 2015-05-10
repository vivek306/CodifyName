using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CodifyName
{
    public class CodifyNameCSharp
    {
        // Initialize and start the Encoding/Decoding
        public String CodifyName(String input, String name, Boolean EncodeMe)
        {
            char[] inputChar = input.ToArray();
            char[] nameChar = name.Replace(@" ", "").Distinct().ToArray();
            char[] alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,!%^&*-_=+[]{}#~'@/?<>|1234567890(); ".ToArray();
            List<Int32> listNamePositions = GetAlphabetPositions(nameChar, alphabets);
            while (listNamePositions.Count > 5)
                listNamePositions.RemoveAt(listNamePositions.Count - 1);
            List<List<Int32>> posNamePermutations = GetPermutations(listNamePositions.ToArray());
            Dictionary<Char, List<Int32>> alphaPosNamePermutation = AssignAlphabetPositionPermutation(alphabets, posNamePermutations);
            String result = "";
            if (EncodeMe)
            {
                result = EncoderCodifyName(inputChar, alphabets, alphaPosNamePermutation);
            }
            else
            {
                result = DecoderCodifyName(inputChar, alphabets, alphaPosNamePermutation, listNamePositions.Count);
            }
            return result;
        }

        // Convert String Array to string
        public String getString(String[] arr)
        {
            StringBuilder builder = new StringBuilder();
            foreach (String s in arr)
            {
                builder.Append(s);
            }
            return builder.ToString();
        }

        // Decode input
        protected String DecoderCodifyName(char[] inputDecode, char[] alphabets, Dictionary<Char, List<Int32>> alphaPosNamePermutation, int divide)
        {
            List<Int32> alphaPos = new List<Int32>();
            String decodedString = "";
            for (int i = 0; i < inputDecode.Count(); i++)
            {
                alphaPos.Add(new String(alphabets).IndexOf(inputDecode[i]));
                if ((i + 1) % divide == 0)
                {
                    decodedString += getKeyByValue(alphaPosNamePermutation, alphaPos);
                    alphaPos = new List<Int32>();
                }
            }
            return decodedString;
        }

        public Char getKeyByValue(Dictionary<Char, List<Int32>> map, List<Int32> value)
        {
            return map.FirstOrDefault(alphabet => alphabet.Value.SequenceEqual(value)).Key;
        }

        // Enccode input
        protected String EncoderCodifyName(char[] input, char[] alphabets, Dictionary<Char, List<Int32>> alphaPosNamePermutation)
        {
            String encodedString = "";
            for (int indexInput = 0; indexInput < input.Length; indexInput++)
            {
                List<Int32> pos = alphaPosNamePermutation[input[indexInput]];
                for (int indexPos = 0; indexPos < pos.Count; indexPos++)
                {
                    encodedString += alphabets[pos[indexPos]];
                }
            }
            return encodedString;
        }

        // Assign every character in the alphabets a permutated combination
        protected Dictionary<Char, List<Int32>> AssignAlphabetPositionPermutation(char[] alphabets, List<List<Int32>> posPermutations)
        {
            Dictionary<Char, List<Int32>> alphaPosPer = new Dictionary<Char, List<Int32>>();
            for (int i = 0; i < alphabets.Count(); i++)
            {
                alphaPosPer.Add(alphabets[i], posPermutations[i]);
            }
            return alphaPosPer;
        }

        // Get char position
        protected List<Int32> GetAlphabetPositions(char[] name, char[] alphabets)
        {
            List<Int32> pos = new List<Int32>();
            for (int index = 0; index < name.Length; index++)
            {
                pos.Add(new String(alphabets).IndexOf(name[index]));
            }
            return pos;
        }

        // Calculate permutation
        protected List<List<Int32>> GetPermutations(Int32[] num)
        {
            List<List<Int32>> result = new List<List<Int32>>();

            //start from an empty list
            result.Add(new List<Int32>());

            for (int i = 0; i < num.Length; i++)
            {
                //list of list in current iteration of the array num
                List<List<Int32>> current = new List<List<Int32>>();

                foreach (List<Int32> l in result)
                {
                    // # of locations to insert is largest index + 1
                    for (int j = 0; j < l.Count + 1; j++)
                    {
                        // + add num[i] to different locations
                        l.Insert(j, num[i]);

                        List<Int32> temp = new List<Int32>(l);
                        current.Add(temp);

                        // - remove num[i] add
                        l.RemoveAt(j);
                    }
                }

                result = new List<List<Int32>>(current);
            }

            return result;
        }
    }
}