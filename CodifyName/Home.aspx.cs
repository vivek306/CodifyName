using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodifyName
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        // Initialize and start the Encoding/Decoding
        public string CodifyName(string input, string name, bool EncodeMe)
        {
            char[] inputChar = input.ToArray();
            char[] nameChar = name.Replace(@" ", "").Distinct().ToArray();
            char[] alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,!£$%^&*-_=+[]{}#~'@/?<>|1234567890(); ".ToCharArray();
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


        protected void DecodeButton_Click(object sender, EventArgs e)
        {
            string input = q4.Value;
            string name = q1.Value;
            q4.Value = CodifyName(input, name, false);
        }

        protected void EncodeButton_Click(object sender, EventArgs e)
        {
            string input = q4.Value;
            string name = q1.Value;
            q4.Value = CodifyName(input, name, true);
        }

        private void Reset(bool message, bool name, bool radio)
        {
            if (message)
            {
                messageLabel.InnerHtml = "Message";
                messageLabel.Style.Add("color", "#3B3F45");
            }
            if (name)
            {
                nameLabel.InnerHtml = "What's your name?";
                nameLabel.Style.Add("color", "#3B3F45");
            }
            if (radio)
            {
                radioLabel.InnerHtml = "What do you want to do?";
                radioLabel.Style.Add("color", "#3B3F45");
            }
            TextRadio.Value = "";
        }


        protected void Button_Click(object sender, EventArgs e)
        {
            try
            {
                string radio = TextRadio.Value;
                string input = q4.Value;
                string name = q1.Value.ToLower() + q1.Value.ToUpper();
                if (!string.IsNullOrEmpty(radio))
                {
                    if (name.ToCharArray().Distinct().Count() > 4)
                    {
                        if (radio == "encode")
                            q4.Value = CodifyName(input, name, true);
                        else if (radio == "decode")
                            q4.Value = CodifyName(input, name, false);
                        else
                            q4.Value = q4.Value;
                        Reset(true, true, true);
                    }
                    else
                    {
                        nameLabel.InnerHtml = "Your name should have atleast 5 distinct characters";
                        nameLabel.Style.Add("color", "red");
                        Reset(true, false, true);
                    }
                }
                else
                {
                    radioLabel.InnerHtml = "Please select one of the two";
                    radioLabel.Style.Add("color", "red");
                    Reset(true, true, false);
                }
            }
            catch (Exception)
            {
                messageLabel.InnerHtml = "Not valid message";
                messageLabel.Style.Add("color", "red");
                TextRadio.Value = "";
            }

        }


    }
}