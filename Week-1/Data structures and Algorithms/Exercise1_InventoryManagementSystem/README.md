# Analysis: Inventory Management System

## Understanding the Problem
Efficient data structures and algorithms are essential in handling large inventories because businesses need to store millions of records and frequently retrieve, update, and delete them. A slow search or update operation can cause severe performance bottlenecks in a live warehouse environment.

## Chosen Data Structure
We used a **Dictionary** (`HashMap` equivalent in C#) to store the products.
A Dictionary stores key-value pairs (Key: `ProductId`, Value: `Product` object). This is highly suitable for inventory systems where we frequently need to look up a product by its unique ID.

## Time Complexity Analysis
- **Add**: $O(1)$ on average. Hashing the key provides direct access to the memory bucket.
- **Update**: $O(1)$ on average. Looking up the product by ID and updating its fields is constant time.
- **Delete**: $O(1)$ on average. Finding and removing the entry by key is constant time.

## Optimization
Operations in a Hash Map are already optimized for $O(1)$ time complexity. However, to optimize further:
- Ensure the initial capacity of the Dictionary is large enough if the number of products is known beforehand, to prevent rehashing overhead as the collection grows.
