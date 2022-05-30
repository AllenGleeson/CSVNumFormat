using System.IO;
using System.Text.RegularExpressions;

namespace CSV_NumberFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter the location of file you want to read from and the file you want to write to eg. @"C:\Users\User\support_engineer_assessment_files\deliver_dot-com_niches.csv"
            using var reader = new StreamReader(@"C:{readfileloction}.csv");
            using var writer = new StreamWriter(@"C:{outfileloction}.csv");

            while (!reader.EndOfStream)
            {
                // Reads in the line and splits by comma using regex to ignore qoutes
                var line = reader.ReadLine();
                var valuesSplitByComma = Regex.Split(line, ",(?=(?:[^']*'[^']*')*[^']*$)");
                for (int i = 0; i < valuesSplitByComma.Length; i++)
                {
                    Regex phoneNumberRegex = new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
                    Match match = phoneNumberRegex.Match(valuesSplitByComma[i]);
                    // Checks if the number is a phone number
                    if (match.Success)
                    {
                        // Adds +353 if the number doesn't have it already
                        if (valuesSplitByComma[i].StartsWith("08") && valuesSplitByComma[i].Length == 10)
                        {
                            var formattedIrishvalue = "+353" + valuesSplitByComma[i].Substring(1);
                            valuesSplitByComma[i] = formattedIrishvalue;
                        }
                    }
                    // Checks if its the last index of the line to determine wether to add a comma to the end or not
                    if (i + 1 == valuesSplitByComma.Length)
                    {
                        writer.Write(valuesSplitByComma[i]);
                    }
                    else
                    {
                        writer.Write(valuesSplitByComma[i] + ",");
                    }

                }


                // Starts a new line
                writer.WriteLine();
            }
        }
    }
}
