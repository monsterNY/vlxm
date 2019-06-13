
枚举可继承结构，但仅限于byte、sbyte、short、ushort、int、uint、long或ulong。

用于限定枚举值

示例:
	
	public enum LoveTypes : int//默认继承int
	{
		...
	}

枚举的基类可通过System.Enum下的GetUnderlyingType获取

	public static Type GetUnderlyingType(Type enumType)

应用了[Flags]特性的Menu，ToString方法的工作过程:

1. 获取枚举类型定义的数值集合，降序排序这些数值

2. 每个数值都和枚举实例中的值进行“按位与”计算，假如结果等于数值，与该数值关联的字符串就附加到输出字符串上，对应的位会被认为已经考虑过了，会被关闭(设为0)。这一步不断重复，直到检查完所有数值，或直到枚举实例的所有位都被关闭。


3. 检查完所有数值后，如果枚举实例仍然不为0，表明枚举实例中一些处于on状态的位不对应任何已定义的符号。在这种情况下，ToString将枚举实例中的原始数值作为字符串返回。


4. 如果枚举实例原始值不为0，返回符号之间以逗号分隔的字符串。


5. 如果枚举实例原始值为0，而且枚举类型定义的一个符号对应的是0值，就返回这个符号


6. 返回“0”

对于没有定义[Flags]特性的枚举，可用"F"格式获取正确的字符串

#### 可空值类型的装箱 ####

CLR对Nuablle<T>实例进行装箱时，会检查它是否为null。如果是，CLR不装箱任何东西，直接返回null。如果不为Null，CLR从可空实例中取出值并进行装箱。

当对可空类型调用GetType时，会返回T的类型而不是Nullable<T>的类型，此功能对typeof无效

#### typeof与GetType的区别 ####

- typeof  是运算符，而 GetType() 是方法


- typeof 获得类型的System.Type对象，GetType()获得当前实例的Type，


- GetType()是基类System.Object的方法，只有建立了一个实例之后才能够被调用


- typeof的参数只能是int, string, class，自定义类型，不能为具体实例，否则编译器会报错

----------

since:6/13/2019 9:39:43 AM 

source:CLR via C#(第四版)

