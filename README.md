# stride-arch-ecs
An example in Stride using Arch ECS.

# Features
## Ease of use
Systems can be added through the GameSettings class in the Stride editor for easy management.
![image](https://github.com/Doprez/stride-arch-ecs/assets/73259914/4840313e-42b5-499e-9ba2-06cb18d77953)

all that you need to do is inherit from the SystemBase class and add it to the list.

### Add Arch components in Strides Editor!
![image](https://github.com/Doprez/stride-arch-ecs/assets/73259914/d8e0f722-253c-4796-8b74-1825684ebee0)

each component attached will create an Entity in the Arch world, with the correct components to be able to be queried later in any system.
Example:
- System:
```
namespace ArchECSStride.Code.Systems;
[DataContract(nameof(TestSystem))]
public class TestSystem : SystemBase
{
	private DebugTextSystem _debugText;
	private QueryDescription _queryDescription;

	public override void Start()
	{
		_debugText = Services.GetService<DebugTextSystem>();

		_queryDescription = new QueryDescription().
			WithAny<Vector3>();
	}

	public override void Update(in GameTime state)
	{
		var result = World.CountEntities(in _queryDescription);

		_debugText.Print($"TestSystem: {result}", new Int2(50, 50));
	}
}
```
- Components:
```
[DataContract(nameof(ArchPosition))]
[ComponentCategory("Arch Components")]
public class ArchPosition : ArchComponent, IArchComponent
{
	[DataMemberIgnore]
	object IArchComponent.ComponentType { get; set; } = new Vector3();
}
```
```
[DataContract(nameof(ArchTest))]
[ComponentCategory("Arch Components")]
public class ArchTest : ArchComponent, IArchComponent
{
	[DataMemberIgnore]
	object IArchComponent.ComponentType { get; set; } = new TestComponent();
}

public struct TestComponent
{
	public int Number;
	public string Text;
}
```


## Easy access
When inheriting from SystemBase and registered in settings each System has access to both the Arch World and Strides Service registry.

# Future goals
- Allow EntityComponents to create Arch Entities with Arch Components (Partially done. I would like to have editable values in editor as well)
- Multithreading
- create a basic game/demo example
