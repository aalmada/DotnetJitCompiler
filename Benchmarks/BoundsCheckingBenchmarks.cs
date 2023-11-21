using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class BoundsCheckingBenchmarks
{
    int[]? array;

    [Params(10, 1_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        array = Enumerable.Range(0, Count).ToArray();
    }

    [Benchmark(Baseline = true)]
    public int ExternalVariable()
    {
        var sum = 0;
        for (var index = 0; index < array!.Length; index++)
            sum += array![index];
        return sum;
    }   

    [Benchmark]
    public int LocalVariable()
    {
        var sum = 0;
        var source = array!;
        for (var index = 0; index < source.Length; index++)
            sum += source[index];
        return sum;
    }   

    [Benchmark]
    public int ForEach()
    {
        var sum = 0;
        foreach (var item in array!)
            sum += item;
        return sum;
    }   

    [Benchmark]
    public int Unrolled()
    {
        var source = array!.AsSpan();
        var sum = 0;
        ref var sourceRef = ref MemoryMarshal.GetReference(source);
        
        var index = 0;
        var end = source.Length - (source.Length % 4);
        for(; index < end; index += 4)
        {
            sum += Unsafe.Add(ref sourceRef, index);
            sum += Unsafe.Add(ref sourceRef, index + 1);
            sum += Unsafe.Add(ref sourceRef, index + 2);
            sum += Unsafe.Add(ref sourceRef, index + 3);
        }
        
        for(; index < source.Length; index++)
            sum += Unsafe.Add(ref sourceRef, index);
        
        return sum;
    }   
}
