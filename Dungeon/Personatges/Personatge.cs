namespace Dungeon.Personatges
{
	public abstract class Personatge
	{
		public string Nom { get; private set; }
		public int Vida { get; set; }
		public int AtacBase { get; private set; }
		public string Classe { get; private set; }

		public Personatge() { }

		public Personatge(string nom, int vida, int atacBase, string classe)
		{
			Nom = nom;
			Vida = vida;
			AtacBase = atacBase;
			Classe = classe;
		}

		public abstract int Atacar();

		public void RebreDany(int dany)
		{
			Vida -= dany;
			if (Vida < 0) Vida = 0;
		}

		public bool EstàViu()
		{
			return Vida > 0;
		}
	}
}
