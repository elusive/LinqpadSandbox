<Query Kind="Program">
  <NuGetReference>Prism.Core</NuGetReference>
  <NuGetReference>Prism.DryIoc</NuGetReference>
  <NuGetReference>Prism.Wpf</NuGetReference>
  <Namespace>Prism.Ioc</Namespace>
  <Namespace>Prism.DryIoc</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>DryIoc</Namespace>
</Query>

void Main()
{	
	var myApp = new App();
	var serviceA = App.AppContainer.Resolve<IServiceA>();
	serviceA.SayHello().Dump("Service A Method");
	
	if (serviceA is IServiceB serviceB) 
	{
		serviceB.SayGoodbye().Dump("Service B Method");	
	}
}

// You can define other methods, fields, classes and namespaces here

public class App: PrismApplication
{
	public static IContainer AppContainer { get; set; }
	
	public App() {
		OnStartup(null);
	}
	
	protected override void OnStartup(StartupEventArgs args)
	{
		base.OnStartup(args);
	}
	
	protected override void RegisterTypes(IContainerRegistry containerRegistry)
	{
		containerRegistry.AddMyNewModulesServices();
		
		AppContainer = containerRegistry.GetContainer();
	}
	
	protected override Window CreateShell() { return new Window(); }
}

public static class MyNewModuleContainerExtensions
{
	public static IContainerRegistry AddMyNewModulesServices(this IContainerRegistry registry)
	{
		registry.Register<IServiceA, ServiceAbc>();
		registry.Register<IServiceB, ServiceAbc>();
		registry.Register<IServiceC, ServiceAbc>();

		return registry;
	}
}

public interface IServiceA { string SayHello(); }
public interface IServiceB { string SayGoodbye(); }
public interface IServiceC { }

public class ServiceAbc : IServiceA, IServiceB, IServiceC 
{
	public ServiceAbc() {}	
	public string SayHello() { return "Hello World"; }
	public string SayGoodbye() { return "Goodbye!"; }
}