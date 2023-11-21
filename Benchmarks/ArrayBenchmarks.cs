using BenchmarkDotNet.Attributes;
using System.Collections.Immutable;
using System.Collections.Frozen;

public class ArrayBenchmarks
{
    int[]? array;
    List<int>? list;
    ImmutableArray<int>? immutableArray;

    [Params(10, 1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var range = Enumerable.Range(0, Count);
        array = range.ToArray();
        list = range.ToList();
        immutableArray = range.ToImmutableArray();
    }

    [Benchmark(Baseline = true)]
    public int List()
    {
        var sum = 0;
        foreach (var item in list!)
            sum += item;
        return sum;
    }   

    [Benchmark]
    public int Array()
    {
        var sum = 0;
        foreach (var item in array!)
            sum += item;
        return sum;
    }   

    [Benchmark]
    public int ImmutableArray()
    {
        var sum = 0;
        foreach (var item in immutableArray!)
            sum += item;
        return sum;
    }   
}
