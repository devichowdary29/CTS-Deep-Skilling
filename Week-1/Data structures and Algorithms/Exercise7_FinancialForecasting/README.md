# Analysis: Financial Forecasting

## Understanding Recursive Algorithms
Recursion is a method where the solution to a problem depends on solutions to smaller instances of the same problem. A recursive function calls itself until it hits a "base case" that stops the recursion. It simplifies problems that can naturally be broken down into identical subproblems.

## Time Complexity Analysis
For our naive recursive algorithm (`PredictFutureValue`), it makes a single recursive call per year. Thus, the time complexity is **$O(n)$**, where $n$ is the number of years. The space complexity is also **$O(n)$** due to the call stack size.

## How to Optimize
In many recursive problems (like calculating the Fibonacci sequence), the naive approach involves redundant calculations causing exponential time complexity ($O(2^n)$). 
To avoid excessive computation, we can use **Memoization** (Top-Down Dynamic Programming). We store the results of expensive function calls in a cache (like a Dictionary) and return the cached result when the same inputs occur again.

*Note: For simple financial forecasting, an even better optimization is using a direct mathematical formula ($Value \times (1 + Rate)^{Years}$), which operates in $O(1)$ time complexity.*
