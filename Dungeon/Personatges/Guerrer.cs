using Dungeon.Interfícies;

namespace Dungeon.Personatges
{
	public class Guerrer : Personatge, IEspecial
	{
		public Guerrer(string nom) : base(nom, 100, 20, "Guerrer") { }

		public override int Atacar()
		{
			return AtacBase;
		}

		public int AtacEspecial()
		{
			return (int)(AtacBase * 1.5);
		}
	}
}
