namespace FluTeLib.Core.helper.Objects
{
	public interface IKeyedObject<T>
	{
		T Key
		{
			get;
		}

		bool KeysEqual(IKeyedObject<T> other);
	}
}