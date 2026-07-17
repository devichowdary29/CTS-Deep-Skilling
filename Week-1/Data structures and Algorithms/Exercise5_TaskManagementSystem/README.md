# Analysis: Task Management System

## Understanding Linked Lists
A linked list is a linear data structure where elements (nodes) are not stored in contiguous memory locations. Instead, each node points to the next node.
- **Singly Linked List**: Each node contains data and a pointer to the *next* node.
- **Doubly Linked List**: Each node contains data, a pointer to the *next* node, and a pointer to the *previous* node, allowing traversal in both directions.

## Time Complexity Analysis
- **Add**: $O(n)$ if adding to the end (since we have to traverse to the end), or $O(1)$ if adding to the head or if we maintain a tail pointer.
- **Search**: $O(n)$, as we must traverse the list node by node from the head to find the element.
- **Traverse**: $O(n)$, visiting every node once.
- **Delete**: $O(n)$ to find the node to delete, but $O(1)$ to actually remove it (by just changing the `Next` pointer of the previous node).

## Advantages of Linked Lists over Arrays
- **Dynamic Size**: Linked lists can grow and shrink dynamically. You don't need to define a maximum size upfront.
- **Efficient Insertion/Deletion**: Once the target node is found, inserting or deleting nodes is $O(1)$ because it only involves updating pointers. Arrays, on the other hand, require shifting elements ($O(n)$) when inserting or deleting in the middle.
