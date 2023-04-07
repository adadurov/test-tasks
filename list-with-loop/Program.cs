var emptyList = default(Node);
var singleNode = new Node();
var listOfTenNoLoops = BuildLinkedList(9);
var listOfTenWithLoop = BuildListWithLoop(10, 3);
var singleNodeWithLoop = BuildListWithLoop(1, 0);

Console.WriteLine($"HasLoop({nameof(emptyList)}) is {HasLoop(emptyList)}");
Console.WriteLine($"HasLoop({nameof(singleNode)}) is {HasLoop(singleNode)}");
Console.WriteLine($"HasLoop({nameof(listOfTenNoLoops)}) is {HasLoop(listOfTenNoLoops)}");
Console.WriteLine($"HasLoop({nameof(listOfTenWithLoop)}) is {HasLoop(listOfTenWithLoop)}");
Console.WriteLine($"HasLoop({nameof(singleNodeWithLoop)}) is {HasLoop(singleNodeWithLoop)}");

bool HasLoop(Node list)
{
    var slow = list;
    var fast = list?.Next;
    
    while (slow is not null && fast is not null)
    {
        if (object.ReferenceEquals(fast, slow))
        {
            return true;
        }
        fast = fast.Next?.Next;
        slow = slow.Next;
    }
    return false;
}

Node BuildLinkedList(int count)
{
    if (count < 1) throw new ArgumentException();

    var nodes = Enumerable
                .Range(0, count)
                .Select(i => new Node { Value = i })
                .ToArray();
    for (int i = 0; i < count - 1; i++)
    {
        nodes[i].AppendWith(nodes[i + 1]);
    }
    return nodes[0];
}

Node BuildListWithLoop(int count, int zeroBasedTargetIndex)
{
    var list = BuildLinkedList(count);
    var last = TrySkip(list, count - 1);
    var target = TrySkip(list, zeroBasedTargetIndex);

    last.AppendWith(target);

    return list;

    Node TrySkip(Node list, int offset)
    {
        while (list?.Next is not null && offset > 0) { list = list.Next; offset--; }
        return list;
    }
}

class Node
{
    public int Value { get; set; }
    public Node Next { get; private set; }
    public Node AppendWith(Node next) { Next = next; return next; }
}
