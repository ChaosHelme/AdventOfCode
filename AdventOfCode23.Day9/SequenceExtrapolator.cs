namespace AdventOfCode23.Day9;

public class SequenceExtrapolator {
    List<int> CalculateDifferences(List<int> original) {
        var differences = new List<int>();
        for (var i = 1; i < original.Count; i++) {
            differences.Add(original[i] - original[i - 1]);
        }
        
        return differences;
    }

    public int PredictNext(List<int> sequence) {
        var differences = CalculateDifferences(sequence);

        if (differences.All(x => x == 0)) {
            return sequence.Last();
        }

        return sequence.Last() + PredictNext(differences);
    }

    public void PrintDifferences(List<int> sequence) {
        PrintDifferencesRecursive(sequence, 0);
    }

    void PrintDifferencesRecursive(List<int> sequence, int indentation) {
        while (true) {
            if (sequence.All(x => x == 0)) {
                Console.WriteLine(string.Concat(Enumerable.Repeat("   ", indentation)) + string.Join("  ", sequence));
                return;
            }

            // Print the current sequence with proper indentation
            Console.WriteLine(string.Concat(Enumerable.Repeat("   ", indentation)) + string.Join("  ", sequence));

            var differences = CalculateDifferences(sequence);
            sequence = differences;
            indentation += 1;
        }
    }
}