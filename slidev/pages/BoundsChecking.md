---
layout: section
---

# Bounds Checking

---

# Bounds Checking in .NET

- **Safety First:**
  - Ensures array and collection indices are within valid ranges.

- **Reliability Boost:**
  - Prevents overflows and index errors for more reliable code.

- **In .NET:**
  - Automatic bounds checking for arrays and collections.

- **Performance Note:**
  - Some overhead for added safety.

---

```csharp
using System;

var array = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
Console.WriteLine(Sum());

int Sum()
{
    var sum = 0;
    for(var index = 0; index < array.Length; index++)
        sum += array[index];
    return sum;
}
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKBoDdsoACRqbATyYF4mA7GAO5MAlrwwBtALpMA3k3Jp5ihUtUr1yzWqYBfANw1i5AJwAKAMoBXfKYCUtg7WqiMTKzds0ZNJr6YNmXGtuJgAGRz8mADNoUwCRXgATGAQQ8ITk1IAeFig2dgA6ABkYXgBzDAALPQyUgGo6z2pIyKD8JjqeVg5xUUzJCL9iAHYmNscdIA==)

---

```csharp {all|5,17-27}
internal class Program
{
    private struct <>c__DisplayClass0_0
    {
        public int[] array;
    }

    private static void <Main>$(string[] args)
    {
        int[] array = new int[12];
        RuntimeHelpers.InitializeArray(array, (RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/);
        <>c__DisplayClass0_0 <>c__DisplayClass0_ = default(<>c__DisplayClass0_0);
        <>c__DisplayClass0_.array = array;
        Console.WriteLine(<<Main>$>g__Sum|0_0(ref <>c__DisplayClass0_));
    }

    private static int <<Main>$>g__Sum|0_0(ref <>c__DisplayClass0_0 P_0)
    {
        int num = 0;
        int num2 = 0;
        while (num2 < P_0.array.Length)
        {
            num += P_0.array[num2];
            num2++;
        }
        return num;
    }
}
```

---

```asm {all|12,20}
Program.<<Main>$>g__Sum|0_0(<>c__DisplayClass0_0 ByRef)
    L0000: sub rsp, 0x28
    L0004: xor eax, eax
    L0006: xor edx, edx
    L0008: mov rcx, [rcx]
    L000b: cmp dword ptr [rcx+8], 0
    L000f: jle short L0038
    L0011: nop [rax]
    L0018: nop [rax+rax]
    L0020: mov r8, rcx
    L0023: cmp edx, [r8+8]
    L0027: jae short L003d
    L0029: mov r9d, edx
    L002c: add eax, [r8+r9*4+0x10]
    L0031: inc edx
    L0033: cmp [rcx+8], edx
    L0036: jg short L0020
    L0038: add rsp, 0x28
    L003c: ret
    L003d: call 0x00007ffa155397a0
    L0042: int3
```

---

```csharp
static T Sum<T>(ReadOnlySpan<T> source) 
    where T: IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity;
    for(var index = 0; index < source.Length; index++)
        sum += source[index];
    return sum;
}
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACY8gOgDkBXfGKASzFwBuGvQYBlABbYoABwAy2YCwBKHAHYZe3YdRoA3aQ2lRsATwYBeBmpgB3Brw0BtALoMA3g3JovP774D/IL8QwIYAXx1mAE4ACjEuAB5HDAA+WOMzAEosnRonACleDABxGBs+MFiMUxkYCAAzWJSclxFyJAYAFXEkrvTlGGwAEwB5NQAbUzEZbDVE/oZcCA4oMBgshhoGHYZbCR4YbpAGAEkAQWHh4t49GFPh8s0ahZ9+nwurm4g1UbqTDDQXCvbpvVI0dzbXYGKBLLiWbosS7XTR3B5PYqmHS7BgNaCxGEONSPBAIuiCIkkhiJJYrNYwFhycoAcwwEgpjhJAGouVkoTidrh4VyrMtVusnJyYAgXNjdsQAOxw/A6cJAA===)

---

```csharp {all|13-20}
internal class Program
{
    private static void <Main>$(string[] args)
    {
        int[] array = new int[12];
        RuntimeHelpers.InitializeArray(array, (RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/);
        Console.WriteLine(<<Main>$>g__Sum|0_0<int>(array));
    }

    internal static T <<Main>$>g__Sum|0_0<T>(ReadOnlySpan<T> source) 
        where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        T additiveIdentity = T.AdditiveIdentity;
        int num = 0;
        while (num < source.Length)
        {
            additiveIdentity += source[num];
            num++;
        }
        return additiveIdentity;
    }
}
```

---

```asm
Program.<<Main>$>g__Sum|0_0[[System.Int32, System.Private.CoreLib]](System.ReadOnlySpan`1<Int32>)
    L0000: mov rax, [rcx]
    L0003: mov edx, [rcx+8]
    L0006: xor ecx, ecx
    L0008: xor r8d, r8d
    L000b: test edx, edx
    L000d: jle short L001e
    L000f: mov r9d, r8d
    L0012: add ecx, [rax+r9*4]
    L0016: inc r8d
    L0019: cmp r8d, edx
    L001c: jl short L000f
    L001e: mov eax, ecx
    L0020: ret
```

---
layout: center
---

## foreach

---

```csharp
static T Sum<T>(ReadOnlySpan<T> source) 
    where T: IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity;
    foreach(ref readonly var item in source)
        sum += item;
    return sum;
}
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACY8gOgDkBXfGKASzFwBuGvQYBlABbYoABwAy2YCwBKHAHYZe3YdRoA3aQ2lRsATwYBeBmpgB3Brw0BtALoMA3g3JovP774D/IL8QwIYAXx1mAE4ACjEuAB5HDAA+WOMzAEosnRonACleDABxGBs+MFiMUxkYCAAzWJSclxFyJAYAFXEkrvTlGGwAEwB5NQAbUzEZbDVE/oZcCA4oMBgshhoGHYZbCR4YbpAGAEkAQWHh4t49GFPh8s0ahZ9+nwurm4g1UbqTDDQXCvbpvVI0dzbXYGKBLLiWbosS7XTR3B5PYqmHS7BgNaBDMASWKwBoMWAjH5TBgwhwYGD4BxqJYrNYbKE4na4eEAaisxXp2N2xAA7HD8DpwkA===)

---

```csharp {all|14}
internal class Program
{
    private static void <Main>$(string[] args)
    {
        int[] array = new int[12];
        RuntimeHelpers.InitializeArray(array, (RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/);
        Console.WriteLine(<<Main>$>g__Sum|0_0<int>(array));
    }

    internal static T <<Main>$>g__Sum|0_0<T>(ReadOnlySpan<T> source) 
        where T : IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
    {
        T additiveIdentity = T.AdditiveIdentity;
        ReadOnlySpan<T> readOnlySpan = source;
        int num = 0;
        while (num < readOnlySpan.Length)
        {
            additiveIdentity += readOnlySpan[num];
            num++;
        }
        return additiveIdentity;
    }
}

```

---
layout: center
---

## Unroll

---

```csharp
static T Sum<T>(ReadOnlySpan<T> source) 
    where T: IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity;
    
    var index = 0;
    var end = source.Length - (source.Length % 4);
    for(; index < end; index += 4)
    {
        sum += source[index];
        sum += source[index + 1];
        sum += source[index + 2];
        sum += source[index + 3];
    }
    
    for(; index < source.Length; index++)
        sum += source[index];
    
    return sum;
}
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACY8gOgDkBXfGKASzFwBuGvQYBlABbYoABwAy2YCwBKHAHYZe3YdRoA3aQ2lRsATwYBeBmpgB3Brw0BtALoMA3g3JovP774D/IL8QwIYAXx1mAE4ACjEuAB5HDAA+WOMzAEosnRonACleDABxGBs+MFiMUxkYCAAzWJSclxFyJAYAFXEkrvTlGGwAEwB5NQAbUzEZbDVE/oZcCA4oMBgshhoGHYZbCR4YbpAGAEkAQWHh4t49GFPh8s0ahZ9+nwurm4g1UbqTDDQXCvbpvVI0dzbXYGKBLLiWbosS7XTR3B5PYqmHS7LbUHEwhxqR4IBF0bHQwzlYYI5ardYsOTlADmGAkDDgDFitLWMAZzNZDAApAwULkoTsGtBYoJCcSGIkGFSZY45QBqKyi8UeLU43Dw9VLFY8pwqmAIFzknE7PX4BgG7nrE1Es12rwWnW7G12qwOmBOtUMUjuvFW636n1Gx2mkmqhgAZmDOPCWq1kqg0tlLoVvr5ahZEmVzoQqtVWQ9Ydt9sjfujid2WuIAHY4fgdMnqEA=)

---

```csharp
static T Sum<T>(ReadOnlySpan<T> source) 
    where T: IAdditiveIdentity<T, T>, IAdditionOperators<T, T, T>
{
    var sum = T.AdditiveIdentity;
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
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACY8gOgDkBXfGKASzFwBuGvQYBlABbYoABwAy2YCwBKHAHYZe3YbUbMV6zdxYBhCPhm8ANjzE8AbvxhCRe1qo1aYLAJIaeEDJ2UI5gzjo09tIM0lDYAJ4MALwMajAA7gy8GgDaALoMAN4M5GglZaXlVZU1FXXVDAC+OswAnAAUYlwAPNkYAHztsQkAlCMR1DkAUrwYAOIwaXxg7RjxMjAQAGbtfWN5IuRIDAAq4j0ng8ow2AAmAPJqVvFiMthq3ZcMuBAcUGEjBg0BgghjpCQ8GCnEAMHwAQVut1mvHsMB8t0WmjWnzKlzK8MRyIganuGziGGguBxp1x/RohWBoKiUG+XGSpxYCKRmlR6Mxs3iOlBDFgWwYzO+v3+MGuYpSooYAFkYPhoPFFdJcFIrCwFhhZZC1GF2j8/gChaDGSCJdkMQh2XQLdbootbuzTdKWHJFgBzDASBhwBgmqVhL2+/0MACkDBQ4ytDC20HagiyajtDG6DFdqdtMHtAGoUnGEwzqMLhbg2UWGABVNS4bBbbxc9oKj1hWVlPMIePlisgqv4Bg1+uN5ucxFtmBijsymfd9P5kclPsDwfVlJjpstqft0PzraLjMFhikNfrocjrcNneT27T2cHrtpk8MADMF5BjQTCaTUBTV9lyzOdwzUP0JFzJcEALAsRgTStNzrW8J1bfczUPY98y/IF+xBYgAHZWXwHQf2oIA=)

---
layout: center
---

## Copy

---

```csharp
static void Copy<T>(ReadOnlySpan<T> source, Span<T> destination)
{
    for(var index = 0; index < source.Length; index++)
        destination[index] = source[index];
}
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACAZQAtsoAHAGW2ADoASgFcAdhgCW+GAG4aNAG7sGuCEKhgYDALwMRMAO4NxYgNoBdBgG8GdNA3J3SdgMx30DAKx2kdgOx2ADjsATjtyRgBfWWpFKAYAExhcCRFsCQgRbV0DI1MVNQ0+LhgRAHMMFjNogGEIDgBPAB5jDAA+AAp89Rg7ROTjNPEMgEpogDNoGGwwFnbYowwYfFyEpJTBkZoGbYZicmD28UX8UblqEwApI4BxEpgocTB2jHqOGAgxw7Fh4bMaPaQuxQDFqDUaABUOgIpvEAPIiAA29SYHGwIghrWUqm6dhRaIxq36qXSImGNEsWx2EygcyUxkSCCydGkuQZDEaWIKMCKJXKLBZ9JgCAA1MKydQdpLCesSSZBQgLDouho5SIGVUaBEgA=)

---

```asm {all|11,21}
Program.<<Main>$>g__Copy|0_0[[System.Int32, System.Private.CoreLib]](System.ReadOnlySpan`1<Int32>, System.Span`1<Int32>)
    L0000: sub rsp, 0x28
    L0004: mov rax, [rcx]
    L0007: mov ecx, [rcx+8]
    L000a: xor r8d, r8d
    L000d: test ecx, ecx
    L000f: jle short L003c
    L0011: nop [rax]
    L0018: nop [rax+rax]
    L0020: cmp r8d, [rdx+8]
    L0024: jae short L0041
    L0026: mov r9, [rdx]
    L0029: mov r10d, r8d
    L002c: mov r11d, [rax+r10*4]
    L0030: mov [r9+r10*4], r11d
    L0034: inc r8d
    L0037: cmp r8d, ecx
    L003a: jl short L0020
    L003c: add rsp, 0x28
    L0040: ret
    L0041: call 0x00007ffa155397a0
    L0046: int3
```

---

```csharp {all|3}
static void Copy<T>(ReadOnlySpan<T> source, Span<T> destination)
{
    for(var index = 0; index < source.Length && index < destination.Length; index++)
        destination[index] = source[index];
}
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACAZQAtsoAHAGW2ADoASgFcAdhgCW+GAG4aNAG7sGuCEKhgYDALwMRMAO4NxYgNoBdBgG8GdNA3J3SdgMx30DAKx2kdgOx2ADjsATjtyRgBfWWpFKAYAExhcCRFsCQgRbV0DI1MVNQ0+LhgRAHMMFjNogGEIDgBPAB5jDAA+AAp89Rg7ROTjNPEMgEpogDNoGGwwFnbYowwYfFyEpJTBkZoGbYZicmD28UX8UblqEwApI4BxEpgocTB2jHqOGAgxw7Fh4bMaPaQuxQDFqDUaABUOgIpvEAPIiAA29SYHGwIghrWUqm6dhRaIxq36qXSImGNEsWx2EygcyUxkSCCydGkuQZDEaWIKMCKJXKLAYADIBayYIyOX11iSeWUKiz6aKANQKsnUHZqwmSoYiEzyhAWHRdDQ6kQMqo0CJAA)

---

```asm
Program.<<Main>$>g__Copy|0_0[[System.Int32, System.Private.CoreLib]](System.ReadOnlySpan`1<Int32>, System.Span`1<Int32>)
    L0000: mov rax, [rdx]
    L0003: mov edx, [rdx+8]
    L0006: mov r8, [rcx]
    L0009: mov ecx, [rcx+8]
    L000c: xor r9d, r9d
    L000f: jmp short L001f
    L0011: mov r10d, r9d
    L0014: mov r11d, [r8+r10*4]
    L0018: mov [rax+r10*4], r11d
    L001c: inc r9d
    L001f: cmp r9d, ecx
    L0022: jge short L0029
    L0024: cmp r9d, edx
    L0027: jl short L0011
    L0029: ret
```

---

# Bounds Checking Benchmarks

|           Method |   Count |           Mean |         Error |        StdDev |        Ratio | RatioSD | 
|----------------- |-------- |---------------:|--------------:|--------------:|-------------:|--------:|
| **ExternalVariable** |      **10** |       **7.844 ns** |     **0.0103 ns** |     **0.0092 ns** |     **baseline** |        **** |
|    LocalVariable |      10 |       3.877 ns |     0.0072 ns |     0.0068 ns | 2.02x faster |   0.00x |
|          ForEach |      10 |       3.874 ns |     0.0083 ns |     0.0070 ns | 2.02x faster |   0.00x |
|         Unrolled |      10 |       2.947 ns |     0.0145 ns |     0.0121 ns | 2.66x faster |   0.01x |

---

# Bounds Checking Benchmarks

|           Method |   Count |           Mean |         Error |        StdDev |        Ratio | RatioSD | 
|----------------- |-------- |---------------:|--------------:|--------------:|-------------:|--------:|
| **ExternalVariable** |    **1000** |     **437.692 ns** |     **1.1091 ns** |     **1.0375 ns** |     **baseline** |        **** |
|    LocalVariable |    1000 |     321.146 ns |     0.3797 ns |     0.3552 ns | 1.36x faster |   0.00x |
|          ForEach |    1000 |     321.288 ns |     0.6956 ns |     0.6166 ns | 1.36x faster |   0.00x |
|         Unrolled |    1000 |     296.343 ns |     0.1148 ns |     0.0896 ns | 1.48x faster |   0.00x |
