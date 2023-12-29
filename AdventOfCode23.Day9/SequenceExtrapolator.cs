namespace AdventOfCode23.Day9;

public class SequenceExtrapolator {

    List<List<int>> Histories { get; }

    public SequenceExtrapolator() {
        this.Histories = new List<List<int>>();
    }

    public void BuildHistories(List<int> sequence) {
        var differences = sequence;
        this.Histories.Clear();
        
        while (differences.Any(x => x != 0)) {
            this.Histories.Add(differences);
            differences = CalculateDifferences(differences);
        }
        this.Histories.Add(differences);
    }

    public int PredictNext() {
        // Ensure Histories is properly populated and isn't empty
        if (this.Histories == null || !this.Histories.Any()) {
            throw new InvalidOperationException("Histories is not populated. Make sure to call BuildHistories and pass the sequence list before calling PredictNext");
        }

        var nextValue = 0;

        // Iterate from last to first sequence in Histories
        for (var i = this.Histories.Count - 1; i >= 0; i--) {
            nextValue += this.Histories[i].Last(); // Add the last value of the sequence to nextValue
        }

        return nextValue;
    }

    public void PrintHistories() {
        var indentation = 0;
        foreach (var history in this.Histories) {
            Console.WriteLine(string.Concat(Enumerable.Repeat("   ", indentation)) + string.Join("  ", history));
            indentation += 1;
        }
    }

    List<int> CalculateDifferences(List<int> original) {
        var differences = new List<int>();
        for (var i = 1; i < original.Count; i++) {
            differences.Add(original[i] - original[i - 1]);
        }
        
        return differences;
    }
}