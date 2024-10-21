using Dungeon.Interfícies;

namespace Dungeon.Personatges
{
	public class Mag : Personatge, IMagic
	{
		public int Mana { get; private set; }

		public Mag(string nom) : base(nom, 70, 10, "Mag")
		{
			Mana = 50;
		}

		public override int Atacar()
		{
			return AtacBase;
		}

		public int LlançarEncantament()
		{
			if (Mana >= 10)
			{
				Mana -= 10;
				Random rand = new Random();
				float percentage = 0.5f + ((float)rand.NextDouble() * 2.5f);

				return (int)(AtacBase * percentage);
			}
			else
			{
				return 0;
			}
		}
	}
}
