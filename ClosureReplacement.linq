<Query Kind="Program" />

void Main()
{
	var _ = new ThisVm();	
}

// You can define other methods, fields, classes and namespaces here
public abstract class VmBase
{
	public string Name {get;set;}
}

public abstract class ApplyVm<TViewModel> : VmBase
{	
}

public class ThisVm : ApplyVm<ThisVm>
{
	public ThisVm()
	{
		Register(() => Name);
	}
	public void Register<T>(Expression<Func<T>> exp)
	{
		ParseExpression(exp);
	}
	private void ParseExpression(LambdaExpression propExp)
	{
		var del = propExp.Compile();
		var targetCls = del.Target;
		Type target = del.Target.GetType();
		var calling = targetCls
		target.Dump();
	}
}