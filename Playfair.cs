using System.Text;

namespace Playfair
{
    internal static class Playfair
    { 
        public static KeyMatrix GenerateKey(string word, char omittedLetter, char omittedLetterPair)
        {
            omittedLetter = char.ToLower(omittedLetter);
            omittedLetterPair = char.ToLower(omittedLetterPair);

            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string keystring = "";
            foreach (char c in word) if (!keystring.Contains(c)) keystring += c;

            alphabet = alphabet.Remove(alphabet.IndexOf(omittedLetter), 1);
            
            foreach (char c in alphabet) if (!keystring.Contains(c)) keystring += c;

            return new KeyMatrix(keystring, omittedLetter, omittedLetterPair);
        }

        public static string Encrypt(string input, KeyMatrix key) => TransformText(input, key, forward: true);
        public static string Decrypt(string input, KeyMatrix key) => TransformText(input, key, forward: false);

        private static string TransformText(string inputText, KeyMatrix key, bool forward)
        {
            int step = forward ? 1 : -1;

            char redundancyChar = key.omittedLetter != 'x' ? 'x' : key.omittedLetterPair;

            string normalisedInput = NormaliseInput(inputText, redundancyChar);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < normalisedInput.Length; i += 2)
            {

                char c1 = normalisedInput[i] == key.omittedLetter ? key.omittedLetterPair : normalisedInput[i];
                char c2 = normalisedInput[i + 1] == key.omittedLetter ? key.omittedLetterPair : normalisedInput[i + 1];

                if (c1 == c2) c2 = redundancyChar;

                var pos1 = key.FindLetter(c1);
                var pos2 = key.FindLetter(c2);

                if (pos1.row == pos2.row)
                {
                    c1 = key[pos1.row, pos1.col + step];
                    c2 = key[pos2.row, pos2.col + step];
                }
                else if (pos1.col == pos2.col)
                {
                    c1 = key[pos1.row + step, pos1.col];
                    c2 = key[pos2.row + step, pos2.col];
                }
                else
                {
                    c1 = key[pos1.row, pos2.col];
                    c2 = key[pos2.row, pos1.col];
                }

                output.Append(c1);
                output.Append(c2);
            }

            return output.ToString();
        }
        
        private static string NormaliseInput(string input, char redundancyChar)
        {
            string normalised = new string(input.Where(Char.IsLetter).ToArray()).ToLower();
            if (normalised.Length % 2 == 1) normalised += redundancyChar;
            
            return normalised;
        }
    }
}