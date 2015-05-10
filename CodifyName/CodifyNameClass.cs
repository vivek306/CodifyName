using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodifyName
{
    public class CodifyNameClass
    {
        // Initialize and start the Encoding/Decoding
        public string CodifyName(string input, string name, bool EncodeMe)
        {
            char[] inputChar = input.ToArray();
            char[] nameChar = name.Replace(@" ", "").Distinct().ToArray();
            char[] alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,!%^&*-_=+[]{}#~'@/?<>|1234567890(); ".ToCharArray();
            var listNamePositions = GetAlphabetPositions(nameChar, alphabets);
            while (listNamePositions.Count > 5)
                listNamePositions.RemoveAt(listNamePositions.Count - 1);
            List<IEnumerable<int>> posNamePermutations = GetPermutations(listNamePositions, listNamePositions.Count).ToList();
            Dictionary<char, IEnumerable<int>> alphaPosNamePermutation = AssignAlphabetPositionPermutation(alphabets, posNamePermutations);
            var result = string.Empty;
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
        // Decode input
        public string DecoderCodifyName(char[] inputDecode, char[] alphabets, Dictionary<char, IEnumerable<int>> alphaPosNamePermutation, int divide)
        {
            List<int> alphaPos = new List<int>();
            string decodedString = string.Empty;
            for (int i = 0; i < inputDecode.Length; i++)
            {
                alphaPos.Add(Array.FindIndex(alphabets, alphabet => alphabet == inputDecode[i]));
                if ((i + 1) % divide == 0)
                {
                    decodedString += alphaPosNamePermutation.FirstOrDefault(alphabet => alphabet.Value.SequenceEqual(alphaPos)).Key;
                    alphaPos = new List<int>();
                }
            }
            return decodedString;
        }
        // Enccode input
        public string EncoderCodifyName(char[] input, char[] alphabets, Dictionary<char, IEnumerable<int>> alphaPosNamePermutation)
        {
            string encodedString = string.Empty;
            foreach (var item in input)
            {
                foreach (var pos in alphaPosNamePermutation[item])
                {
                    encodedString += alphabets[pos];
                }
            }

            return encodedString;
        }
        // Assign every character in the alphabets a permutated combination
        public Dictionary<char, IEnumerable<int>> AssignAlphabetPositionPermutation(char[] alphabets, List<IEnumerable<int>> posPermutations)
        {
            Dictionary<char, IEnumerable<int>> alphaPosPer = new Dictionary<char, IEnumerable<int>>();
            for (int i = 0; i < alphabets.Length; i++)
            {
                alphaPosPer.Add(alphabets[i], posPermutations[i]);
            }
            return alphaPosPer;
        }
        // Get Char Positions
        public List<int> GetAlphabetPositions(char[] name, char[] alphabets)
        {
            List<int> pos = new List<int>();
            foreach (var character in name)
                pos.Add(Array.FindIndex(alphabets, alphabet => alphabet == character));
            return pos;
        }
        // Permutations
        public IEnumerable<IEnumerable<T>> GetPermutations<T>(List<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}