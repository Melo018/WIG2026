
using System;
using System.Collections.Generic;

public static class MatrixLoader
{
    public static Dictionary<string, List<string>> Load(string csv)
    {
        string[] lines = csv.Split('\n');
        string[] headers = lines[0].TrimEnd('\r').Split(';');
        HashSet<string> known = new HashSet<string>(IngredientData.Ingredients);
        Dictionary<string, Dictionary<string, List<string>>> matrix = new Dictionary<string, Dictionary<string, List<string>>>();
        Dictionary<string, List<string>> associations = new Dictionary<string, List<string>>();

        if (headers.Length != known.Count + 1)
            throw new Exception("Matrix.csv : nombre de colonnes invalide.");

        for (int j = 1; j < headers.Length; j++)
            if (!known.Contains(headers[j].Trim()))
                throw new Exception("Matrix.csv : ingrédient inconnu ou en-tête invalide : " + headers[j]);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] cells = line.Split(';');
            if (cells.Length != headers.Length)
                throw new Exception("Matrix.csv : nombre de colonnes invalide ligne " + (i + 1));

            string row = cells[0].Trim();
            if (!known.Contains(row) || matrix.ContainsKey(row))
                throw new Exception("Matrix.csv : ingrédient inconnu ou ligne en double : " + row);

            matrix[row] = new Dictionary<string, List<string>>();

            for (int j = 1; j < cells.Length; j++)
            {
                string column = headers[j].Trim();
                string value = cells[j].Trim();

                if (row == column)
                {
                    if (value.Length > 0)
                        throw new Exception("Matrix.csv : diagonale non vide : " + row);
                    continue;
                }

                if (value.Length == 0)
                    throw new Exception("Association manquante : " + row + " / " + column);

                List<string> tags = new List<string>();
                foreach (string rawTag in value.Split('+'))
                {
                    string tag = rawTag.Trim();
                    if (tag.Length == 0 || tags.Contains(tag))
                        throw new Exception("Tag vide ou répété : " + row + " / " + column);
                    tags.Add(tag);
                }

                matrix[row][column] = tags;
            }
        }

        if (matrix.Count != known.Count)
            throw new Exception("Matrix.csv : il manque une ou plusieurs lignes d'ingrédients.");

        foreach (string a in IngredientData.Ingredients)
        {
            foreach (string b in IngredientData.Ingredients)
            {
                if (a == b) continue;
                if (!matrix.ContainsKey(a) || !matrix[a].ContainsKey(b))
                    throw new Exception("Association manquante : " + a + " / " + b);

                List<string> tagsA = matrix[a][b];
                List<string> tagsB = matrix[b][a];

                if (tagsB == null || !new HashSet<string>(tagsA).SetEquals(tagsB))
                    throw new Exception("Association asymétrique : " + a + " / " + b);

                string key = GetKey(a, b);
                if (!associations.ContainsKey(key))
                    associations.Add(key, tagsA);
            }
        }

        return associations;
    }

    public static string GetKey(string a, string b)
    {
        return string.CompareOrdinal(a, b) < 0 ? a + "|" + b : b + "|" + a;
    }
}