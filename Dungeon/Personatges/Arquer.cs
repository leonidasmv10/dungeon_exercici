using Dungeon.Interfícies;

namespace Dungeon.Personatges
{
	public class Arquer : Personatge, IEspecial
	{
		public Arquer(string nom) : base(nom, 80, 15, "Arquer") { }

		public override int Atacar()
		{
			return AtacBase;
		}

		public int AtacEspecial()
		{
			Random rand = new Random();
			float percentage = (float)rand.NextDouble() * 1.5f + 1.5f;
			return (int)(AtacBase * percentage);
		}
	}
}
