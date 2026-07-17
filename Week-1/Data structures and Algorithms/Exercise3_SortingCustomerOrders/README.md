# Analysis: Sorting Customer Orders

## Understanding Sorting Algorithms
Sorting algorithms arrange data in a specific order (ascending or descending). 
- **Bubble Sort**: A simple algorithm that repeatedly steps through the list, compares adjacent elements, and swaps them if they are in the wrong order.
- **Quick Sort**: A divide-and-conquer algorithm that selects a 'pivot' element and partitions the other elements into two sub-arrays according to whether they are less than or greater than the pivot.

## Time Complexity Comparison

### Bubble Sort
- **Best Case**: $O(n)$ (if the array is already sorted and optimized with a `swapped` flag).
- **Average & Worst Case**: $O(n^2)$. 

### Quick Sort
- **Best Case & Average Case**: $O(n \log n)$.
- **Worst Case**: $O(n^2)$ (occurs when the pivot chosen is always the smallest or largest element, e.g., already sorted array without random pivot selection).

## Why Quick Sort is Preferred
Quick Sort is generally preferred over Bubble Sort because its average time complexity is $O(n \log n)$, which makes it significantly faster for large datasets. Bubble Sort is highly inefficient for large arrays due to its $O(n^2)$ complexity, where the number of operations grows quadratically with the input size. Quick Sort also has excellent cache locality, making it very fast in practice.
