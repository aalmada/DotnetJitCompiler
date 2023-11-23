---
layout: section
---

# Single Instruction, Multiple Data (SIMD)

---

# Single Instruction, Multiple Data (SIMD)

- **Parallel Processing Essence:**
  - A technique facilitating the execution of a single instruction on multiple data elements concurrently.

- **Efficient Data Handling:**
  - Empowers efficient and high-performance execution of repetitive operations, ideal for tasks like vector and matrix computations.

- **Hardware Support:**
  - Leverages specialized hardware or instruction sets present in contemporary CPUs for optimized processing.

---

```csharp
readonly record struct Vector2<T>(T X, T Y)
    where T : struct, IAdditionOperators<T, T, T>
{
    public static Vector2<T> operator +(Vector2<T> left, Vector2<T> right)
        => new(left.X + right.X, left.Y + right.Y);
}
```


[SharpLab](https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AXEBDAzgWwB8ABAJgEYBYAKGIAYACY8gOgDkBXfGKASzFwBuGvQYBlABbYoABwAy2YCwBKHAHYZe3YdRHkAnAAo1MAO4MAajDAZopADy8NAPkPk0DUgEoGAagYm5lY2do4uhgDMHiheXjo0ANoAUrwYAOIwJnxghhgAnjIwEABmhk4YsQC6iSnpmTz8uQVFpcUANhDYFV7V1LDYACYQam15DLCQUAMMuBhQHDaW1rZQDgAqrmsMABoeWwCaXjQMJwymEjwwDFsgM3MLGB4AkgCCAwOpvMMA8oVQXdBcPY1ns9s4aABvY6nYgRJjkJBLEKrYHOBgQP4AqB+QzBFbrNFtGDFR5I/Go8a8ADmEgq0NOpwAvGjAoYiSSWNs/JSaRhOR52Xz9ty+LyWIcdABfIA)


---
layout: two-cols
---

# int

```asm
Vector2`1[[System.Int32, System.Private.CoreLib]].op_Addition(Vector2`1<Int32>, Vector2`1<Int32>)
    L0000: push esi
    L0001: mov eax, [esp+0x10]
    L0005: add eax, [esp+8]
    L0009: mov edx, [esp+0x14]
    L000d: mov esi, [esp+0xc]
    L0011: add edx, esi
    L0013: mov [ecx], eax
    L0015: mov [ecx+4], edx
    L0018: pop esi
    L0019: ret 0x10
```

::right::

# float

```asm
Vector2`1[[System.Single, System.Private.CoreLib]].op_Addition(Vector2`1<Single>, Vector2`1<Single>)
    L0000: push rax
    L0001: vzeroupper
    L0004: mov [rsp+0x10], rcx
    L0009: mov [rsp+0x18], rdx
    L000e: vmovss xmm0, [rsp+0x10]
    L0014: vaddss xmm0, xmm0, [rsp+0x18]
    L001a: vmovss xmm1, [rsp+0x14]
    L0020: vmovss xmm2, [rsp+0x1c]
    L0026: vmovss [rsp], xmm0
    L002b: vaddss xmm0, xmm1, xmm2
    L002f: vmovss [rsp+4], xmm0
    L0035: mov rax, [rsp]
    L0039: add rsp, 8
    L003d: ret
```
