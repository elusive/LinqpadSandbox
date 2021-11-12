<Query Kind="Program" />

void Main()
{
	var items = new[] {
		new MyItem(1, 0),
		new MyItem(2, 3),
		new MyItem(3, 1),
		new MyItem(4, 0),
	};
	
	var result = HierarchyHelper.GetHierarchy(items.ToList());
	result.Dump("Hierarchy Result");
}

// You can define other methods, fields, classes and namespaces here

public class MyItem
{
	public MyItem(int id, int parentId) {
		Id = id;
		ParentId = parentId;
		Children = new List<MyItem>();
	}
	public int Id;
	public int ParentId;
	public List<MyItem> Children;
}

public static class HierarchyHelper
{
	public static List<MyItem> GetHierarchy(List<MyItem> itemsList)
	{
		// find root
		var rootItems = itemsList.Where(i => i.ParentId == 0).ToList();

		for (var x = 0; x < rootItems.Count(); x++)
		{
			var ri = rootItems[x];
			ri.Children = HierarchyHelper.FindChildren(ri, itemsList);
		}
		
		return rootItems;
	}
	
	private static List<MyItem> FindChildren(MyItem item, List<MyItem> itemsList)
	{
		// loop thru root items and find children
		var children = itemsList.Where(i => item.Id == i.ParentId).ToList();	
		children.ForEach(c => c.Children = HierarchyHelper.FindChildren(c, itemsList));
		return children;
	}
}
