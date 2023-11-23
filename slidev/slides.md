---
highlighter: shiki
layout: cover
background: "https://source.unsplash.com/1600x900/?nature,water"
---

# JIT Compilation in .NET

---
layout: intro
---

**Antão Almada**<br>
Principal Engineer<br>
Tech R&D<br>
Farfetch

---

# Programming Languages

- Formal systems enabling humans to communicate instructions to computers for task execution.

---

# Machine Code

- Low-level programming language, binary code directly executable by a computer's CPU.

---

# Compiler/Interpreter

- Software tools with the capability to translate high-level programming code into machine code.

---

# Compiler

- **Build-time Operation:**
  - Executes on the build machine before deployment.

- **Execution Architecture Awareness:**
  - Limited knowledge of the execution machine architecture (e.g., x86, x64, ARM, RISC-V).

- **Independent Executables:**
  - Generates a standalone executable for deployment.

---

# Interpreter

- **Runtime Operation:**
  - Executes on the execution machine during runtime.

- **Startup Consideration:**
  - Significantly increases startup time due to interpretation.

- **In-Depth Machine Insight:**
  - Possesses detailed knowledge of the machine architecture.

- **Time Constraints on Optimization:**
  - Opportunities for optimization are limited by the time-intensive nature of the process.

---
layout: two-cols-header
---

# .NET Common Language Infrastructure (CLI)

Describes executable code and a runtime environment that allows multiple high-level languages to be used on different computer platforms without being rewritten for specific architectures.

::left::
### Compiler 

- **Code Translation:**
  - Translates high-level programming code into Intermediate Language (IL).
  - Existing compilers: C#, F#, VB.NET, [more](https://en.wikipedia.org/wiki/List_of_CLI_languages)

- **Build-time Execution:**
  - Operates on the build machine before deployment.

::right::
### JIT (Just-In-Time) Compiler

- **Dynamic Translation:**
  - Translates IL into machine code on-the-fly.
  - x86, x64, arm, arm64, ...

- **Runtime Operation:**
  - Operates on the execution machine during runtime.

---

## C#
```csharp
static int Add(int left, int right)
    => left + right;
```

## IL
```
.method assembly hidebysig static int32 'Add' (int32 left, int32 right) cil managed 
{
    ldarg.0
    ldarg.1
    add
    ret
}
```

## ASM
```asm
lea eax, [rcx+rdx]
ret
```

---

# .NET Compilation: Advantages

- **Multilingual Support:**
  - Enables compatibility with multiple programming languages.

- **Cross-Language Utilization:**
  - Facilitates seamless use of libraries across different languages.

- **Efficient Deployment:**
  - Streamlines deployment by consolidating code into a single binary.

- **Performance Optimization:**
  - Enhances execution speed by optimizing code for the target machine architecture.

---

# JIT Compiler: Balancing Startup and Throughput

- **Optimization Dynamics:**
  - Optimizations consume time, leading to increased startup duration.

- **Tradeoff Consideration:**
  - Achieving better throughput often extends startup time, while prioritizing quicker startup may reduce overall throughput. 

---

# JIT Compiler: Usage Scenarios

- **Service Deployment:**
  - For services that start only once, several extra seconds of startup time are usually inconsequential.

- **Console Applications:**
  - In console applications with quick computations and swift exits, prioritizing minimized startup time is crucial.

---

# JIT Compiler: Early Versions

- **Initial Compilation Approach:**
  - In the early versions, a .NET method was compiled solely upon its first invocation.

- **Optimization Challenges:**
  - Optimization, a resource-intensive process, presented challenges due to its cost.

- **Limited Method Invocations:**
  - The majority of methods in an application were invoked only once or a few times.

---

# JIT Compiler: Tiered Compilation

- **Introduction:**
  - Debuted in .NET Core 3.0.

- **Adaptive Compilation:**
  - Utilizes multiple compilation rounds for the same code.

- **Swift Initial Compilation (Tier-0):**
  - Prioritizes speed in the first compilation, generating fast yet relatively unoptimized assembly code.

- **Instrumentation Integration:**
  - Includes instrumentation in the assembly to monitor method call frequencies.

- **Strategic Recompilation (Tier-1):**
  - Recompiles frequently called methods, applying comprehensive optimizations for superior performance.

- **Loop Optimization Emphasis:**
  - Methods executing in loops directly enter tier-1 due to their potential for substantial work.

---

# JIT Compiler: On-Stack Replacement (OSR)

- **Introduction:**
  - Debuted in .NET 7.

- **Loop Optimization Focus:**
  - Identifies methods executing repetitive work in a loop but called infrequently.

- **Recompilation Trigger:**
  - After a specific number of loop iterations, the method undergoes recompilation.

- **Efficient Runtime Replacement:**
  - Employs On-Stack Replacement (OSR) for dynamic runtime replacement, enhancing overall efficiency.

---

# JIT Compiler: Dynamic Profile-Guided Optimization (Dynamic PGO)

- **Introduction:**
  - Introduced in .NET 6, with default activation beginning in .NET 8.

- **Cost of Instrumentation:**
  - Instrumentation incurs a cost, leading to the introduction of a new uninstrumented tier.

- **Instrumentation Activation:**
  - After a specific number of calls, the method undergoes instrumentation.

- **Optimization Trigger:**
  - Once instrumented, the method is optimized based on data collected from prior calls after a specific number of subsequent calls.

---
src: ./pages/Optimizations.md
---
---
src: ./pages/BoundsChecking.md
---
--- 
src: ./pages/Simd.md
---
--- 
src: ./pages/Linq.md
---
--- 

# Conclusions

- **Versatile Compilation:**
  - .NET offers diverse compilation approaches to meet varying application needs.

- **Adaptive Performance:**
  - The JIT Compiler balances startup and throughput, adapting to usage scenarios.

- **Tiered Compilation Dynamics:**
  - JIT tiered compilation, including OSR, optimizes code based on runtime behavior.

- **Efficient SIMD Integration:**
  - Single Instruction, Multiple Data (SIMD) enhances parallel execution for data-intensive tasks.

- **Continuous Evolution:**
  - .NET's compilation techniques evolve for optimized performance on modern hardware.

- **Tailored Efficiency:**
  - .NET's compilation tools provide tailored solutions for efficiency and adaptability.

---

## References

- https://devblogs.microsoft.com/dotnet/ryujit-the-next-generation-jit-compiler-for-net/
- https://devblogs.microsoft.com/dotnet/the-ryujit-transition-is-complete/
- https://devblogs.microsoft.com/dotnet/performance_improvements_in_net_7/#jit
- https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/

---
layout: center
---

![aalmada.github.io/DotnetJitCompiler/](/images/qrcode.png)

aalmada.github.io/DotnetJitCompiler/
