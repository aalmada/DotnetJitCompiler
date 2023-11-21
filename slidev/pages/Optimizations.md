---
layout: section
---

# Optimizations

---

```csharp
Console.WriteLine(Square(2));

static int Square(int value) 
    => value * value;
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKBuPIE4AKAZQEcBXbWJ0gSj4BuGnXJIABAEsAdhnHsuPGXIBu2ADYcYfcTXH7xAXgB84tZpjiAVGY1bh1IA=)

---

```asm {all|3}
Program.<Main>$(System.String[])
    L0000: sub rsp, 0x28
    L0004: mov ecx, 4
    L0009: call qword ptr [0x7ff9b68ffed0]
    L000f: nop
    L0010: add rsp, 0x28
    L0014: ret

Program.<<Main>$>g__Square|0_0(Int32)
    L0000: mov eax, ecx
    L0002: imul eax, ecx
    L0005: ret
```

---

```csharp
readonly record struct Vector2<T>(T X, T Y)
    where T : struct
{
    public static Vector2<T> operator +(in Vector2<T> left, in Vector2<T> right)
        => new(Scalar.Add(left.X, right.X), Scalar.Add(left.Y, right.Y));
}

static class Scalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Add<T>(T a, T b)
        where T : struct
    {
        if (typeof(T) == typeof(int))
            return (T)(object)(((int)(object)a) + (int)(object)b);

        if (typeof(T) == typeof(float))
            return (T)(object)(((float)(object)a) + (float)(object)b);

        if (typeof(T) == typeof(double))
            return (T)(object)(((double)(object)a) + (double)(object)b);

        throw new NotSupportedException();
    }
}
```

[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACY8gOgCUBXAOwwEt8YLAMIR8AB14AbGFADKMgG68wMXAG4a9BrIAW2KGIAy2YO258BG6pvIBOABRcYAdwYA1GGAzRSAHl48AHz25GgMpACUDADUDE6uHl4+/kH2AMxhKBERVjQA2gBSvBgA4jBOUMr2GACeYjAQAGb2ARjZALo0sNgAJhBckjUMsJBQPQy4GFAcXu6e3lB+ACrBSwwAGmFrAJoRNAwHDM46MjAMayATUzMYNADe+4fEaUzkSHNJi74rDBD1UNgFjEWlwPgtloEGNJGhgwgEwckfpUAOY6NqPQ6HAC8kPi9lkYGwkn0LAAgj0evZoRgWJthrxUTT1hEwgSiSTyZTqSxtmEUWiedkrABfGg2d5kbSE4lQe4Yhh5ACyMAwOggPQAkuJJPZlar1VqxJIAPJiPj9XBk5HI2C4XC8BQwDUDAIBZERTrUTHPV7vNac76rBjYLYMYB7L2Yw7HU7nBiXSbTLzyh6RqMMXiNBjVOoNZpLKJYrEMWr1JogtoR9Pp4gAdmzBfsEGAACt5hF7J3Wh3m22vBFsFFYhWe632+Hcmmo5ns6W8/YCwwiyXc+XGpIIIDsvLq0x6wvR322p37OvN8fe+3BzFs2et02x/2J2Kp5iZzmy/nC8W5+W+hxgGkbdX2rOsG0PdsT3/QCYAg/tr2HaCgIfI8ImfawQIOVUoAgVx4gYAA5CAMFkDgxDEaAMBgHoAFEEBUM1eH6ewcnlUVqHYoA===)

---

```asm
Vector2`1[[System.Int32, System.Private.CoreLib]].op_Addition(Vector2`1<Int32> ByRef, Vector2`1<Int32> ByRef)
    L0000: push rax
    L0001: mov eax, [rcx]
    L0003: add eax, [rdx]
    L0005: mov ecx, [rcx+4]
    L0008: add ecx, [rdx+4]
    L000b: mov [rsp], eax
    L000e: mov [rsp+4], ecx
    L0012: mov rax, [rsp]
    L0016: add rsp, 8
    L001a: ret
```

---
